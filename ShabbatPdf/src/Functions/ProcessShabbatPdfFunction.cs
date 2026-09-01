using ShabbatPdf.Core.Models;
using ShabbatPdf.Core.Options;
using ShabbatPdf.Core.Pipeline;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ShabbatPdf.Functions;

/// <summary>
/// Event Grid on <c>shabbat-service</c>: slice teaching pages and upload the same file name
/// to <c>shabbat-service-md</c>.
/// </summary>
public sealed class ProcessShabbatPdfFunction
{
    private readonly IParsePipeline _pipeline;
    private readonly ParseOptions _parseOptions;
    private readonly BlobOptions _blobOptions;
    private readonly ILogger<ProcessShabbatPdfFunction> _logger;

    public ProcessShabbatPdfFunction(
        IParsePipeline pipeline,
        IOptions<ParseOptions> parseOptions,
        IOptions<BlobOptions> blobOptions,
        ILogger<ProcessShabbatPdfFunction> logger)
    {
        _pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
        _parseOptions = parseOptions?.Value ?? throw new ArgumentNullException(nameof(parseOptions));
        _blobOptions = blobOptions?.Value ?? throw new ArgumentNullException(nameof(blobOptions));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Fires on Microsoft.Storage.BlobCreated for the compressed service container.
    /// </summary>
    [Function(nameof(ProcessShabbatPdf))]
    public async Task ProcessShabbatPdf(
        [EventGridTrigger] EventGridEvent eventGridEvent,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(eventGridEvent);

        _logger.LogInformation(
            "EventGrid event Type={Type} Subject={Subject} Id={Id}",
            eventGridEvent.EventType,
            eventGridEvent.Subject,
            eventGridEvent.Id);

        if (!EventGridBlobParser.TryGetBlobName(eventGridEvent, out var sourceName, out var container))
        {
            _logger.LogWarning(
                "Could not parse blob name from event. Subject={Subject} Data={Data}",
                eventGridEvent.Subject,
                eventGridEvent.Data?.ToString());
            return;
        }

        if (!string.IsNullOrEmpty(container)
            && !string.Equals(container, _blobOptions.SourceContainer, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogInformation(
                "Skipping blob in container {Container} (expected {Expected}): {Name}",
                container,
                _blobOptions.SourceContainer,
                sourceName);
            return;
        }

        if (!ShabbatBlobTriggerFilter.ShouldProcess(sourceName))
        {
            _logger.LogInformation("Skipping blob (filter): {Name}", sourceName);
            return;
        }

        _logger.LogInformation("Extracting teaching PDF from {Name}", sourceName);

        var request = new ParseRequest(
            SourceName: sourceName,
            BlobMode: true,
            Overwrite: _parseOptions.Overwrite,
            SkipIfDestinationExists: false,
            DryRun: false,
            RequireStandardBlobName: _parseOptions.RequireStandardBlobName,
            EnsureDestinationContainer: true,
            TeachingOnly: true);

        var result = await _pipeline.RunAsync(request, cancellationToken).ConfigureAwait(false);

        if (result.Success)
        {
            _logger.LogInformation(
                "OK {Name} teaching={Teaching} pages={Start}-{End}",
                sourceName,
                result.TeachingPdfUri ?? "(none)",
                result.Anchors?.ContentStartPage,
                result.Anchors?.ContentEndPage);
            return;
        }

        if (IsNonRetriable(result.Message))
        {
            _logger.LogError(
                "Parse failed (non-retriable) for {Name}: {Message}",
                sourceName,
                result.Message);
            return;
        }

        _logger.LogError(
            "Parse failed (retriable) for {Name}: {Message}",
            sourceName,
            result.Message);
        throw new InvalidOperationException(result.Message);
    }

    private static bool IsNonRetriable(string message) =>
        message.StartsWith(ParseErrorCodes.InvalidName, StringComparison.Ordinal)
        || message.StartsWith("AnchorNotFound", StringComparison.Ordinal)
        || message.StartsWith("EmptySlice", StringComparison.Ordinal)
        || message.StartsWith("StartAnchorNotFound", StringComparison.Ordinal)
        || message.StartsWith("EndAnchorNotFound", StringComparison.Ordinal);
}

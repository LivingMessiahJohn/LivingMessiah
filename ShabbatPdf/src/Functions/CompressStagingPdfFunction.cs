using ShabbatPdf.Core.Compression;
using ShabbatPdf.Core.Options;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ShabbatPdf.Functions;

/// <summary>
/// Event Grid on <c>shabbat-service-staging</c>: compress (or copy if already small)
/// into <c>shabbat-service</c>. That write triggers teaching extract.
/// </summary>
public sealed class CompressStagingPdfFunction
{
    private readonly ISourcePdfShrinker _sourcePdfShrinker;
    private readonly BlobOptions _blobOptions;
    private readonly ILogger<CompressStagingPdfFunction> _logger;

    public CompressStagingPdfFunction(
        ISourcePdfShrinker sourcePdfShrinker,
        IOptions<BlobOptions> blobOptions,
        ILogger<CompressStagingPdfFunction> logger)
    {
        _sourcePdfShrinker = sourcePdfShrinker ?? throw new ArgumentNullException(nameof(sourcePdfShrinker));
        _blobOptions = blobOptions?.Value ?? throw new ArgumentNullException(nameof(blobOptions));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [Function(nameof(CompressStagingPdf))]
    public async Task CompressStagingPdf(
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
            && !string.Equals(container, _blobOptions.StagingContainer, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogInformation(
                "Skipping blob in container {Container} (expected {Expected}): {Name}",
                container,
                _blobOptions.StagingContainer,
                sourceName);
            return;
        }

        if (!ShabbatBlobTriggerFilter.ShouldProcess(sourceName))
        {
            _logger.LogInformation("Skipping blob (filter): {Name}", sourceName);
            return;
        }

        _logger.LogInformation(
            "Publishing staging blob {Name} → {Dest}",
            sourceName,
            _blobOptions.SourceContainer);

        var publish = await _sourcePdfShrinker
            .PublishAsync(
                _blobOptions.StagingContainer,
                _blobOptions.SourceContainer,
                sourceName,
                cancellationToken)
            .ConfigureAwait(false);

        if (!publish.Success)
        {
            _logger.LogError(
                "Publish failed for {Name}: {Message} original={Original} final={Final}",
                sourceName,
                publish.Message,
                SourcePdfShrinkResult.FormatMb(publish.OriginalBytes),
                SourcePdfShrinkResult.FormatMb(publish.FinalBytes));

            if (IsNonRetriablePublish(publish.Message))
            {
                return;
            }

            throw new InvalidOperationException($"PdfCompress failed: {publish.Message}");
        }

        _logger.LogInformation(
            "Published {Name}: compressed={Compressed} copied={Copied} original={Original} final={Final} — {Message}",
            sourceName,
            publish.Compressed,
            publish.Copied,
            SourcePdfShrinkResult.FormatMb(publish.OriginalBytes),
            SourcePdfShrinkResult.FormatMb(publish.FinalBytes),
            publish.Message);
    }

    private static bool IsNonRetriablePublish(string message) =>
        message.Contains("Ghostscript executable not found", StringComparison.OrdinalIgnoreCase)
        || message.Contains("still over limit", StringComparison.OrdinalIgnoreCase)
        || message.Contains("Blob not found", StringComparison.OrdinalIgnoreCase)
        || message.Contains("Container not found", StringComparison.OrdinalIgnoreCase);
}

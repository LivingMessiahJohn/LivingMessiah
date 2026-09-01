using ShabbatPdf.Core.Options;
using ShabbatPdf.Core.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace ShabbatPdf.Core.Compression;

/// <summary>
/// Downloads a staging agenda PDF, compresses when over the mobile size limit, and publishes
/// to the public service container. Staging is never overwritten.
/// </summary>
public sealed class SourcePdfShrinker : ISourcePdfShrinker
{
    private readonly IBlobStore _blobStore;
    private readonly IPdfCompressor _compressor;
    private readonly PdfCompressOptions _options;
    private readonly ILogger<SourcePdfShrinker> _logger;

    public SourcePdfShrinker(
        IBlobStore blobStore,
        IPdfCompressor compressor,
        IOptions<PdfCompressOptions> options,
        ILogger<SourcePdfShrinker>? logger = null)
    {
        _blobStore = blobStore ?? throw new ArgumentNullException(nameof(blobStore));
        _compressor = compressor ?? throw new ArgumentNullException(nameof(compressor));
        _options = options?.Value ?? new PdfCompressOptions();
        _logger = logger ?? NullLogger<SourcePdfShrinker>.Instance;
    }

    public async Task<SourcePdfShrinkResult> PublishAsync(
        string sourceContainer,
        string destinationContainer,
        string blobName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceContainer);
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationContainer);
        ArgumentException.ThrowIfNullOrWhiteSpace(blobName);

        var maxBytes = _options.MaxBytes > 0
            ? _options.MaxBytes
            : 65L * 1024 * 1024;

        var sameContainer = string.Equals(
            sourceContainer,
            destinationContainer,
            StringComparison.OrdinalIgnoreCase);

        long? length = await _blobStore
            .GetContentLengthAsync(sourceContainer, blobName, cancellationToken)
            .ConfigureAwait(false);

        if (length is null)
        {
            return SourcePdfShrinkResult.Fail(
                $"Blob not found: {sourceContainer}/{blobName}");
        }

        var originalBytes = length.Value;
        var compressNeeded = _options.Enabled && originalBytes > maxBytes;

        if (!compressNeeded)
        {
            if (sameContainer)
            {
                if (!_options.Enabled)
                {
                    _logger.LogInformation("PdfCompress disabled; skipping shrink for {Blob}", blobName);
                    return SourcePdfShrinkResult.SkippedDisabled();
                }

                _logger.LogInformation(
                    "Skip compress {Container}/{Blob}: {Size} <= {Max}",
                    sourceContainer,
                    blobName,
                    SourcePdfShrinkResult.FormatMb(originalBytes),
                    SourcePdfShrinkResult.FormatMb(maxBytes));
                return SourcePdfShrinkResult.AlreadyUnderLimit(originalBytes, maxBytes);
            }

            return await CopyToDestinationAsync(
                    sourceContainer,
                    destinationContainer,
                    blobName,
                    originalBytes,
                    maxBytes,
                    disabled: !_options.Enabled,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        _logger.LogInformation(
            "Compress needed {Source}/{Blob} → {Dest}: {Size} > {Max}",
            sourceContainer,
            blobName,
            destinationContainer,
            SourcePdfShrinkResult.FormatMb(originalBytes),
            SourcePdfShrinkResult.FormatMb(maxBytes));

        string? tempIn = null;
        string? tempOut = null;
        try
        {
            tempIn = CreateTempPdfPath(blobName, "in");
            tempOut = CreateTempPdfPath(blobName, "out");

            await _blobStore
                .DownloadToFileAsync(sourceContainer, blobName, tempIn, cancellationToken)
                .ConfigureAwait(false);

            originalBytes = new FileInfo(tempIn).Length;
            if (originalBytes <= maxBytes)
            {
                if (sameContainer)
                {
                    return SourcePdfShrinkResult.AlreadyUnderLimit(originalBytes, maxBytes);
                }

                return await UploadFileAsync(
                        destinationContainer,
                        blobName,
                        tempIn,
                        originalBytes,
                        copied: true,
                        disabled: false,
                        maxBytes,
                        cancellationToken)
                    .ConfigureAwait(false);
            }

            var compress = await _compressor
                .CompressAsync(tempIn, tempOut, cancellationToken)
                .ConfigureAwait(false);

            if (!compress.Success)
            {
                return SourcePdfShrinkResult.Fail(
                    compress.Message,
                    originalBytes);
            }

            var finalBytes = compress.OutputBytes;
            if (finalBytes > maxBytes)
            {
                return SourcePdfShrinkResult.Fail(
                    $"Compressed PDF still over limit: {SourcePdfShrinkResult.FormatMb(finalBytes)} > {SourcePdfShrinkResult.FormatMb(maxBytes)}. " +
                    "Try a stronger PdfCompress:PdfSettings (e.g. /screen) or raise MaxBytes.",
                    originalBytes,
                    finalBytes);
            }

            var result = await UploadFileAsync(
                    destinationContainer,
                    blobName,
                    tempOut,
                    originalBytes,
                    copied: false,
                    disabled: false,
                    maxBytes,
                    cancellationToken)
                .ConfigureAwait(false);

            if (result.Success)
            {
                _logger.LogInformation(
                    "Published compressed PDF {Dest}/{Blob}: {Message}",
                    destinationContainer,
                    blobName,
                    result.Message);
            }

            return result;
        }
        catch (FileNotFoundException ex)
        {
            return SourcePdfShrinkResult.Fail(ex.Message, originalBytes);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or InvalidOperationException)
        {
            _logger.LogError(
                ex,
                "Publish failed for {Source}/{Blob} → {Dest}",
                sourceContainer,
                blobName,
                destinationContainer);
            return SourcePdfShrinkResult.Fail(ex.Message, originalBytes);
        }
        finally
        {
            DeleteTempQuietly(tempIn);
            DeleteTempQuietly(tempOut);
        }
    }

    private async Task<SourcePdfShrinkResult> CopyToDestinationAsync(
        string sourceContainer,
        string destinationContainer,
        string blobName,
        long originalBytes,
        long maxBytes,
        bool disabled,
        CancellationToken cancellationToken)
    {
        string? tempIn = null;
        try
        {
            tempIn = CreateTempPdfPath(blobName, "copy");
            await _blobStore
                .DownloadToFileAsync(sourceContainer, blobName, tempIn, cancellationToken)
                .ConfigureAwait(false);

            originalBytes = new FileInfo(tempIn).Length;
            return await UploadFileAsync(
                    destinationContainer,
                    blobName,
                    tempIn,
                    originalBytes,
                    copied: true,
                    disabled,
                    maxBytes,
                    cancellationToken)
                .ConfigureAwait(false);
        }
        catch (FileNotFoundException ex)
        {
            return SourcePdfShrinkResult.Fail(ex.Message, originalBytes);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or InvalidOperationException)
        {
            _logger.LogError(
                ex,
                "Copy failed for {Source}/{Blob} → {Dest}",
                sourceContainer,
                blobName,
                destinationContainer);
            return SourcePdfShrinkResult.Fail(ex.Message, originalBytes);
        }
        finally
        {
            DeleteTempQuietly(tempIn);
        }
    }

    private async Task<SourcePdfShrinkResult> UploadFileAsync(
        string destinationContainer,
        string blobName,
        string localPath,
        long originalBytes,
        bool copied,
        bool disabled,
        long maxBytes,
        CancellationToken cancellationToken)
    {
        var bytes = await File.ReadAllBytesAsync(localPath, cancellationToken).ConfigureAwait(false);
        await _blobStore
            .UploadBinaryAsync(
                destinationContainer,
                blobName,
                bytes,
                contentType: "application/pdf",
                overwrite: true,
                cancellationToken)
            .ConfigureAwait(false);

        if (copied)
        {
            var result = disabled
                ? SourcePdfShrinkResult.CopiedDisabled(originalBytes)
                : SourcePdfShrinkResult.CopiedAsIs(originalBytes, maxBytes);
            _logger.LogInformation(
                "Copied {Dest}/{Blob}: {Message}",
                destinationContainer,
                blobName,
                result.Message);
            return result;
        }

        return SourcePdfShrinkResult.CompressedOk(originalBytes, bytes.LongLength, maxBytes);
    }

    private static string CreateTempPdfPath(string blobName, string tag)
    {
        var safe = string.Concat(
            Path.GetFileName(blobName)
                .Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));
        if (string.IsNullOrWhiteSpace(safe))
        {
            safe = "source.pdf";
        }

        var dir = Path.Combine(Path.GetTempPath(), "lmm-parse-pdf", "compress");
        Directory.CreateDirectory(dir);
        return Path.Combine(dir, $"{Guid.NewGuid():N}-{tag}-{safe}");
    }

    private void DeleteTempQuietly(string? path)
    {
        if (path is null)
        {
            return;
        }

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to delete temp PDF {Path}", path);
        }
    }
}

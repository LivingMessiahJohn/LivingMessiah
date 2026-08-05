using ShabbatPdf.Core.Options;
using ShabbatPdf.Core.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace ShabbatPdf.Core.Compression;

/// <summary>
/// Downloads an oversized source PDF, compresses it with <see cref="IPdfCompressor"/>,
/// and overwrites the same blob so Current Service mobile downloads stay under the limit.
/// Teaching PDF + Markdown then run against the smaller file (via the normal parse pipeline).
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
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLogger<SourcePdfShrinker>.Instance;
    }

    public async Task<SourcePdfShrinkResult> EnsureUnderMaxSizeAsync(
        string container,
        string blobName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(container);
        ArgumentException.ThrowIfNullOrWhiteSpace(blobName);

        if (!_options.Enabled)
        {
            _logger.LogInformation("PdfCompress disabled; skipping shrink for {Blob}", blobName);
            return SourcePdfShrinkResult.SkippedDisabled();
        }

        var maxBytes = _options.MaxBytes > 0
            ? _options.MaxBytes
            : 65L * 1024 * 1024;

        long? length = await _blobStore
            .GetContentLengthAsync(container, blobName, cancellationToken)
            .ConfigureAwait(false);

        if (length is null)
        {
            return SourcePdfShrinkResult.Fail(
                $"Blob not found: {container}/{blobName}");
        }

        var originalBytes = length.Value;
        if (originalBytes <= maxBytes)
        {
            _logger.LogInformation(
                "Skip compress {Container}/{Blob}: {Size} <= {Max}",
                container,
                blobName,
                SourcePdfShrinkResult.FormatMb(originalBytes),
                SourcePdfShrinkResult.FormatMb(maxBytes));
            return SourcePdfShrinkResult.AlreadyUnderLimit(originalBytes, maxBytes);
        }

        _logger.LogInformation(
            "Compress needed {Container}/{Blob}: {Size} > {Max}",
            container,
            blobName,
            SourcePdfShrinkResult.FormatMb(originalBytes),
            SourcePdfShrinkResult.FormatMb(maxBytes));

        string? tempIn = null;
        string? tempOut = null;
        try
        {
            tempIn = CreateTempPdfPath(blobName, "in");
            tempOut = CreateTempPdfPath(blobName, "out");

            await _blobStore
                .DownloadToFileAsync(container, blobName, tempIn, cancellationToken)
                .ConfigureAwait(false);

            // Prefer measured file size after download (authoritative).
            originalBytes = new FileInfo(tempIn).Length;
            if (originalBytes <= maxBytes)
            {
                return SourcePdfShrinkResult.AlreadyUnderLimit(originalBytes, maxBytes);
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

            var bytes = await File.ReadAllBytesAsync(tempOut, cancellationToken)
                .ConfigureAwait(false);

            await _blobStore
                .UploadBinaryAsync(
                    container,
                    blobName,
                    bytes,
                    contentType: "application/pdf",
                    overwrite: true,
                    cancellationToken)
                .ConfigureAwait(false);

            var result = SourcePdfShrinkResult.CompressedOk(originalBytes, finalBytes, maxBytes);
            _logger.LogInformation(
                "Uploaded compressed source {Container}/{Blob}: {Message}",
                container,
                blobName,
                result.Message);
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
            _logger.LogError(ex, "Shrink failed for {Container}/{Blob}", container, blobName);
            return SourcePdfShrinkResult.Fail(ex.Message, originalBytes);
        }
        finally
        {
            DeleteTempQuietly(tempIn);
            DeleteTempQuietly(tempOut);
        }
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

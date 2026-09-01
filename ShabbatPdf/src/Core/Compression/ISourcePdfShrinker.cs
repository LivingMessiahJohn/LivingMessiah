namespace ShabbatPdf.Core.Compression;

/// <summary>
/// Publishes a staging agenda PDF into the public service container, compressing when oversized.
/// </summary>
public interface ISourcePdfShrinker
{
    /// <summary>
    /// Copy <paramref name="blobName"/> from <paramref name="sourceContainer"/> (staging)
    /// to <paramref name="destinationContainer"/> (shabbat-service).
    /// If the blob is larger than <c>PdfCompress:MaxBytes</c>, Ghostscript compresses first.
    /// Staging is never overwritten. Already-small files are copied as-is so the extract
    /// function still sees a BlobCreated event on the service container.
    /// </summary>
    Task<SourcePdfShrinkResult> PublishAsync(
        string sourceContainer,
        string destinationContainer,
        string blobName,
        CancellationToken cancellationToken = default);
}

namespace ShabbatPdf.Core.Compression;

/// <summary>
/// Ensures a full-agenda PDF in blob storage is under the mobile download size limit.
/// Oversized blobs are compressed and overwritten in place.
/// </summary>
public interface ISourcePdfShrinker
{
    /// <summary>
    /// If the blob is larger than <c>PdfCompress:MaxBytes</c>, download → compress → upload overwrite.
    /// Idempotent for already-small blobs (re-entry after overwrite is a no-op).
    /// </summary>
    Task<SourcePdfShrinkResult> EnsureUnderMaxSizeAsync(
        string container,
        string blobName,
        CancellationToken cancellationToken = default);
}

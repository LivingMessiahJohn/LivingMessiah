namespace LivingMessiah.ShabbatPdf.Core.Compression;

/// <summary>
/// Compresses a PDF on disk (typically via Ghostscript) to reduce mobile download size.
/// </summary>
public interface IPdfCompressor
{
    /// <summary>
    /// Compress <paramref name="inputPath"/> into <paramref name="outputPath"/>.
    /// Paths must be different files. Caller owns both files.
    /// </summary>
    Task<PdfCompressResult> CompressAsync(
        string inputPath,
        string outputPath,
        CancellationToken cancellationToken = default);
}

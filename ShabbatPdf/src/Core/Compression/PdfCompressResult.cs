namespace ShabbatPdf.Core.Compression;

/// <summary>
/// Outcome of compressing a single PDF file on disk.
/// </summary>
public sealed class PdfCompressResult
{
    public bool Success { get; init; }

    public string Message { get; init; } = string.Empty;

    public long InputBytes { get; init; }

    public long OutputBytes { get; init; }

    public string? OutputPath { get; init; }

    public static PdfCompressResult Ok(string outputPath, long inputBytes, long outputBytes) =>
        new()
        {
            Success = true,
            Message = "Compressed.",
            InputBytes = inputBytes,
            OutputBytes = outputBytes,
            OutputPath = outputPath
        };

    public static PdfCompressResult Fail(string message, long inputBytes = 0) =>
        new()
        {
            Success = false,
            Message = message,
            InputBytes = inputBytes,
            OutputBytes = 0
        };
}

namespace ShabbatPdf.Core.Compression;

/// <summary>
/// Outcome of ensuring a source agenda PDF is under the configured size limit.
/// </summary>
public sealed class SourcePdfShrinkResult
{
    public bool Success { get; init; }

    /// <summary>True when Ghostscript ran and the blob was overwritten.</summary>
    public bool Compressed { get; init; }

    public long OriginalBytes { get; init; }

    public long FinalBytes { get; init; }

    public string Message { get; init; } = string.Empty;

    public static SourcePdfShrinkResult SkippedDisabled() =>
        new()
        {
            Success = true,
            Compressed = false,
            Message = "PdfCompress disabled; left source unchanged."
        };

    public static SourcePdfShrinkResult AlreadyUnderLimit(long bytes, long maxBytes) =>
        new()
        {
            Success = true,
            Compressed = false,
            OriginalBytes = bytes,
            FinalBytes = bytes,
            Message =
                $"Already under limit ({FormatMb(bytes)} <= {FormatMb(maxBytes)}); no compress."
        };

    public static SourcePdfShrinkResult CompressedOk(
        long originalBytes,
        long finalBytes,
        long maxBytes) =>
        new()
        {
            Success = true,
            Compressed = true,
            OriginalBytes = originalBytes,
            FinalBytes = finalBytes,
            Message =
                $"Compressed {FormatMb(originalBytes)} → {FormatMb(finalBytes)} (limit {FormatMb(maxBytes)})."
        };

    public static SourcePdfShrinkResult Fail(
        string message,
        long originalBytes = 0,
        long finalBytes = 0) =>
        new()
        {
            Success = false,
            Compressed = false,
            OriginalBytes = originalBytes,
            FinalBytes = finalBytes,
            Message = message
        };

    public static string FormatMb(long bytes) =>
        $"{bytes / (1024d * 1024d):F1} MB";
}

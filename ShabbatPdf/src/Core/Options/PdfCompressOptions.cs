namespace LivingMessiah.ShabbatPdf.Core.Options;

/// <summary>
/// PDF size limits and Ghostscript settings. Binds from configuration section "PdfCompress".
/// Used by the Azure Function to shrink oversized weekly service PDFs before parse/export.
/// </summary>
public sealed class PdfCompressOptions
{
    public const string SectionName = "PdfCompress";

    /// <summary>
    /// When false, the shrink step is skipped entirely.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Target maximum size in bytes (acceptance: under 65 MB). Default 65 MiB.
    /// Blobs already at or under this size are not recompressed.
    /// </summary>
    public long MaxBytes { get; set; } = 65L * 1024 * 1024;

    /// <summary>
    /// Full path to the Ghostscript executable, or empty to auto-detect
    /// (<c>gswin64c</c> / <c>gswin32c</c> on Windows, <c>gs</c> on Linux/macOS).
    /// On Azure Flex, set to a mounted path (e.g. <c>/home/site/wwwroot/tools/gs</c>
    /// or an Azure Files mount).
    /// </summary>
    public string GhostscriptPath { get; set; } = string.Empty;

    /// <summary>
    /// Ghostscript <c>-dPDFSETTINGS</c> value (e.g. <c>/screen</c>, <c>/ebook</c>, <c>/printer</c>).
    /// <c>/ebook</c> is a good default for image-heavy service decks (~150 dpi).
    /// </summary>
    public string PdfSettings { get; set; } = "/ebook";

    /// <summary>
    /// PDF compatibility level passed to Ghostscript.
    /// </summary>
    public string CompatibilityLevel { get; set; } = "1.4";

    /// <summary>
    /// Max wall-clock time for one Ghostscript run.
    /// Large 250+ MB decks can take 1–3 minutes.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 600;
}

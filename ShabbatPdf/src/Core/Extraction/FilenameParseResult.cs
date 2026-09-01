namespace ShabbatPdf.Core.Extraction;

/// <summary>
/// Result of parsing a Shabbat agenda PDF file name.
/// </summary>
public sealed record FilenameParseResult(
    string SourceFileName,
    bool IsStandardPattern,
    string? ServiceDate,
    string Citation,
    string BaseNameWithoutExtension)
{
    /// <summary>Destination blob/file name with .md extension.</summary>
    public string MarkdownFileName => BaseNameWithoutExtension + ".md";

    /// <summary>
    /// Local teaching-only PDF name: same base with <c>-teaching</c> before the extension
    /// so it does not overwrite the full agenda file in the same folder.
    /// Blob mode uses <see cref="SourceFileName"/> in the destination container instead.
    /// </summary>
    public string TeachingPdfFileName => BaseNameWithoutExtension + "-teaching.pdf";
}

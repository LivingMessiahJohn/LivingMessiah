namespace ShabbatPdf.Functions;

/// <summary>
/// Decides which blobs the Azure Function should process.
/// Skips leftover <c>*-teaching.pdf</c> blobs in <c>shabbat-service</c> (legacy naming).
/// New teaching PDFs live in <c>shabbat-service-md</c> with the agenda file name.
/// </summary>
public static class ShabbatBlobTriggerFilter
{
    /// <summary>
    /// Returns true when the blob looks like a full agenda PDF that should be parsed.
    /// </summary>
    public static bool ShouldProcess(string? blobName)
    {
        if (string.IsNullOrWhiteSpace(blobName))
        {
            return false;
        }

        // Blob trigger {name} may include virtual folder prefixes.
        var name = Path.GetFileName(blobName.Trim());
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        if (!name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        // Legacy pipeline wrote *-teaching.pdf into shabbat-service; skip those.
        if (name.EndsWith("-teaching.pdf", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return true;
    }
}

namespace ShabbatPdf.Core.Options;

/// <summary>
/// Azure Blob Storage settings. Binds from configuration section "Blob".
/// </summary>
public sealed class BlobOptions
{
    public const string SectionName = "Blob";

    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Account URI for Managed Identity (e.g. https://livingmessiahstorage.blob.core.windows.net).
    /// </summary>
    public string ServiceUri { get; set; } = string.Empty;

    /// <summary>
    /// Admin upload target. Compress function reads from here.
    /// </summary>
    public string StagingContainer { get; set; } = "shabbat-service-staging";

    /// <summary>
    /// Compressed full-service PDF (public Current Service download).
    /// </summary>
    public string SourceContainer { get; set; } = "shabbat-service";

    /// <summary>
    /// Teaching-only PDF, same file name as the agenda (no <c>-teaching</c> suffix).
    /// </summary>
    public string DestinationContainer { get; set; } = "shabbat-service-md";

    public bool UseDefaultAzureCredential { get; set; }
}

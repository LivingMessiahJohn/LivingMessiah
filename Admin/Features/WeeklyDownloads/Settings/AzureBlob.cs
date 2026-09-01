namespace Admin.Features.WeeklyDownloads.Settings;

public class AzureBlob
{
	public string? ConnectionString { get; set; } // = string.Empty;
	/// <summary>Shabbat agenda upload container (staging). Default: shabbat-service-staging.</summary>
	public string? WeeklyDownloadContainer { get; set; } // = string.Empty;
}


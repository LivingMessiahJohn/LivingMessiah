namespace LivingMessiahAdmin.Features.Sukkot.Dashboard.Constants;

public static class ExportCSVHelper
{
	public const string JavaScriptMethod = "downloadFile";
	public const string DownloadFileName = "registration.csv";

	public static string Dump()
	{
		return $"JavaScriptMethod: {JavaScriptMethod}, DownloadFileName: {DownloadFileName}";
	}
}

namespace LivingMessiahSukkot.Features.LandingPage.Constants;

public static class Blob
{
	private const string root = "https://livingmessiahstorage.blob.core.windows.net/images/";
	private const string maps = "https://livingmessiahstorage.blob.core.windows.net/images/events/";
	private const string pdf = "https://livingmessiahstorage.blob.core.windows.net/pdfs/";

	public static string UrlPDF(string blob)
	{
		return pdf + blob;
	}
	public static string UrlRoot(string blob)
	{
		return root + blob;
	}
}


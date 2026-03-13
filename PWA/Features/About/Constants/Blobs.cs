namespace PWA.Features.About.Constants;

public static class Blobs
{
	public static string Link(string blob)
	{
		return "https://livingmessiahstorage.blob.core.windows.net/images/" + blob;
	}
	public static string Pdf(string blob)
	{
		return "https://livingmessiahstorage.blob.core.windows.net/pdfs/" + blob;
	}
}

namespace PWA.Features.Liturgy.Constants;

public static class Blobs
{
	public static string Link(string blob)
	{
		return "https://livingmessiahstorage.blob.core.windows.net/images/shabbatservice/" + blob;
	}

	public static string OtherLink(string blob)
	{
		return "https://livingmessiahstorage.blob.core.windows.net/images/other/" + blob;
	}
}

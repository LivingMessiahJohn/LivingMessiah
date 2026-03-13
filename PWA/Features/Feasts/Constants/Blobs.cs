namespace PWA.Features.Feasts.Constants;

public static class Blobs
{
	public static string Link(string blob)
	{
		return "https://livingmessiahstorage.blob.core.windows.net/images/" + blob;
	}

	public static string EventsLink(string blob)
	{
		return "https://livingmessiahstorage.blob.core.windows.net/images/events/" + blob;
	}
}

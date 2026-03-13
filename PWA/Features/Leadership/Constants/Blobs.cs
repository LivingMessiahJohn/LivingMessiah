namespace PWA.Features.Leadership.Constants;

public static class Blobs
{
	public static string Link(string blob)
	{
		return "https://livingmessiahstorage.blob.core.windows.net/leadership/" + blob;
	}

	public static string Footer(string blob, bool IsXs)
	{
		if (IsXs) 
		{
			return "https://livingmessiahstorage.blob.core.windows.net/images/" + blob;
		} 
		else 
		{ 
		return "https://livingmessiahstorage.blob.core.windows.net/images/" + blob;
		}
	}
}

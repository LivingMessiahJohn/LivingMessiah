namespace PWA.Features.FurtherStudies.Constants;

public static class Blobs
{
	public static string Link(string blob)
	{
		return "https://livingmessiahstorage.blob.core.windows.net/images/" + blob;
	}

	public static string LearningHebrewLink() //string blob
	{
		return "https://livingmessiahstorage.blob.core.windows.net/pdfs/LearningHebrew.pdf"; // + blob;
	}

	public static string HebrewHandoutsLink(string blob)
	{
		return "https://livingmessiahstorage.blob.core.windows.net/images/" + blob;
	}
}

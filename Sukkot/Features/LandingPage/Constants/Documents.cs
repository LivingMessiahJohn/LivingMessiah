namespace Sukkot.Features.LandingPage.Constants;

public static class Documents
{
	private const string PdfBlobFolder = "https://livingmessiahstorage.blob.core.windows.net/pdfs/";
	private const string SukkotContentFolder = "https://livingmessiahstorage.blob.core.windows.net/sukkot-content/sukkot/";
	
	public static class PDFs
	{
		//public const string Schedule = "sukkot-2025-schedule.pdf"; This is online and is dynamically updated
		public const string LiabilityWaiver = "sukkot-2022-liability-waiver.pdf"; // NOT DONE YET
		public const string HouseRules = "sukkot-2026-house-rules.pdf";
		public const string PocketModScheduledEvents = "pocketmod-scheduled-events.pdf";
	}

	public static string Url(string blob) => PdfBlobFolder + blob;

	public static string PocketModUrl => SukkotContentFolder + PDFs.PocketModScheduledEvents;


	//public static string UrlPDF(string blob)
	//{
	//	return PdfBlobFolder + blob;
	//}

	//public static class PdfBlobFolder
	//{
	//	private const string pdf = "https://livingmessiahstorage.blob.core.windows.net/pdfs/";
	//}

}

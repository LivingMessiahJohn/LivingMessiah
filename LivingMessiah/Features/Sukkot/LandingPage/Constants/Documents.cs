namespace LivingMessiah.Features.Sukkot.LandingPage.Constants;

public static class Documents
{
	private const string PdfBlobFolder = "https://livingmessiahstorage.blob.core.windows.net/pdfs/";
	
	public static class PDFs
	{
		public const string Schedule = "sukkot-2025-schedule.pdf";
		public const string LiabilityWaiver = "sukkot-2022-liability-waiver.pdf"; // NOT DONE YET
		public const string HouseRules = "sukkot-2025-house-rules.pdf";
		public const string WindmillRanchSurroundingArea = "windmill-ranch-surrounding-area.pdf";
	}

	public static string Url(string blob) => PdfBlobFolder + blob;


	//public static string UrlPDF(string blob)
	//{
	//	return PdfBlobFolder + blob;
	//}

	//public static class PdfBlobFolder
	//{
	//	private const string pdf = "https://livingmessiahstorage.blob.core.windows.net/pdfs/";
	//}

}

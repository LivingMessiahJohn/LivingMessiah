namespace PWA.Features.Home.Shabbat.Teaching.Enums;

public static class PdfTypeHelper
{
	public static string GetPdfTypeText(PdfType? pdfType)
	{
		return pdfType switch
		{
			PdfType.CompleteService => "Complete Service",
			PdfType.TeachingOnly => "Teaching Only",
			_ => "Teaching Only"
		};
	}
}
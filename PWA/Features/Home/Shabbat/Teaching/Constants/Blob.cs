using RCL.Features.Parasha.Enums;

namespace PWA.Features.Home.Shabbat.Teaching.Constants;

public class Blob
{
	public const string ServiceContainer = "shabbat-service";
	public const string TeachingContainer = "shabbat-service-teaching";
	public const string BaseUrl = "https://livingmessiahstorage.blob.core.windows.net/shabbat-service/";
	public const string BaseTeachingUrl = "https://livingmessiahstorage.blob.core.windows.net/shabbat-service-teaching/";

	public static string ContainerName(PdfType pdfType) =>
		pdfType == PdfType.TeachingOnly ? TeachingContainer : ServiceContainer;

	public static string PublicUrl(string blobName, PdfType pdfType) =>
		$"{(pdfType == PdfType.TeachingOnly ? BaseTeachingUrl : BaseUrl)}{blobName}";
}

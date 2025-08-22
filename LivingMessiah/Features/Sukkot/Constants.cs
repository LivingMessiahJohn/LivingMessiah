
namespace LivingMessiah.Features.Sukkot.Constants;


public static class PDFs
{
	public const string StripeWalkThrough = "sukkot-making-a-payment-with-stripe.pdf";  // ToDo: these needs to be updated
	public const string RegistrationWalkThrough = "sukkot-registration-walkthrough-users-manual.pdf";
}

public static class Blobs
{
	private const string root = "https://livingmessiahstorage.blob.core.windows.net/images/";
	private const string maps = "https://livingmessiahstorage.blob.core.windows.net/images/events/";
	private const string pdf = "https://livingmessiahstorage.blob.core.windows.net/pdfs/";

	public static string UrlPDF(string blob)
	{
		return pdf + blob;
	}
}


public static class Debug 
{ 
	public const bool ShowStatusEnum = false; // Set to true to enable debug mode
}

public static class Breadcrumbs
{
	public static class Start
	{
		public const string Text = "Begin Registration";
		public const string Icon = "fas fa-caret-right";
	}

	public static class BackTo
	{
		public const string Text = "Registration Steps";
		public const string Icon = "fas fa-campground";
	}
}
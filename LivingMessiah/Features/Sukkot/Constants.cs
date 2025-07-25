﻿namespace LivingMessiah.Features.Sukkot.Constants;

// ToDo: Namespace does not match the folder structure

/*
namespace LivingMessiah.Features.Sukkot;

using static LivingMessiah.Features.Sukkot.Constants.Blobs;
- see Features\Sukkot\Documents.razor
*/

public static class AttendanceDateRange
{
	public static DateTime Start { get; set; } = DateTime.Parse("2025-10-05");
	public static DateTime Finish { get; set; } = DateTime.Parse("2025-10-14");
}

public static class LodgingDateRange
{
	public static DateTime Start { get; set; } = AttendanceDateRange.Start; // DateTime.Parse("2025-10-05");
	public static DateTime Finish { get; set; } = AttendanceDateRange.Finish; // DateTime.Parse("2025-10-14");
}


public static class Year
{
	public const int Int = 2025;
	public const string String = "2025";
}

public static class RegistrationClosedEmail
{
	public static string Subject { get; set; } = $"Late Sukkot {Year.String} Registration Question";
	public const string Name = "Mark";
	public const string Email = "mark@livingmessiah.com";
}
//ToDo not being used
public static class Stripe
{
	public static string ItemMessage { get; set; } = $"Sukkot {Year.String} Payment";
	public const string PaymentQuestionsEmail = "mailto:john@livingmessiah.com";
}

//ToDo: not sure if this is used anywhere
public static class Routing
{
	public const string PaymentConfirm = "Sukkot/PaymentConfirm";
	public const string PaymentConfirmHtml = "Sukkot/PaymentConfirm.html";
	public const string PaymentCanceled = "Sukkot/PaymentCanceled";
	public const string PaymentCanceledHtml = "Sukkot/PaymentCanceled.html";
}

public static class TShirts
{
	public static bool IsAvailableForSale { get; set; } = false;
	public static string ForSaleMessage { get; set; } = "T-Shirts are available for sale, click the image below.";
	public static string ComingSoonMessage { get; set; } = "T-Shirts not yet available, check back later. ";

	public static string ImgSrc
	{
		get
		{
			return Blobs.UrlRoot("sukkot-2024-tee-shirts.jpg");
		}
	}
	public static string Href { get; set; } = "https://aboveallimages.net/shop/ols/categories/sukkot";
	public static string Title { get; set; } = "Sukkot T-Shirts for sale";
}
// ToDo: not being used
public static class Features
{
	public static bool IsTrackingPassports { get; set; } = false;
}

//ToDo not being used
public static class PostActions
{
	public const string Create = "Create";
	public const string Edit = "Edit";
}

public static class PDFs
{
	public const string RegistrationWalkThrough = "sukkot-registration-walkthrough-users-manual.pdf";
	public const string StripeWalkThrough = "sukkot-making-a-payment-with-stripe.pdf";  // ToDo: these needs to be updated
	public const string Schedule = "sukkot-2025-schedule.pdf";
	public const string LiabilityWaiver = "sukkot-2022-liability-waiver.pdf"; // NOT DONE YET
	public const string HouseRules = "sukkot-2025-house-rules.pdf";
	public const string LegalAgreementVerbiage = "Sukkot-2024-Legal-Agreement-Verbiage.pdf";

	// Where are these used?, both written 8/7/2021
	public const string WindmillRanchSupplementalRules = "windmill-ranch-supplemental-rules.pdf";
	public const string WindmillRanchSurroundingArea = "windmill-ranch-surrounding-area.pdf";

	//public const string ActivitiesForChildren = "sukkot-2019-activities.pdf";
	public const string Passport = "passport-v1.pdf";
}

public static class Blobs
{
	private const string root = "https://livingmessiahstorage.blob.core.windows.net/images/";
	private const string maps = "https://livingmessiahstorage.blob.core.windows.net/images/events/";
	private const string pdf = "https://livingmessiahstorage.blob.core.windows.net/pdfs/";
	private const string events = "https://livingmessiahstorage.blob.core.windows.net/images/events/";

	public static string Url(string blob)
	{
		return maps + blob;
	}
	public static string UrlPDF(string blob)
	{
		return pdf + blob;
	}

	public static string UrlRoot(string blob)
	{
		return root + blob;
	}

	public static string UrlEvents(string blob)
	{
		return events + blob;
	}
}

public static class RegistrationMeta
{
	public static bool IsThereEarlyRegistration { get; set; } = false;
	public static System.DateTime EarlyRegistrationLastDay = new System.DateTime(Year.Int, 9, 16);
	public const decimal EarlyRegistrationFee = 75.0m;
	public static System.DateTime RegistrationLastDay = new System.DateTime(Year.Int, 10, 1);
	public const decimal RegistrationFee = 100.0m;
}

public static class Other
{
	public const string Banner = "2025-sukkot-banner-550-x-550.jpg";
	public const string BannerAlt = $"{Year.String} Sukkot Registration Banner";
	public static string PrintTitle { get; set; } = $"Living Messiah Sukkot Registration {Year.String}";

	public const bool IsRvHookupsAvailable = true;
	public const string ModalIdHouseRulesXs = "houserulesxs";
	public const string ModalIdHouseRules = "houserules";

	public const string PayWithCheckModalMessage = @$"
By clicking the <i class='fa fa-print'></i> <span class='text-muted'>Print</span> button,
you can choose where to print your registration form.
If for some reason you can not print the form that is OK, it just makes it convenient to process and it shows clearly the intent of your check.
<br /><br />The address of Living Messiah Ministries will be on the bottom of the form.
Make the check payable to <b>Living Messiah</b> and attach it to the printed out form before you mail it.
<br /><br />Please put the <b>registration id</b> on the check and write <b>Sukkot {Year.String} Payment</b>.  
<br /><br />Thanks!
";
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
namespace LivingMessiah.Features.Sukkot.Enums;

using Ardalis.SmartEnum;

public abstract class Pages : SmartEnum<Pages>
{
	#region Id's
	private static class Id
	{
		internal const int RegistrationSteps = 1;
		internal const int Print = 2;

		//Stripe
		internal const int Payment = 3;
		internal const int CancelRegistration = 4;
		internal const int ConfirmRegistration = 5;

		//Manage
		internal const int ManageRegistration = 6;
		internal const int ManageNotes = 7;
		internal const int DeleteConfirmation = 8;

		//Admin
		internal const int AttendanceAllFeastDays = 9;
		internal const int AttendanceChart = 10;
		internal const int Donations = 11;
		internal const int LegalAgreementVerbiage = 12;

	}
	#endregion

	#region  Declared Public Instances
	public static readonly Pages RegistrationSteps = new RegistrationStepsSE();
	public static readonly Pages Print = new PrintSE();
	public static readonly Pages Payment = new PaymentSE();
	public static readonly Pages CancelRegistration = new CancelRegistrationSE();
	public static readonly Pages ConfirmRegistration = new ConfirmRegistrationSE();
	public static readonly Pages ManageRegistration = new ManageRegistrationSE();
	public static readonly Pages ManageNotes = new ManageNotesSE();
	public static readonly Pages DeleteConfirmation = new DeleteConfirmationSE();
/*
	public static readonly Pages AttendanceAllFeastDays = new AttendanceAllFeastDaysSE();
	public static readonly Pages AttendanceChart = new AttendanceChartSE();
*/
	public static readonly Pages Donations = new DonationsSE();
	public static readonly Pages LegalAgreementVerbiage = new LegalAgreementVerbiageSE();
	//
	// SE=SmartEnum
	#endregion

	private Pages(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	#endregion

	#region Private Instantiation

	private sealed class RegistrationStepsSE : Pages
	{
		public RegistrationStepsSE() : base($"{nameof(Id.RegistrationSteps)}", Id.RegistrationSteps) { }
		public override string Index => "/Sukkot/RegistrationSteps";
		public override string Title => "Registration Steps";
		public override string Icon => "fas fa-campground";
		/*
		See LivingMessiah.Features.Sukkot.Constants!Breadcrumbs.[Start][BackTo]
		 */
	}

	private sealed class PrintSE : Pages
	{
		public PrintSE() : base($"{nameof(Id.Print)}", Id.Print) { }
		public override string Index => "/Sukkot/Print"; // used to be /Sukkot/Details
		public override string Title => "Sukkot Print"; // or Sukkot Print Friendly
		public override string Icon => "";
	}

	private sealed class PaymentSE : Pages
	{
		public PaymentSE() : base($"{nameof(Id.Payment)}", Id.Payment) { }
		public override string Index => "/Sukkot/Payment";
		public override string Title => "Donations Earmarked for Sukkot";
		public override string Icon => "fab fa-cc-stripe";
	}

	private sealed class CancelRegistrationSE : Pages
	{
		public CancelRegistrationSE() : base($"{nameof(Id.CancelRegistration)}", Id.CancelRegistration) { }
		public override string Index => "/cancel_donation.html";
		public override string Title => "Sukkot Registration Canceled";
		public override string Icon => "fab fa-cc-stripe"; // fab fa-cc-stripe fab fa-stripe-s fab fa-stripe
	}

	private sealed class ConfirmRegistrationSE : Pages
	{
		public ConfirmRegistrationSE() : base($"{nameof(Id.ConfirmRegistration)}", Id.ConfirmRegistration) { }
		public override string Index => "/confirm_donation.html";
		public override string Title => "Sukkot Registration Confirmed";
		public override string Icon => "fab fa-cc-stripe";
	}

	private sealed class ManageRegistrationSE : Pages
	{
		public ManageRegistrationSE() : base($"{nameof(Id.ManageRegistration)}", Id.ManageRegistration) { }
		public override string Index => "Sukkot/ManageRegistration";
		public override string Title => "Manage Registration";
		public override string Icon => "fas fa-mask";
	}

	private sealed class ManageNotesSE : Pages
	{
		public ManageNotesSE() : base($"{nameof(Id.ManageNotes)}", Id.ManageNotes) { }
		public override string Index => "/SukkotAdmin/Notes";
		public override string Title => "Sukkot Registration Notes";
		//public const string IconText = "Notes";
		public override string Icon => "far fa-sticky-note";
	}

	private sealed class DeleteConfirmationSE : Pages
	{
		public DeleteConfirmationSE() : base($"{nameof(Id.DeleteConfirmation)}", Id.DeleteConfirmation) { }
		public override string Index => "/Sukkot/DeleteConfirmation";
		public override string Title => "Delete Sukkot Registration?";
		//public const string DeleteConfirmationSubTitle = "Delete Registration? | ";
		public override string Icon => "";
	}

	/*
	public const string AttendanceAllFeastDays = "/SukkotAdmin/AttendanceAllFeastDays";
	public const string AttendanceChart = "/SukkotAdmin/AttendanceChart";
	*/
	
	private sealed class DonationsSE : Pages
	{
		public DonationsSE() : base($"{nameof(Id.Donations)}", Id.Donations) { }
		public override string Index => "/SukkotAdmin/DonationsGrid";
		public override string Title => "Sukkot Admin DonationsGrid";
		public override string Icon => "";
	}

	private sealed class LegalAgreementVerbiageSE : Pages
	{
		public LegalAgreementVerbiageSE() : base($"{nameof(Id.LegalAgreementVerbiage)}", Id.LegalAgreementVerbiage) { }
		public override string Index => "/SukkotAdmin/LegalAgreementVerbiage";
		public override string Title => "Legal Agreement Verbiage";
		public override string Icon => "fas fa-balance-scale";
	}


	#endregion

}


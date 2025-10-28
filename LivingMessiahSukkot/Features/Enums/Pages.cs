namespace LivingMessiahSukkot.Features.Enums;

using Ardalis.SmartEnum;

// ToDo: rename this as `NavSukkot` 
public abstract class Pages : SmartEnum<Pages>
{
	#region Id's
	private static class Id
	{
		internal const int RegistrationSteps = 1;
		internal const int Print = 2;
		internal const int Payment = 3;
		internal const int CancelRegistration = 4;
		internal const int ConfirmRegistration = 5;
		//internal const int DeleteConfirmation = 6;  // ToDo: Not implemented yet

	}
	#endregion

	#region  Declared Public Instances
	public static readonly Pages RegistrationSteps = new RegistrationStepsSE();
	public static readonly Pages Print = new PrintSE();
	public static readonly Pages Payment = new PaymentSE();
	public static readonly Pages CancelRegistration = new CancelRegistrationSE();
	public static readonly Pages ConfirmRegistration = new ConfirmRegistrationSE();
	//public static readonly Pages DeleteConfirmation = new DeleteConfirmationSE(); // ToDo: Not implemented yet

	// SE=SmartEnum
	#endregion

	private Pages(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	#endregion

	#region Private Instantiation

	/* See LivingMessiah.Features.Sukkot.Constants!Breadcrumbs.[Start][BackTo] */
	private sealed class RegistrationStepsSE : Pages
	{
		public RegistrationStepsSE() : base($"{nameof(Id.RegistrationSteps)}", Id.RegistrationSteps) { }
		public override string Index => "/RegistrationSteps";
		public override string Title => "Registration Steps";
		public override string Icon => "fas fa-campground";
	}

	private sealed class PrintSE : Pages
	{
		public PrintSE() : base($"{nameof(Id.Print)}", Id.Print) { }
		public override string Index => "/Print"; // used to be /Sukkot/Details
		public override string Title => "Sukkot Print"; // or Sukkot Print Friendly
		public override string Icon => "";
	}

	private sealed class PaymentSE : Pages
	{
		public PaymentSE() : base($"{nameof(Id.Payment)}", Id.Payment) { }
		public override string Index => "/Payment";
		public override string Title => "Donations Earmarked for Sukkot";
		public override string Icon => "fab fa-cc-stripe";
	}

	private sealed class CancelRegistrationSE : Pages
	{
		public CancelRegistrationSE() : base($"{nameof(Id.CancelRegistration)}", Id.CancelRegistration) { }
		public override string Index => "/cancel_donation.html";
		public override string Title => "Transaction Canceled";
		public override string Icon => "fab fa-cc-stripe"; // fab fa-cc-stripe fab fa-stripe-s fab fa-stripe
	}

	private sealed class ConfirmRegistrationSE : Pages
	{
		public ConfirmRegistrationSE() : base($"{nameof(Id.ConfirmRegistration)}", Id.ConfirmRegistration) { }
		public override string Index => "/confirm_donation.html";
		public override string Title => "Transaction Confirmed";
		public override string Icon => "fab fa-cc-stripe";
	}

	/*
	private sealed class DeleteConfirmationSE : Pages
	{
		public DeleteConfirmationSE() : base($"{nameof(Id.DeleteConfirmation)}", Id.DeleteConfirmation) { }
		public override string Index => "/Sukkot/DeleteConfirmation";
		public override string Title => "Delete Sukkot Registration?";
		//public const string DeleteConfirmationSubTitle = "Delete Registration? | ";
		public override string Icon => "";
	}
	*/
	#endregion

}

namespace LivingMessiahSukkot.Enums;

using Ardalis.SmartEnum;

public abstract class Nav : SmartEnum<Nav>
{
	#region Id's
	private static class Id
	{
		internal const int LandingPage = 1;
		internal const int RegistrationSteps = 2;
		internal const int Print = 3;
		internal const int Payment = 4;
		internal const int CancelRegistration = 5;
		internal const int ConfirmRegistration = 6;
		//internal const int DeleteConfirmation = 7;  // ToDo: Not implemented yet
		internal const int Profile = 8;
		internal const int AuthCanceled = 9;

	}
	#endregion

	#region  Declared Public Instances
	public static readonly Nav LandingPage = new LandingPageSE();
	public static readonly Nav RegistrationSteps = new RegistrationStepsSE();
	public static readonly Nav Print = new PrintSE();
	public static readonly Nav Payment = new PaymentSE();
	public static readonly Nav CancelRegistration = new CancelRegistrationSE();
	public static readonly Nav ConfirmRegistration = new ConfirmRegistrationSE();
	//public static readonly Pages DeleteConfirmation = new DeleteConfirmationSE(); // ToDo: Not implemented yet
	public static readonly Nav Profile = new ProfileSE();
	public static readonly Nav AuthCanceled = new AuthCanceledSE();

	// SE=SmartEnum
	#endregion

	private Nav(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	#endregion

	#region Private Instantiation

	private sealed class LandingPageSE : Nav
	{
		public LandingPageSE() : base($"{nameof(Id.LandingPage)}", Id.LandingPage) { }
		public override string Index => "/";
		public override string Title => "Sukkot";
		public override string Icon => "fas fa-home";
	}

	private sealed class RegistrationStepsSE : Nav
	{
		public RegistrationStepsSE() : base($"{nameof(Id.RegistrationSteps)}", Id.RegistrationSteps) { }
		public override string Index => "/RegistrationSteps";
		public override string Title => "Registration Steps";
		public override string Icon => "fas fa-campground";
	}

	private sealed class PrintSE : Nav
	{
		public PrintSE() : base($"{nameof(Id.Print)}", Id.Print) { }
		public override string Index => "/Print"; // used to be /Sukkot/Details
		public override string Title => "Sukkot Print"; // or Sukkot Print Friendly
		public override string Icon => "";
	}

	private sealed class PaymentSE : Nav
	{
		public PaymentSE() : base($"{nameof(Id.Payment)}", Id.Payment) { }
		public override string Index => "/Payment";
		public override string Title => "Donations Earmarked for Sukkot";
		public override string Icon => "fab fa-cc-stripe";
	}

	private sealed class CancelRegistrationSE : Nav
	{
		public CancelRegistrationSE() : base($"{nameof(Id.CancelRegistration)}", Id.CancelRegistration) { }
		public override string Index => "/cancel_donation.html";
		public override string Title => "Transaction Canceled";
		public override string Icon => "fab fa-cc-stripe"; // fab fa-cc-stripe fab fa-stripe-s fab fa-stripe
	}

	private sealed class ConfirmRegistrationSE : Nav
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
		public override string Index => "/DeleteConfirmation";
		public override string Title => "Delete Sukkot Registration?";
		//public const string DeleteConfirmationSubTitle = "Delete Registration? | ";
		public override string Icon => "";
	}
	*/

	private sealed class ProfileSE : Nav
	{
		public ProfileSE() : base($"{nameof(Id.Profile)}", Id.Profile) { }
		public override string Index => "/Profile";
		public override string Title => "Profile";
		public override string Icon => "fab fa-superpowers";
	}

	private sealed class AuthCanceledSE : Nav
	{
		public AuthCanceledSE() : base($"{nameof(Id.AuthCanceled)}", Id.AuthCanceled) { }
		public override string Index => "/auth-failed";
		public override string Title => "Login Canceled";
		public override string Icon => "far fa-window-close";  
	}	

	// AuthCanceledSE

	#endregion

}

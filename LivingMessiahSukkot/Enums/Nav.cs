namespace LivingMessiahSukkot.Enums;

using Ardalis.SmartEnum;

public abstract class Nav : SmartEnum<Nav>
{
	#region Id's
	private static class Id
	{
		internal const int LandingPage = 1;
		internal const int Steps = 2;
		internal const int Print = 3;
		internal const int CancelRegistration = 4;
		internal const int ConfirmRegistration = 5;
		//internal const int DeleteConfirmation = 6;  // ToDo: Not implemented yet
		internal const int Profile = 7;
		internal const int AuthCanceled = 8;

	}
	#endregion

	#region  Declared Public Instances
	public static readonly Nav LandingPage = new LandingPageSE();
	public static readonly Nav Steps = new StepsSE();
	public static readonly Nav Print = new PrintSE();
	public static readonly Nav CancelRegistration = new CancelRegistrationSE();
	public static readonly Nav ConfirmRegistration = new ConfirmRegistrationSE();
	//public static readonly Nav DeleteConfirmation = new DeleteConfirmationSE(); // ToDo: Not implemented yet
	public static readonly Nav Profile = new ProfileSE();
	public static readonly Nav AuthCanceled = new AuthCanceledSE();

	// SE=SmartEnum
	#endregion

	private Nav(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract bool IsOnToolbar { get; }
	//public abstract string Label { get; } ToDo: do I need this? gotten from deleted NavButton.cs
	#endregion

	#region Private Instantiation

	private sealed class LandingPageSE : Nav
	{
		public LandingPageSE() : base($"{nameof(Id.LandingPage)}", Id.LandingPage) { }
		public override string Index => "/";
		public override string Title => "Sukkot";
		public override string Icon => "fas fa-home";
		public override bool IsOnToolbar => false;
	}

	private sealed class StepsSE : Nav
	{
		public StepsSE() : base($"{nameof(Id.Steps)}", Id.Steps) { }
		public override string Index => "/Steps";
		public override string Title => "Steps";
		public override string Icon => "fas fa-shoe-prints";  // fas fa-campground
		public override bool IsOnToolbar => true;
	}

	private sealed class PrintSE : Nav
	{
		public PrintSE() : base($"{nameof(Id.Print)}", Id.Print) { }
		public override string Index => "/Print"; 
		public override string Title => "Print"; 
		public override string Icon => "fas fa-print";
		public override bool IsOnToolbar => true;
	}

	private sealed class CancelRegistrationSE : Nav
	{
		public CancelRegistrationSE() : base($"{nameof(Id.CancelRegistration)}", Id.CancelRegistration) { }
		public override string Index => "/cancel_donation.html";
		public override string Title => "Transaction Canceled";
		public override string Icon => "fab fa-cc-stripe"; // fab fa-cc-stripe fab fa-stripe-s fab fa-stripe
		public override bool IsOnToolbar => false;
	}

	private sealed class ConfirmRegistrationSE : Nav
	{
		public ConfirmRegistrationSE() : base($"{nameof(Id.ConfirmRegistration)}", Id.ConfirmRegistration) { }
		public override string Index => "/confirm_donation.html";
		public override string Title => "Transaction Confirmed";
		public override string Icon => "fab fa-cc-stripe";
		public override bool IsOnToolbar => false;
	}

	/*
	private sealed class DeleteConfirmationSE : Nav
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
		public override bool IsOnToolbar => true;
	}

	private sealed class AuthCanceledSE : Nav
	{
		public AuthCanceledSE() : base($"{nameof(Id.AuthCanceled)}", Id.AuthCanceled) { }
		public override string Index => "/auth-failed";
		public override string Title => "Login Canceled";
		public override string Icon => "far fa-window-close";  
		public override bool IsOnToolbar => false;
	}	

	// AuthCanceledSE

	#endregion

}

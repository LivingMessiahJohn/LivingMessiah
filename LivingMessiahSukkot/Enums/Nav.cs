using Ardalis.SmartEnum;
using LivingMessiahSukkot.Security.Constants;

namespace LivingMessiahSukkot.Enums;

public abstract class Nav : SmartEnum<Nav>
{
	#region Id's
	private static class Id
	{
		internal const int LandingPage = 1;
		internal const int Steps = 2;
		internal const int Print = 3;
		internal const int About = 4;
		internal const int CancelRegistration = 5;
		internal const int ConfirmRegistration = 6;
		//internal const int DeleteConfirmation = 7;  // ToDo: Not implemented yet
		internal const int Profile = 8;
		internal const int AuthCanceled = 9;
		internal const int Login = 10;
		internal const int Logout = 11;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Nav LandingPage = new LandingPageSE();
	public static readonly Nav Steps = new StepsSE();
	public static readonly Nav Print = new PrintSE();
	public static readonly Nav About = new AboutSE();
	public static readonly Nav CancelRegistration = new CancelRegistrationSE();
	public static readonly Nav ConfirmRegistration = new ConfirmRegistrationSE();
	//public static readonly Nav DeleteConfirmation = new DeleteConfirmationSE(); // ToDo: Not implemented yet
	public static readonly Nav Profile = new ProfileSE();
	public static readonly Nav AuthCanceled = new AuthCanceledSE();

	public static readonly Nav Login = new LoginSE();
	public static readonly Nav Logout = new LogoutSE();

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

	private sealed class AboutSE : Nav
	{
		public AboutSE() : base($"{nameof(Id.About)}", Id.About) { }
		public override string Index => "/About";
		public override string Title => "About";
		public override string Icon => "fas fa-info";
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
		public override string Index => AuthFailedRedirect.Url;   // This is resolved by AddAuth0Authentication
		public override string Title => "Login Canceled";
		public override string Icon => "far fa-window-close";  
		public override bool IsOnToolbar => false;
	}

	private sealed class LoginSE : Nav
	{
		public LoginSE() : base($"{nameof(Id.Login)}", Id.Login) { }
		public override string Index => "/Account/Login";
		public override string Title => "Login";  //Log in
		public override string Icon => "fas fa-sign-in-alt";
		public override bool IsOnToolbar => false;
	}

	private sealed class LogoutSE : Nav
	{
		public LogoutSE() : base($"{nameof(Id.Logout)}", Id.Logout) { }
		public override string Index => "/Account/Logout";
		public override string Title => "Log out";
		public override string Icon => "fas fa-sign-in-alt";
		public override bool IsOnToolbar => false;
	}


	// AuthCanceledSE

	#endregion

}

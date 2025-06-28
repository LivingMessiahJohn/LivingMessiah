using Ardalis.SmartEnum;

namespace LivingMessiah.Enums;

public abstract class Account : SmartEnum<Account>
{
	#region Id's
	private static class Id
	{
		internal const int Login = 1;
		internal const int Logout = 2;
		//internal const int PasswordChanged = 3; Moved to Nav
		//internal const int Profile = 4; Moved to Nav
	}
	#endregion

	#region Declared Public Instances
	public static readonly Account Login = new LoginSE();
	public static readonly Account Logout = new LogoutSE();
	//public static readonly Account PasswordChanged = new PasswordChangedSE(); Moved to Nav
	//public static readonly Account Profile = new ProfileSE();  Moved to Nav
	#endregion

	private Account(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract string Action { get; }
	#endregion

	#region Private Instantiation

	private sealed class LoginSE : Account
	{
		public LoginSE() : base($"{nameof(Id.Login)}", Id.Login) { }
		public override string Index => "/Account/Login";
		public override string Title => "Login";  //Log in
		public override string Icon => "fas fa-sign-in-alt";
		public override string Action => "";
	}

	private sealed class LogoutSE : Account
	{
		public LogoutSE() : base($"{nameof(Id.Logout)}", Id.Logout) { }
		public override string Index => "/Account/Logout";
		public override string Title => "Log out";
		public override string Icon => "fas fa-sign-in-alt";
		public override string Action => "Account/LogOut";
	}

	/*
	PasswordChanged and Profile moved to Nav

	private sealed class PasswordChangedSE : Account
	{
		public PasswordChangedSE() : base($"{nameof(Id.PasswordChanged)}", Id.PasswordChanged) { }
		public override string Index => "/Account/PasswordChanged";
		public override string Title => "Password Changed Successfully";
		//public const string PageTitle = " Password Changed";
		public override string Icon => "fas fa-key";
		public override string Action => "";
	}

	private sealed class ProfileSE : Account
	{
		public ProfileSE() : base($"{nameof(Id.Profile)}", Id.Profile) { }
		public override string Index => "/account/profile";
		public override string Title => " Profile";
		public override string Icon => "fab fa-superpowers";
	}
	*/
	#endregion
}



/*
Used in Login-/Logout- and Profile-views
public static class AccountConstants
{
	public const string LogoutAction = "Account/LogOut";
	public const string TitleAccessDenied = "Access Denied.";
	public const string TitleProfile = "Profile";
	
	public static class Icons
	{
		public const string Claims = "fab fa-superpowers";
		public const string ProfileVerified = "fas fa-check";
		public const string ProfileNotVerified = "fas fa-question";
		public const string Profile = "fas fa-user";
	}
}
*/




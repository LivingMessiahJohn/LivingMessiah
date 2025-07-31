using Ardalis.SmartEnum;

namespace LivingMessiahAdmin.Enums;

public abstract class Account : SmartEnum<Account>
{
	#region Id's
	private static class Id
	{
		internal const int Login = 1;
		internal const int Logout = 2;
	}
	#endregion

	#region Declared Public Instances
	public static readonly Account Login = new LoginSE();
	public static readonly Account Logout = new LogoutSE();
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

	#endregion
}






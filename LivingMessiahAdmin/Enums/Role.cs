using Ardalis.SmartEnum;

namespace LivingMessiahAdmin.Enums;

public abstract class Role : SmartEnum<Role>
{

	#region Id's
	private static class Id
	{
		internal const int Admin = 1;
		internal const int Announcements = 2; // Features\SpecialEvents\Index.razor  RoleAnnouncements@livingmessiah.com

		//internal const int Elder = 3;         // Features\Contacts\Index.razor
		internal const int KeyDates = 4;      // Features\Calendar\ManageKeyDates\EditGrid.razor.cs
		internal const int Sukkot = 5;        // Features\Sukkot\*
		internal const int SukkotHost = 6;    // Features\Sukkot\Reports
		internal const int SukkotOrSukkotHost = 7;    // Features???
		//internal const int User = 8;
	}
	#endregion

	#region Declared Public Instances
	public static readonly Role Admin = new AdminSE();
	public static readonly Role Announcements = new AnnouncementsSE();
	//public static readonly Role Elder = new ElderSE();
	public static readonly Role KeyDates = new KeyDatesSE();
	public static readonly Role Sukkot = new SukkotSE();
	public static readonly Role SukkotHost = new SukkotHostSE();
	public static readonly Role SukkotOrSukkotHost = new SukkotOrSukkotHostSE();
	//public static readonly Role User = new UserSE();
	#endregion

	private Role(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Claim { get; }

	//Properties

	// *.Claim + Admin.Claim
	//public string AndAdminClaim => $"{this.Claim}, {Admin.Claim}";

	// Combinations
	//public string SukkotOrSukkotHost = $"{Sukkot.Claim}, {SukkotHost.Claim}";
	//public string AdminOrSukkotOrSukkotHost = $"{Admin.Claim}, {Sukkot.Claim}, {SukkotHost.Claim}";
	
	/*
	public string AdminOrAnnouncements = $"{Announcements.Claim}, {Admin.Claim}";
	public string AdminOrElder = $"{Elder.Claim}, {Admin.Claim}";
	public string AdminOrKeyDates = $"{KeyDates.Claim}, {Admin.Claim}";
	public string AdminOrSukkot = $"{Sukkot.Claim}, {Admin.Claim}"; 
	public string AdminOrSukkotHost = $"{SukkotHost.Claim}, {Admin.Claim}";
	*/

	// "admin, sukkot";

	#endregion

	#region Private Instantiation

	private sealed class AdminSE : Role
	{
		public AdminSE() : base($"{nameof(Id.Admin)}", Id.Admin) { }
		public override string Claim => "admin";
	}

	private sealed class AnnouncementsSE : Role
	{
		public AnnouncementsSE() : base($"{nameof(Id.Announcements)}", Id.Announcements) { }
		public override string Claim => "announcements";
	}

	/*
	private sealed class ElderSE : Role
	{
		public ElderSE() : base($"{nameof(Id.Elder)}", Id.Elder) { }
		public override string Claim => "elder";
	}
	*/

	private sealed class KeyDatesSE : Role
	{
		public KeyDatesSE() : base($"{nameof(Id.KeyDates)}", Id.KeyDates) { }
		public override string Claim => "keydates";
	}

	private sealed class SukkotSE : Role
	{
		public SukkotSE() : base($"{nameof(Id.Sukkot)}", Id.Sukkot) { }
		public override string Claim => "sukkot";
	}

	private sealed class SukkotHostSE : Role
	{
		public SukkotHostSE() : base($"{nameof(Id.SukkotHost)}", Id.SukkotHost) { }
		public override string Claim => "sukkothost";
	}

	private sealed class SukkotOrSukkotHostSE : Role
	{
		public SukkotOrSukkotHostSE() : base($"{nameof(Id.SukkotOrSukkotHost)}", Id.SukkotOrSukkotHost) { }
		public override string Claim => $"{Sukkot.Claim}, {SukkotHost.Claim}";
	}


	/*	
	private sealed class UserSE : Role
	{
		public UserSE() : base($"{nameof(Id.User)}", Id.User) { }
		public override string Claim => "user";
	}
	*/

	#endregion
}

public static class RoleGroup
{
	public const string Announcements = "Announcements";
	public const string KeyDates = "KeyDates";
	public const string Sukkot = "Sukkot";
	public const string SukkotHost = "SukkotHost";

	public static class EmailVerified
	{
		public const string Name = "EmailVerified";
		public const string Claim = "email_verified";
	}


}


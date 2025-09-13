using Ardalis.SmartEnum;

namespace LivingMessiahAdmin.Enums;

public abstract class Role : SmartEnum<Role>
{
	// This is a SmartEnum the leverages Bitwise, therefor all the Id values need to be powers of two

	#region Id's
	private static class BitwiseId
	{
		//internal const int All = -1;
		internal const int None = 0;
		internal const int Admin = 1;
		internal const int Announcements = 2; // AKA SpecialEvents, not yet done
		internal const int KeyDates = 4;  // RoleGroup.KeyDates, policy =>	policy.RequireAssertion(context => (RoleEnum.KeyDates.Claim) || (RoleEnum.KeyDates.Admin)
		internal const int Sukkot = 8;
		internal const int SukkotHost = 16;
	}
	#endregion

	#region Declared Public Instances
	public static readonly Role Admin = new AdminSE();
	public static readonly Role Announcements = new AnnouncementsSE();
	public static readonly Role KeyDates = new KeyDatesSE();
	public static readonly Role Sukkot = new SukkotSE();
	public static readonly Role SukkotHost = new SukkotHostSE();
	#endregion

	private Role(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Claim { get; }
	#endregion

	#region Private Instantiation

	private sealed class AdminSE : Role
	{
		public AdminSE() : base($"{nameof(BitwiseId.Admin)}", BitwiseId.Admin) { }
		public override string Claim => "admin";
	}

	private sealed class AnnouncementsSE : Role
	{
		public AnnouncementsSE() : base($"{nameof(BitwiseId.Announcements)}", BitwiseId.Announcements) { }
		public override string Claim => "announcements";
	}

	private sealed class KeyDatesSE : Role
	{
		public KeyDatesSE() : base($"{nameof(BitwiseId.KeyDates)}", BitwiseId.KeyDates) { }
		public override string Claim => "keydates";
	}

	private sealed class SukkotSE : Role
	{
		public SukkotSE() : base($"{nameof(BitwiseId.Sukkot)}", BitwiseId.Sukkot) { }
		public override string Claim => "sukkot";
	}

	private sealed class SukkotHostSE : Role
	{
		public SukkotHostSE() : base($"{nameof(BitwiseId.SukkotHost)}", BitwiseId.SukkotHost) { }
		public override string Claim => "sukkothost";
	}

	#endregion
}

public static class RoleGroup
{
	public const string EmailVerifiedWithAtLeastOneRole = "EmailVerifiedWithAtLeastOneRole";
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


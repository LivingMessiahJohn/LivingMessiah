namespace LivingMessiah.SecurityRoot;

public static class Auth0
{
	public const string MicrosoftSchemaIdentityClaimsRole = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

	public static class Configuration
	{
		public const string Domain = "auth0:Domain";
		public const string ClientId = "auth0:ClientId";
		public const string ClientSecret = "auth0:ClientSecret";
	}

	/*
	public static class Tokens
	{
		public const string Access = "access_token";
		public const string Id = "id_token";
	}
	*/

	public static class Roles
	{
		public const string Admin = "admin";
		public const string User = "user";
		public const string ManageRegistration = "superuser";
		public const string Sukkot = "sukkot";
		public const string KeyDates = "keydates";
		public const string Elder = "elder";
		public const string AudioVisual = "audiovisual";
		public const string AdminOrElder = "admin, elder";
		public const string Announcements = "announcements";
		public const string AdminOrSukkot = "admin, sukkot";
		public const string AdminOrAnnouncements = "admin, announcements";
		public const string AdminOrSukkotOrElder = "admin, sukkot, elder";
		public const string AdminOrAudiovisual = "admin, audiovisual";
		public const string AdminOrKeyDates = "admin, keydates";
		public const string SukkotMenuBar = Elder + "," + Admin + "," + ManageRegistration + "," + Sukkot;
	}

}

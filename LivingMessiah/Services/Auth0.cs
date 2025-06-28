
namespace LivingMessiah.Services;

public static class Auth0
{
	public const string SchemeName = "Auth0";
	public const string CallbackPath = "/callback";
	public const string SchemaNameSpace = "https://schemas.livingmessiah.com/roles";

	public static class Configuration
	{
	// using auth0 instead of Auth0 which was in version 7
		public const string Domain = "auth0:Domain";
		public const string ClientId = "auth0:ClientId";
		public const string ClientSecret = "auth0:ClientSecret";
	}

	public static class Tokens
	{
		public const string Access = "access_token";
		public const string Id = "id_token";
	}

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

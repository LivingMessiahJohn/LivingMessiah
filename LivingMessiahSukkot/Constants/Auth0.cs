namespace LivingMessiahSukkot.Constants;

public static class Auth0
{
	public const string MicrosoftSchemaIdentityClaimsRole = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

	public static class Configuration
	{
		public const string Domain = "auth0:Domain";
		public const string ClientId = "auth0:ClientId";
		public const string ClientSecret = "auth0:ClientSecret";
	}

	public static class Policy
	{
		public const string Name = "EmailVerified";
		public const string Claim = "email_verified";
	}

	public static class Roles
	{
		public const string User = "user";
		public const string Admin = "admin";
	}

}

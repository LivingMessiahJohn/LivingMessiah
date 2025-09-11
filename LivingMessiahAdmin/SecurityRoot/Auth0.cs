namespace LivingMessiahAdmin.SecurityRoot;

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

}

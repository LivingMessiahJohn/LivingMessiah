namespace Admin.Features.Users.Settings;

/// <summary>
/// Machine-to-Machine credentials for Auth0 Management API (read-only user directory).
/// Separate from the Regular Web App <c>auth0:*</c> login settings.
/// </summary>
public class Auth0M2M
{
	public const string Section = "Auth0M2M";

	public string? Domain { get; set; }
	public string? ClientId { get; set; }
	public string? ClientSecret { get; set; }
}

using LivingMessiahSukkot.Security.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace LivingMessiahSukkot.Security;

public interface ISecurityHelper
{
	Task<string?> GetEmail();
	Task<(bool Passed, string ErrorMsg, bool SecurityOverride)> DoAuthentication(string email, string vwEmail);
	/*
	Task<bool> IsAuthenticated();
	Task<ClaimsPrincipal?> GetUser();
	*/
}

public class SecurityHelper : ISecurityHelper
{
	private readonly AuthenticationStateProvider _authenticationStateProvider;

	public SecurityHelper(AuthenticationStateProvider authenticationStateProvider)
	{
		_authenticationStateProvider = authenticationStateProvider;
	}

	public async Task<string?> GetEmail()
	{
		var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();

		if (authenticationState?.User?.Identity is null || !authenticationState.User.Identity.IsAuthenticated)	return null;

		return authenticationState.User.FindFirst(ClaimTypes.Email)?.Value;
	}

	public async Task<(bool Passed, string ErrorMsg, bool SecurityOverride)> DoAuthentication(string email, string queryEmail)
	{
		var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
		if (authenticationState is null) { return (false, "Authentication state is null.", false); }
		if (authenticationState?.User?.Identity is null) { return (false, "User identity is null.", false); }

		if (email == queryEmail) { return (true, string.Empty, false); }

		string[] _roles = authenticationState.User.Claims
		.Where(c => c.Type == Configuration.MicrosoftSchemaIdentityClaimsRole)
		.Select(c => c.Value)
		.ToArray();

		return _roles.Any() ? (true, string.Empty, true) : (false, "Security override also failed", false);
	}


	/*
	public async Task<bool> IsAuthenticated()
	{
		var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
		return authenticationState?.User?.Identity?.IsAuthenticated ?? false;
	}

	public async Task<ClaimsPrincipal?> GetUser()
	{
		var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
		return authenticationState?.User;
	}
	*/
}

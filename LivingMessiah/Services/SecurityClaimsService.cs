using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using static LivingMessiah.Services.Auth0;

namespace LivingMessiah.Services;

public interface ISecurityClaimsService
{
	Task<string> GetEmail();
	Task<string> GetRoles();
	Task<string> GetUserName();
	Task<bool> IsUserAuthorized(string registrationEmail);
	Task<bool> RoleHasAdminOrSukkot();
	Task<bool> AdminOrSukkotOverride();
	bool IsVerified();
	//bool Verified(ClaimsPrincipal user);
	//Task<bool> Verified(ClaimsPrincipal user);	
	//public static bool Verified(this ClaimsPrincipal user)
}

//ToDo:
//  Make this into a fluent API, because they all need to do GetUser
public class SecurityClaimsService : ISecurityClaimsService
{
	#region Constructor and DI

	private AuthenticationStateProvider ASP;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public SecurityClaimsService(AuthenticationStateProvider aSP)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{
		ASP = aSP;
	}
	#endregion

	ClaimsPrincipal _user;

	private async Task<ClaimsPrincipal> GetUser()
	{
		var authState = await ASP.GetAuthenticationStateAsync();
		return authState.User;
	}

	public async Task<string> GetEmail()
	{
		/*
		var authState = await ASP.GetAuthenticationStateAsync();
		ClaimsPrincipal User;
		User = authState.User;
		return User.GetUserEmail();
		ClaimsPrincipal User = await GetUser();
		return User.GetUserEmail();
		//return _user.GetUserEmail();
		*/
		_user = await GetUser();
		return _user.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;

	}
	
	public async Task<string> GetUserName()
	{
		_user = await GetUser();
		return _user.Claims?.FirstOrDefault(c => c.Type == "name")?.Value!;
	}
	/*
	public static string? GetUserName(this ClaimsPrincipal user)
	{
		return user.Claims?.FirstOrDefault(c => c.Type == "name")?.Value;
	}
	*/

	public async Task<string> GetRoles()
	{
		_user = await GetUser();

		string roles = "";
		foreach (var claim in _user.Claims)
		{
			if (claim.Type == SchemaNameSpace)
			{
				roles += claim.Value;
			}
		}

		if (roles.Length > 0 && roles.IndexOf(',') > 0)
		{
			roles.Remove(roles.IndexOf(','));
		}

		return roles;
	}

	// Authorized
	public async Task<bool> IsUserAuthorized(string registrationEmail)
	{
		_user = await GetUser();
		string sEmail = _user.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;

		if (sEmail == registrationEmail) { return true; }
		
		return SearchRoles(_user.Claims!, Roles.Admin, Roles.Sukkot);

	}

	//ToDo: Refactor this so that it can receive an array of strings, and/or convert the foreach into a LINQ statement
	private bool SearchRoles(IEnumerable<Claim> claims, string role1, string role2)
	{
		foreach (var claim in claims)
		{
			if (claim.Type == SchemaNameSpace && claim.Value == role1)
			{
				return true;
			}
			else
			{
				if (claim.Type == SchemaNameSpace && claim.Value == role2)
				{
					return true;
				}
			}
		}
		return false;
	}


	public async Task<bool> RoleHasAdminOrSukkot()
	{
		_user = await GetUser();
		return SearchRoles(_user.Claims, Roles.Admin, Roles.Sukkot);
	}

	public async Task<bool> AdminOrSukkotOverride()
	{
		string roles = await this.GetRoles();

		if (roles == Auth0.Roles.Admin | roles == Auth0.Roles.Sukkot)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool IsVerified()
	{
		// Check if the current user has an "email_verified" claim and parse its value as a bool
		if (_user != null && _user.Claims != null)
		{
			var emailVerifiedValue = _user.Claims.FirstOrDefault(c => c.Type == "email_verified")?.Value;
			if (!string.IsNullOrEmpty(emailVerifiedValue) && bool.TryParse(emailVerifiedValue, out bool isVerified))
			{
				return isVerified;
			}
		}
		return false;
	}

}



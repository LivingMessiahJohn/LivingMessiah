using System.Security.Claims;
using Admin.Security.Constants;
using RoleEnum = Admin.Security.Enums.Role;

namespace Admin.Security;

/// <summary>
/// Bitwise role helpers for Home <c>NavTree</c> filtering against <c>Nav.RequiredRoles</c>.
/// Driven by <see cref="RoleEnum.List"/> so new roles do not need a hardcoded claim loop.
/// </summary>
public static class NavRoleAuthorization
{
	public static int GetUserRoleBitmask(ClaimsPrincipal? user)
	{
		if (user?.Identity?.IsAuthenticated != true)
		{
			return 0;
		}

		var roleClaims = user.Claims
			.Where(c => c.Type == Configuration.MicrosoftSchemaIdentityClaimsRole)
			.Select(c => c.Value)
			.ToHashSet(StringComparer.Ordinal);

		int userRoles = 0;
		foreach (var role in RoleEnum.List)
		{
			if (roleClaims.Contains(role.Claim))
			{
				userRoles |= role.Value;  // `|=` is the bitwise OR assignment operator.
			}
		}

		return userRoles;
	}

	/// <summary>
	/// <paramref name="requiredRoles"/> == 0 means no specific role required (visible to authenticated users).
	/// Otherwise the user must have any overlapping bit with <paramref name="userRoles"/>.
	/// </summary>
	public static bool HasRequiredRole(int requiredRoles, int userRoles)
		=> requiredRoles == 0 || (userRoles & requiredRoles) != 0;
}

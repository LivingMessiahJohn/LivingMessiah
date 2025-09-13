using Microsoft.AspNetCore.Authorization;
using RoleEnum = LivingMessiahAdmin.Enums.Role;
using RoleGroup = LivingMessiahAdmin.Enums.RoleGroup;

namespace LivingMessiahAdmin.SecurityRoot;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
	{
		services.AddAuthorizationBuilder()
			.AddPolicy(RoleGroup.EmailVerified.Name, policy => policy.RequireClaim(RoleGroup.EmailVerified.Claim, "true"))

			// New combined policy: Email verified AND at least one role
			.AddPolicy(RoleGroup.EmailVerifiedWithAtLeastOneRole, policy =>
				policy.RequireAssertion(context =>
					context.User.HasClaim(RoleGroup.EmailVerified.Claim, "true") &&
					(context.User.IsInRole(RoleEnum.Announcements.Claim) ||
					 context.User.IsInRole(RoleEnum.KeyDates.Claim) ||
					 context.User.IsInRole(RoleEnum.Sukkot.Claim) ||
					 context.User.IsInRole(RoleEnum.SukkotHost.Claim) ||
					 context.User.IsInRole(RoleEnum.Admin.Claim))))

			.AddPolicy(RoleGroup.Announcements, policy =>
				policy.RequireAssertion(context =>
					context.User.IsInRole(RoleEnum.Announcements.Claim) ||
					context.User.IsInRole(RoleEnum.Admin.Claim)))

			.AddPolicy(RoleGroup.KeyDates, policy =>
				policy.RequireAssertion(context =>
					context.User.IsInRole(RoleEnum.KeyDates.Claim) ||
					context.User.IsInRole(RoleEnum.Admin.Claim)))

			.AddPolicy(RoleGroup.Sukkot, policy =>
				policy.RequireAssertion(context =>
					context.User.IsInRole(RoleEnum.Sukkot.Claim) ||
					context.User.IsInRole(RoleEnum.SukkotHost.Claim) ||
					context.User.IsInRole(RoleEnum.Admin.Claim)))

			.AddPolicy(RoleGroup.SukkotHost, policy =>
				policy.RequireAssertion(context =>
					context.User.IsInRole(RoleEnum.SukkotHost.Claim) ||
					context.User.IsInRole(RoleEnum.Admin.Claim)))

			.AddPolicy(RoleEnum.Admin.Name, policy => policy.RequireRole(RoleEnum.Admin.Claim, "true"));

		return services;
	}
}
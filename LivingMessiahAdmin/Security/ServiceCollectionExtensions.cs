using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using RoleEnum = LivingMessiahAdmin.Security.Enums.Role;
using RoleGroup = LivingMessiahAdmin.Security.Enums.RoleGroup;
using LivingMessiahAdmin.Security.Policies;
using ConfigurationConstants = LivingMessiahAdmin.Security.Constants.Configuration;
using AuthFailedRedirectConstants = LivingMessiahAdmin.Security.Constants.AuthFailedRedirect;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace LivingMessiahAdmin.SecurityRoot;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
	{
		services.AddAuthorizationBuilder()
				.AddPolicy(Authentication.Name, policy => policy.RequireClaim(Authentication.Claim, "true"))

				.AddPolicy(RoleGroup.EmailVerifiedWithAtLeastOneRole, policy =>
						policy.RequireAssertion(context =>
								context.User.HasClaim(Authentication.Claim, "true") &&
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

	public static IServiceCollection AddAuth0Authentication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuth0WebAppAuthentication(options =>
		{
			options.Domain = configuration[ConfigurationConstants.Domain] ?? "";
			options.ClientId = configuration[ConfigurationConstants.ClientId] ?? "";
			options.ClientSecret = configuration[ConfigurationConstants.ClientSecret] ?? "";
			options.Scope = "openid profile email roles";
			options.OpenIdConnectEvents = new OpenIdConnectEvents
			{
				OnRemoteFailure = ctx =>
						{
							var error = ctx.Request.Query[AuthFailedRedirectConstants.ErrorSPFQ].ToString();
							var errorDesc = ctx.Request.Query[AuthFailedRedirectConstants.ErrorDescriptionSPFQ].ToString();
							ctx.Response.Redirect(GetUrl(error, errorDesc));
							ctx.HandleResponse();  // Prevent the middleware from rethrowing the exception
							return Task.CompletedTask;
						}
			};
		});

		return services;
	}

	private static string GetUrl(string? error, string? errorDesc)
	{
		if (string.Equals(error, "access_denied", StringComparison.OrdinalIgnoreCase))
		{
			string q = $"?{AuthFailedRedirectConstants.ErrorSPFQ}={Uri.EscapeDataString(error ?? "")}&{AuthFailedRedirectConstants.ErrorDescriptionSPFQ}={Uri.EscapeDataString(errorDesc ?? "")}";
			return AuthFailedRedirectConstants.Url + q;
		}

		// For any other failure, return a generic remote_failure indicator.
		return $"{AuthFailedRedirectConstants.Url}?{AuthFailedRedirectConstants.ErrorSPFQ}=remote_failure";
	}
}

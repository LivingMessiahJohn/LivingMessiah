using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using LivingMessiahAdmin.Security.Constants;

namespace LivingMessiahAdmin.Security;

public static class ServiceCollectionExtensions
{
	// Registers Auth0 web app authentication
	public static IServiceCollection AddAuth0Authentication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuth0WebAppAuthentication(options =>
		{
			options.Domain = configuration[Configuration.Domain] ?? "";
			options.ClientId = configuration[Configuration.ClientId] ?? "";
			options.ClientSecret = configuration[Configuration.ClientSecret] ?? "";
			options.Scope = "openid profile email roles";
			options.OpenIdConnectEvents = new OpenIdConnectEvents
			{
				OnRemoteFailure = ctx =>
				{
					// When the user cancels / denies consent Auth0 returns error=access_denied
					var error = ctx.Request.Query[AuthFailedRedirect.ErrorSPFQ].ToString();
					var errorDesc = ctx.Request.Query[AuthFailedRedirect.ErrorDescriptionSPFQ].ToString();
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
		// If the error indicates the user denied access, include the error and description.
		if (string.Equals(error, "access_denied", StringComparison.OrdinalIgnoreCase))
		{
			string q = $"?{AuthFailedRedirect.ErrorSPFQ}={Uri.EscapeDataString(error ?? "")}&{AuthFailedRedirect.ErrorDescriptionSPFQ}={Uri.EscapeDataString(errorDesc ?? "")}";
			return AuthFailedRedirect.Url + q;
		}

		// For any other failure, return a generic remote_failure indicator.
		return $"{AuthFailedRedirect.Url}?{AuthFailedRedirect.ErrorSPFQ}=remote_failure";
	}

}

using Admin.Features.Users.Settings;
using Microsoft.Extensions.Options;

namespace Admin.Features.Users.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddUsersFeature(this IServiceCollection services)
	{
		services.AddHttpClient(Auth0UserDirectory.HttpClientName, (sp, client) =>
		{
			var options = sp.GetRequiredService<IOptions<Auth0M2M>>().Value;
			if (string.IsNullOrWhiteSpace(options.Domain) ||
					string.IsNullOrWhiteSpace(options.ClientId) ||
					string.IsNullOrWhiteSpace(options.ClientSecret))
			{
				throw new InvalidOperationException(
					"Missing Auth0M2M configuration. Add Auth0M2M:Domain, Auth0M2M:ClientId, and Auth0M2M:ClientSecret to user-secrets or app settings.");
			}

			var domain = Auth0UserDirectory.NormalizeDomain(options.Domain);
			client.BaseAddress = new Uri($"https://{domain}/api/v2/");
			client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
		});

		// Singleton so M2M access tokens are reused across requests.
		services.AddSingleton<IAuth0UserDirectory, Auth0UserDirectory>();
		return services;
	}
}

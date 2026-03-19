using PWA.Features.Home.WeeklyDownload;

namespace PWA.Features.Home;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddBlobApiService(this IServiceCollection services, string baseAddress)
	{
		services.AddHttpClient<IBlobApiService, BlobApiService>(client =>
		{
			// For Azure Static Web Apps in production, use the app's base address
			// For local development, use the Azure Functions local endpoint
			client.BaseAddress = new Uri(baseAddress);
		});

		return services;
	}
}


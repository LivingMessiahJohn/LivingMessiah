namespace PWA.Features.SpecialEvents.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSpecialEventsApiService(this IServiceCollection services, string baseAddress)
	{
		services.AddHttpClient<ISpecialEventsApiService, SpecialEventsApiService>(client =>
		{
			// Aspire: services:api:*; standalone dev: http://localhost:7071; production SWA: app base address
			client.BaseAddress = new Uri(baseAddress);
		});

		return services;
	}
}

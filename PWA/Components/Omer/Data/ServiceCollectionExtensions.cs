namespace PWA.Components.Omer.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddOmer(this IServiceCollection services)
	{
		services
		.AddSingleton<IService, Service>();
		return services;
	}
}

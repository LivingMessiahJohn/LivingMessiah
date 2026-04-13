namespace PWA.Components.Moon.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddMoon(this IServiceCollection services)
	{
		services
		.AddSingleton<IService, Service>();
		return services;
	}
}

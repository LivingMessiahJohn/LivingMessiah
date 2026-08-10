
namespace Sukkot.Endpoints.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddEndpointsData(this IServiceCollection services)
	{
		services
			.AddTransient<IRepository, Repository>();
		return services;
	}
}

namespace LivingMessiahAdmin.Features.Sukkot.Reports.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddReports(this IServiceCollection services)
	{
		services
			.AddSingleton<IRepository, Repository>();
		
		return services;
	}
}

namespace LivingMessiahAdmin.Features.Database;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDatabase(this IServiceCollection services)
	{
		services
			//.AddTransient<LM.IRepository, LM.Repository>()
			.AddTransient<Sukkot.IRepository, Sukkot.Repository>();
		return services;
	}
}

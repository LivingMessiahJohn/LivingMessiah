namespace Admin.Features.Database;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDatabase(this IServiceCollection services)
	{
		// SpecialEvent error-log actions use SpecialEvents.Data.IRepository (registered in AddSpecialEvents).
		services
			.AddTransient<LM.IRepository, LM.Repository>()
			.AddTransient<Sukkot.IRepository, Sukkot.Repository>();
		return services;
	}
}

using RCL.Features.Sukkot;

namespace Sukkot.Features.LandingPage.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSukkotDailyScheduleData(this IServiceCollection services)
	{
		services
			.AddTransient<IRepository, Repository>()
			.AddTransient<IScheduleQueryLoader>(sp => sp.GetRequiredService<IRepository>());
		return services;
	}
}

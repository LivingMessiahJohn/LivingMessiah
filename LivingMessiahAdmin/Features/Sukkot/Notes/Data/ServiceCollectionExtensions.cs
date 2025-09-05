namespace LivingMessiahAdmin.Features.Sukkot.Notes.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddManageNotes(this IServiceCollection services)
	{
		services
			.AddSingleton<IRepository, Repository>();
		
		return services;
	}
}

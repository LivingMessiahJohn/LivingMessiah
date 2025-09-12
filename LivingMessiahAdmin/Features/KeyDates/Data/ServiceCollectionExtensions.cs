//using FluentValidation;
namespace LivingMessiahAdmin.Features.KeyDates.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddManageKeyDates(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>();
		//.AddTransient<IValidator<FormVM>, FormVMValidator>();
		return services;
	}
}


 
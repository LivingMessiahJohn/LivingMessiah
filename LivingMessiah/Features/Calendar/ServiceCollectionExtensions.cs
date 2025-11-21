
namespace LivingMessiah.Features.Calendar;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCalendar(this IServiceCollection services)
	{
		services
		.AddSingleton<IService, Service>(); // ToDo: can this be moved to RCL?

		//.AddSingleton<IRepository, Repository>();
		//using FluentValidation;
		//.AddTransient<IValidator<FormVM>, FormVMValidator>();
		return services;
	}
}


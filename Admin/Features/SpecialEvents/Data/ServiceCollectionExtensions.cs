using FluentValidation;

namespace Admin.Features.SpecialEvents.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSpecialEvents(this IServiceCollection services)
	{
		services
		.AddSingleton<IRepository, Repository>()
		.AddTransient<IValidator<FormVM>, FormVMValidator>();
		return services;
	}
}

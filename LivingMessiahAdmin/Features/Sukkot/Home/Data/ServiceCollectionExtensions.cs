using FluentValidation;
using LivingMessiahAdmin.Features.Sukkot.Components.RegistrationForm;

namespace LivingMessiahAdmin.Features.Sukkot.Home.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSukkotData(this IServiceCollection services)
	{
		services
			//.AddTransient<ISecurityHelper, SecurityHelper>()
			.AddTransient<IRepository, Repository>()
			.AddTransient<IValidator<VM>, VMValidator>();

		return services;
	}
}


using FluentValidation;

namespace LivingMessiahAdmin.Features.Sukkot.Home.Donations.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSukkotDonationsData(this IServiceCollection services)
	{
		services
			//.AddTransient<ISecurityHelper, SecurityHelper>()
			.AddTransient<IRepository, Repository>()

			.AddTransient<IValidator<Donations.VM>, Donations.VMValidator>();

		return services;
	}
}


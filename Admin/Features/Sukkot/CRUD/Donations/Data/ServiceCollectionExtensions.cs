using Admin.Features.Sukkot.CRUD.Donations;
using FluentValidation;

namespace Admin.Features.Sukkot.CRUD.Donations.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSukkotDonationsData(this IServiceCollection services)
	{
		services
			//.AddTransient<ISecurityHelper, SecurityHelper>()
			.AddTransient<IRepository, Repository>()

			.AddTransient<IValidator<VM>, VMValidator>();

		return services;
	}
}


using FluentValidation;

namespace LivingMessiahSukkot.Features.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSukkotData(this IServiceCollection services)
	{
		services
			.AddTransient<ISecurityHelper, SecurityHelper>()
			.AddTransient<IRepository, Repository>()
			.AddTransient<IValidator<NormalUser.EntryFormVM>, NormalUser.EntryFormVMValidator>();
		return services;
	}
}

/*
ToDo: remove after adding a new Admin WebApp
using  LivingMessiah.Features.SukkotAdmin.Data;
using  LivingMessiah.Features.SukkotAdmin.Donations.Data;
...

		.AddTransient<IDonationRepository, DonationRepository>()
		.AddTransient<ISukkotAdminRepository, SukkotAdminRepository>()
		.AddScoped<AppState>();
*/

using FluentValidation;

using SukkotData = LivingMessiah.Features.Sukkot.Data;
using LivingMessiah.Features.Sukkot.Services;

/*
using  LivingMessiah.Features.SukkotAdmin.Data;
using  LivingMessiah.Features.SukkotAdmin.Donations.Data;
*/
using LivingMessiah.Services;

namespace LivingMessiah;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDataStores(this IServiceCollection services)
	{
		services
			.AddTransient<ISecurityClaimsService, SecurityClaimsService>()
			.AddTransient<ISukkotService, SukkotService>()

			.AddTransient<IService, Service>()        //Pages.Sukkot.Services;
			.AddTransient<SukkotData.IRepository, SukkotData.Repository>()

			.AddTransient<IValidator<Features.Sukkot.NormalUser.EntryFormVM>, Features.Sukkot.NormalUser.EntryFormVMValidator>()
			.AddTransient<SukkotData.ISukkotRepositoryUsedBySukkotService, SukkotData.SukkotRepositoryUsedBySukkotService>();

		//.AddTransient<IDonationRepository, DonationRepository>()
		//.AddTransient<ISukkotAdminRepository, SukkotAdminRepository>()
		//.AddScoped<AppState>();

		return services;
	}

}

using FluentValidation;

using SukkotData = LivingMessiah.Features.Sukkot.Data;

/*
using  LivingMessiah.Features.SukkotAdmin.Data;
using  LivingMessiah.Features.SukkotAdmin.Donations.Data;
*/
using LivingMessiah.SecurityRoot;
using LivingMessiah.Features.Sukkot;

namespace LivingMessiah;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDataStores(this IServiceCollection services)
	{
		services
			.AddTransient<ISecurityHelper, SecurityHelper>()
			.AddTransient<SukkotData.IRepository, SukkotData.Repository>()
			.AddTransient<IValidator<Features.Sukkot.NormalUser.EntryFormVM>, Features.Sukkot.NormalUser.EntryFormVMValidator>();

		//.AddTransient<IDonationRepository, DonationRepository>()
		//.AddTransient<ISukkotAdminRepository, SukkotAdminRepository>()
		//.AddScoped<AppState>();

		return services;
	}

}

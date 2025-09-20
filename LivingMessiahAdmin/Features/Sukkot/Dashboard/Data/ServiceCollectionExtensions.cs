//using FluentValidation;

using LivingMessiahAdmin.Features.Sukkot.Dashboard;
namespace LivingMessiahAdmin.Features.Sukkot.Dashboard.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSukkotGridData(this IServiceCollection services)
	{
		services
			//.AddTransient<ISecurityHelper, SecurityHelper>()
			.AddTransient<IRepository, Repository>()
			.AddScoped<ExportCSV>();
			//.AddTransient<IValidator<NormalUser.EntryFormVM>, NormalUser.EntryFormVMValidator>();
		return services;
	}
}


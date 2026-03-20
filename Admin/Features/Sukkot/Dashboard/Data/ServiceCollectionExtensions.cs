//using FluentValidation;
using Admin.Features.Sukkot.Dashboard.Services;

namespace Admin.Features.Sukkot.Dashboard.Data;

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


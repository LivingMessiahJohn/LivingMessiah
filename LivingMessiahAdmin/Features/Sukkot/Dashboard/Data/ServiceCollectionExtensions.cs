//using FluentValidation;

namespace LivingMessiahAdmin.Features.Sukkot.Dashboard.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSukkotGridData(this IServiceCollection services)
	{
		services
			//.AddTransient<ISecurityHelper, SecurityHelper>()
			.AddTransient<IRepository, Repository>();
			//.AddTransient<IValidator<NormalUser.EntryFormVM>, NormalUser.EntryFormVMValidator>();
		return services;
	}
}


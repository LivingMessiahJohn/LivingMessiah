using FluentValidation;

namespace LivingMessiahAdmin.Features.Sukkot.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSukkotData(this IServiceCollection services)
	{
		services
			//.AddTransient<ISecurityHelper, SecurityHelper>()
			.AddTransient<IRepository, Repository>();
			//.AddTransient<IValidator<NormalUser.EntryFormVM>, NormalUser.EntryFormVMValidator>();
		return services;
	}
}


using FluentValidation;

namespace LivingMessiahAdmin.Features.Sukkot.Notes.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddManageNotes(this IServiceCollection services)
	{
		services
			.AddTransient<IRepository, Repository>()
			.AddTransient<IValidator<EditFormVM>, EditFormVMValidator>();
		return services;
	}
}

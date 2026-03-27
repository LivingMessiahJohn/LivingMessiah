using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using SpecialEventsSettings = Admin.Features.SpecialEvents.Settings;
using RCL.Features.Storage;

namespace Admin.Features.SpecialEvents.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSpecialEvents(this IServiceCollection services)
	{
		services
			.AddSingleton<IRepository, Repository>()
			.AddTransient<IValidator<FormVM>, FormVMValidator>()
			.AddKeyedSingleton<IAzureBlobService, AzureBlobService>("SpecialEvents", (sp, key) =>
			{
				var options = sp.GetRequiredService<IOptions<SpecialEventsSettings.AzureBlob>>();
				var azureBlob = options.Value;
				var loggerFactory = sp.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
				var logger = loggerFactory.CreateLogger<AzureBlobService>();

				if (string.IsNullOrWhiteSpace(azureBlob.ConnectionString) || 
						string.IsNullOrWhiteSpace(azureBlob.SpecialEventsContainer))
				{
					throw new InvalidOperationException("Missing AzureBlob configuration. Add 'AzureBlob:ConnectionString' and 'AzureBlob:SpecialEventsContainer' to your appsettings (or environment variables).");
				}

				//logger.LogWarning("Registering AzureBlobService (SpecialEvents) with container '{SpecialEventsContainer}'", azureBlob.SpecialEventsContainer);

				return new AzureBlobService(azureBlob.ConnectionString, azureBlob.SpecialEventsContainer, logger);
			});

		return services;
	}
}

using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using WeeklyDownloadsSettings = LivingMessiahAdmin.Features.WeeklyDownloads.Settings;

namespace LivingMessiahAdmin.Features.WeeklyDownloads.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAzureBlobService(this IServiceCollection services)
	{
		// Register AzureBlobService using factory so we can log the container name via the DI logger.
		services.AddSingleton<AzureBlobService>(sp =>
		{
			var options = sp.GetRequiredService<IOptions<WeeklyDownloadsSettings.AzureBlob>>();
			var azureBlob = options.Value;
			var loggerFactory = sp.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
			var logger = loggerFactory.CreateLogger<AzureBlobService>();
			
			if (string.IsNullOrWhiteSpace(azureBlob.ConnectionString) || 
					string.IsNullOrWhiteSpace(azureBlob.ContainerName))
			{
				throw new InvalidOperationException("Missing AzureBlob configuration. Add 'AzureBlob:ConnectionString' and 'AzureBlob:ContainerName' to your appsettings (or environment variables).");
			}

			//logger.LogWarning("Registering AzureBlobService with container '{ContainerName}'", azureBlob.ContainerName);
			//logger.LogDebug("Register AzureBlobService. ConnectionString: '{ConnectionString}'", azureBlob.ConnectionString);
			//logger.LogDebug("Register AzureBlobService. ContainerName: '{ContainerName}'", azureBlob.ContainerName);

			return new AzureBlobService(azureBlob.ConnectionString, azureBlob.ContainerName, logger);
		});

		return services;
	}
}

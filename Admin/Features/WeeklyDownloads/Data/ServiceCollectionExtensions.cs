using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using WeeklyDownloadsSettings = Admin.Features.WeeklyDownloads.Settings;
using RCL.Features.Storage;

namespace Admin.Features.WeeklyDownloads.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddWeeklyDownloads(this IServiceCollection services)
	{
		services.AddKeyedSingleton<IAzureBlobService, AzureBlobService>("WeeklyDownloads", (sp, key) =>
		{
			var options = sp.GetRequiredService<IOptions<WeeklyDownloadsSettings.AzureBlob>>();
			var azureBlob = options.Value;
			var loggerFactory = sp.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
			var logger = loggerFactory.CreateLogger<AzureBlobService>();

			if (string.IsNullOrWhiteSpace(azureBlob.ConnectionString) || 
					string.IsNullOrWhiteSpace(azureBlob.WeeklyDownloadContainer))
			{
				throw new InvalidOperationException("Missing AzureBlob configuration. Add 'AzureBlob:ConnectionString' and 'AzureBlob:WeeklyDownloadContainer' to your appsettings (or environment variables).");
			}

			//logger.LogWarning("Registering AzureBlobService (WeeklyDownloads) with container '{WeeklyDownloadContainer}'", azureBlob.WeeklyDownloadContainer);

			return new AzureBlobService(azureBlob.ConnectionString, azureBlob.WeeklyDownloadContainer, logger);
		});

		return services;
	}
}

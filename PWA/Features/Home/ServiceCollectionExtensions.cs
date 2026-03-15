using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using WeeklyDownloadsSettings = PWA.Features.Home.WeeklyDownload.Settings;
using RCL.Features.Storage;

namespace PWA.Features.Home;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAzureBlobService(this IServiceCollection services)
	{
		services.AddSingleton<IAzureBlobService, AzureBlobService>(sp =>
		{
			var options = sp.GetRequiredService<IOptions<WeeklyDownloadsSettings.AzureBlob>>(); 
			var azureBlob = options.Value;
			var loggerFactory = sp.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
			var Logger = loggerFactory.CreateLogger<AzureBlobService>();

			if (string.IsNullOrWhiteSpace(azureBlob.ConnectionString) ||
				string.IsNullOrWhiteSpace(azureBlob.ContainerName))
			{
				throw new InvalidOperationException("Missing AzureBlob configuration. Add 'AzureBlob:ConnectionString' and 'AzureBlob:ContainerName' to your appsettings (or environment variables).");
			}
			return new AzureBlobService(azureBlob.ConnectionString, azureBlob.ContainerName, Logger);
		});

		return services;
	}
}


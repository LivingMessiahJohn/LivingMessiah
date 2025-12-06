namespace LivingMessiah.Features.Home;

using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using RCL.Features.Storage;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAzureBlobService(this IServiceCollection services)
	{
		// Register AzureBlobService using factory so we can log the container name via the DI logger.
		services.AddSingleton<AzureBlobService>(sp =>
		{
			var options = sp.GetRequiredService<IOptions<AzureBlob>>();  // WeeklyDownloadsSettings.AzureBlob
			var azureBlob = options.Value;
			var loggerFactory = sp.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
			var logger = loggerFactory.CreateLogger<AzureBlobService>();

			if (string.IsNullOrWhiteSpace(azureBlob.ConnectionString) ||
					string.IsNullOrWhiteSpace(azureBlob.ContainerName))
			{
				throw new InvalidOperationException("Missing AzureBlob configuration. Add 'AzureBlob:ConnectionString' and 'AzureBlob:ContainerName' to your appsettings (or environment variables).");
			}

			return new AzureBlobService(azureBlob.ConnectionString, azureBlob.ContainerName, logger);
		});

		return services;
	}
}

// Do I need this ???
public class AzureBlob
{
	public string? ConnectionString { get; set; } // = string.Empty;
	public string? ContainerName { get; set; } // = string.Empty;
}

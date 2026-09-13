using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RCL.Features.Storage;
using RCL.Features.Sukkot.Constants;

namespace RCL.Features.Sukkot.Data.DailySchedule;

public static class ServiceCollectionExtensions
{
	public const string BlobServiceKey = "SukkotSchedule";

	/// <summary>
	/// Registers private-container blob access for the Sukkot schedule and
	/// <see cref="IBlobLoader"/> / <see cref="IBlobWriter"/>.
	/// Requires config key <c>AzureBlob:ConnectionString</c>. Container/blob names are constants.
	/// </summary>
	public static IServiceCollection AddBlob(this IServiceCollection services)
	{
		services.TryAddKeyedSingleton<IAzureBlobService>(BlobServiceKey, (sp, _) =>
		{
			var config = sp.GetRequiredService<IConfiguration>();
			string? connectionString = config["AzureBlob:ConnectionString"];
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new InvalidOperationException(
					"Missing AzureBlob:ConnectionString. Set it in user-secrets, appsettings, or environment variables.");
			}

			var loggerFactory = sp.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
			var logger = loggerFactory.CreateLogger<AzureBlobService>();
			return new AzureBlobService(connectionString, ScheduleBlob.ContainerName, logger);
		});

		services.AddTransient<IBlobLoader>(sp =>
		{
			var blobs = sp.GetRequiredKeyedService<IAzureBlobService>(BlobServiceKey);
			var logger = sp.GetRequiredService<ILogger<BlobLoader>>();
			return new BlobLoader(blobs, logger);
		});

		services.AddTransient<IBlobWriter>(sp =>
		{
			var blobs = sp.GetRequiredKeyedService<IAzureBlobService>(BlobServiceKey);
			var logger = sp.GetRequiredService<ILogger<BlobWriter>>();
			return new BlobWriter(blobs, logger);
		});

		return services;
	}
}

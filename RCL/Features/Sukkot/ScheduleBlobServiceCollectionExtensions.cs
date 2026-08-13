using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RCL.Features.Storage;
using RCL.Features.Sukkot.Constants;

namespace RCL.Features.Sukkot;

public static class ScheduleBlobServiceCollectionExtensions
{
	public const string BlobServiceKey = "SukkotSchedule";

	/// <summary>
	/// Registers private-container blob access for the Sukkot schedule and
	/// <see cref="IScheduleQueryLoader"/> / <see cref="IScheduleQueryWriter"/>.
	/// Requires config key <c>AzureBlob:ConnectionString</c>. Container/blob names are constants.
	/// </summary>
	public static IServiceCollection AddSukkotScheduleFromBlob(this IServiceCollection services)
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

		services.AddTransient<IScheduleQueryLoader>(sp =>
		{
			var blobs = sp.GetRequiredKeyedService<IAzureBlobService>(BlobServiceKey);
			var logger = sp.GetRequiredService<ILogger<ScheduleBlobQueryLoader>>();
			return new ScheduleBlobQueryLoader(blobs, logger);
		});

		services.AddTransient<IScheduleQueryWriter>(sp =>
		{
			var blobs = sp.GetRequiredKeyedService<IAzureBlobService>(BlobServiceKey);
			var logger = sp.GetRequiredService<ILogger<ScheduleBlobQueryWriter>>();
			return new ScheduleBlobQueryWriter(blobs, logger);
		});

		return services;
	}
}

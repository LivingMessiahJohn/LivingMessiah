namespace LivingMessiahAdmin.Features.WeeklyDownloads.Data;


// Install-Package Azure.Storage.Blobs >= 12.20.0
using Azure;
using Azure.Storage.Blobs;
using LivingMessiahAdmin.Features.WeeklyDownloads;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.IO;


public class AzureBlobService
{
	private readonly BlobContainerClient _container;
	private readonly ILogger<AzureBlobService> Logger;

	public AzureBlobService(string connectionString, string containerName, ILogger<AzureBlobService>? logger = null)
	{
		_container = new BlobContainerClient(connectionString, containerName);
		Logger = logger ?? NullLogger<AzureBlobService>.Instance;
	}

	public async Task<FileUploadResultRecord> UploadAsync(string sourceFilePath, string blobName, CancellationToken ct = default)
	{
		string message = string.Empty;
		Logger.LogInformation("{Method}, {Message}", nameof(UploadAsync), $"source: {sourceFilePath}, target blob: {blobName}");

		sourceFilePath = sourceFilePath?.Trim() ?? string.Empty;
		blobName = blobName?.Trim() ?? string.Empty;

		BlobClient targetBlob = _container.GetBlobClient(blobName);

		try
		{
			if (!File.Exists(sourceFilePath))
			{
				message = $"file '{sourceFilePath}' not found.";
				Logger.LogWarning("{Method}, {Message}", nameof(UploadAsync), message);
				return new FileUploadResultRecord(false, message);
			}

			// Check if the blob already exists so we can report whether we overwrote it.
			Response<bool> existsResponse = await targetBlob.ExistsAsync(ct);
			bool blobExists = existsResponse.Value;

			// Open the source file and upload (overwrite=true).
			await using FileStream fs = File.OpenRead(sourceFilePath);
			await targetBlob.UploadAsync(fs, overwrite: true, cancellationToken: ct);

			message = blobExists
				? $"File '{blobName}' overwritten successfully."
				: $"File '{blobName}' uploaded successfully.";

			Logger.LogInformation("{Method}, {Message}", nameof(UploadAsync), message);
			return new FileUploadResultRecord(true, message);
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "{Method}, {Message}", nameof(UploadAsync), $"source: {sourceFilePath}, target blob: {blobName}");
			return new FileUploadResultRecord(false, ex.Message);
		}
	}

}




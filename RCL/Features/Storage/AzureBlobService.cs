using Azure;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace RCL.Features.Storage;

/// <summary>
/// Lightweight, RCL-hosted Azure blob helper.
/// Intended to be moved into the RCL project so other projects (Admin, Blazor) can reuse it.
/// - Add NuGet: Azure.Storage.Blobs (>= 12.20.0) to the RCL project.
/// - Register this service in DI (see note below).
/// </summary>
public class AzureBlobService
{
	private readonly BlobContainerClient _container;
	private readonly ILogger<AzureBlobService> Logger;

	public AzureBlobService(string connectionString, string containerName, ILogger<AzureBlobService>? logger = null)
	{
		_container = new BlobContainerClient(connectionString, containerName);
		Logger = logger ?? NullLogger<AzureBlobService>.Instance;
	}

	/// <summary>
	/// Upload a local file to the blob container. Overwrites existing blob.
	/// Returns a simple result record that is local to the RCL (avoid cross-project type coupling).
	/// </summary>
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

	/// <summary>
	/// Check if a blob exists in the configured container.
	/// Useful for validating an anchor URL or showing/hiding a download button.
	/// </summary>
	public async Task<bool> ExistsAsync(string blobName, CancellationToken ct = default)
	{
		try
		{
			BlobClient blob = _container.GetBlobClient(blobName?.Trim() ?? string.Empty);
			var resp = await blob.ExistsAsync(ct);
			return resp.Value;
		}
		catch (Exception ex)
		{
			Logger.LogWarning(ex, "{Method} failed for blob '{BlobName}'", nameof(ExistsAsync), blobName);
			return false;
		}
	}

	/// <summary>
	/// Get a public URL for the blob (container must allow public read or client must provide SAS).
	/// </summary>
	public string GetBlobUrl(string blobName)
	{
		return _container.GetBlobClient(blobName?.Trim() ?? string.Empty).Uri.ToString();
	}
}

public record FileUploadResultRecord(bool Success, string Message);
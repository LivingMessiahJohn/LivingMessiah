namespace LivingMessiahAdmin.Features.WeeklyDownloads;


// Install-Package Azure.Storage.Blobs >= 12.20.0
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;


public class BlobReplaceService
{
	private readonly BlobContainerClient _container;
	private readonly ILogger<BlobReplaceService> Logger;

	public BlobReplaceService(string connectionString, string containerName, ILogger<BlobReplaceService>? logger = null)
	{
		_container = new BlobContainerClient(connectionString, containerName);
		Logger = logger ?? NullLogger<BlobReplaceService>.Instance;
	}

	public async Task<ReplaceBlobResult> ReplaceBlobAsync(
			string targetBlobName,           // e.g. "Foo.pdf"
			string holdBlobName,             // e.g. "Foo-HOLD.pdf"
			CancellationToken ct = default)
	{
		Logger.LogInformation("ReplaceBlobAsync started. Target='{TargetBlob}', Hold='{HoldBlob}'", targetBlobName, holdBlobName);

		// Normalize names (trim, lowercase if your container is case-sensitive)
		targetBlobName = targetBlobName.Trim();
		holdBlobName = holdBlobName.Trim();

		BlobClient targetBlob = _container.GetBlobClient(targetBlobName);
		BlobClient holdBlob = _container.GetBlobClient(holdBlobName);

		// 1. Check that the HOLD file actually exists
		if (!await holdBlob.ExistsAsync(ct))
		{
			Logger.LogWarning("Hold file '{HoldBlob}' not found.", holdBlobName);
			return new ReplaceBlobResult(false, $"Hold file '{holdBlobName}' not found.");
		}

		try
		{
			// 2. If target exists → delete it first (so copy can succeed)
			if (await targetBlob.ExistsAsync(ct))
			{
				Logger.LogInformation("Target blob '{TargetBlob}' exists - deleting before copy.", targetBlobName);
				await targetBlob.DeleteAsync(cancellationToken: ct);
				Logger.LogInformation("Target blob '{TargetBlob}' deleted.", targetBlobName);
			}
			else
			{
				Logger.LogInformation("Target blob '{TargetBlob}' does not exist - proceeding to copy.", targetBlobName);
			}

			// 3. Copy HOLD → target (this is the rename/replace)
			//    StartCopyFromUri is atomic and very fast because it's server-side
			Logger.LogInformation("Starting copy from '{HoldBlobUri}' to '{TargetBlob}'.", holdBlob.Uri, targetBlobName);
			await targetBlob.StartCopyFromUriAsync(holdBlob.Uri, cancellationToken: ct);

			// Wait for copy completion (usually instantaneous for same container)
			await targetBlob.SyncCopyWaitForCompletionAsync(ct);
			Logger.LogInformation("Copy completed from '{HoldBlobUri}' to '{TargetBlob}'.", holdBlob.Uri, targetBlobName);

			// 4. Delete the old HOLD file (the "rename" part)
			Logger.LogInformation("Deleting hold blob '{HoldBlob}'.", holdBlobName);
			await holdBlob.DeleteAsync(cancellationToken: ct);
			Logger.LogInformation("Hold blob '{HoldBlob}' deleted.", holdBlobName);

			Logger.LogInformation("ReplaceBlobAsync succeeded. Target='{TargetBlob}', Hold='{HoldBlob}'", targetBlobName, holdBlobName);
			return new ReplaceBlobResult(true, "File replaced successfully.");
		}
		catch (RequestFailedException ex) when (ex.Status == 404)
		{
			Logger.LogWarning(ex, "One of the blobs was not found during operation. Target='{Target}', Hold='{Hold}'", targetBlobName, holdBlobName);
			//Logger!.LogWarning(ex, "{Method}, {Message}", nameof(ReplaceBlobAsync), $"targetBlobName: {targetBlobName}, holdBlobName:{holdBlobName}");
			return new ReplaceBlobResult(false, "One of the blobs was not found during operation.");
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "{Method}, {Message}"
				, nameof(ReplaceBlobAsync), $"targetBlobName: {targetBlobName}, holdBlobName:{holdBlobName}");
			return new ReplaceBlobResult(false, ex.Message);
		}
	}
}




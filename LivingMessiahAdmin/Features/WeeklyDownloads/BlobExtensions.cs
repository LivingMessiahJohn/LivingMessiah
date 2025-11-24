using Azure.Storage.Blobs;

namespace LivingMessiahAdmin.Features.WeeklyDownloads;

// Helper extension to wait for copy completion cleanly
public static class BlobExtensions
{
	public static async Task SyncCopyWaitForCompletionAsync(
			this BlobClient blob,
			CancellationToken ct = default)
	{
		while (true)
		{
			var props = await blob.GetPropertiesAsync(cancellationToken: ct);
			if (props.Value.CopyStatus != Azure.Storage.Blobs.Models.CopyStatus.Pending)
				break;

			await Task.Delay(100, ct);
		}
	}
}

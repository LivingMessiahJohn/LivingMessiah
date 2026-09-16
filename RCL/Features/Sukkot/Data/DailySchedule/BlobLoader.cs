using Microsoft.Extensions.Logging;
using RCL.Features.Storage;
using RCL.Features.Sukkot.Constants;

namespace RCL.Features.Sukkot.Data.DailySchedule;

public interface IBlobLoader
{
	Task<MarkdownRecord[]> GetAsyncList();
}


/// <summary>
/// Loads daily schedule markdown from <c>10.md</c>–<c>19.md</c> in the private Sukkot content container.
/// </summary>
public sealed class BlobLoader : IBlobLoader
{
	private readonly IAzureBlobService _blobs;
	private readonly ILogger<BlobLoader> _logger;

	public BlobLoader(IAzureBlobService blobs, ILogger<BlobLoader> logger)
	{
		_blobs = blobs;
		_logger = logger;
	}

	public async Task<MarkdownRecord[]> GetAsyncList()
	{
		var listResult = await _blobs.ListBlobNamesAsync(ScheduleBlob.DailyEventsFolder);
		if (!listResult.IsSuccess || listResult.Data is null)
		{
			_logger.LogWarning(
				"Daily-events folder list failed for {Prefix}: {Message}",
				ScheduleBlob.DailyEventsFolder,
				listResult.Message);
			return [];
		}

		var mdNames = listResult.Data
			.Where(DailyEventFiles.IsScheduledFile)
			.OrderBy(name => Path.GetFileName(name), StringComparer.OrdinalIgnoreCase)
			.ToArray();

		if (mdNames.Length == 0)
		{
			_logger.LogDebug(
				"No {Min}.md–{Max}.md blobs under {Prefix}",
				ScheduleBlob.DailyEventFileNumberMin,
				ScheduleBlob.DailyEventFileNumberMax,
				ScheduleBlob.DailyEventsFolder);
			return [];
		}

		var items = new List<MarkdownRecord>(mdNames.Length);
		foreach (var blobName in mdNames)
		{
			var result = await _blobs.DownloadTextAsync(blobName);
			if (!result.IsSuccess || result.Data is null)
			{
				_logger.LogWarning(
					"Daily-event blob load failed for {BlobName}: {Message}",
					blobName,
					result.Message);
				continue;
			}

			string fileName = Path.GetFileName(blobName);
			if (string.IsNullOrEmpty(fileName))
				fileName = blobName;

			items.Add(new MarkdownRecord(fileName, result.Data.Text ?? string.Empty));
		}

		return items.ToArray();
	}
}

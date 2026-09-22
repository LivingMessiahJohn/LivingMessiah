using Microsoft.Extensions.Logging;
using RCL.Features.Storage;
using RCL.Features.Sukkot.Constants;
using RCL.Features.Sukkot.Enums;

namespace RCL.Features.Sukkot.Data.DailySchedule;

public interface IBlobLoader
{
	Task<MarkdownRecord[]> GetAsyncList();
}


/// <summary>
/// Loads daily schedule markdown for each <see cref="DailyEvent"/> whose blob exists
/// under <see cref="ScheduleBlob.DailyEventsFolder"/>.
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

		var existingLeaves = listResult.Data
			.Select(Path.GetFileName)
			.Where(leaf => !string.IsNullOrEmpty(leaf) && DailyEvent.TryFromMarkdownFileName(leaf, out _))
			.ToHashSet(StringComparer.OrdinalIgnoreCase);

		if (existingLeaves.Count == 0)
		{
			_logger.LogDebug(
				"No DailyEvent markdown blobs under {Prefix}",
				ScheduleBlob.DailyEventsFolder);
			return [];
		}

		var items = new List<MarkdownRecord>(existingLeaves.Count);
		foreach (var evt in DailyEvent.List.OrderBy(e => e.Value))
		{
			if (!existingLeaves.Contains(evt.MarkdownFileName))
				continue;

			string blobName = ScheduleBlob.DailyEventsFolder + evt.MarkdownFileName;
			var result = await _blobs.DownloadTextAsync(blobName);
			if (!result.IsSuccess || result.Data is null)
			{
				_logger.LogWarning(
					"Daily-event blob load failed for {BlobName}: {Message}",
					blobName,
					result.Message);
				continue;
			}

			items.Add(new MarkdownRecord(evt.MarkdownFileName, result.Data.Text ?? string.Empty));
		}

		return items.ToArray();
	}
}

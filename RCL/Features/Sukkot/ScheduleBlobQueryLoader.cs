using Microsoft.Extensions.Logging;
using RCL.Features.Storage;
using RCL.Features.Sukkot.Constants;

namespace RCL.Features.Sukkot;

/// <summary>
/// Loads the daily schedule markdown from the private Sukkot schedule blob (#215).
/// </summary>
public sealed class ScheduleBlobQueryLoader : IScheduleQueryLoader
{
	private readonly IAzureBlobService _blobs;
	private readonly ILogger<ScheduleBlobQueryLoader> _logger;

	public ScheduleBlobQueryLoader(IAzureBlobService blobs, ILogger<ScheduleBlobQueryLoader> logger)
	{
		_blobs = blobs;
		_logger = logger;
	}

	public async Task<ScheduleQuery?> GetAsync()
	{
		var result = await _blobs.DownloadTextAsync(ScheduleBlob.BlobName);
		if (!result.IsSuccess || result.Data is null)
		{
			_logger.LogWarning(
				"Schedule blob load failed for {BlobName}: {Message}",
				ScheduleBlob.BlobName,
				result.Message);
			return null;
		}

		return new ScheduleQuery
		{
			Markdown = result.Data.Text ?? string.Empty,
			LastRevised = result.Data.LastRevised
		};
	}

	public async Task<DailyEventMarkdown[]> GetAsyncList()
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
			.Where(name => name.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
			.OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
			.ToArray();

		if (mdNames.Length == 0)
		{
			_logger.LogDebug("No .md blobs under {Prefix}", ScheduleBlob.DailyEventsFolder);
			return [];
		}

		var items = new List<DailyEventMarkdown>(mdNames.Length);
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

			items.Add(new DailyEventMarkdown(fileName, result.Data.Text ?? string.Empty));
		}

		return items.ToArray();
	}
}

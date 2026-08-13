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
}

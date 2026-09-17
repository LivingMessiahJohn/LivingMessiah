using System.Text;
using Microsoft.Extensions.Logging;
using RCL.Features.Storage;
using RCL.Features.Sukkot.Constants;

namespace RCL.Features.Sukkot.Data.DailySchedule;

public interface IBlobWriter
{
	Task SaveAsync(string fileName, string markdownBody, DateTime lastRevised, CancellationToken ct = default);
}


/// <summary>
/// Saves one daily-event markdown blob (<c>10.md</c>–<c>19.md</c>) in the private Sukkot content container.
/// Sets optional <c>lastrevised</c> metadata; loaders also accept blob LastModified.
/// </summary>
public sealed class BlobWriter : IBlobWriter
{
	private readonly IAzureBlobService _blobs;
	private readonly ILogger<BlobWriter> _logger;

	public BlobWriter(IAzureBlobService blobs, ILogger<BlobWriter> logger)
	{
		_blobs = blobs;
		_logger = logger;
	}

	public async Task SaveAsync(string fileName, string markdownBody, DateTime lastRevised, CancellationToken ct = default)
	{
		if (!DailyEventFiles.TryGetBlobName(fileName, out string blobName))
		{
			throw new ArgumentException(
				$"File '{fileName}' is not a daily schedule markdown file ({ScheduleBlob.DailyEventFileNumberMin}.md–{ScheduleBlob.DailyEventFileNumberMax}.md).",
				nameof(fileName));
		}

		byte[] bytes = Encoding.UTF8.GetBytes(markdownBody ?? string.Empty);
		await using var stream = new MemoryStream(bytes);

		var metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			[ScheduleBlob.LastRevisedMetadataKey] = lastRevised.ToString("o")
		};

		var result = await _blobs.UploadStreamAsync(
			stream,
			blobName,
			ScheduleBlob.ContentType,
			metadata,
			ct);

		if (!result.IsSuccess)
		{
			_logger.LogError(
				result.Exception,
				"Daily-event blob save failed for {BlobName}: {Message}",
				blobName,
				result.Message);
			throw new InvalidOperationException(
				result.Message ?? "Failed to save daily schedule blob.",
				result.Exception);
		}

		_logger.LogInformation(
			"Daily-event blob saved: {BlobName}, LastRevised {LastRevised}",
			blobName,
			lastRevised);
	}
}

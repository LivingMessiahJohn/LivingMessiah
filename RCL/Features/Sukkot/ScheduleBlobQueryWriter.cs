using System.Text;
using Microsoft.Extensions.Logging;
using RCL.Features.Storage;
using RCL.Features.Sukkot.Constants;

namespace RCL.Features.Sukkot;

/// <summary>
/// Saves daily schedule markdown to the private Sukkot schedule blob (#215).
/// Sets optional <c>lastrevised</c> metadata; loaders also accept blob LastModified.
/// </summary>
public sealed class ScheduleBlobQueryWriter : IScheduleQueryWriter
{
	private readonly IAzureBlobService _blobs;
	private readonly ILogger<ScheduleBlobQueryWriter> _logger;

	public ScheduleBlobQueryWriter(IAzureBlobService blobs, ILogger<ScheduleBlobQueryWriter> logger)
	{
		_blobs = blobs;
		_logger = logger;
	}

	public async Task SaveAsync(string markdownBody, DateTime lastRevised, CancellationToken ct = default)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(markdownBody ?? string.Empty);
		await using var stream = new MemoryStream(bytes);

		// Metadata is optional for display (LastModified works), but cheap and useful for Admin saves.
		var metadata = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			[ScheduleBlob.LastRevisedMetadataKey] = lastRevised.ToString("o")
		};

		var result = await _blobs.UploadStreamAsync(
			stream,
			ScheduleBlob.BlobName,
			ScheduleBlob.ContentType,
			metadata,
			ct);

		if (!result.IsSuccess)
		{
			_logger.LogError(
				result.Exception,
				"Schedule blob save failed for {BlobName}: {Message}",
				ScheduleBlob.BlobName,
				result.Message);
			throw new InvalidOperationException(
				result.Message ?? "Failed to save schedule blob.",
				result.Exception);
		}

		_logger.LogInformation(
			"Schedule blob saved: {BlobName}, LastRevised {LastRevised}",
			ScheduleBlob.BlobName,
			lastRevised);
	}
}

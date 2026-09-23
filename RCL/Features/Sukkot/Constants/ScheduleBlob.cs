namespace RCL.Features.Sukkot.Constants;

/// <summary>
/// Fixed Azure Blob location for the Sukkot daily schedule markdown (#215).
/// Container name is not a secret; connection string stays in config/secrets.
/// LastRevised is stored on the blob as metadata (key <see cref="LastRevisedMetadataKey"/>), not front matter.
/// Daily event leaf names come from <c>DailyEvent.MarkdownFileName</c>, not a numeric range.
/// </summary>
public static class ScheduleBlob
{
	public const string ContainerName = "sukkot-content";
	public const string BlobName = "sukkot/scheduled-events.md";
	public const string DailyEventsFolder = "daily-event/";
	public const string PocketModInputPdfBlobName = "scheduled-events-pocketmod-input.pdf";
	public const string PocketModPdfBlobName = "scheduled-events-pocketmod.pdf";
	public const string ContentType = "text/markdown; charset=utf-8";

	/// <summary>Azure blob metadata key (stored lowercase). Value: ISO-8601 datetime.</summary>
	public const string LastRevisedMetadataKey = "lastrevised";
}

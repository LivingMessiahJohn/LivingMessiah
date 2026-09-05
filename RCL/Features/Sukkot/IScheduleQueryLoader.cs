namespace RCL.Features.Sukkot;

/// <summary>
/// Loads the Sukkot daily schedule. Hosts register a blob-backed implementation
/// via <see cref="ScheduleBlobServiceCollectionExtensions.AddSukkotScheduleFromBlob"/> (#215).
/// </summary>
public interface IScheduleQueryLoader
{
	Task<ScheduleQuery?> GetAsync();

	/// <summary>
	/// Loads file name plus Markdown from each <c>.md</c> blob under
	/// <c>sukkot/daily-events-folder/</c> (see <see cref="Constants.ScheduleBlob.DailyEventsFolder"/>).
	/// </summary>
	Task<DailyEventMarkdown[]> GetAsyncList();
}

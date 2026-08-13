namespace RCL.Features.Sukkot;

/// <summary>
/// Loads the Sukkot daily schedule. Hosts register a blob-backed implementation
/// via <see cref="ScheduleBlobServiceCollectionExtensions.AddSukkotScheduleFromBlob"/> (#215).
/// </summary>
public interface IScheduleQueryLoader
{
	Task<ScheduleQuery?> GetAsync();
}

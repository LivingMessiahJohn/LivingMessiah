namespace RCL.Features.Sukkot.Data;

/// <summary>
/// Reads the singleton schedule markdown row from the Sukkot database.
/// Host apps (Sukkot, eventually Admin) provide the SQL implementation.
/// </summary>
public interface IScheduledEventsMarkdownRepository
{
	Task<ScheduledEventsMarkdownQuery?> GetAsync();
}

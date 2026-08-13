namespace RCL.Features.Sukkot;

/// <summary>
/// Admin-only save path for the daily schedule (blob-backed in #215).
/// Sukkot public app registers <see cref="IScheduleQueryLoader"/> only.
/// </summary>
public interface IScheduleQueryWriter
{
	/// <summary>
	/// Persist schedule markdown body; store <paramref name="lastRevised"/> on blob metadata.
	/// </summary>
	Task SaveAsync(string markdownBody, DateTime lastRevised, CancellationToken ct = default);
}

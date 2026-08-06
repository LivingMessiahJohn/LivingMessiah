namespace RCL.Features.Sukkot.Data;

/// <summary>
/// Single-row schedule content from dbo.ScheduledEventsMarkdown.
/// </summary>
public sealed class ScheduledEventsMarkdownQuery
{
	public string Markdown { get; set; } = string.Empty;
	public DateTime LastRevised { get; set; }
}

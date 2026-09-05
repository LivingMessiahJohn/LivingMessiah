namespace RCL.Features.Sukkot;

/// <summary>
/// One <c>.md</c> blob from <see cref="Constants.ScheduleBlob.DailyEventsFolder"/>.
/// <see cref="FileName"/> is the leaf name (e.g. <c>friday.md</c>), not the folder prefix.
/// </summary>
public sealed record DailyEventMarkdown(string FileName, string Markdown);

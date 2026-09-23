namespace RCL.Features.Sukkot.Data.DailySchedule;

/// <summary>
/// One <c>.md</c> blob from <see cref="Constants.ScheduleBlob.DailyEventsFolder"/>.
/// <see cref="FileName"/> is the leaf name from <c>DailyEvent.MarkdownFileName</c>, not the folder prefix.
/// </summary>
public sealed record MarkdownRecord(string FileName, string Markdown);

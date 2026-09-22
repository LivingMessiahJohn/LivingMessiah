using RCL.Features.Sukkot.Constants;
using RCL.Features.Sukkot.Enums;

namespace RCL.Features.Sukkot.Data.DailySchedule;

/// <summary>
/// Maps daily-event blobs onto <see cref="DailyEvent"/> (leaf names from
/// <see cref="DailyEvent.MarkdownFileName"/>, dates from
/// <see cref="RCL.Features.Calendar.Constants.FeastDayDates.Tabernacles"/>).
/// </summary>
public static class DailyEventFiles
{
	public static bool TryGetBlobName(string fileName, out string blobName)
	{
		blobName = string.Empty;
		if (!DailyEvent.TryFromMarkdownFileName(fileName, out _))
			return false;

		string leaf = Path.GetFileName(fileName);
		if (string.IsNullOrEmpty(leaf))
			return false;

		blobName = ScheduleBlob.DailyEventsFolder + leaf;
		return true;
	}

	public static string InvalidFileMessage(string? fileName) =>
		$"File '{fileName}' is not a DailyEvent markdown file.";

	/// <summary>
	/// Public schedule / print preview: only days whose <see cref="DailyEvent.Include"/> is true.
	/// Admin edit can show every DailyEvent file that exists in the container.
	/// </summary>
	public static MarkdownRecord[] WhereIncluded(IReadOnlyList<MarkdownRecord> days)
	{
		if (days is null || days.Count == 0)
			return [];

		return [.. days
			.Select(d => (day: d, matched: DailyEvent.TryFromMarkdownFileName(d.FileName, out var evt), evt))
			.Where(x => x.matched && x.evt.Include)
			.OrderBy(x => x.evt.Value)
			.Select(x => x.day)];
	}

	public static DateTime ArizonaToday()
	{
		TimeZoneInfo arizona = TimeZoneInfo.FindSystemTimeZoneById("America/Phoenix");
		return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, arizona).Date;
	}

	/// <summary>
	/// Index of today's file when today matches a loaded day's <see cref="DailyEvent.Date"/>;
	/// otherwise 0 (first day).
	/// </summary>
	public static int IndexForToday(IReadOnlyList<MarkdownRecord> days, DateTime? today = null)
	{
		if (days is null || days.Count == 0)
			return 0;

		DateOnly arizonaToday = DateOnly.FromDateTime((today ?? ArizonaToday()).Date);
		for (int i = 0; i < days.Count; i++)
		{
			if (DailyEvent.TryFromMarkdownFileName(days[i].FileName, out var evt) && evt.Date == arizonaToday)
				return i;
		}

		return 0;
	}

	public static string TitleFor(MarkdownRecord day)
	{
		if (!DailyEvent.TryFromMarkdownFileName(day.FileName, out var evt))
			return day.FileName;

		return evt.DateLabel;
	}

	public static DailyEvent? EventFor(MarkdownRecord day) =>
		DailyEvent.TryFromMarkdownFileName(day.FileName, out var evt) ? evt : null;

	public static string PrintTitleFor(MarkdownRecord day)
	{
		var evt = EventFor(day);
		return evt is null ? day.FileName : $"{evt.DateLabel} — {evt.Title}";
	}

	/// <summary>
	/// Groups included days into eight print pages using <see cref="DailyEvent.PrintPage"/>:
	/// page 1 is Pre-Prep (when included) + Prep + Day 1, pages 2–7 are Days 2–7,
	/// page 8 is Day 8 + Camp Clean Up + Post (when included).
	/// Empty slots (missing blobs) are omitted; a page is skipped if it has no files.
	/// </summary>
	public static MarkdownRecord[][] GroupForEightPrintPages(IReadOnlyList<MarkdownRecord> days)
	{
		if (days is null || days.Count == 0)
			return [];

		var byFileName = new Dictionary<string, MarkdownRecord>(StringComparer.OrdinalIgnoreCase);
		foreach (var day in days)
		{
			if (DailyEvent.TryFromMarkdownFileName(day.FileName, out var evt) && evt.Include)
				byFileName[evt.MarkdownFileName] = day;
		}

		var pages = new List<MarkdownRecord[]>();
		foreach (var group in DailyEvent.Included.GroupBy(e => e.PrintPage).OrderBy(g => g.Key))
		{
			var page = group
				.OrderBy(e => e.Value)
				.Where(e => byFileName.ContainsKey(e.MarkdownFileName))
				.Select(e => byFileName[e.MarkdownFileName])
				.ToArray();
			if (page.Length > 0)
				pages.Add(page);
		}

		return [.. pages];
	}
}

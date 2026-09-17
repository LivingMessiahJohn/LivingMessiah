using RCL.Features.Sukkot.Constants;
using RCL.Features.Sukkot.Enums;
using AttendanceDates = RCL.Features.Sukkot.Enums.Constants.DateRange;

namespace RCL.Features.Sukkot.Data.DailySchedule;

/// <summary>
/// Maps daily-event blobs <c>10.md</c>…<c>19.md</c> onto the Sukkot attendance
/// date range (file 10 = start date, then one day per file).
/// </summary>
public static class DailyEventFiles
{
	public static bool IsScheduledFile(string blobName) =>
		TryGetFileNumber(blobName, out _);

	public static bool TryGetBlobName(string fileName, out string blobName)
	{
		blobName = string.Empty;
		if (string.IsNullOrWhiteSpace(fileName))
			return false;

		string leaf = Path.GetFileName(fileName);
		if (string.IsNullOrEmpty(leaf) || !TryGetFileNumber(leaf, out _))
			return false;

		blobName = ScheduleBlob.DailyEventsFolder + leaf;
		return true;
	}

	public static string BlobNameFor(string fileName)
	{
		if (TryGetBlobName(fileName, out string blobName))
			return blobName;

		throw new ArgumentException(
			$"File '{fileName}' is not a daily schedule markdown file ({ScheduleBlob.DailyEventFileNumberMin}.md–{ScheduleBlob.DailyEventFileNumberMax}.md).",
			nameof(fileName));
	}

	public static bool TryGetFileNumber(string blobName, out int fileNumber)
	{
		fileNumber = 0;
		if (string.IsNullOrWhiteSpace(blobName))
			return false;

		string fileName = Path.GetFileName(blobName);
		if (string.IsNullOrEmpty(fileName))
			return false;

		if (!fileName.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
			return false;

		string stem = Path.GetFileNameWithoutExtension(fileName);
		return int.TryParse(stem, out fileNumber)
			&& fileNumber >= ScheduleBlob.DailyEventFileNumberMin
			&& fileNumber <= ScheduleBlob.DailyEventFileNumberMax;
	}

	public static DateTime DateForFileNumber(int fileNumber) =>
		AttendanceDates.Attendance.Start.Date.AddDays(fileNumber - ScheduleBlob.DailyEventFileNumberMin);

	public static DateTime ArizonaToday()
	{
		TimeZoneInfo arizona = TimeZoneInfo.FindSystemTimeZoneById("America/Phoenix");
		return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, arizona).Date;
	}

	/// <summary>
	/// Index of today's file when today falls in the attendance range; otherwise 0 (first day).
	/// </summary>
	public static int IndexForToday(IReadOnlyList<MarkdownRecord> days, DateTime? today = null)
	{
		if (days is null || days.Count == 0)
			return 0;

		DateTime arizonaToday = (today ?? ArizonaToday()).Date;
		DateTime start = AttendanceDates.Attendance.Start.Date;
		DateTime finish = AttendanceDates.Attendance.Finish.Date;
		if (arizonaToday < start || arizonaToday > finish)
			return 0;

		int expected = ScheduleBlob.DailyEventFileNumberMin + (arizonaToday - start).Days;
		for (int i = 0; i < days.Count; i++)
		{
			if (TryGetFileNumber(days[i].FileName, out int n) && n == expected)
				return i;
		}

		return 0;
	}

	public static string TitleFor(MarkdownRecord day)
	{
		if (!TryGetFileNumber(day.FileName, out int fileNumber))
			return day.FileName;

		DateTime date = DateForFileNumber(fileNumber);
		AttendanceDate? attendance = AttendanceDate.List
			.FirstOrDefault(d => d != AttendanceDate.None && d.Date.Date == date.Date);

		return attendance?.Title ?? date.ToString("ddd MM/dd");
	}
}

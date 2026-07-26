namespace RCL.Features.Sukkot.Enums.Constants;

public static class DateRange
{
	public static class Attendance
	{
		// From Sukkot.vwDateRangeTypeCodeGen / Constants (2026)
		public static DateTime Start { get; set; } = DateTime.Parse("2026-09-25");
		public static DateTime Finish { get; set; } = DateTime.Parse("2026-10-04");
	}

	public static class Lodging
	{
		public static DateTime Start { get; set; } = Attendance.Start; // DateTime.Parse("2026-09-25");
		public static DateTime Finish { get; set; } = Attendance.Finish; // DateTime.Parse("2026-10-04");
	}

}

namespace RCL.Features.Sukkot.Enums.Constants;

public static class DateRange
{
	public static class Attendance
	{
		public static DateTime Start { get; set; } = DateTime.Parse("2025-10-05");
		public static DateTime Finish { get; set; } = DateTime.Parse("2025-10-14");
	}

	public static class Lodging
	{
		public static DateTime Start { get; set; } = Attendance.Start; // DateTime.Parse("2025-10-05");
		public static DateTime Finish { get; set; } = Attendance.Finish; // DateTime.Parse("2025-10-14");
	}

}

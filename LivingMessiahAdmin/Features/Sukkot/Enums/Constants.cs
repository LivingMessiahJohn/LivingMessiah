namespace LivingMessiahAdmin.Features.Sukkot.Enums;

public class Constants
{
	public static class AttendanceDateRange
	{
		public static DateTime Start { get; set; } = DateTime.Parse("2025-10-05");
		public static DateTime Finish { get; set; } = DateTime.Parse("2025-10-14");
	}

	public static class LodgingDateRange
	{
		public static DateTime Start { get; set; } = AttendanceDateRange.Start; // DateTime.Parse("2025-10-05");
		public static DateTime Finish { get; set; } = AttendanceDateRange.Finish; // DateTime.Parse("2025-10-14");
	}
}

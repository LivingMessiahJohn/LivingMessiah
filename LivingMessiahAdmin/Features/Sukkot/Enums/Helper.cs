namespace LivingMessiahAdmin.Features.Sukkot.Enums; //Copied from LivingMessiah

public class Helper
{
	// used only by Features\Sukkot\Dashboard\Data\GridQuery.cs
	// public string AttendanceColumnValue => LivingMessiahAdmin.Features.Sukkot.Enums.Helper.GetAttendanceDatesColumnValue(AttendanceBitwise);	
	public static string GetAttendanceDatesColumnValue(int attendanceBitwise)
	{
		// Assumes AttendanceDate.List contains all AttendanceDate instances in order
		return string.Join(" ", AttendanceDate.List
			.Where(d => d != AttendanceDate.None) // Skip 'None' if present
			.Select(d => (attendanceBitwise & d.Value) == d.Value ? $"{d.Day}" : "  "));
		//.Select(d => (attendanceBitwise & d.Value) == d.Value ? "  X" : "  "));
	}

}

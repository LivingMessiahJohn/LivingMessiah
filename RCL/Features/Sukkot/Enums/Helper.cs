using AttendanceDateEnums = RCL.Features.Sukkot.Enums.AttendanceDate;

namespace RCL.Features.Sukkot.Enums;

// ToDo: you have a class called Helper and a folder called Helpers.  Consider renaming one of them to avoid confusion.
public class Helper
{
	// used only by Features\Sukkot\Dashboard\Data\GridQuery.cs
	// public string AttendanceColumnValue => LivingMessiahAdmin.Features.Sukkot.Enums.Helper.GetAttendanceDatesColumnValue(AttendanceBitwise);	
	public static string GetAttendanceDatesColumnValue(int attendanceBitwise)
	{
		// Assumes AttendanceDate.List contains all AttendanceDate instances in order
		return string.Join(" ", AttendanceDateEnums.List
			.Where(d => d != AttendanceDateEnums.None) // Skip 'None' if present
			.Select(d => (attendanceBitwise & d.Value) == d.Value ? $"{d.Day}" : "  "));
		//.Select(d => (attendanceBitwise & d.Value) == d.Value ? "  X" : "  "));
	}

}

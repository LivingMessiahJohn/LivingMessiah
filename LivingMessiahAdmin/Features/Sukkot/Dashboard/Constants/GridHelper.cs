namespace LivingMessiahAdmin.Features.Sukkot.Dashboard.Constants;

public class GridHelper
{
	public static string GetAttendanceDatesColumnHeader()
	{
		return " 5 6 7 8 9 10 11 12 13 14";
		/*
		 ToDo: figure out how to get automatically return this string using AttendanceDate

		 	foreach (var date in Attendance.List.Where(w => w.Value != 0)) ;
			
			return string.Join(" ", AttendanceDate.FromValue(attendanceBitwise).Select(s => s.Date.ToString("dd")));


		 return "05 06 07 08 09 10 11 12 13 14";

		 */
		//

	}
}

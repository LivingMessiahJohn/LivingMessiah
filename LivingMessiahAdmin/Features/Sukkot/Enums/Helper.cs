namespace LivingMessiahAdmin.Features.Sukkot.Enums; //Copied from LivingMessiah

public class Helper
{
	public static int GetDaysBitwise(DateTime[] selectedDateArray, DateTime[] selectedDateArray2ndMonth, DateRangeType dateRangeType)
	{
		int bitwise = 0;
		AttendanceDate? attendanceDate;

		//if (dateRangeType == DateRangeType.Attendance)	{ 	}

		if (selectedDateArray is null || selectedDateArray.Length == 0)
		{
		}
		else
		{
			foreach (var item in selectedDateArray!)
			{
				attendanceDate = AttendanceDate.List.Where(w => w.Date == item).SingleOrDefault();
				if (attendanceDate is not null)
				{
					bitwise += attendanceDate.Value;  // ToDo: .Bitwise is going away
				}
				else
				{
					//ExceptionMessage = $"...Acceptance Date:{item.ToShortDateString()} is out of range; range is {DateRangeType.Attendance.Range.Min.ToShortDateString()} to {DateRangeType.Attendance.Range.Max.ToShortDateString()}";
					//Logger.LogWarning(ExceptionMessage);
					//throw new RegistratationException(ExceptionMessage);
				}
			}
		}



		if (dateRangeType.HasSecondMonth)
		{
			if (selectedDateArray2ndMonth is null || selectedDateArray2ndMonth.Length == 0)
			{
			}
			else
			{
				foreach (var item in selectedDateArray2ndMonth)
				{
					attendanceDate = AttendanceDate.List.Where(w => w.Date == item).SingleOrDefault();
					if (attendanceDate is not null)
					{
						bitwise += attendanceDate.Value;
					}
					else
					{
						//ExceptionMessage = $"...Acceptance Date:{item.ToShortDateString()} is out of range; range is {DateRangeType.Attendance.Range.Min.ToShortDateString()} to {DateRangeType.Attendance.Range.Max.ToShortDateString()}";
						//Logger.LogWarning(ExceptionMessage);
						//throw new RegistratationException(ExceptionMessage);
					}
				}
			}


		}

		return bitwise;
	}

	/*
	This information is gotten from LivingMessiahAdmin\Features\Sukkot\Dashboard\Constants\GridHelper.cs
	public static string GetAttendanceDatesColumnHeader(int attendanceBitwise) 
	{ 
		return string.Join(" ", AttendanceDate.FromValue(attendanceBitwise).Select(s => s.Date.ToString("dd")));	
	}
	*/

	public static string GetAttendanceDatesColumnValue(int attendanceBitwise)
	{
		// Assumes AttendanceDate.List contains all AttendanceDate instances in order
		return string.Join(" ", AttendanceDate.List
			.Where(d => d != AttendanceDate.None) // Skip 'None' if present
			.Select(d => (attendanceBitwise & d.Value) == d.Value ? $"{d.Day}" : "  "));
		//.Select(d => (attendanceBitwise & d.Value) == d.Value ? "  X" : "  "));
	}

}

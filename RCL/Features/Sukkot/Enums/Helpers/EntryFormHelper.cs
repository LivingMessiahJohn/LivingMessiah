using AttendanceDateEnums = RCL.Features.Sukkot.Enums.AttendanceDate;

namespace RCL.Features.Sukkot.Enums.Helpers;

public class EntryFormHelper
{
	public static (DateTime[]? week1, DateTime[]? week2) GetAttendanceDatesArray(int attendanceBitwise)
	{
		if (!RCL.Features.Sukkot.Enums.DateRangeType.Attendance.HasSecondMonth)
		{
			if (AttendanceDateEnums.FromValue(attendanceBitwise)?.Any() != true)
			{
				return (null, null);
			}
			else
			{
				return (AttendanceDateEnums.FromValue(attendanceBitwise).Select(s => s.Date).ToArray(), null);
			}
		}
		else
		{
			DateTime[]? wk1;
			DateTime[]? wk2;
			if (AttendanceDateEnums.FromValue(attendanceBitwise)?.Any() != true)
			{
				wk1 = null;
			}
			else
			{
				wk1 = AttendanceDateEnums.FromValue(attendanceBitwise).Where(w => w.Week == 1).Select(s => s.Date).ToArray();
			}

			if (AttendanceDateEnums.FromValue(attendanceBitwise)?.Any() != true)
			{
				wk2 = null;
			}
			else
			{
				wk2 = AttendanceDateEnums.FromValue(attendanceBitwise).Where(w => w.Week == 2).Select(s => s.Date).ToArray();
			}
			return (wk1, wk2);
		}
	}

	public static int GetDaysBitwise(DateTime[] selectedDateArray, DateTime[] selectedDateArray2ndMonth, RCL.Features.Sukkot.Enums.DateRangeType dateRangeType)
	{
		int bitwise = 0;
		AttendanceDateEnums? attendanceDate;

		//if (dateRangeType == DateRangeType.Attendance)	{ 	}

		if (selectedDateArray is null || selectedDateArray.Length == 0)
		{
		}
		else
		{
			foreach (var item in selectedDateArray!)
			{
				attendanceDate = AttendanceDateEnums.List.Where(w => w.Date == item).SingleOrDefault();
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
					attendanceDate = AttendanceDateEnums.List.Where(w => w.Date == item).SingleOrDefault();
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

}

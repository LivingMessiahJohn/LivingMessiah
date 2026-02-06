using RCL.Features.Calendar.Enums;
using System.Globalization;
using CalendarEnums = LivingMessiah.Features.Calendar.Enums;

namespace LivingMessiah.Helpers;

public static class DateUtil
{
	//public static string ToTransliteratedHebrewDateString(this DateTime date)
	public static string ToTransliteratedHebrewDateString(this DateOnly date)
	{
		HebrewCalendar hc = new HebrewCalendar();
		// Convert DateOnly to DateTime for HebrewCalendar.GetMonth
		DateTime dateTime = date.ToDateTime(new TimeOnly(0));
		LunarMonth? lm = LunarMonth.List
			.Where(w => w.HebrewSort == hc.GetMonth(dateTime))
			.OrderBy(o => o.Date)
			.FirstOrDefault();

		string monthName = "???";
		if (lm != null)
		{
			monthName = lm!.Name;
		}
		else
		{
			monthName = hc.GetMonth(dateTime).ToString();
		}

		string hebrewDate = $" {monthName} {hc.GetDayOfMonth(dateTime)}, {hc.ToFourDigitYear(hc.GetYear(dateTime))}";
		return hebrewDate;
	}

	//public static DateOnly GetDateWithoutTime(DateTime dateTimeWithTime) {  return DateOnly.FromDateTime(dateTimeWithTime);	}
	  public static DateOnly GetDateTimeWithoutTime(DateTime dateTimeWithTime) { return DateOnly.FromDateTime(dateTimeWithTime); }


	public static (string, string) GetCurrentGregorianAndHebrewDates(int testAddDays = 0)
	{
		DateOnly today = GetDateTimeWithoutTime(DateTime.Now.AddDays(testAddDays).AddHours(Utc.ArizonaUtcMinus7));
		string gregorianDate = today.ToString(DateFormat.FeastDayPlanner);
		// HebrewCalendar APIs require DateTime, convert back to midnight DateTime
		string hebrewDate = today.ToTransliteratedHebrewDateString();
		return (gregorianDate, hebrewDate);
	}

	public static int GetNextShabbatWeek()
	{
		DateTime start = DateTime.Today;
		int daysToAdd = ((int)DayOfWeek.Saturday - (int)start.DayOfWeek + 7) % 7;
		start = start.AddDays(daysToAdd);
		return (start.Day - 1) / 7 + 1;
	}

	//https://stackoverflow.com/questions/2050805/getting-day-suffix-when-using-datetime-tostring
	public static string GetDaySuffix(int day)
	{
		switch (day)
		{
			case 1:
			case 21:
			case 31:
				return "st";
			case 2:
			case 22:
				return "nd";
			case 3:
			case 23:
				return "rd";
			default:
				return "th";
		}
	}
}

/*
Codes not ported t
 GetGregorianYmdFromHebrewYmd
 */
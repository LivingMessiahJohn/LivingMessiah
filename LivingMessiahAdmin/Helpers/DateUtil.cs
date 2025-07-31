using System.Globalization;
//using CalendarEnums = LivingMessiahAdmin.Features.Calendar.Enums;

namespace LivingMessiahAdmin.Helpers;

public static class DateUtil
{
	//public static string ToTransliteratedHebrewDateString(this DateTime date)
	//{
	//	HebrewCalendar hc = new HebrewCalendar();
	//	CalendarEnums.LunarMonth? lm = CalendarEnums.LunarMonth.List
	//	.Where(w => w.HebrewSort == hc.GetMonth(date))
	//	.OrderBy(o => o.Date)
	//	.FirstOrDefault();

	//	string monthName = "???";
	//	if (lm != null)
	//	{
	//		monthName = lm!.Name;
	//	}
	//	else
	//	{
	//		monthName = hc.GetMonth(date).ToString();
	//	}

	//	//string hebrewDate = $" {hc.ToFourDigitYear(hc.GetYear(date))} / {monthName} / {hc.GetDayOfMonth(date)}";
	//	string hebrewDate = $" {monthName} {hc.GetDayOfMonth(date)}, {hc.ToFourDigitYear(hc.GetYear(date))}";
	//	return hebrewDate;
	//}

	public static DateTime GetDateTimeWithoutTime(DateTime dateTimeWithTime)
	{
		return dateTimeWithTime.Date;
	}


	//public static (string, string) GetCurrentGregorianAndHebrewDates(int testAddDays = 0)
	//{
	//	DateTime today = GetDateTimeWithoutTime(DateTime.Now.AddDays(testAddDays).AddHours(Utc.ArizonaUtcMinus7));
	//	string gregorianDate = today.ToString(DateFormat.FeastDayPlanner);
	//	string hebrewDate = today.ToTransliteratedHebrewDateString();
	//	return (gregorianDate, hebrewDate);
	//}

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

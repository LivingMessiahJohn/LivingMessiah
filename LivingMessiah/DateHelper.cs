namespace LivingMessiah;

// Portion copied from C:\Users\JohnM\source\repos\LivingMessiahBlazor\LivingMessiah\Infrastructure\DateUtil.cs

public static class DateHelper
{
	public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
	{
		// The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
		int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
		return start.AddDays(daysToAdd);
	}

}

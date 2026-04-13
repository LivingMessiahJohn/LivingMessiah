using PWA.Helpers;
using RCL.Features.Calendar.Enums;

namespace PWA.Components.Moon.Data;

public interface IService
{
	DTO? GetEventMoon(DateTime date);
}

public class Service : IService
{
	public DTO? GetEventMoon(DateTime date)
	{
		EventMoon? currentMonth = null;
		EventMoon? nextMonth = null;

		DateOnly dateAzUTC = DateUtil.GetDateTimeWithoutTime(date.AddHours(Utc.ArizonaUtcMinus7));
		currentMonth = EventMoon.List
			.Where(w => w.Date < dateAzUTC)
			.OrderByDescending(o => o.Date)
			.FirstOrDefault();

		nextMonth = EventMoon.List
			.Where(w => w.Date >= dateAzUTC)
			.OrderBy(o => o.Date)
			.FirstOrDefault();

		if (nextMonth is not null)
		{
			int daysToGo = nextMonth.Date!.Value.DayNumber - dateAzUTC.DayNumber;
			int daysOld = 30 - daysToGo;

			if (daysOld < 0)
			{
				daysOld = 1;
			}

			string daysToGoText;
			string imageFile;

			if (daysOld == 0)
			{
				daysToGoText = "New Moon";
				imageFile = "/images/moons/01.jpg";
			}
			else
			{
				daysToGoText = $"{daysToGo} days";
				imageFile = (daysOld >= 0 && daysOld <= 30)
					? $"/images/moons/{FormatDaysOld(daysOld)}.jpg"
					: "/images/moons/01.jpg";
			}

			return new DTO(daysToGo, daysOld, daysToGoText, imageFile, currentMonth?.FullName ?? "", nextMonth.FullName);
		}

		return null;
	}

	private static string FormatDaysOld(int daysOld)
	{
		return daysOld.ToString("D2");
	}
}

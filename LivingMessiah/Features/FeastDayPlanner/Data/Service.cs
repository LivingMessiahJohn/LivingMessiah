using LivingMessiah.Helpers;
using FeastDayType = RCL.Features.Calendar.Enums.FeastDay;


using LunarMonthType = RCL.Features.Calendar.Enums.LunarMonth;

namespace LivingMessiah.Features.FeastDayPlanner.Data;

public interface IService
{
	HeaderServiceModel GetHeaderServiceModel(FeastDayType feastDay);
	LunarMonths.ProgressBarVM GetHeaderServiceModelLunarMonth(LunarMonthType lunarMonth);
}

public class Service : IService
{
	public LunarMonths.ProgressBarVM GetHeaderServiceModelLunarMonth(LunarMonthType lunarMonth)
	{
		DateOnly today = DateUtil.GetDateTimeWithoutTime
			(
				DateTime.Now.AddDays
				(
					Test.AddDays).AddHours(Utc.ArizonaUtcMinus7
				)
			);

		LunarMonths.ProgressBarVM model = new LunarMonths.ProgressBarVM();

		model.GregorianDate = today.ToString(DateFormat.FeastDayPlanner);
		model.HebrewDate = today.ToTransliteratedHebrewDateString();

		decimal avgDaysPerMonth = 29.5m;

		// .Where(w => w.Date >= dateTimeWithoutTime)

		if (today >= lunarMonth!.Date && today <= lunarMonth!.Date)  //if (today >= lunarMonth!.DateTime2 && today <= lunarMonth!.DateTime2)
		{
			model.BadgeColor = "bg-success-subtle";
			model.SuffixDescription = $"day";
			model.DaysDifferent = today.DayNumber - lunarMonth!.Date.DayNumber;
			model.DaysDifferentFormat = $"{model.DaysDifferent}{DateUtil.GetDaySuffix(model.DaysDifferent)}";
			model.PercentUntilNewMoon = (int)Math.Round((model.DaysDifferent / avgDaysPerMonth) * 100);
			model.DaysOld = 100 - model.PercentUntilNewMoon;
		}
		else
		{
			if (today < lunarMonth!.Date)
			{
				model.BadgeColor = "bg-warning-subtle";
				model.SuffixDescription = "days ahead";
				model.DaysDifferent = lunarMonth!.Date.DayNumber - today.DayNumber;
				model.DaysDifferentFormat = model.DaysDifferent.ToString();
				model.PercentUntilNewMoon = (int)Math.Round((model.DaysDifferent / avgDaysPerMonth) * 100);
				model.DaysOld = 100 - model.PercentUntilNewMoon;
			}
			else
			{
				model.BadgeColor = "bg-danger-subtle";
				model.SuffixDescription = "days in the past";
				model.DaysDifferent = today.DayNumber - lunarMonth!.Date.DayNumber;
				model.DaysDifferentFormat = model.DaysDifferent.ToString();
				model.PercentUntilNewMoon = (int)Math.Round((model.DaysDifferent / avgDaysPerMonth) * 100);
				model.DaysOld = 100 - model.PercentUntilNewMoon;
			}
		}

		return model;
	}

	public HeaderServiceModel GetHeaderServiceModel(FeastDayType feastDay)
	{
		DateOnly today = DateUtil.GetDateTimeWithoutTime
			(
				DateTime.Now.AddDays
				(
					Test.AddDays).AddHours(Utc.ArizonaUtcMinus7
				)
			);

		HeaderServiceModel model = new HeaderServiceModel();

		if (today >= feastDay!.DateOnlyRange.Min && today <= feastDay!.DateOnlyRange.Max)
		{
			model.BadgeColor = "bg-success-subtle";
			model.SuffixDescription = $"day";
			model.DaysDifferent = today.DayNumber - feastDay!.DateOnlyRange.Min.DayNumber;
			model.DaysDifferentFormat = $"{model.DaysDifferent}{DateUtil.GetDaySuffix(model.DaysDifferent)}";
		}
		else
		{
			if (today < feastDay!.DateOnlyRange.Min)
			{
				model.BadgeColor = "bg-warning-subtle";
				model.SuffixDescription = "days ahead";
				model.DaysDifferent = feastDay!.DateOnlyRange.Min.DayNumber - today.DayNumber;
				model.DaysDifferentFormat = model.DaysDifferent.ToString();
			}
			else
			{
				model.BadgeColor = "bg-danger-subtle";
				model.SuffixDescription = "days in the past";
				model.DaysDifferent = today.DayNumber - feastDay!.DateOnlyRange.Max.DayNumber;
				model.DaysDifferentFormat = model.DaysDifferent.ToString();
			}
		}

		return model;
	}
}

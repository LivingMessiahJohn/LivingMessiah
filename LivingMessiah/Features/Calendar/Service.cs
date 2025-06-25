using LivingMessiah.Helpers;
using LivingMessiah.Features.Shavuot.Domain;

namespace LivingMessiah.Features.Calendar;

public interface IService
{
	List<ScheduleData.ReadonlyEventsData> GetData();
}

public class Service(ILogger<Service> logger) : IService
{
	private readonly ILogger<Service> Logger = logger; // DI via primary constructor

	public List<ScheduleData.ReadonlyEventsData> GetData()
	{
		int _RunningCount = 0;
		Logger.LogDebug("{Method}", nameof(GetData));

		List<ScheduleData.ReadonlyEventsData>? _DataList = new List<ScheduleData.ReadonlyEventsData>();

		var _Tuple1 = LoadFeastDaysExceptHanukkah(_RunningCount, _DataList);
		Logger.LogDebug("...{After}: _Tuple1.RunningCount: {RunningCount}", nameof(LoadFeastDaysExceptHanukkah), _Tuple1.RunningCount);
	
		var _Tuple2 = LoadFeastDayDetails(_Tuple1.RunningCount, _Tuple1.DataList);
		Logger.LogDebug("...{After}: _Tuple2.RunningCount: {RunningCount}", nameof(LoadFeastDayDetails), _Tuple2.RunningCount);

		var _Tuple3 = LoadOmerDates(_Tuple2.RunningCount, _Tuple2.DataList);
		Logger.LogDebug("...{After}: _Tuple3.RunningCount: {RunningCount}", nameof(LoadOmerDates), _Tuple3.RunningCount);

		var _Tuple4 = LoadHanukkahDates(_Tuple3.RunningCount, _Tuple3.DataList);
		Logger.LogDebug("...{After}: _Tuple4.RunningCount: {RunningCount}", nameof(LoadHanukkahDates), _Tuple4.RunningCount);

		var _Tuple5 = LoadMonths(_Tuple4.RunningCount, _Tuple4.DataList);
		Logger.LogDebug("...{After}: _Tuple5.RunningCount: {RunningCount}", nameof(LoadMonths), _Tuple5.RunningCount);

		var _Tuple6 = LoadSeasons(_Tuple5.RunningCount, _Tuple5.DataList);
		Logger.LogDebug("...{After}: _Tuple6.RunningCount: {RunningCount}", nameof(LoadSeasons), _Tuple6.RunningCount);

		return _Tuple6.DataList;
	}
	private static (int RunningCount, List<ScheduleData.ReadonlyEventsData> DataList)
	LoadFeastDaysExceptHanukkah(int runningCount, List<ScheduleData.ReadonlyEventsData> dataList)
	{
		int i = 0;

		foreach (var fd in Enums.FeastDay.List
												.Where(w => w.Value != Enums.FeastDay.Hanukkah & w.Value != Enums.FeastDay.Passover)
												.OrderBy(o => o.Value).ToList())
		{
			i += 1;
			dataList!.Add(new ScheduleData.ReadonlyEventsData
			{
				Id = i,
				Subject = fd.CalendarTitle,
				Description = fd.Details,
				StartTime = fd.Date,
				EndTime = fd.Date,
				CategoryColor = Enums.DateType.Feast.CalendarColor,
				IsAllDay = true,
				IsReadonly = true
			}
			);
		}

		return (runningCount + i, dataList);
	}

	// FeastDayDetails
	private static (int RunningCount, List<ScheduleData.ReadonlyEventsData> DataList)
		LoadFeastDayDetails(int runningCount, List<ScheduleData.ReadonlyEventsData> dataList)
	{
		DateTime date;
		int i = 0;

		foreach (var item in Enums.FeastDayDetail.List.ToList())
		{
			i += 1;
			date = Enums.FeastDay.FromValue(item.ParentFeastDayId).Date;
			dataList!.Add(new ScheduleData.ReadonlyEventsData
			{
				Id = i + runningCount,
				Subject = item.Title,
				Description = item.Description,
				StartTime = date.AddDays(item.AddDays),
				EndTime = date.AddDays(item.AddDays),
				CategoryColor = item.CalendarColor,
				IsAllDay = true,
				IsReadonly = true
			}
			);
		}
		return (runningCount + i, dataList);
	}

	// Omer
	private static (int RunningCount, List<ScheduleData.ReadonlyEventsData> DataList)
		 LoadOmerDates(int runningCount, List<ScheduleData.ReadonlyEventsData> dataList)
	{
		DateTime startDate = Enums.FeastDay.Passover.Date.AddDays(1);

		int i;
		for (i = 1; i < 50; i++)
		{
			dataList!.Add(new ScheduleData.ReadonlyEventsData
			{
				Id = i + runningCount,
				Subject = $"Omer {i} {OmerGematriaDictionary.GetHebrew(i)}",
				Description = "Omer Count, Day " + i,
				StartTime = startDate.AddDays(i - 1),
				EndTime = startDate.AddDays(i - 1),
				CategoryColor = CalendarColors.Info,
				IsAllDay = true,
				IsReadonly = true
			}
			);
		}
		return (runningCount + i, dataList);

	}

	private static (int RunningCount, List<ScheduleData.ReadonlyEventsData> DataList)
		LoadHanukkahDates(int runningCount, List<ScheduleData.ReadonlyEventsData> dataList)
	{
		DateTime startDate = Calendar.Enums.FeastDay.Hanukkah.Date.AddDays(0);
		string candle = "🕯️";

		int i;
		for (i = 0; i < 8; i++)
		{
			dataList!.Add(new ScheduleData.ReadonlyEventsData
			{
				Id = i + runningCount,
				Subject = $"Hanukkah {candle.Repeat(i + 1)}",
				Description = "8 Days of Hanukkah; dates determined by Rabbinic sources",
				StartTime = startDate.AddDays(i),
				EndTime = startDate.AddDays(i),
				CategoryColor = Enums.DateType.Feast.CalendarColor,
				IsAllDay = true,
				IsReadonly = true
			}
			);
		}
		return (runningCount + i, dataList);
	}

	private static (int RunningCount, List<ScheduleData.ReadonlyEventsData> DataList)
		LoadMonths(int runningCount, List<ScheduleData.ReadonlyEventsData> dataList)
	{
		string moon = "🌙";

		int i = 0;
		foreach (var month in Enums.LunarMonth.List.ToList())
		{
			i += 1;
			dataList!.Add(new ScheduleData.ReadonlyEventsData
			{
				Id = i + runningCount,
				Subject = $"{moon} {month.FullName}",
				Description = month.Description,
				StartTime = month.Date,
				EndTime = month.Date,
				CategoryColor = Enums.DateType.Month.CalendarColor,
				IsAllDay = true,
				IsReadonly = true
			}
			);
		}
		return (runningCount + i, dataList);
	}

	private static (int RunningCount, List<ScheduleData.ReadonlyEventsData> DataList)
		LoadSeasons(int runningCount, List<ScheduleData.ReadonlyEventsData> dataList)
	{
		int i = 0;
		foreach (var season in Enums.Season.List.ToList())
		{
			i += 1;
			dataList!.Add(new ScheduleData.ReadonlyEventsData
			{
				Id = i + runningCount,
				Subject = $"{season.Emoji} {season.Name}",
				Description = $"{season.Name} | {season.Type}",
				StartTime = season.Date,
				EndTime = season.Date,
				CategoryColor = season.CalendarColor,
				IsAllDay = true,
				IsReadonly = true
			}
			);
		}
		return (runningCount + i, dataList);
	}

}

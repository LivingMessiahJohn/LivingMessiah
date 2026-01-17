using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RCL.Features.Calendar.Enums;
using static RCL.Features.Calendar.Enums.ProjectionExtensions;
//using static RCL.Features.Calendar.Constants.TextAlign;

//using RCL.Features.FeastDayPlanner.Enums;

using PlannerDetailEnum = RCL.Features.FeastDayPlanner.Enums.PlannerDetail;
using RCL.Features.Calendar.Constants.CSS;

namespace RCL.Features.Calendar.Data;


public interface IService
{
  List<DtoCombined>? GetData();
  DateOnlyRange? GetDateRange();
  DateOnlyRange? GetDateRange2();
  public List<DtoCombined>? GetByMonth(DateOnly date);
  public List<DayCellRecord>? GetByDate(DateOnly date);
  public List<DayCellRecord>? GetAll();
}

public class Service : IService
{
  private readonly ILogger<Service>? Logger;

  public Service(ILogger<Service> logger)
  {
    Logger = logger;
    Logger?.LogDebug("RCL.Features.Calendar.Data.Service instantiated");
  }

  public List<DayCellRecord>? GetAll()
  {
    //var results = new List<DayCellRecord>();

    List<DayCellRecord> results = new();


    foreach (var item in LunarMonth.List)
    {
      results.AddRange(GetByDate(item.ErevDate) ?? Enumerable.Empty<DayCellRecord>());
      results.AddRange(GetByDate(item.DayTimeDate) ?? Enumerable.Empty<DayCellRecord>());
    }


    //List<DayCellRecord> omerDays = new();
    //for (int i = 0; i < 49; i++)
    //{
    //  //omerDays.AddRange(GetByDate(PlannerDetailEnum.StartOmer.DateOnly.AddDays(i)));
    //  var dayRecords = GetByDate(PlannerDetailEnum.StartOmer.DateOnly.AddDays(i));
    //  omerDays.AddRange(dayRecords ?? Enumerable.Empty<DayCellRecord>());
    //}

    //Logger!.LogDebug("{Method} {omerDays.Count}", nameof(GetAll), omerDays.Count());

    foreach (var item in Season.List)
    {
      results.AddRange(GetByDate(item.Date) ?? Enumerable.Empty<DayCellRecord>());
    }

    // Missing Matzah Day 7
    foreach (var item in FeastDay.List)
    {
      results.AddRange(GetByDate(item.ErevDate) ?? Enumerable.Empty<DayCellRecord>());
      results.AddRange(GetByDate(item.DayTimeDate) ?? Enumerable.Empty<DayCellRecord>());
    }

    //return results.OrderBy(o => o.Date).ToList();
    return results.OrderBy(o => o.Date).Where(w => w.Date.Year == 2026).ToList();

  }

  public List<DayCellRecord>? GetByDate(DateOnly date)
  {
    const string spanPrimary = "<span class='badge bg-primary fs-6 text-white'>";
    const string spanPrimaryFloatEnd = "<span class='badge bg-primary fs-6 text-white float-end'>";
    const string badgeWarning = "'badge bg-warning fs-6 text-black-50'";
    const string badgeWarningHebrew = "'badge bg-warning text-black hebrew16'";
    int dayCount = 0;

    var combined = new List<DayCellRecord>();

    try
    {
      if (date == DateOnly.MinValue) { return null; }

      // Lunar Months
      LunarMonth? lunarMonth = LunarMonth.List.FirstOrDefault(x => x.ErevDate == date);
      if (lunarMonth != null) { combined.AddRange(new DayCellRecord(lunarMonth.ErevDate, DateType.Month, IsErev: true, lunarMonth.CalendarDayErevHtml, TextAlign.End)); }

      lunarMonth = LunarMonth.List.FirstOrDefault(x => x.DayTimeDate == date);
      if (lunarMonth != null) { combined.AddRange(new DayCellRecord(lunarMonth.DayTimeDate, DateType.Month, IsErev: false, lunarMonth.CalendarDayDayTimeHtml, TextAlign.Start)); }

      // Seasons
      Season? season = Season.List.FirstOrDefault(x => x.Date == date);  //
      if (season != null) { combined.AddRange(new DayCellRecord(season.Date, DateType.Season, IsErev: false, season.CalendarDayHtml, TextAlign.Center)); }

      // Hanukkah Week
      if (FeastDay.Hanukkah.DateOnlyRange.Min >= date && FeastDay.Hanukkah.DateOnlyRange.Max <= date)
      {
        //dayCount = 0;
        string candle = "🕯️"; // ToDo: make this a FontAwesome icon
        FeastDay hanukkah = FeastDay.Hanukkah;
        dayCount = (hanukkah.DateOnlyRange.Min).Day + date.Day;

        combined.AddRange(new DayCellRecord(date.AddDays(-1), DateType.Feast, IsErev: true, (MarkupString)$"{spanPrimary} <i class='fas fa-hanukiah'></i></span>", TextAlign.End)); 
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: false, (MarkupString)$"{spanPrimary} {dayCount} {candle}</span>", TextAlign.Start));
      }


      // Purim
      if (FeastDay.Purim.ErevDate == date)
      {
        // $"<span class='badge bg-info fs-6 text-white float-end'><i class='far fa-moon'></i></span>";
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: true, (MarkupString)$"{spanPrimaryFloatEnd}<i class='{FeastDay.Purim.Icon}'></i></span>", TextAlign.End));
      }
      if (FeastDay.Purim.DayTimeDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: false, (MarkupString)$"{spanPrimary} {FeastDay.Purim.Name}</span>", TextAlign.Start));
      }


      // Passover
      if (FeastDay.Passover.DateOnlyRange.Min == date)
      {
        combined.AddRange(new DayCellRecord(date.AddDays(-1), DateType.Feast, IsErev: false, (MarkupString)FeastDay.Passover.CalendarDayHtml, TextAlign.Center)); // combine icon and text together
      }

      // Unleavened Bread: two pairs of days; Day 1 and Day 7, each with Erev and DayTime
      if (PlannerDetailEnum.UnleavenedBreadDay1.ErevDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: true, (MarkupString)$"{spanPrimaryFloatEnd}<i class='{FeastDayDetail.UnleavenedBreadDay1.Icon}'></i></span>", TextAlign.End));
      }
      if (PlannerDetailEnum.UnleavenedBreadDay1.DayTimeDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: false, (MarkupString)$"{spanPrimary} {FeastDayDetail.UnleavenedBreadDay1.Title}</span>", TextAlign.Start));
      }

      if (PlannerDetailEnum.UnleavenedBreadDay7.ErevDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: true, (MarkupString)$"{spanPrimaryFloatEnd}<i class='{FeastDayDetail.UnleavenedBreadDay7.Icon}'></i></span>", TextAlign.End));
      }
      if (PlannerDetailEnum.UnleavenedBreadDay7.DayTimeDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: false, (MarkupString)$"{spanPrimary} {FeastDayDetail.UnleavenedBreadDay7.Title}</span>", TextAlign.Start));
      }


      // Omer: generate 1..49 days starting at Passover
      if (PlannerDetailEnum.StartOmer.ErevDate >= date && PlannerDetailEnum.StopOmer.DayTimeDate <= date)
      {
        dayCount = (PlannerDetailEnum.StartOmer.ErevDate).Day + date.Day;
        combined.AddRange(new DayCellRecord(date.AddDays(-1), DateType.Feast, IsErev: true, (MarkupString)$"<span class={badgeWarning}><i class='fas calendar-day'></i></span>", TextAlign.End));
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: false, (MarkupString)$"<span class={badgeWarning}>{dayCount}</span>&nbsp;<span class={badgeWarningHebrew}>{OmerGematriaDictionary.GetHebrew(dayCount)} </span>", TextAlign.Start));
      }

      // Weeks (_Shavuot_): 
      if (FeastDay.Weeks.ErevDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: true, (MarkupString)$"{spanPrimaryFloatEnd}<i class='{FeastDay.Weeks.Icon}'></i></span>", TextAlign.End));
      }
      if (FeastDay.Weeks.DayTimeDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: false, (MarkupString)$"{spanPrimary} {FeastDay.Weeks.Name}</span>", TextAlign.Start));
      }

      // Trumpets (_Yom Teruah_): 
      if (FeastDay.Trumpets.ErevDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: true, (MarkupString)$"{spanPrimaryFloatEnd}<i class='{FeastDay.Trumpets.Icon}'></i></span>", TextAlign.End));
      }
      if (FeastDay.Trumpets.DayTimeDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: false, (MarkupString)$"{spanPrimary} {FeastDay.Trumpets.Name}</span>", TextAlign.Start));
      }

      // YomKippur (_Yom Kippur_): 
      if (FeastDay.YomKippur.ErevDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: true, (MarkupString)$"{spanPrimaryFloatEnd}<i class='{FeastDay.YomKippur.Icon}'></i></span>", TextAlign.End));
      }
      if (FeastDay.YomKippur.DayTimeDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: false, (MarkupString)$"{spanPrimary} {FeastDay.YomKippur.CalendarTitle}</span>", TextAlign.Start));
      }

      // # Sukkot:

      // ## Sukkot: 1st pair of days; Day 1 and Day 8, each with Erev and DayTime
      if (PlannerDetailEnum.SukkotDay1.ErevDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: true, (MarkupString)$"{spanPrimaryFloatEnd}<i class='{FeastDay.Tabernacles.Icon}'></i></span>", TextAlign.End));
      }
      if (PlannerDetailEnum.SukkotDay1.DayTimeDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: false, (MarkupString)$"{spanPrimary}{PlannerDetailEnum.SukkotDay1.Description}</span>", TextAlign.Start));
      }


      if (FeastDay.Tabernacles.DateOnlyRange.Min == date)
      {
        combined.AddRange(new DayCellRecord(date.AddDays(-1), DateType.Feast, IsErev: false, (MarkupString)FeastDay.Tabernacles.CalendarDayHtml, TextAlign.Center));
      }


      if (PlannerDetailEnum.SukkotDay8.ErevDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: true, (MarkupString)$"{spanPrimaryFloatEnd}<i class='{FeastDay.Tabernacles.Icon}'></i></span>", TextAlign.End));
      }
      if (PlannerDetailEnum.SukkotDay8.DayTimeDate == date)
      {
        combined.AddRange(new DayCellRecord(date, DateType.Feast, IsErev: false, (MarkupString)$"{spanPrimary}{PlannerDetailEnum.SukkotDay8.Description}</span>", TextAlign.Start));
      }

      if (FeastDay.Tabernacles.DateOnlyRange.Max == date)
      {
        combined.AddRange(new DayCellRecord(date.AddDays(+1), DateType.Feast, IsErev: false, (MarkupString)FeastDay.Tabernacles.CalendarDayHtml, TextAlign.Center));
      }


    }
    catch (Exception ex)
    {
      Logger!.LogWarning(ex, "{Method} {date}", nameof(GetByMonth), date.ToShortDateString());
      return null;
    }

    //if (combined is not null && combined.Any())  {  }
    //Logger!.LogDebug("{Method} {Message}", nameof(GetByDate), $"date: {date.ToShortDateString()}, combined.Count: {combined.Count()}");

    return combined;
  }

  public List<DtoCombined>? GetData()
  {
    //List<DayCellRecord> LunarMonthList = LunarMonth.List.ProjectToLunarMonth(isErev: true);
    //List<DayCellRecord> SeasonList = Season.List.ProjectToSeason(isErev: true);
    //List<DayCellRecord> NoHanukkahList = FeastDay.List.ProjectToFeastDayNoHanukkah(isErev: true);
    //List<DayCellRecord> OmerList = FeastDay.List.ProjectToOmer(isErev: true);
    //List<DayCellRecord> HanukkahList = FeastDay.List.ProjectToHanukkah(isErev: true);
    //List<DayCellRecord> FeastDayDetailList = FeastDayDetail.List.ProjectToFeastDayDetail();

    // combine all Dto lists into a single list
    var combined = new List<DayCellRecord>();
    //combined.AddRange(LunarMonthList);
    //combined.AddRange(SeasonList);
    //combined.AddRange(NoHanukkahList);
    //combined.AddRange(OmerList);
    //combined.AddRange(HanukkahList);
    //combined.AddRange(FeastDayDetailList);

    // create an ordered sequence and attach an overall row index (running row number)
    var orderedWithIndex = combined
      .OrderBy(x => x.Date)
      .ThenByDescending(x => x.DateType)
      .Select((item, index) => new
      {
        item.Date,
        item.DateType,
        item.Description,
        OverallIndex = index // zero-based overall index
      });

    List<DtoCombined>? _DtoCombinedList;
    // group by date, but use the OverallIndex as the running total / overall row number
    _DtoCombinedList = orderedWithIndex
      .GroupBy(x => x.Date)
      .SelectMany(g =>
      {
        bool hasDupes = g.Count() > 1;
        return g.Select(x => new DtoCombined(
          x.Date,
          x.OverallIndex + 1,  // Running total (overall row number, 1-based)
          hasDupes,            // HasDupes
          g.Count(),           // DupeCount (number of items for this date)
          x.DateType, x.Description
        ));
      })
      .ToList();

    return _DtoCombinedList;
  }
  
  public DateOnlyRange? GetDateRange()
  {
    List<DtoCombined>? _DtoCombinedList = GetData();

    DateOnlyRange? _dateOnlyrange;

    if (_DtoCombinedList is not null && _DtoCombinedList.Any())
    {
      var minDate = _DtoCombinedList.Min(d => d.Date);
      var maxDate = _DtoCombinedList.Max(d => d.Date);
      _dateOnlyrange = new DateOnlyRange(minDate, maxDate);
      return _dateOnlyrange;
    }
    else
    {
      //Logger!.LogDebug("{Method} {Message}", nameof(GetDateRange), "_DtoCombinedList is null or not Any");
      return null;
    }
  }

  public List<DtoCombined>? GetByMonth(DateOnly date)
  {
    List<DtoCombined>? _DtoCombinedList = GetData();

    if (_DtoCombinedList is null || _DtoCombinedList.Count == 0) 
    {
      Logger!.LogDebug("{Method} {Message}", nameof(GetByMonth), "_DtoCombinedList is null or Count=0");
      return null; 
    }

    return _DtoCombinedList
      .Where(d => d.Date.Year == date.Year && d.Date.Month == date.Month)
      .OrderBy(d => d.Date)
      .ThenByDescending(d => d.DateType)
      .ToList();
  }

  public DateOnlyRange? GetDateRange2()
  {
    try
    {
      // gather date/time values from the three sources (they already filter out DateTime.MinValue by default)
      var seasonDates = RCL.Features.Calendar.Constants.SeasonDates.List();
      var feastDates = RCL.Features.Calendar.Constants.FeastDayDates.List();
      var lunarDates = RCL.Features.Calendar.Constants.LunarMonthDates.List();

      var allDates = seasonDates
        .Concat(feastDates)
        .Concat(lunarDates)
        .Where(d => d != DateTime.MinValue)
        .ToList();

      if (!allDates.Any())
      {
        Logger!.LogDebug("{Method} {Message}", nameof(GetDateRange2), "no dates found");
        return null;
      }

      var minDateTime = allDates.Min();
      var maxDateTime = allDates.Max();

      var minDateOnly = DateOnly.FromDateTime(minDateTime);
      var maxDateOnly = DateOnly.FromDateTime(maxDateTime);

      return new DateOnlyRange(minDateOnly, maxDateOnly);
    }
    catch (Exception ex)
    {
      Logger!.LogWarning(ex, "{Method} failed", nameof(GetDateRange2));
      return null;
    }
  }
}
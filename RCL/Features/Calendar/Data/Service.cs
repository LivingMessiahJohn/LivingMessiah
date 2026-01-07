using Microsoft.AspNetCore.Components;
using RCL.Features.Calendar.Enums;

namespace RCL.Features.Calendar.Data;


public interface IService
{
  List<DtoCombined>? GetData();
  DateOnlyRange? GetDateRange();
  public List<DtoCombined>? GetByMonth(DateOnly date);
}


public class Service : IService
{

	public List<DtoCombined>? GetData()
	{
    List<Dto> LunarMonthList = new();
    foreach (var item in LunarMonth.List
      .Where(w => w.Date > DateTime.MinValue)  // If LunarMonth.Adar2 has Date == DateTime.MinValue then exclude it
      .ToList())
    {
      LunarMonthList.Add(new Dto(DateOnly.FromDateTime(item.Date), 
        DateType.Month, 
        item.CalendarDayHtml));
    }

    List<Dto> SeasonList = new();
    foreach (var item in Season.List.ToList())
    {
      SeasonList.Add(new Dto(DateOnly.FromDateTime(item.Range.Min), 
        DateType.Season,
        item.CalendarDayHtml)); 
    }

    // No Hanukkah
    List<Dto> NoHanukkahList = new();
    foreach (var item in FeastDay.List
      .Where(w => w.Value != FeastDay.Hanukkah & w.Value != FeastDay.Passover)
      .ToList())
    {
      NoHanukkahList.Add(new Dto(DateOnly.FromDateTime(item.Date), 
        DateType.Feast,
        item.CalendarDayHtml)); 
    }

    List<Dto> OmerList = new();
    int i;
    for (i = 1; i < 50; i++)
    {
      OmerList.Add(new Dto(DateOnly.FromDateTime(FeastDay.FromValue(FeastDay.Passover).Date).AddDays(i - 1), 
        DateType.Feast,
        (MarkupString)$"<span class='badge bg-warning fs-5 text-black-50'><i class='fas fa-calendar-day'></i>&nbsp;{i}</span>&nbsp;&nbsp;&nbsp;<span class='badge bg-warning text-black hebrew16'>{OmerGematriaDictionary.GetHebrew(i)} </span>"));
    }

    List<Dto> HanukkahList = new();
    string candle = "🕯️"; 
    for (i = 1; i < 9; i++)
    {
      var candles = string.Concat(Enumerable.Repeat(candle, i));
      HanukkahList.Add(new Dto(
        DateOnly.FromDateTime(FeastDay.Hanukkah.Date).AddDays(i - 1), 
        DateType.Feast,
        (MarkupString)$"<span class='badge bg-primary fs-5 text-white' title='ToDo: add a tooltip and onclick()'><i class='fas fa-hanukiah'></i> {i} {candle}</span>"));
    }

    List<Dto> FeastDayDetailList = new();
    DateTime date;
    foreach (var item in FeastDayDetail.List.ToList())
    {
      date = FeastDay.FromValue(item.ParentFeastDayId).Date;
      FeastDayDetailList.Add(new Dto(DateOnly.FromDateTime(date.AddDays(item.AddDays)), 
        DateType.Feast, 
        item.CalendarDayHtml));
    }

    // combine all Dto lists into a single list
    var combined = new List<Dto>();
    combined.AddRange(LunarMonthList);
    combined.AddRange(SeasonList);
    combined.AddRange(NoHanukkahList);
    combined.AddRange(OmerList);
    combined.AddRange(HanukkahList);
    combined.AddRange(FeastDayDetailList);

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
      return null;
    }
  }

  public List<DtoCombined>? GetByMonth(DateOnly date)
  {
    List<DtoCombined>? _DtoCombinedList = GetData();

    if (_DtoCombinedList is null || _DtoCombinedList.Count == 0) { return null;  }

    return _DtoCombinedList
      .Where(d => d.Date.Year == date.Year && d.Date.Month == date.Month)
      .OrderBy(d => d.Date)
      .ThenByDescending(d => d.DateType)
      .ToList();
  }


}
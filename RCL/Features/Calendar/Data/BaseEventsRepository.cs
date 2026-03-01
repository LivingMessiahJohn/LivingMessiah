using Microsoft.AspNetCore.Components;
using RCL.Features.Calendar.Enums;
using CSS =       RCL.Features.Calendar.Constants.CSS.JustifyContentSuffix;
using CSSDayCell = RCL.Features.Calendar.Constants.CSS.DayCell;

using ParashaEnums = RCL.Features.Parasha.Enums;


namespace RCL.Features.Calendar.Data;

public abstract class BaseEventsRepository : ComponentBase
{
  protected List<Enums.EventRecord>? BaseData { get; } = 
    Enums.EventFeast.List
      .Where(f => f.IsSplit).Where(f => f != EventFeast.Hanukkah)
      .Select(f => new Enums.EventRecord(f.ErevDate, Filter.Feast
        , GetJustifyContent(CSS.End), f.BorderColor
        , f.SpanIcon, f.Category.Value, f.EventSort))
      .Concat(Enums.EventFeast.List
      .Where(f => f.IsSplit).Where(f => f != EventFeast.Hanukkah)
      .Select(f => new Enums.EventRecord(f.DayTimeDate, Filter.Feast
        , GetJustifyContent(CSS.Start), f.BorderColor
        , f.SpanText, f.Category.Value, f.EventSort)))

      .Concat(Enums.EventFeast.List
      .Where(f => !f.IsSplit).Where(f => f != EventFeast.Omer)
      .Select(f => new Enums.EventRecord(f.DayTimeDate, Filter.Feast
        , GetJustifyContent(CSS.Center), f.BorderColor
        , f.SpanIconAndText, f.Category.Value, f.EventSort)))

      .Concat(Enums.EventFeast.List
      .Where(f => !f.IsSplit).Where(f => f != EventFeast.Omer)
      .Select(f => new Enums.EventRecord(f.DayTimeDate.AddDays(1), Filter.Feast
        , GetJustifyContent(CSS.Center)
        , f.BorderColor, f.SpanBlank, f.Category.Value, f.EventSort)))


      .Concat(Enumerable.Range(0, 49)
      .Select(i => new Enums.EventRecord(
          Enums.EventFeast.Omer.Date.AddDays(i),
          Filter.Feast,
          GetJustifyContent(CSS.Center), "",
          $"<span class='badge bg-primary bg-opacity-50'><i class='fas fa-hashtag'></i> {i+1}</span>",
          Enums.EventFeast.Omer.Category.Value, Enums.EventFeast.Omer.EventSort)))

      // Hanukkah
      .Concat(Enums.EventFeast.List
      .Where(f => f == EventFeast.Hanukkah)
      .Select(i => new Enums.EventRecord(
          Enums.EventFeast.Hanukkah.FeastDateRange.Min,
          Filter.Feast,
          GetJustifyContent(CSS.End), i.BorderColor,
          $"<span class='badge bg-danger'><i class='fas fa-hanukiah'></i></span>",
          Enums.EventFeast.Hanukkah.Category.Value, Enums.EventFeast.Hanukkah.EventSort)))

       .Concat(Enumerable.Range(1, 7)
       .Select(i => new Enums.EventRecord(
          Enums.EventFeast.Hanukkah.FeastDateRange.Min.AddDays(i),
          Filter.Feast,
          GetJustifyContent(CSS.Between), Enums.EventFeast.Hanukkah.BorderColor,
          $"</span><span class='badge bg-danger text-white'>{i}</span><span class='badge bg-danger'><i class='fas fa-hanukiah'></i>",
          Enums.EventFeast.Hanukkah.Category.Value, Enums.EventFeast.Hanukkah.EventSort)))

      .Concat(Enums.EventFeast.List
      .Where(f => f == EventFeast.Hanukkah)
      .Select(i => new Enums.EventRecord(
          Enums.EventFeast.Hanukkah.FeastDateRange.Max,
          Filter.Feast,
          GetJustifyContent(CSS.Start), i.BorderColor,
          $"<span class='badge bg-danger text-white'>8</span>",
          Enums.EventFeast.Hanukkah.Category.Value, Enums.EventFeast.Hanukkah.EventSort)))


      .Concat(Enums.EventSeason.List
      .Select(s => new Enums.EventRecord(
          s.Date, 
          Filter.Season, 
          GetJustifyContent(CSS.Center), "", 
          s.SpanIconAndText, 
          Filter.Season.Value, 
          s.EventSort)))

      .Concat(Enums.EventMoon.List
      .Where(m => m.ErevDate is not null)
      .Select(m => new Enums.EventRecord(m.Date.HasValue ? m.Date.Value.AddDays(-1) : default, Filter.Month
        , GetJustifyContent(CSS.End), "", 
        m.SpanIcon, Filter.Month.Value, m.EventSort)))

      .Concat(Enums.EventMoon.List
      .Where(m => m.DayTimeDate is not null)
      .Select(m => new Enums.EventRecord(m.Date ?? default, Filter.Month
        , GetJustifyContent(CSS.Start), ""
        , m.SpanText, Filter.Month.Value, m.EventSort)))

    .OrderBy(r => r.Date)
    .ThenBy(r => r.Sort1)
    .ThenBy(r => r.Sort2)
    .ToList();

  protected static string GetJustifyContent(string css)
  {
    return $"justify-content-{css}";
  }

  public  List<Enums.EventRecord>? GetParasha(int year, int month)
  {
    return ParashaEnums.Triennial.List
      .Where(t => t.Date.Year == year && t.Date.Month == month)
      .SelectMany(t => new List<Enums.EventRecord>
    {
      new Enums.EventRecord(t.DateOnly, Filter.Parasha
      , GetJustifyContent(CSS.Center), ""
      , t.SpanIconAndTextTorah, Filter.Parasha.Value, 0)
    })
      .Where(r => r != null)
      .OrderBy(r => r.Date)
      .ThenBy(r => r.Sort1)
      .ThenBy(r => r.Sort2)
      .ToList();
  }
}

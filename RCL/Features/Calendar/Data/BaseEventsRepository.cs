using Microsoft.AspNetCore.Components;
using RCL.Features.Calendar.Enums;
using CSS = RCL.Features.Calendar.Constants.CSS.JustifyContentSuffix;
using ParashaEnums = RCL.Features.Parasha.Enums;


namespace RCL.Features.Calendar.Data;

public abstract class BaseEventsRepository : ComponentBase
{
  protected List<Enums.EventRecord>? BaseData { get; } = 
    Enums.EventFeast.List
      .Where(f => f.IsSplit)
      .Select(f => new Enums.EventRecord(f.ErevDate, Filter.Feast, GetJustifyContent(CSS.End), f.SpanIcon, f.Category.Value, f.EventSort))
      .Concat(Enums.EventFeast.List
      .Where(f => f.IsSplit)
      .Select(f => new Enums.EventRecord(f.DayTimeDate, Filter.Feast, GetJustifyContent(CSS.Start), f.SpanText, f.Category.Value, f.EventSort)))

      .Concat(Enums.EventFeast.List
      .Where(f => !f.IsSplit)
      .Select(f => new Enums.EventRecord(f.DayTimeDate, Filter.Feast, GetJustifyContent(CSS.Center), f.SpanIconAndText, f.Category.Value, f.EventSort)))

      .Concat(Enums.EventFeast.List
      .Where(f => !f.IsSplit)
      .Select(f => new Enums.EventRecord(f.DayTimeDate.AddDays(1), Filter.Feast, GetJustifyContent(CSS.Center), f.SpanBlank, f.Category.Value, f.EventSort)))

      .Concat(Enums.EventSeason.List
      .Select(s => new Enums.EventRecord(s.Date, Filter.Season, GetJustifyContent(CSS.Center), s.SpanIconAndText, Filter.Season.Value, s.EventSort)))

      .Concat(Enums.EventMoon.List
      .Where(m => m.ErevDate is not null)
      .Select(m => new Enums.EventRecord(m.Date.HasValue ? m.Date.Value.AddDays(-1) : default, Filter.Month, GetJustifyContent(CSS.End), m.SpanIcon, Filter.Month.Value, m.EventSort)))

      .Concat(Enums.EventMoon.List
      .Where(m => m.DayTimeDate is not null)
      .Select(m => new Enums.EventRecord(m.Date ?? default, Filter.Month, GetJustifyContent(CSS.Start), m.SpanText, Filter.Month.Value, m.EventSort)))

    .OrderBy(r => r.Date)
    .ThenBy(r => r.Sort1)
    .ThenBy(r => r.Sort2)
    .ToList();

  protected static string GetJustifyContent(string css)
  {
    return $"justify-content-{css}";
  }

  public  List<Enums.EventRecord>? GetParash(int year, int month)
  {
    return ParashaEnums.Triennial.List
      .Where(t => t.Date.Year == year && t.Date.Month == month)
      .SelectMany(t => new List<Enums.EventRecord>
    {
      new Enums.EventRecord(t.DateOnly, Filter.Parasha, GetJustifyContent(CSS.Center), t.SpanIconAndTextTorah, Filter.Parasha.Value, 0)
    })
      .Where(r => r != null)
      .OrderBy(r => r.Date)
      .ThenBy(r => r.Sort1)
      .ThenBy(r => r.Sort2)
      .ToList();
  }
  /*
      new Enums.EventRecord(t.DateOnly, Filter.Parasha, GetJustifyContent(CSS.Center), t.SpanIconAndTextTorah, Filter.Parasha.Value, 0),
        t.HaftorahVerses != null ? new Enums.EventRecord(t.DateOnly, Filter.Parasha, GetJustifyContent(CSS.Center), t.HaftorahAbrv, Filter.Parasha.Value, 0) : null,
        t.BritVerses != null ? new Enums.EventRecord(t.DateOnly, Filter.Parasha, GetJustifyContent(CSS.Center), t.BritAbrv, Filter.Parasha.Value, 0) : null

   */
}

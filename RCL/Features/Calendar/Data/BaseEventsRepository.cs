using Microsoft.AspNetCore.Components;
using RCL.Features.Calendar.Enums;
using CSS = RCL.Features.Calendar.Constants.CSS.JustifyContentSuffix;

namespace RCL.Features.Calendar.Data;

public abstract class BaseEventsRepository : ComponentBase
{
  protected List<Enums.EventRecord>? BaseData { get; } = Enums.EventFeast.List
    .Where(f => f.IsSplit)
    .Select(f => new Enums.EventRecord(f.ErevDate, DateType.Feast, GetJustifyContent(CSS.End), f.SpanIcon, f.Category.Value, f.EventSort))
    .Concat(Enums.EventFeast.List
      .Where(f => f.IsSplit)
      .Select(f => new Enums.EventRecord(f.DayTimeDate, DateType.Feast, GetJustifyContent(CSS.Start), f.SpanText, f.Category.Value, f.EventSort)))

    .Concat(Enums.EventFeast.List
      .Where(f => !f.IsSplit)
      .Select(f => new Enums.EventRecord(f.DayTimeDate, DateType.Feast, GetJustifyContent(CSS.Center), f.SpanIconAndText, f.Category.Value, f.EventSort)))

    .Concat(Enums.EventFeast.List
      .Where(f => !f.IsSplit)
      .Select(f => new Enums.EventRecord(f.DayTimeDate.AddDays(1), DateType.Feast, GetJustifyContent(CSS.Center), f.SpanBlank, f.Category.Value, f.EventSort)))

    .Concat(Enums.EventSeason.List
      .Select(s => new Enums.EventRecord(s.Date, DateType.Season, GetJustifyContent(CSS.Center), s.SpanIconAndText, DateType.Season.Value, s.EventSort)))

    .Concat(Enums.EventMoon.List
      .Where(m => m.ErevDate is not null)
      .Select(m => new Enums.EventRecord(m.Date.HasValue ? m.Date.Value.AddDays(-1) : default, DateType.Month, GetJustifyContent(CSS.End), m.SpanIcon, DateType.Month.Value, m.EventSort)))

    .Concat(Enums.EventMoon.List
      .Where(m => m.DayTimeDate is not null)
      .Select(m => new Enums.EventRecord(m.Date ?? default, DateType.Month, GetJustifyContent(CSS.Start), m.SpanText, DateType.Month.Value, m.EventSort)))

    .OrderBy(r => r.Date)
    .ThenBy(r => r.Sort1)
    .ThenBy(r => r.Sort2)
    .ToList();

  protected static string GetJustifyContent(string css)
  {
    return $"justify-content-{css}";
  }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Microsoft.AspNetCore.Components;
using RCL.Features.Calendar.Data;

namespace RCL.Features.Calendar.Enums;

public static class ProjectionExtensions
{
  /* 
  Deprecated: not used
  
  public static List<DayCellRecord> ProjectToLunarMonth(this IEnumerable<LunarMonth> items, bool isErev, MarkupString markupString, DateOnly dateOnly)
  {
    //var MarkupString = isErev ? item.Date.AddDays(-1) : item.Date;
    return items
        .Select(item =>
        {
          return new DayCellRecord(dateOnly, DateType.Month, isErev, markupString); 
        }).ToList();
  }

  public static List<DayCellRecord> ProjectToSeason(this IEnumerable<Season> items, bool isErev)
  {
    return items
        .Select(item =>
        {
          var dateTime = isErev ? item.Range.Min.AddDays(-1) : item.Range.Min;
          return new DayCellRecord(
              DateOnly.FromDateTime(dateTime),
              DateType.Season,
              isErev,
              item.CalendarDayHtml);
        })
        .ToList();
  }

  
  // Hanukkah: generate 1..8 days starting at Hanukkah date (if present)
  public static List<DayCellRecord> ProjectToHanukkah(this IEnumerable<FeastDay> items, bool isErev)
  {
    var han = items.FirstOrDefault(w => w.Value == FeastDay.Hanukkah);
    if (han is null) { return new List<DayCellRecord>(); }

    var baseDate = DateOnly.FromDateTime(isErev ? han.Date.AddDays(-1) : han.Date);
    var list = new List<DayCellRecord>(8);
    string candle = "🕯️";
    for (int i = 1; i < 9; i++)
    {
      list.Add(new DayCellRecord(
          baseDate.AddDays(i - 1),
          DateType.Feast,
          isErev,
          (MarkupString)$"<span class='badge bg-primary fs-5 text-white' title='ToDo: add a tooltip and onclick()'><i class='fas fa-hanukiah'></i> {i} {candle}</span>"));
    }
    return list;
  }

 // Omer: generate 1..49 days starting at Passover
  public static List<DayCellRecord> ProjectToOmer(this IEnumerable<FeastDay> items, bool isErev)
  {
    var passover = items.FirstOrDefault(w => w.Value == FeastDay.Passover);
    if (passover is null) { return new List<DayCellRecord>(); }

    var baseDate = DateOnly.FromDateTime(isErev ? passover.Date.AddDays(-1) : passover.Date);
    var list = new List<DayCellRecord>(49);
    for (int i = 1; i < 50; i++)
    {
      list.Add(new DayCellRecord(
          baseDate.AddDays(i - 1),
          DateType.Feast,
          isErev,
          (MarkupString)$"<span class='badge bg-warning fs-5 text-black-50'><i class='fas fa-calendar-day'></i>&nbsp;{i}</span>&nbsp;&nbsp;&nbsp;<span class='badge bg-warning text-black hebrew16'>{OmerGematriaDictionary.GetHebrew(i)} </span>"));
    }
    return list;
  }



  // Exclude Hanukkah and Passover
  // Include Purim, Unleavened Bread, First Fruits, Weeks, Trumpets, Yom Kippur, Tabernacles
  public static List<DayCellRecord> ProjectToFeastDayNoHanukkah(this IEnumerable<FeastDay> items, bool isErev)
  {
    return items
        .Where(w => w.Value != FeastDay.Hanukkah & w.Value != FeastDay.Passover)
        .Select(item =>
        {
          var date = isErev ? item.Date.AddDays(-1) : item.Date;
          return new DayCellRecord(
              DateOnly.FromDateTime(date),
              DateType.Feast,
              isErev,
              item.CalendarDayHtml);
        })
        .ToList();
  }

 
  public static List<DayCellRecord> ProjectToUnleavenedBread(this IEnumerable<FeastDayDetail> items)
  {
    //, bool isErev
    //var unleavenedBread = items.FirstOrDefault(w => w.Value == FeastDay.Passover);
    //var baseDate = DateOnly.FromDateTime(isErev ? unleavenedBread.Date.AddDays(-1) : passover.Date);
    return items
        .Where(w => w.Value == FeastDayDetail.UnleavenedBreadDay1 | w.Value == FeastDayDetail.UnleavenedBreadDay7)
        .Select(item =>
        {
          var parentDate = FeastDay.FromValue(item.ParentFeastDayId).Date;
          return new DayCellRecord(
                    DateOnly.FromDateTime(parentDate.AddDays(item.AddDays)),
                    DateType.Feast,
                    true,
                    item.CalendarDayHtml);
        })
        .ToList();
  }


  public static List<DayCellRecord> ProjectToPassover(this IEnumerable<FeastDayDetail> items)
  {
    //, bool isErev
    //var unleavenedBread = items.FirstOrDefault(w => w.Value == FeastDay.Passover);
    //var baseDate = DateOnly.FromDateTime(isErev ? unleavenedBread.Date.AddDays(-1) : passover.Date);
    return items
        .Where(w => w.Value == FeastDayDetail.UnleavenedBreadDay1 | w.Value == FeastDayDetail.UnleavenedBreadDay7)
        .Select(item =>
        {
          var parentDate = FeastDay.FromValue(item.ParentFeastDayId).Date;
          return new DayCellRecord(
                    DateOnly.FromDateTime(parentDate.AddDays(item.AddDays)),
                    DateType.Feast,
                    true,
                    item.CalendarDayHtml);
        })
        .ToList();
  }

  */

  /* 
    FeastDayDetail: project details relative to their parent feast day
     PassoverDay, UnleavenedBreadDay 1 & 7; TrumpetsErev; YomKippurErev; Sukkot: Day0, LastGreatDay & EndsAtSundown = 8;
      I don't think I need to consider IsErev here, since the offsets are already baked into FeastDayDetail.AddDays

  FeastDayDetail 1-8
  - 1-3
    - 1 is Passover and it's special in that it starts in the afternoon
    - 2 & 3, Matzah 1st & 7th I need to get from FeastDayDetail
  - 4-5 is just Erev of Trumpets & YomKippur I can ignore those because they are already handled above
  - 6-8
    - 6 is Sukkot Day 0 is Preparation Day, it's special in that it starts in the afternoon 
    - 7 is Sukkot Day 1 (starts at sundown of previous day)
    - 8 is Sukkot Last Great Day (starts at sundown of previous day)

  For Passover you could have the Passover lamb; 10th day select the lamb,  14th day slaughter it, 15th day eat it  
   */

  /*
  public static List<DayCellRecord> ProjectToFeastDayDetail(this IEnumerable<FeastDayDetail> items)
  {
    return items
        .Select(item =>
        {
          var parentDate = FeastDay.FromValue(item.ParentFeastDayId).Date;
          return new DayCellRecord(
                    DateOnly.FromDateTime(parentDate.AddDays(item.AddDays)),
                    DateType.Feast,
                    true,
                    item.CalendarDayHtml);
        })
        .ToList();
  }
   */
}
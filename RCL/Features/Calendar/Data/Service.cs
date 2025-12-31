using RCL.Features.Calendar.Enums;

namespace RCL.Features.Calendar.Data;


public interface IService
{
  List<DtoCombined> GetData();
  DateOnlyRange? GetDateRange();
}


public class Service () : IService
{

	public List<DtoCombined> GetData()
	{
    //return new List<DtoCombined>(); 
    List<Dto> LunarMonthList = new();
    string moon = "🌙";
    foreach (var item in LunarMonth.List.ToList())
    {
      LunarMonthList.Add(new Dto(DateOnly.FromDateTime(item.Date), DateType.Month, $"{moon} {item.FullName}"));
    }

    List<Dto> SeasonList = new();
    foreach (var item in Season.List.ToList())
    {
      SeasonList.Add(new Dto(DateOnly.FromDateTime(item.Range.Min), DateType.Season, $"{item.Emoji} {item.Name}"));
    }

    // No Hanukkah
    List<Dto> NoHanukkahList = new();
    foreach (var item in FeastDay.List
      .Where(w => w.Value != FeastDay.Hanukkah & w.Value != FeastDay.Passover)
      .ToList())
    {
      NoHanukkahList.Add(new Dto(DateOnly.FromDateTime(item.Date), DateType.Feast, item.CalendarTitle));
    }

    List<Dto> OmerList = new();
    int i;
    for (i = 1; i < 50; i++)
    {
      OmerList.Add(new Dto(DateOnly.FromDateTime(FeastDay.FromValue(FeastDay.Passover).Date).AddDays(i - 1), DateType.Feast, $"Omer {i} {OmerGematriaDictionary.GetHebrew(i)}"));
    }

    List<Dto> HanukkahList = new();
    for (i = 1; i < 9; i++)
    {
      HanukkahList.Add(new Dto(DateOnly.FromDateTime(FeastDay.Hanukkah.Date).AddDays(i - 1), DateType.Feast, $"Hanukkah Day {i}"));
    }

    List<Dto> FeastDayDetailList = new();
    DateTime date;
    foreach (var item in FeastDayDetail.List.ToList())
    {
      date = FeastDay.FromValue(item.ParentFeastDayId).Date;
      FeastDayDetailList.Add(new Dto(DateOnly.FromDateTime(date.AddDays(item.AddDays)), DateType.Feast, item.Title));
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
          x.DateType,
          x.Description
        ));
      })
      .ToList();

    return _DtoCombinedList;
  }

  //private DateOnlyRange? _dateOnlyRange;

  public DateOnlyRange GetDateRange()
  {
    List<DtoCombined>? _DtoCombinedList = GetData();  

    DateOnlyRange? _dateOnlyRange;

    if (_DtoCombinedList is not null && _DtoCombinedList.Any())
    {
      var minDate = _DtoCombinedList.Min(d => d.Date);
      var maxDate = _DtoCombinedList.Max(d => d.Date);
      _dateOnlyRange = new DateOnlyRange(minDate, maxDate);
      return _dateOnlyRange;
    }
    else
    {
      return null;
    }

    

    /* 
    if (_dateOnlyRange is not null) {  return _dateOnlyRange;  }
        


    */
  }
}
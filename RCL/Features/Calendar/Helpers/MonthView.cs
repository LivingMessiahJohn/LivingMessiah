namespace RCL.Features.Calendar.Helpers;

public static class MonthView
{
  public static List<DateOnly> GetDates(DateOnly seedDate)
  {
    //Logger!.LogDebug("{Method}, CurrentDateOnly: {CurrentDateOnly}", nameof(OnParametersSet), CurrentDateOnly.ToShortDateString());
    int year = seedDate.Year;
    int month = seedDate.Month;
    var firstDay = new DateOnly(year, month, 1);
    var lastDay = new DateOnly(year, month, DateTime.DaysInMonth(year, month));
    var start = firstDay.AddDays(-(int)firstDay.DayOfWeek); // Start from Sunday
    var end = lastDay.AddDays(6 - (int)lastDay.DayOfWeek); // End on Saturday
    var dates = new List<DateOnly>();
    for (var date = start; date <= end; date = date.AddDays(1))
    {
      dates.Add(date);
    }
    return dates;
  }
    /*
    try
    {

    }
    catch (Exception ex)
    {
      // Warning: The variable 'ex' is declared but never used
      //Logger!.LogError(ex, "{Method} failed for CurrentDate: {CurrentDate}", nameof(OnParametersSet), CurrentDate.ToShortDateString());
    }
    */
  }

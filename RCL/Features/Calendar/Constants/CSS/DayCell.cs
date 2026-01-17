namespace RCL.Features.Calendar.Constants.CSS;

public static class DayCell
{
  public const string Today = "today";
  public const string CurrentMonth = "current-month";
  public const string OtherMonth = "other-month";
  public const string SpecialDate = "special-date";
}

//ToDo: this isn't being used
public class DayCellHelper
{
  public static string GetCss(DateOnly currentDate, DateOnly currentRowColDate)
  {

    if (currentDate.Month == currentRowColDate.Month)
    {
      if (currentDate == currentRowColDate)
      {
        return DayCell.Today;
      }
      else
      {
        return DayCell.CurrentMonth;
      }
    }
    else
    {
      return DayCell.OtherMonth;
    }
  }

}

/*

calendar-day *

Describes 
- box background-color etc, and 
- day number: position, if current highlight back ground
- rows

- if (Date.Month == MonthId)
  - if (Date == CurrentDate): today
  - else 
    - if specialDate(s): special-date
    - else: current-month
- else: `other-month`     
 */
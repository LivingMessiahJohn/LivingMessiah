namespace RCL.Features.Calendar.Enums;

using ConstantsCSS = RCL.Features.Calendar.Constants.CSS.DayCell;

public static class EventFeastHelper
{
  public static string GetCSSFeastBorder(DateOnly date)
  {
    string s = string.Empty;
      if (Enums.EventFeast.List.FirstOrDefault(f => f.FeastDateRange.Min <= date && f.FeastDateRange.Max >= date) is not null)
      {
        return ConstantsCSS.FeastBorder;
      }
    return s;
  }
}

namespace LivingMessiahAdmin.Features.KeyDates.Constants;

public static class Omer
{
	//public static System.DateTime Date { get; set; } = System.DateTime.Parse(Dates._12_Passover).AddDays(CalendarEnumFeastDayDetail.OmerStart.AddDays);
	//public static System.DateTime Date { get; set; } =
	//	System.DateTime.Parse(Dates._12_Passover).AddDays(CalendarEnumFeastDay.Passover.AddDays);

	public static System.DateTime Date { get; set; } = Enums.FeastDay.Passover.Date.AddDays(2);
}

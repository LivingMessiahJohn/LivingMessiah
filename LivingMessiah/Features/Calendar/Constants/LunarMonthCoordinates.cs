namespace LivingMessiah.Features.Calendar.Constants;

public static class LunarMonthCoordinates
{
	public static readonly CoordinateRecord HeshvanPrevGregYrMonth = new(TimeOnly.Parse("16:24"), "112°", TimeOnly.Parse("04:26"), "244°");
	public static readonly CoordinateRecord KislevPrevGregYrMonth = new(TimeOnly.Parse("14:10"), "82°", TimeOnly.Parse("02:13"), "274°");
	public static readonly CoordinateRecord TevetPrevGregYrMonth = new(TimeOnly.Parse("09:15"), "119°", TimeOnly.Parse("19:27"), "243°");
	public static readonly CoordinateRecord Shevat = new(TimeOnly.Parse("08:26"), "109°", TimeOnly.Parse("19:30"), "254°");
	public static readonly CoordinateRecord Adar = new(TimeOnly.Parse("07:56"), "88°", TimeOnly.Parse("20:39"), "276°");
	public static readonly CoordinateRecord Adar2 = Adar; // FN2
	public static readonly CoordinateRecord Nissan = new(TimeOnly.Parse("06:55"), "75°", TimeOnly.Parse("20:41"), "289°");
	public static readonly CoordinateRecord Iyar = new(TimeOnly.Parse("06:50"), "59°", TimeOnly.Parse("22:01"), "303°");
	public static readonly CoordinateRecord Sivan = new(TimeOnly.Parse("06:28"), "55°", TimeOnly.Parse("21:53"), "304°");
	public static readonly CoordinateRecord Tammuz = new(TimeOnly.Parse("07:34"), "63°", TimeOnly.Parse("22:02"), "294°");
	public static readonly CoordinateRecord Av = new(TimeOnly.Parse("07:28"), "73°", TimeOnly.Parse("21:02"), "284°");
	public static readonly CoordinateRecord Elul = new(TimeOnly.Parse("08:15"), "92°", TimeOnly.Parse("20:20"), "265°");
	public static readonly CoordinateRecord Tishri = new(TimeOnly.Parse("08:00"), "103°", TimeOnly.Parse("19:13"), "254°");
	public static readonly CoordinateRecord Heshvan = new(TimeOnly.Parse("08:46"), "118°", TimeOnly.Parse("18:47"), "240°");
	public static readonly CoordinateRecord Kislev = new(TimeOnly.Parse("08:35"), "124°", TimeOnly.Parse("18:11"), "236°");
	public static readonly CoordinateRecord Tevet = new(TimeOnly.Parse("09:02"), "122°", TimeOnly.Parse("18:52"), "239°");
}

/* 
### Footnotes
- FN2: I want all CoordinateRecords to be defined, so if there is no Adar II then just set it to the same as Adar
	- therefore when the year is a _pregnant_ year then you need to populate Adar2 with the correct values
*/


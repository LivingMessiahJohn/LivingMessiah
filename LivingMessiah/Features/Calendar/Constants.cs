namespace LivingMessiah.Features.Calendar;


public static class CalendarDates
{
	// Replaces IRepository ! Task<List<CalendarQuery>> GetCalendarQuery(int yearId);
	public static List<CalendarQuery> Get() =>
	[
 		new() { Date = new DateTime(2024, 10, 31), Detail = 1, DateTypeId = 1, EnumId = 1, Description = "Heshvan | Bul" },
		new() { Date = new DateTime(2024, 11, 30), Detail = 2, DateTypeId = 1, EnumId = 2, Description = "Kislev | 9th" },
		new() { Date = new DateTime(2024, 12, 20), Detail = 4, DateTypeId = 3, EnumId = 1, Description = "Winter" },
		new() { Date = new DateTime(2024, 12, 25), Detail = 3, DateTypeId = 2, EnumId = 1, Description = "Hanukkah" },
		new() { Date = new DateTime(2025, 1, 1), Detail = 5, DateTypeId = 1, EnumId = 3, Description = "Tevet | 10th" },
		new() { Date = new DateTime(2025, 1, 30), Detail = 6, DateTypeId = 1, EnumId = 4, Description = "Shevat | 11th" },
		new() { Date = new DateTime(2025, 3, 1), Detail = 7, DateTypeId = 1, EnumId = 5, Description = "Adar | 12th" },
		new() { Date = new DateTime(2025, 3, 14), Detail = 9, DateTypeId = 2, EnumId = 2, Description = "Purim" },
		new() { Date = new DateTime(2025, 3, 17), Detail = 8, DateTypeId = 3, EnumId = 2, Description = "Spring" },
		new() { Date = new DateTime(2025, 3, 30), Detail = 10, DateTypeId = 1, EnumId = 6, Description = "Nissan | Abib" },
		new() { Date = new DateTime(2025, 4, 13), Detail = 11, DateTypeId = 2, EnumId = 3, Description = "Passover" },
 		new() { Date = new DateTime(2025, 4, 29), Detail = 12, DateTypeId = 1, EnumId = 7, Description = "Iyar | Ziv" },
		new() { Date = new DateTime(2025, 5, 28), Detail = 13, DateTypeId = 1, EnumId = 8, Description = "Sivan | 3rd" },
		new() { Date = new DateTime(2025, 6, 2), Detail = 14, DateTypeId = 2, EnumId = 4, Description = "Weeks" },
		new() { Date = new DateTime(2025, 6, 20), Detail = 15, DateTypeId = 3, EnumId = 3, Description = "Summer" },
		new() { Date = new DateTime(2025, 6, 27), Detail = 16, DateTypeId = 1, EnumId = 9, Description = "Tammuz | 4th" },
		new() { Date = new DateTime(2025, 7, 26), Detail = 17, DateTypeId = 1, EnumId = 10, Description = "Av | 5th" },
		new() { Date = new DateTime(2025, 8, 25), Detail = 18, DateTypeId = 1, EnumId = 11, Description = "Elul | 6th" },
		new() { Date = new DateTime(2025, 9, 23), Detail = 19, DateTypeId = 1, EnumId = 12, Description = "Tishri | Ethanim" },
		new() { Date = new DateTime(2025, 9, 23), Detail = 21, DateTypeId = 2, EnumId = 5, Description = "Trumpets" },
		new() { Date = new DateTime(2025, 9, 26), Detail = 20, DateTypeId = 3, EnumId = 4, Description = "Fall" },
		new() { Date = new DateTime(2025, 10, 2), Detail = 22, DateTypeId = 2, EnumId = 6, Description = "Yom Kippur" },
		new() { Date = new DateTime(2025, 10, 6), Detail = 23, DateTypeId = 2, EnumId = 7, Description = "Tabernacles" },
		new() { Date = new DateTime(2025, 10, 23), Detail = 24, DateTypeId = 1, EnumId = 13, Description = "Heshvan | Bul" },
		new() { Date = new DateTime(2025, 11, 21), Detail = 25, DateTypeId = 1, EnumId = 14, Description = "Kislev | 9th" },
		new() { Date = new DateTime(2025, 12, 21), Detail = 26, DateTypeId = 1, EnumId = 15, Description = "Tevet | 10th" },
	];

/*
DECLARE @yearId int=2025
SELECT
--	Date, Detail, DateTypeId, EnumId, Description
    CONCAT(
        'new() { ',
        'Date = new DateTime(', YEAR(Date), ', ', MONTH(Date), ', ', DAY(Date), '), ',
        'Detail = ', Detail, ', ',
        'DateTypeId = ', DateTypeId, ', ',
        'EnumId = ', EnumId, ', ',
        'Description = "', Description, '" },'
    ) AS CodeGen
FROM KeyDate.Calendar
WHERE YearId=@yearId
ORDER BY Date
 */
}

public static class Year
{
	public const string ImageName = "2025";
}



public static class CarouselSettings
{
	public const bool Display = true;
}

public static class LMMCalendar
{
	public const string ImageXSorSM = "lmm-calendar-2024-600-408.jpg";
	public const string Image = "lmm-calendar-2024-1020-693.jpg";
}

// Ignore Spelling: Sor
﻿using Microsoft.AspNetCore.Components;
using LivingMessiah.Enums;

namespace LivingMessiah.Features.Parasha.Enums;

public static class ParashaFacts
{
	public const int FirstParashaId = 1;
	public const int LastParashaId = 155;
}

public static class Constants
{
	public const string BaseUrl = "Parasha";

	public static string PrevNextUrl(Triennial triennial)
	{
		return $"parasha/{triennial.Value}/{BibleBook.FromValue(triennial.TorahVerse.BibleBook).Abrv}_{triennial.TorahVerse.ChapterVerse.Replace("-", "-to-").Replace(":", "-")}";
	}

	public static Triennial? GetCurrentReading()
	{
		Triennial? _reading =
				Triennial.List
				.Where(w => w.Date == Constants.GetNextShabbatDate())
				.SingleOrDefault();

		if (_reading is not null)
		{
			return _reading;
		}
		else
		{
			return null;
		}
	}

	public static string? GetUrl()
	{
		string? url = null;

		try
		{
			Triennial? _reading =
					Triennial.List
					.Where(w => w.Date == Constants.GetNextShabbatDate())
					.SingleOrDefault();

			if (_reading is not null)
			{
				url = _reading.Url;
			}
			else
			{
				Triennial? _defaultReading = Triennial.List.FirstOrDefault();
				if (_defaultReading is not null)
				{
					url = _defaultReading.Url;
				}
				else
				{
					//ToDo: add logging
					Console.WriteLine($"Warning: {nameof(Constants)}!{nameof(GetUrl)}; {nameof(_defaultReading)} is null");
					throw new InvalidOperationException($"{nameof(Constants)}!{nameof(GetUrl)}; {nameof(_defaultReading)} is null");
				}
			}
		}
		catch (Exception ex)
		{
			//ToDo: add logging
			Console.WriteLine($"Exception: {nameof(Constants)}!{nameof(GetUrl)}; ex: {ex}");
			throw;
		}

		return url;
	}

	public static string CurrentReadDateTextFormat(DateTime readDate)
	{
		DateTime compareDate = DateTime.Today;
		if (readDate >= compareDate & readDate <= compareDate.AddDays(6))
		{
			return "text-danger";
		}
		else
		{
			return "";
		}
	}

	//public static DateTime TriennialSeedDate = DateTime.Parse("2021-10-02"); // 2021-09-25 Last date from previous triennial
	public static DateTime TriennialSeedDate = DateTime.Parse("2024-10-26");

	// Not used
	public static DateTime GetUsersUTC()
	{
		DateTime utcNow = DateTime.UtcNow;
		return utcNow; //Console.WriteLine($"The current UTC time is: {utcNow}");
	}

	// Not used
	public static TimeSpan GetUsersUTCOffset()
	{
		TimeZoneInfo localZone = TimeZoneInfo.Local;
		DateTimeOffset localTime = DateTimeOffset.Now;
		TimeSpan utcOffset = localZone.GetUtcOffset(localTime);
		return utcOffset;
	}

	public static MarkupString DaysFromOrToShabbat(DateTime dt)
	{
		TimeSpan timeSpan = dt - GetNextShabbatDate();
		int days = timeSpan.Days;
		if (days < 0)
		{
			return (MarkupString)$" <b>{Math.Abs(days)}</b> days <b class='text-danger'>before</b> next Shabbat";
		}
		else
		{
			if (days > 0)
			{
				return (MarkupString)$" <b>{Math.Abs(days)}</b> days <b class='text-primary'>after</b> next Shabbat";
			}
			else
			{
				return (MarkupString)$" today is Shabbat! <b class='text-success fs-5'>Shabbat Shalom!</b>";
			}
		}
	}

	// ToDo: probable need to remove `bool OverRideWithSaturday6PM` and `GetNextArizonaSaturday6PM` and write some unit tests
	public static DateTime GetNextShabbatDate(bool OverRideWithSaturday6PM = false)
	{
		TimeZoneInfo arizonaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Phoenix");
		DateTime utcNow;

		if (OverRideWithSaturday6PM)
		{
			utcNow = GetNextArizonaSaturday6PM();
		}
		else
		{
			utcNow = DateTime.UtcNow;
		}

		DateTime arizonaNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, arizonaTimeZone);

		int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)arizonaNow.DayOfWeek + 7) % 7;
		DateTime nextSaturday = arizonaNow.AddDays(daysUntilSaturday).Date;

		return nextSaturday;
	}

	// See comment above
	public static DateTime GetNextArizonaSaturday6PM()
	{
		DateTime utcNow = DateTime.UtcNow;

		TimeZoneInfo arizonaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Phoenix");
		DateTime arizonaNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, arizonaTimeZone);

		int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)arizonaNow.DayOfWeek + 7) % 7;
		DateTime nextSaturday = arizonaNow.AddDays(daysUntilSaturday).Date;

		DateTime nextSaturdayAt6PM = nextSaturday.AddHours(18);
		return nextSaturdayAt6PM;
	}

	public static string HoursAndDaysUntilNextShabbat()
	{
		DateTime startDate0 = DateTime.UtcNow;
		TimeZoneInfo arizonaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Phoenix");
		DateTime startDate1 = TimeZoneInfo.ConvertTimeFromUtc(startDate0, arizonaTimeZone);

		TimeSpan difference = GetNextShabbatDate() - startDate1;
		return string.Format("{0:%d} days and {0:%h} hours", difference);
	}

	public static DateTime GetNextArizonaSunday1AM()
	{
		DateTime utcNow = DateTime.UtcNow;

		TimeZoneInfo arizonaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Phoenix");
		DateTime arizonaNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, arizonaTimeZone);

		int daysUntilSunday = ((int)DayOfWeek.Sunday - (int)arizonaNow.DayOfWeek + 7) % 7;
		DateTime nextSunday = arizonaNow.AddDays(daysUntilSunday).Date;

		DateTime nextSundayAt1AM = nextSunday.AddHours(1);
		return nextSundayAt1AM;
	}

	public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
	{
		// The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
		int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
		return start.AddDays(daysToAdd);
	}
}
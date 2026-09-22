using Ardalis.SmartEnum;
using RCL.Constants;
using RCL.Features.Calendar.Constants;

namespace RCL.Features.Sukkot.Enums;

public abstract class DailyEvent : SmartEnum<DailyEvent>
{
	#region Id's
	private static class Id
	{
		internal const int PrePrepDay = 1;  // occurs if PrepDay is Saturday / Shabbat
		internal const int PrepDay = 2;
		internal const int Day1 = 3;
		internal const int Day2 = 4;
		internal const int Day3 = 5;
		internal const int Day4 = 6;
		internal const int Day5 = 7;
		internal const int Day6 = 8;
		internal const int Day7 = 9;
		internal const int Day8 = 10;
		internal const int CampCleanUpDay = 11;
		internal const int PostCampCleanUpDay = 12; // occurs if CampCleanUpDay is Saturday / Shabbat
	}
	#endregion

	#region  Declared Public Instances
	public static readonly DailyEvent PrePrepDay = new PrePrepDaySE();
	public static readonly DailyEvent PrepDay = new PrepDaySE();
	public static readonly DailyEvent Day1 = new Day1SE();
	public static readonly DailyEvent Day2 = new Day2SE();
	public static readonly DailyEvent Day3 = new Day3SE();
	public static readonly DailyEvent Day4 = new Day4SE();
	public static readonly DailyEvent Day5 = new Day5SE();
	public static readonly DailyEvent Day6 = new Day6SE();
	public static readonly DailyEvent Day7 = new Day7SE();
	public static readonly DailyEvent Day8 = new Day8SE();
	public static readonly DailyEvent CampCleanUpDay = new CampCleanUpDaySE();
	public static readonly DailyEvent PostCampCleanUpDay = new PostCampCleanUpDaySE();

	// SE=SmartEnum
	#endregion

	private DailyEvent(string name, int value) : base(name, value) { }

	#region Extra Fields
	public abstract string Title { get; }
	public abstract string MarkdownFileName { get; }
	public abstract DateOnly Date { get; }
	public abstract bool Include { get; }

	/// <summary>
	/// 1-based PocketMod page. Page 1 is the lead-in days, pages 2–7 are
	/// single feast days, page 8 is the close-out days.
	/// </summary>
	public abstract int PrintPage { get; }
	#endregion

	#region Extra Properties
	public string DateLabel => Date.ToString(DateFormat.ddd_mm_dd);

	public static IReadOnlyList<DailyEvent> Included => [.. List.Where(e => e.Include)];
	#endregion

	public static bool TryFromMarkdownFileName(string? fileName, out DailyEvent dailyEvent)
	{
		dailyEvent = null!;
		if (string.IsNullOrWhiteSpace(fileName))
			return false;

		string leaf = Path.GetFileName(fileName);
		if (string.IsNullOrEmpty(leaf))
			return false;

		dailyEvent = List.FirstOrDefault(e =>
			string.Equals(e.MarkdownFileName, leaf, StringComparison.OrdinalIgnoreCase))!;
		return dailyEvent is not null;
	}

	#region Private Instantiation
	private sealed class PrePrepDaySE : DailyEvent
	{
		public PrePrepDaySE() : base($"{nameof(Id.PrePrepDay)}", Id.PrePrepDay) { }
		public override string Title => "Pre Prep Day";
		public override string MarkdownFileName => "a-pre-prep-day.md";
		public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(-2);
		public override bool Include => PrepDay.Date.DayOfWeek == DayOfWeek.Saturday;
		public override int PrintPage => 1;
	}

	private sealed class PrepDaySE : DailyEvent
	{
		public PrepDaySE() : base($"{nameof(Id.PrepDay)}", Id.PrepDay) { }
		public override string Title => "Prep Day";
		public override string MarkdownFileName => "b-prep-day.md";
		public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(-1);
		public override bool Include => true;
		public override int PrintPage => 1;
	}

	private sealed class Day1SE : DailyEvent
	{
		public Day1SE() : base($"{nameof(Id.Day1)}", Id.Day1) { }
		public override string Title => "Day 1";
		public override string MarkdownFileName => "c-day-1.md";
		public override DateOnly Date => FeastDayDates.Tabernacles;
		public override bool Include => true;
		public override int PrintPage => 1;
	}

	private sealed class Day2SE : DailyEvent
	{
		public Day2SE() : base($"{nameof(Id.Day2)}", Id.Day2) { }
		public override string Title => "Day 2";
		public override string MarkdownFileName => "d-day-2.md";
		public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(1);
		public override bool Include => true;
		public override int PrintPage => 2;
	}

	private sealed class Day3SE : DailyEvent
	{
		public Day3SE() : base($"{nameof(Id.Day3)}", Id.Day3) { }
		public override string Title => "Day 3";
		public override string MarkdownFileName => "e-day-3.md";
		public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(2);
		public override bool Include => true;
		public override int PrintPage => 3;
	}

	private sealed class Day4SE : DailyEvent
	{
		public Day4SE() : base($"{nameof(Id.Day4)}", Id.Day4) { }
		public override string Title => "Day 4";
		public override string MarkdownFileName => "f-day-4.md";
		public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(3);
		public override bool Include => true;
		public override int PrintPage => 4;
	}

	private sealed class Day5SE : DailyEvent
	{
		public Day5SE() : base($"{nameof(Id.Day5)}", Id.Day5) { }
		public override string Title => "Day 5";
		public override string MarkdownFileName => "g-day-5.md";
		public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(4);
		public override bool Include => true;
		public override int PrintPage => 5;
	}

	private sealed class Day6SE : DailyEvent
	{
		public Day6SE() : base($"{nameof(Id.Day6)}", Id.Day6) { }
		public override string Title => "Day 6";
		public override string MarkdownFileName => "h-day-6.md";
		public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(5);
		public override bool Include => true;
		public override int PrintPage => 6;
	}

	private sealed class Day7SE : DailyEvent
	{
		public Day7SE() : base($"{nameof(Id.Day7)}", Id.Day7) { }
		public override string Title => "Day 7";
		public override string MarkdownFileName => "i-day-7.md";
		public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(6);
		public override bool Include => true;
		public override int PrintPage => 7;
	}

	private sealed class Day8SE : DailyEvent
	{
		public Day8SE() : base($"{nameof(Id.Day8)}", Id.Day8) { }
		public override string Title => "Day 8";
		public override string MarkdownFileName => "j-day-8.md";
		public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(7);
		public override bool Include => true;
		public override int PrintPage => 8;
	}

	private sealed class CampCleanUpDaySE : DailyEvent
	{
		public CampCleanUpDaySE() : base($"{nameof(Id.CampCleanUpDay)}", Id.CampCleanUpDay) { }
		public override string Title => "Camp Clean Up Day";
		public override string MarkdownFileName => "k-clean-up-day.md";
		public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(8);
		public override bool Include => true;
		public override int PrintPage => 8;
	}

	private sealed class PostCampCleanUpDaySE : DailyEvent
	{
		public PostCampCleanUpDaySE() : base($"{nameof(Id.PostCampCleanUpDay)}", Id.PostCampCleanUpDay) { }
		public override string Title => "Post Camp Clean Up Day";
		public override string MarkdownFileName => "l-post-clean-up-day.md";
		public override DateOnly Date => FeastDayDates.Tabernacles.AddDays(9);
		public override bool Include => CampCleanUpDay.Date.DayOfWeek == DayOfWeek.Saturday;
		public override int PrintPage => 8;
	}
	#endregion
}

// Ignore Spelling: PrePrep
// Ignore Spelling: CleanUp

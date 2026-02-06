using Ardalis.SmartEnum;
using Microsoft.AspNetCore.Components;
using PlannerDetailEnum = RCL.Features.FeastDayPlanner.Enums.PlannerDetail;
using RCL.Features.Calendar.Constants;

namespace RCL.Features.Calendar.Enums;

public abstract class FeastDay : SmartEnum<FeastDay>
{
	#region Id's
	private static class Id
	{
		internal const int Hanukkah = 1;
		internal const int Purim = 2;
		internal const int Passover = 3;
		internal const int Weeks = 4;
		internal const int Trumpets = 5;
		internal const int YomKippur = 6;
		internal const int Tabernacles = 7;
	}
	#endregion

	#region Declared Public Instances
	public static readonly FeastDay Hanukkah = new HanukkahSE();
	public static readonly FeastDay Purim = new PurimSE();
	public static readonly FeastDay Passover = new PassoverSE();
	public static readonly FeastDay Weeks = new WeeksSE();
	public static readonly FeastDay Trumpets = new TrumpetsSE();
	public static readonly FeastDay YomKippur = new YomKippurSE();
	public static readonly FeastDay Tabernacles = new TabernaclesSE();
	// Note; SE=SmartEnum
	#endregion

	private FeastDay(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Hebrew { get; }              // used by LivingMessiah\Features\FeastDayPlanner\Header.razor
  public abstract string Details { get; }             // used by LivingMessiah\RCL\Features\Calendar\FeastTable.razor
  public abstract string CalendarTitle { get; }
	public abstract string TabTitle { get; }            
  public abstract string PlannerTitle { get; }
	public abstract string Icon { get; }                // used by LivingMessiah\Features\FeastDayPlanner\Header.razor
  public abstract string VerseReferenceCard { get; }
	public abstract bool IsStartOfEdge { get; }         // used by LivingMessiah\Features\FeastDayPlanner\HeaderXsSm.razor
  public abstract bool IsEndOfEdge { get; }           // used by LivingMessiah\Features\FeastDayPlanner\HeaderXsSm.razor

  public abstract bool HasCalendarDetails { get; }
	public abstract DateOnly Date { get; }  
  public abstract DateOnlyRange DateOnlyRange { get; } // Can I get rid of Date and rename this as Date?

  // This is a sanity check based on the idea for some feast days you can determine how many days are in between the dates
  // E.g. There's 9 days between Trumpets and Yom Kippur, so if the difference between those dates is off then one of the dates is wrong.
  public abstract int? DaysFromPrevFeast { get; }
  #endregion


  #region Extra Properties

  public DateOnly ErevDate => Date.AddDays(-1);  
  public DateOnly DayTimeDate => Date;           

  public string RangeFormatted =>
  DateOnlyRange.Min != DateOnlyRange.Max
		? $"{DateOnlyRange.Min.ToShortDateString()} - {DateOnlyRange.Max.ToShortDateString()}"
		: Date.ToShortDateString();


	public DateOnly StartShowingCard => DateOnlyRange.Min.AddDays(-45);

  // Using `Range.Min:dd MMM yyyy HH` instead of DateOnlyRange.Min.ToString("dd MMM yyyy HH")`
  // FormatException: String 'dd MMM yyyy HH' contains parts which are not specific to the DateOnly.
  public string FirstAndLastDates => $"DateRange: {DateOnlyRange.Min:dd MMM yyyy} - {DateOnlyRange.Max:dd MMM yyyy}";

  public MarkupString DayCellHtml => (MarkupString)$"<span class='badge bg-primary fs-6 text-white'><i class='{Icon}'></i> {CalendarTitle}</span>";

  public MarkupString HasCalendarDetailsFormatted => HasCalendarDetails 
    ? (MarkupString)"<mark><b><span>&#10003;</span></b></mark>" 
    : (MarkupString)string.Empty;
  #endregion


  #region Private Instantiation

  private sealed class HanukkahSE : FeastDay
	{
		public HanukkahSE() : base($"{nameof(Id.Hanukkah)}", Id.Hanukkah) { }
		public override string Hebrew => "חֲנֻכָּה";
		public override string Details => "First day of Hanukkah; Date determined by Rabbinic sources";
		public override string CalendarTitle => nameof(Id.Hanukkah);
		public override string TabTitle => $"{nameof(Id.Hanukkah)}";
		public override string PlannerTitle => $"{nameof(Id.Hanukkah)} Week";
		public override string Icon => "fas fa-hanukiah";
		public override string VerseReferenceCard => "Joh_10_22";

		public override bool IsStartOfEdge => true;
		public override bool IsEndOfEdge => false;

		public override bool HasCalendarDetails => false;
		public override int? DaysFromPrevFeast => null;  // This is the beginning of the year
		public override DateOnly Date => FeastDayDates.Hanukkah;
    public override DateOnlyRange DateOnlyRange => new(Date, Date.AddDays(8)); // ToDo: Remove DateOnly.FromDateTime
  } 

  private sealed class PurimSE : FeastDay
	{
		public PurimSE() : base($"{nameof(Id.Purim)}", Id.Purim) { }
		public override string Hebrew => "פוּרִים";
		public override string Details => "Tradition is to read the book of Esther; date determined by Rabbinic sources";
		public override string CalendarTitle => nameof(Id.Purim);
		public override string TabTitle => $"{nameof(Id.Purim)}";
		public override string PlannerTitle => nameof(Id.Purim);
		public override string Icon => "fas fa-dice";  // far fa-square
    public override string VerseReferenceCard => "Est_9_24_32";
		public override bool IsStartOfEdge => false;
		public override bool IsEndOfEdge => false;

		public override bool HasCalendarDetails => false;
		public override int? DaysFromPrevFeast => null;  // Hanukkah comes before and it's to fluid to track, so null

		public override DateOnly Date => FeastDayDates.Purim;
    public override DateOnlyRange DateOnlyRange => new(Date, Date);
  }

	//ToDo, Rename this PassoverWeek(SE)
	private sealed class PassoverSE : FeastDay
	{
		public PassoverSE() : base($"{nameof(Id.Passover)}", Id.Passover) { }
		public override string Hebrew => "פֶּסַח";
		public override string Details => "The Seder Meal is prepared on the 14th of Aviv. As evening starts, the meal is eaten. Also, this becomes the first day of Unleavened bread";
		public override string CalendarTitle => nameof(Id.Passover);
		public override string TabTitle => $"{nameof(Id.Passover)}";
		public override string PlannerTitle => $"{nameof(Id.Passover)} Week";
		public override string Icon => "fas fa-door-open";
		public override string VerseReferenceCard => "Lev_23_04_08";
		public override bool IsStartOfEdge => false;
		public override bool IsEndOfEdge => false;

		public override bool HasCalendarDetails => true;
		public override int? DaysFromPrevFeast => null;  // Purim comes before and it's to fluid to track, so null
		public override DateOnly Date => FeastDayDates.Passover;
    public override DateOnlyRange DateOnlyRange => new(Date, Date);
  }

	private sealed class WeeksSE : FeastDay
	{
		public WeeksSE() : base($"{nameof(Id.Weeks)}", Id.Weeks) { }
		public override string Hebrew => "שָׁבוּעוֹת";
		public override string Details => "The hight sabbath begins the evening before; This is also called Pentecost";
		public override string CalendarTitle => nameof(Id.Weeks);
		public override string TabTitle => $"{nameof(Id.Weeks)}";
		public override string PlannerTitle => nameof(Id.Weeks);
		public override string Icon => "fab fa-creative-commons-zero";
		public override string VerseReferenceCard => "Lev_23_15_21"; //  "Lev_23_10_11_and_15_21";
		public override bool IsStartOfEdge => false;
		public override bool IsEndOfEdge => false;

		public override bool HasCalendarDetails => false;  // this is the only one that isn't true
		public override int? DaysFromPrevFeast => 51;  // Pesach is before and so a hard business rule can be made ... I think ... why isn't it 50?
		public override DateOnly Date => FeastDayDates.Weeks;
    public override DateOnlyRange DateOnlyRange => new(Date, Date);
  }

	private sealed class TrumpetsSE : FeastDay
	{
		public TrumpetsSE() : base($"{nameof(Id.Trumpets)}", Id.Trumpets) { }
		public override string Hebrew => "יוֹם תְּרוּעָה";
		public override string Details => "A high holy day sabbath";
		public override string CalendarTitle => nameof(Id.Trumpets);
		public override string TabTitle => $"{nameof(Id.Trumpets)}";
		public override string PlannerTitle => nameof(Id.Trumpets);
		public override string Icon => "fas fa-bullhorn";
		public override string VerseReferenceCard => "Lev_23_23_25";
		public override bool IsStartOfEdge => false;
		public override bool IsEndOfEdge => false;

		public override bool HasCalendarDetails => true;
		public override int? DaysFromPrevFeast => 113;  // Shavuot / Weeks is before and so a hard business rule can be made ... I think 
		public override DateOnly Date => FeastDayDates.Trumpets;
    public override DateOnlyRange DateOnlyRange => new(Date, Date);  
  }

	private sealed class YomKippurSE : FeastDay
	{
		public YomKippurSE() : base($"{nameof(Id.YomKippur)}", Id.YomKippur) { }
		public override string Hebrew => "יוֹם כִּיפּוּר";
		public override string Details => "In the afternoon we have our Yom Kippur service and break the fast after the sun sets";
		public override string CalendarTitle => "Yom Kippur";
		public override string TabTitle => CalendarTitle;
		public override string PlannerTitle => CalendarTitle;
		public override string Icon => "fas fa-hands-helping";
		public override string VerseReferenceCard => "Lev_23_26_32";
		public override bool IsStartOfEdge => false;
		public override bool IsEndOfEdge => false;

		public override bool HasCalendarDetails => true;
		public override int? DaysFromPrevFeast => 9;  // Trumpets is before and so a hard business rule can be made
		public override DateOnly Date => FeastDayDates.YomKippur;
    public override DateOnlyRange DateOnlyRange => new(Date, Date);
  }

	private sealed class TabernaclesSE : FeastDay
	{
		public TabernaclesSE() : base($"{nameof(Id.Tabernacles)}", Id.Tabernacles) { }
		public override string Hebrew => "סֻּכּוֹת";
		public override string Details => "Preparation Day, High Sabbath begins at sunset";
		public override string CalendarTitle => "Sukkot | Day 1";
		public override string TabTitle => "Tabernacles";
		public override string PlannerTitle => $"Tabernacle Week";
		public override string Icon => "fas fa-campground";
		public override string VerseReferenceCard => "Lev_23_33_44";
		public override bool IsStartOfEdge => false;
		public override bool IsEndOfEdge => true;

		public override bool HasCalendarDetails => true;
		public override int? DaysFromPrevFeast => 5;  // Yom Kippur is before and so a hard business rule can be made 
		public override DateOnly Date => FeastDayDates.Tabernacles;
    public override DateOnlyRange DateOnlyRange => new(Date, Date.AddDays(8)); 
  }

	#endregion
	
}

//public record DateRange(DateOnly Min, DateOnly Max);

public record DateOnlyRange(DateOnly Min, DateOnly Max);


// Ignore Spelling: Yom Kippur
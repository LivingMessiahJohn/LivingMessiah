using Ardalis.SmartEnum;
using RCL.Features.FeastDayPlanner.Constants;

using CalendarEnumsFeastDay = RCL.Features.Calendar.Enums.FeastDay;

namespace RCL.Features.FeastDayPlanner.Enums;

// Compare with Calendar\Enums\FeastDayDetail.cs
// ToDo: Can I get rid of this
public abstract class PlannerDetail : SmartEnum<PlannerDetail>
{
	#region Id's
	private static class Id
	{
		internal const int HanukkahDay1 = 1;
		internal const int HanukkahDay8 = 2;

		internal const int Purim = 3;

		internal const int UnleavenedBreadDay1 = 4;
		internal const int StartOmer = 5;
		internal const int UnleavenedBreadDay7 = 6;

		internal const int StopOmer = 7;
		internal const int Weeks = 8;

		internal const int Trumpets = 9;

		internal const int YomKippur = 10;

		internal const int SukkotDay1 = 11;
		internal const int SukkotDay8 = 12;
		internal const int SukkotCampTearDown = 13;
	}
	#endregion


	#region  Declared Public Instances
	public static readonly PlannerDetail HanukkahDay1 = new HanukkahDay1SE();
	public static readonly PlannerDetail HanukkahDay8 = new HanukkahDay8SE();

	public static readonly PlannerDetail Purim = new PurimSE();

	public static readonly PlannerDetail UnleavenedBreadDay1 = new UnleavenedBreadDay1SE();
	public static readonly PlannerDetail StartOmer = new StartOmerSE();
	public static readonly PlannerDetail UnleavenedBreadDay7 = new UnleavenedBreadDay7SE();

	public static readonly PlannerDetail StopOmer = new StopOmerSE();
	public static readonly PlannerDetail Weeks = new WeeksSE();

	public static readonly PlannerDetail Trumpets = new TrumpetsSE();
	public static readonly PlannerDetail YomKippur = new YomKippurSE();

	public static readonly PlannerDetail SukkotDay1 = new SukkotDay1SE();
	public static readonly PlannerDetail SukkotDay8 = new SukkotDay8SE();
	public static readonly PlannerDetail SukkotCampTearDown = new SukkotCampTearDownSE();
	// SE=SmartEnum
	#endregion

	private PlannerDetail(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract int ParentFeastDayId { get; }
	public abstract DateTime Date { get; }
  public abstract DateOnly DateOnly { get; }
  //public abstract DateOnlyRange DateOnlyRange { get; } // Can I get rid of Date and rename this as Date?
  public abstract string Description { get; }
	public abstract string HebrewDate { get; }
	public abstract string HebrewBGColor { get; }
	public abstract string HebrewTextColor { get; }
	public abstract string PreHebrewDate { get; }
  #endregion

  #region Extra Properties
  public DateOnly ErevDate => DateOnly.FromDateTime(Date.AddDays(-1));  // ToDo: Remove DateOnly.FromDateTime
  public DateOnly DayTimeDate => DateOnly.FromDateTime(Date);           // ToDo: Remove DateOnly.FromDateTime
  #endregion

  #region Private Instantiation


  private sealed class HanukkahDay1SE : PlannerDetail
	{
		public HanukkahDay1SE() : base($"{nameof(Id.HanukkahDay1)}", Id.HanukkahDay1) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.Hanukkah.Value;
		public override DateTime Date => CalendarEnumsFeastDay.Hanukkah.Date;
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.Hanukkah.Date);
		public override string Description => "Day 1";
		public override string HebrewDate => "Kislev 25th";
		public override string HebrewBGColor => BarColor.HebrewBGColor;
		public override string HebrewTextColor => BarColor.HebrewTextColor;
		public override string PreHebrewDate => "";
  }

	private sealed class HanukkahDay8SE : PlannerDetail
	{
		public HanukkahDay8SE() : base($"{nameof(Id.HanukkahDay8)}", Id.HanukkahDay8) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.Hanukkah.Value;
		public override DateTime Date => CalendarEnumsFeastDay.Hanukkah.Date.AddDays(8);
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.Hanukkah.Date.AddDays(8));
    public override string Description => "Day 8";
		public override string HebrewDate => "Tevet 2nd";
		public override string HebrewBGColor => BarColor.HebrewBGColor;
		public override string HebrewTextColor => BarColor.HebrewTextColor;
		public override string PreHebrewDate => "";
	}

	private sealed class PurimSE : PlannerDetail
	{
		public PurimSE() : base($"{nameof(Id.Purim)}", Id.Purim) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.Purim.Value;
		public override DateTime Date => CalendarEnumsFeastDay.Purim.Date;
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.Purim.Date);
    public override string Description => "Purim";
		public override string HebrewDate => HebrewYear.IsLeapYear ? "Adar II 14" : "Adar I 14";
		public override string HebrewBGColor => BarColor.HebrewBGColor;
		public override string HebrewTextColor => BarColor.HebrewTextColor;
		public override string PreHebrewDate => "";
	}

	private sealed class UnleavenedBreadDay1SE : PlannerDetail
	{
		public UnleavenedBreadDay1SE() : base($"{nameof(Id.UnleavenedBreadDay1)}", Id.UnleavenedBreadDay1) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.Passover.Value;
		public override DateTime Date => CalendarEnumsFeastDay.Passover.Date;
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.Passover.Date);
    public override string Description => "Unleavened Bread Day 1";
		public override string HebrewDate => "Nissan 15";
		public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
		public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
		public override string PreHebrewDate => "Seder";
	}

	private sealed class StartOmerSE : PlannerDetail
	{
		public StartOmerSE() : base($"{nameof(Id.StartOmer)}", Id.StartOmer) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.Passover.Value;

    // ToDo: this isn't right, it's the first Sunday after Passover
    public override DateTime Date => CalendarEnumsFeastDay.Passover.Date.AddDays(1);
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.Passover.Date.AddDays(1));
    
    public override string Description => "Start Omer Count";
		public override string HebrewDate => "Nissan 16";
		public override string HebrewBGColor => BarColor.HebrewBGColor;
		public override string HebrewTextColor => BarColor.HebrewTextColor;
		public override string PreHebrewDate => "";
	}

	private sealed class UnleavenedBreadDay7SE : PlannerDetail
	{
		public UnleavenedBreadDay7SE() : base($"{nameof(Id.UnleavenedBreadDay7)}", Id.UnleavenedBreadDay7) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.Passover.Value;
		public override DateTime Date => CalendarEnumsFeastDay.Passover.Date.AddDays(6);
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.Passover.Date.AddDays(6));
    public override string Description => "Unleavened Bread Day 7";
		public override string HebrewDate => "Nissan 21";
		public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
		public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
		public override string PreHebrewDate => "";
	}

	private sealed class StopOmerSE : PlannerDetail
	{
		public StopOmerSE() : base($"{nameof(Id.StopOmer)}", Id.StopOmer) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.Weeks.Value;
		public override DateTime Date => CalendarEnumsFeastDay.Weeks.Date.AddDays(-1);
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.Weeks.Date.AddDays(-1));
    public override string Description => "Finish Omer Count";
		public override string HebrewDate => "Sivan 6";
		public override string HebrewBGColor => BarColor.HebrewBGColor;
		public override string HebrewTextColor => BarColor.HebrewTextColor;
		public override string PreHebrewDate => "Omer 49!";
	}

	private sealed class WeeksSE : PlannerDetail
	{
		public WeeksSE() : base($"{nameof(Id.Weeks)}", Id.Weeks) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.Weeks.Value;
		public override DateTime Date => CalendarEnumsFeastDay.Weeks.Date;
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.Weeks.Date);
    public override string Description => "Weeks | Shavuot";
		public override string HebrewDate => "Sivan 7";
		public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
		public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
		public override string PreHebrewDate => "";
	}

	private sealed class TrumpetsSE : PlannerDetail
	{
		public TrumpetsSE() : base($"{nameof(Id.Trumpets)}", Id.Trumpets) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.Trumpets.Value;
		public override DateTime Date => CalendarEnumsFeastDay.Trumpets.Date;
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.Trumpets.Date);
    public override string Description => "Yom Teruah";
		public override string HebrewDate => "Tishrei 1";
		public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
		public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
		public override string PreHebrewDate => "";
	}

	private sealed class YomKippurSE : PlannerDetail
	{
		public YomKippurSE() : base($"{nameof(Id.YomKippur)}", Id.YomKippur) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.YomKippur.Value;
		public override DateTime Date => CalendarEnumsFeastDay.YomKippur.Date;
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.YomKippur.Date);
    public override string Description => "Day of Atonements";
		public override string HebrewDate => "Tishrei 10";
		public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
		public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
		public override string PreHebrewDate => "";
	}

	private sealed class SukkotDay1SE : PlannerDetail
	{
		public SukkotDay1SE() : base($"{nameof(Id.SukkotDay1)}", Id.SukkotDay1) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.Tabernacles.Value;
		public override DateTime Date => CalendarEnumsFeastDay.Tabernacles.Date;
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.Tabernacles.Date);
    public override string Description => "Sukkot 1";
		public override string HebrewDate => "Tishrei 15";
		public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
		public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
		public override string PreHebrewDate => "Prep Day";
	}

	private sealed class SukkotDay8SE : PlannerDetail
	{
		public SukkotDay8SE() : base($"{nameof(Id.SukkotDay8)}", Id.SukkotDay8) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.Tabernacles.Value;
		public override DateTime Date => CalendarEnumsFeastDay.Tabernacles.Date.AddDays(7);
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.Tabernacles.Date.AddDays(7));
    public override string Description => "Sukkot 8";
		public override string HebrewDate => "Tishrei 22";
		public override string HebrewBGColor => BarColor.HebrewSabbathBGColor;
		public override string HebrewTextColor => BarColor.HebrewSabbathTextColor;
		public override string PreHebrewDate => "";
	}

	private sealed class SukkotCampTearDownSE : PlannerDetail
	{
		public SukkotCampTearDownSE() : base($"{nameof(Id.SukkotCampTearDown)}", Id.SukkotCampTearDown) { }
		public override int ParentFeastDayId => CalendarEnumsFeastDay.Tabernacles.Value;
		public override DateTime Date => CalendarEnumsFeastDay.Tabernacles.Date.AddDays(8);
    public override DateOnly DateOnly => DateOnly.FromDateTime(CalendarEnumsFeastDay.Tabernacles.Date.AddDays(8));  
    public override string Description => "Camp Tear Down";
		public override string HebrewDate => "Tishrei 23";
		public override string HebrewBGColor => BarColor.HebrewBGColor;
		public override string HebrewTextColor => BarColor.HebrewTextColor;
		public override string PreHebrewDate => "";

	}
	#endregion
}

// Ignore Spelling: Descr
// Ignore Spelling: Erev
// Ignore Spelling: Sivan
// Ignore Spelling: Teruah
// Ignore Spelling: Tishrei
// Ignore Spelling: Yom Kippur
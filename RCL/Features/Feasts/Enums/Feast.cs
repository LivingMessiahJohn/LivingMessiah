using Ardalis.SmartEnum;

using FeastDayType = RCL.Features.Calendar.Enums.FeastDay;


namespace RCL.Features.Feasts.Enums;

/*

ToDo: Move to RCL

Observations and what makes this SmartEnums unique from the others
1. It's a annual event and therefore tied to KeyDate and Calendar
2. These have special attributes or...
		- string HomeFloatRightHebrew, string HomeTitleSuffix
		- bool SitemapUsage, bool HomeSidebarUsage, 
*/

public static class ColorConstants
{
	public const string CssForMoadim = "warning";
	public const string CssForNonMoadim = "success";
}

public record class Hebrew
{
	public string? FloatRightHebrew { get; set; }
	public string? TitleSuffix { get; set; }
	public string? Strongs { get; set; }
}

public abstract class Feast : SmartEnum<Enums.Feast>
{
	#region Id's
	private static class Id
	{
		internal const int Shabbat = 1;
		internal const int Hanukkah = 2;
		internal const int Purim = 3;
		internal const int Passover = 4;
		//internal const int Omer = 5;
		internal const int Weeks = 6;
		internal const int Trumpets = 7;
		internal const int YomKippur = 8;
		internal const int Tabernacles = 9;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Enums.Feast Shabbat = new Enums.Feast.ShabbatSE();
	public static readonly Enums.Feast Hanukkah = new Enums.Feast.HanukkahSE();
	public static readonly Enums.Feast Purim = new Enums.Feast.PurimSE();
	public static readonly Enums.Feast Passover = new Enums.Feast.PassoverSE();
	//public static readonly Feast Omer = new OmerSE();
	public static readonly Enums.Feast Weeks = new Enums.Feast.WeeksSE();
	public static readonly Enums.Feast Trumpets = new Enums.Feast.TrumpetsSE();
	public static readonly Enums.Feast YomKippur = new Enums.Feast.YomKippurSE();
	public static readonly Enums.Feast Tabernacles = new Enums.Feast.TabernaclesSE();
	// SE=SmartEnum
	#endregion

	private Feast(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract string Image { get; }  
	public abstract FeastDayType? FeastDay  { get; }
	public abstract Enums.Hebrew Hebrew { get; }
	public abstract string SpecialEventIndex { get; }

	//Properties
	public bool IsMoadim => this.Value == Enums.Feast.Id.Hanukkah | this.Value == Enums.Feast.Id.Purim ? false : true;
	public string MoadimColor => this.IsMoadim ? Enums.ColorConstants.CssForMoadim : Enums.ColorConstants.CssForNonMoadim;
	#endregion

	#region Private Instantiation
	private sealed class ShabbatSE : Enums.Feast
	{
		public ShabbatSE() : base($"{nameof(Enums.Feast.Id.Shabbat)}", Enums.Feast.Id.Shabbat) { }
		public override string Index => "/Shabbat";
		public override string Title => "Shabbat";
		public override string Icon => "far fa-hand-spock";
		public override string Image => "/images/feasts/challah-400-319.jpg";
		public override FeastDayType? FeastDay => null;
		public override Enums.Hebrew Hebrew => new() { TitleSuffix = "Shabbat", FloatRightHebrew = "שַׁבָּת", Strongs = "H7676" };
		public override string SpecialEventIndex => "";
	}

	private sealed class HanukkahSE : Enums.Feast
	{
		public HanukkahSE() : base($"{nameof(Enums.Feast.Id.Hanukkah)}", Enums.Feast.Id.Hanukkah) { }
		public override string Index => "/Hanukkah"; 
		public override string Title => "Hanukkah";
		public override string Icon => "fas fa-hanukiah";
		public override string Image => "/images/feasts/hanukkiah-400-x-400.jpg";
		public override FeastDayType? FeastDay => FeastDayType.Hanukkah;
		public override Enums.Hebrew Hebrew => new() { TitleSuffix = "Hanukkah", FloatRightHebrew = "חֲנֻכָּה", Strongs = "H2598" };
		public override string SpecialEventIndex => "";
	}

	private sealed class PurimSE : Enums.Feast
	{
		public PurimSE() : base($"{nameof(Enums.Feast.Id.Purim)}", Enums.Feast.Id.Purim) { }
		public override string Index => "/Purim"; 
		public override string Title => "Purim";
		public override string Icon => "far fa-square";
		public override string Image => "/images/feasts/purim-400-x-400.jpg";
		public override FeastDayType? FeastDay => FeastDayType.Purim;
		public override Enums.Hebrew Hebrew => new() { TitleSuffix = "Purim", FloatRightHebrew = "פּוּר", Strongs = "H6332" };
		public override string SpecialEventIndex => "";
	}

	private sealed class PassoverSE : Enums.Feast
	{
		public PassoverSE() : base($"{nameof(Enums.Feast.Id.Passover)}", Enums.Feast.Id.Passover) { }
		public override string Index => "/Passover"; 
		public override string Title => "Passover";
		public override string Icon => "fas fa-door-open";
		public override string Image => "/images/feasts/passover-crossover-400-x-308.jpg";  // passover-lamb-bread-400-302.jpg
		public override FeastDayType? FeastDay => FeastDayType.Passover;
		public override Enums.Hebrew Hebrew => new() { TitleSuffix = "Pesach", FloatRightHebrew = "פֶּסַח", Strongs = "H6453" };
		public override string SpecialEventIndex => "";
	}

	//ToDo: Can some MarkupString be used to keep track of the Omer count?
	//private sealed class OmerSE : Feast
	//{
	//	public OmerSE() : base($"{nameof(Id.Omer)}", Id.Omer) { }
	//  public override string Index => "/Omer";
	//	public override string Title => "Omer";
	//	public override string Icon => "far fa-calendar";
	//	public override Hebrew Hebrew => new() { TitleSuffix = "Omer", FloatRightHebrew = "עֹמֶר", Strongs = "H6016" };
	//  public override SeasonEnum? Season => "Spring";
	//	public override string SpecialEventIndex => "";
	//}

	private sealed class WeeksSE : Enums.Feast
	{
		public WeeksSE() : base($"{nameof(Enums.Feast.Id.Weeks)}", Enums.Feast.Id.Weeks) { }
		public override string Index => "/Weeks"; 
		public override string Title => "Weeks";
		public override string Icon => "fab fa-creative-commons-zero";
		public override string Image => "/images/feasts/shavuot-moses-tablets-400-x-400.jpg"; // Weeks-400-x-400.jpg
		public override FeastDayType? FeastDay => FeastDayType.Weeks;
		public override Enums.Hebrew Hebrew => new() { TitleSuffix = "Shavu'ot", FloatRightHebrew = "שָׁבוּעוֹת", Strongs = "H7620" };
		public override string SpecialEventIndex => "";
	}

	private sealed class TrumpetsSE : Enums.Feast
	{
		public TrumpetsSE() : base($"{nameof(Enums.Feast.Id.Trumpets)}", Enums.Feast.Id.Trumpets) { }
		public override string Index => "/Trumpets"; 
		public override string Title => "Trumpets";
		public override string Icon => "fas fa-bullhorn";
		public override string Image => "/images/feasts/yom-teruah-400-x-400.jpg";
		public override FeastDayType? FeastDay => FeastDayType.Trumpets;
		public override Enums.Hebrew Hebrew => new() { TitleSuffix = "Yom Teruah", FloatRightHebrew = "יוֹם תְּרוּעָה", Strongs = "H8643" };
		public override string SpecialEventIndex => "";
	}

	private sealed class YomKippurSE : Enums.Feast
	{
		public YomKippurSE() : base($"{nameof(Enums.Feast.Id.YomKippur)}", Enums.Feast.Id.YomKippur) { }
		public override string Index => "/YomKippur"; 
		public override string Title => "Yom Kippur";
		public override string Icon => "fas fa-hands-helping";
		public override string Image => "/images/feasts/yom-kippur-400-400.jpg";
		public override FeastDayType? FeastDay => FeastDayType.YomKippur;
		public override Enums.Hebrew Hebrew => new() { TitleSuffix = "Yom Kippur", FloatRightHebrew = "יוֹם כִּיפּוּר", Strongs = "H3725" };
		public override string SpecialEventIndex => "";
	}

	private sealed class TabernaclesSE : Enums.Feast
	{
		public TabernaclesSE() : base($"{nameof(Enums.Feast.Id.Tabernacles)}", Enums.Feast.Id.Tabernacles) { }
		public override string Index => "/Tabernacles"; 
		public override string Title => "Tabernacles";
		public override string Icon => "fas fa-campground";
		public override string Image => "/images/feasts/tabernacles-400-x-400.jpg";
		public override FeastDayType? FeastDay => FeastDayType.Tabernacles;
		public override Enums.Hebrew Hebrew => new() { TitleSuffix = "Sukkot", FloatRightHebrew = "סֻּכּוֹת", Strongs = "H5523" };
		public override string SpecialEventIndex => "";
	}

	#endregion

}


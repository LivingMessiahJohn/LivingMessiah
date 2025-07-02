using Ardalis.SmartEnum;
using Microsoft.AspNetCore.Components;
using FeastDayType = LivingMessiah.Features.Calendar.Enums.FeastDay;
using SeasonEnum = LivingMessiah.Features.Calendar.Enums.Season;

namespace LivingMessiah.Features.Feasts.Enums;

/*
Observations and what makes this SmartEnums unique from the others
1. It's a annual event and therefore tied to KeyDate and Calendar
2. Currently, if you have advanced autorization you can see these pages
		- Maybe that's not necessary i.e. going forward it can be open to the public 
		- If open to the public maybe have a PastDaysOld / FutureDaysUntil component
3. These have special attributes or...
		- string HomeFloatRightHebrew, string HomeTitleSuffix
		- bool SitemapUsage, bool HomeSidebarUsage, 
*/

public record class Hebrew
{
	public string? FloatRightHebrew { get; set; }
	public string? TitleSuffix { get; set; }
	public string? Strongs { get; set; }
}

public abstract class Feast : SmartEnum<Feast>
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
	public static readonly Feast Shabbat = new ShabbatSE();
	public static readonly Feast Hanukkah = new HanukkahSE();
	public static readonly Feast Purim = new PurimSE();
	public static readonly Feast Passover = new PassoverSE();
	//public static readonly Feast Omer = new OmerSE();
	public static readonly Feast Weeks = new WeeksSE();
	public static readonly Feast Trumpets = new TrumpetsSE();
	public static readonly Feast YomKippur = new YomKippurSE();
	public static readonly Feast Tabernacles = new TabernaclesSE();
	// SE=SmartEnum
	#endregion

	private Feast(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract string Image { get; }  
	public abstract FeastDayType? FeastDay  { get; }
	public abstract string ListGroupItemColor { get; }
	public abstract Hebrew Hebrew { get; }
	public abstract SeasonEnum? Season { get; }
	public abstract MarkupString Verses { get; }
	#endregion

	#region Private Instantiation
	private sealed class ShabbatSE : Feast
	{
		public ShabbatSE() : base($"{nameof(Id.Shabbat)}", Id.Shabbat) { }
		public override string Index => "/Shabbat";
		public override string Title => "Shabbat";
		public override string Icon => "far fa-hand-spock";
		public override string Image => "/images/feasts/challah-400-319.jpg";
		public override FeastDayType? FeastDay => null;
		public override string ListGroupItemColor => "list-group-item-warning";
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Shabbat", FloatRightHebrew = "שַׁבָּת", Strongs = "H7676" };
		public override SeasonEnum? Season => null;
		public override MarkupString Verses => new MarkupString($"");
	}

	private sealed class HanukkahSE : Feast
	{
		public HanukkahSE() : base($"{nameof(Id.Hanukkah)}", Id.Hanukkah) { }
		public override string Index => "/Hanukkah"; 
		public override string Title => "Hanukkah";
		public override string Icon => "fas fa-hanukiah";
		public override string Image => "/images/feasts/hanukkiah-400-x-400.jpg";
		public override FeastDayType? FeastDay => FeastDayType.Hanukkah;
		public override string ListGroupItemColor => "list-group-item-success";
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Hanukkah", FloatRightHebrew = "חֲנֻכָּה", Strongs = "H2598" };
		public override SeasonEnum? Season => SeasonEnum.Winter;
		public override MarkupString Verses => new MarkupString($"");
	}

	private sealed class PurimSE : Feast
	{
		public PurimSE() : base($"{nameof(Id.Purim)}", Id.Purim) { }
		public override string Index => "/Purim"; 
		public override string Title => "Purim";
		public override string Icon => "far fa-square";
		public override string Image => "/images/feasts/purim-400-x-400.jpg";
		public override FeastDayType? FeastDay => FeastDayType.Purim;
		public override string ListGroupItemColor => "list-group-item-success";
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Purim", FloatRightHebrew = "פּוּר", Strongs = "H6332" };
		public override SeasonEnum? Season => SeasonEnum.Winter;
		public override MarkupString Verses => new MarkupString($"");
	}

	private sealed class PassoverSE : Feast
	{
		public PassoverSE() : base($"{nameof(Id.Passover)}", Id.Passover) { }
		public override string Index => "/Passover"; 
		public override string Title => "Passover";
		public override string Icon => "fas fa-door-open";
		public override string Image => "/images/feasts/passover-crossover-400-x-308.jpg";  // passover-lamb-bread-400-302.jpg
		public override FeastDayType? FeastDay => FeastDayType.Passover;
		public override string ListGroupItemColor => "list-group-item-warning";
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Pesach", FloatRightHebrew = "פֶּסַח", Strongs = "H6453" };
		public override SeasonEnum? Season => SeasonEnum.Spring;
		public override MarkupString Verses => new MarkupString($"");
	}

	//ToDo: Can some MarkupString be used to keep track of the Omer count?
	//private sealed class OmerSE : Feast
	//{
	//	public OmerSE() : base($"{nameof(Id.Omer)}", Id.Omer) { }
	//  public override string Index => "/Omer";
	//	public override string Title => "Omer";
	//	public override string Icon => "far fa-calendar";
	//	public override string ListGroupItemColor => "list-group-item-success";
	//	public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Omer", FloatRightHebrew = "עֹמֶר", Strongs = "H6016" };
	//  public override SeasonEnum? Season => "Spring";
	//	public override MarkupString Verses => new MarkupString($"");
	//}

	private sealed class WeeksSE : Feast
	{
		public WeeksSE() : base($"{nameof(Id.Weeks)}", Id.Weeks) { }
		public override string Index => "/Weeks"; 
		public override string Title => "Weeks";
		public override string Icon => "fab fa-creative-commons-zero";
		public override string Image => "/images/feasts/shavuot-moses-tablets-400-x-400.jpg"; // Weeks-400-x-400.jpg
		public override FeastDayType? FeastDay => FeastDayType.Weeks;
		public override string ListGroupItemColor => "list-group-item-warning";
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Shavu'ot", FloatRightHebrew = "שָׁבוּעוֹת", Strongs = "H7620" };
		public override SeasonEnum? Season => SeasonEnum.Summer;
		public override MarkupString Verses => new MarkupString($"");
	}

	private sealed class TrumpetsSE : Feast
	{
		public TrumpetsSE() : base($"{nameof(Id.Trumpets)}", Id.Trumpets) { }
		public override string Index => "/Trumpets"; 
		public override string Title => "Trumpets";
		public override string Icon => "fas fa-bullhorn";
		public override string Image => "/images/feasts/yom-teruah-400-x-343.jpg";
		public override FeastDayType? FeastDay => FeastDayType.Trumpets;
		public override string ListGroupItemColor => "list-group-item-warning";
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Yom Teruah", FloatRightHebrew = "יוֹם תְּרוּעָה", Strongs = "H8643" };
		public override SeasonEnum? Season => SeasonEnum.Fall;
		public override MarkupString Verses => new MarkupString($"");
	}

	private sealed class YomKippurSE : Feast
	{
		public YomKippurSE() : base($"{nameof(Id.YomKippur)}", Id.YomKippur) { }
		public override string Index => "/YomKippur"; 
		public override string Title => "Yom Kippur";
		public override string Icon => "fas fa-hands-helping";
		public override string Image => "/images/feasts/yom-kippur-400-313.jpg";
		public override FeastDayType? FeastDay => FeastDayType.YomKippur;
		public override string ListGroupItemColor => "list-group-item-warning";
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Yom Kippur", FloatRightHebrew = "יוֹם כִּיפּוּר", Strongs = "H3725" };
		public override SeasonEnum? Season => SeasonEnum.Fall;
		public override MarkupString Verses => new MarkupString($"");
	}

	private sealed class TabernaclesSE : Feast
	{
		public TabernaclesSE() : base($"{nameof(Id.Tabernacles)}", Id.Tabernacles) { }
		public override string Index => "/Tabernacles"; 
		public override string Title => "Tabernacles";
		public override string Icon => "fas fa-campground";
		public override string Image => "/images/feasts/tabernacles-400-x-400.jpg";
		public override FeastDayType? FeastDay => FeastDayType.Tabernacles;
		public override string ListGroupItemColor => "list-group-item-warning";
		public override Hebrew Hebrew => new Hebrew { TitleSuffix = "Sukkot", FloatRightHebrew = "סֻּכּוֹת", Strongs = "H5523" };
		public override SeasonEnum? Season => SeasonEnum.Fall;
		public override MarkupString Verses => new MarkupString($"");
	}

	#endregion

}


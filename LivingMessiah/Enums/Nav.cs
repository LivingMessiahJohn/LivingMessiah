
using Ardalis.SmartEnum;

namespace LivingMessiah.Enums;

[Flags]
public enum PageListType
{
	None = 0,
	SitemapPage = 1,
	Footer = 2,
	Layout = 4,
	HealthCheck = 8,
	Reply = 16,
	LayoutXs = 32,
	LayoutSm = 64,
	LayoutMd = 128,
	LayoutLg = 256,
	LayoutXl = 512,
}


public abstract class Nav : SmartEnum<Nav>
{

	#region Id's
	private static class Id
	{
		internal const int Home = 1;
		internal const int Calendar = 2;
		internal const int FeastTable = 3;
		internal const int CalendarHealthCheck = 4;
		internal const int Donate = 5;
		internal const int DonateReplyConfirm = 6;
		internal const int Sitemap = 7;
		internal const int HealthCheckTestLogger = 8;
		internal const int Feasts = 9;
		internal const int LunarMonth = 10;
		internal const int Leadership = 11;
		internal const int HeavensDeclare = 12;
		internal const int ThresholdCovenant = 13;
		internal const int Podcast = 14;
		internal const int Parasha = 15;  // PageParasha.Archive
		internal const int IntroductionAndWelcome = 16;
		internal const int ShabbatService = 17; // should I call it Liturgy
		internal const int UpcomingEvents = 18;
		internal const int WindmillRanch = 19;
		internal const int TorahTuesday = 20;
		internal const int IndepthStudy = 21;
		internal const int About = 22;
		internal const int BloodMoons = 23;
		internal const int Articles = 24;
		internal const int FurtherStudies = 25;	
		internal const int ImportantLinks = 26;	
	}
	#endregion

	#region Declared Public Instances
	public static readonly Nav Home = new HomeSE();
	public static readonly Nav Calendar = new CalendarSE();
	public static readonly Nav FeastTable = new FeastTableSE();
	public static readonly Nav CalendarHealthCheck = new CalendarHealthCheckSE();
	public static readonly Nav Donate = new DonateSE();
	public static readonly Nav DonateReplyConfirm = new DonateReplyConfirmSE();
	public static readonly Nav Sitemap = new SitemapSE();
	public static readonly Nav HealthCheckTestLogger = new HealthCheckTestLoggerSE();
	public static readonly Nav Feasts = new FeastsSE();
	public static readonly Nav LunarMonth = new LunarMonthSE();
	public static readonly Nav Leadership = new LeadershipSE();
	public static readonly Nav HeavensDeclare = new HeavensDeclareSE();
	public static readonly Nav ThresholdCovenant = new ThresholdCovenantSE();
	public static readonly Nav Podcast = new PodcastSE();
	public static readonly Nav Parasha = new ParashaSE();
	public static readonly Nav IntroductionAndWelcome = new IntroductionAndWelcomeSE();

	public static readonly Nav ShabbatService = new ShabbatServiceSE(); 
	public static readonly Nav UpcomingEvents = new UpcomingEventsSE();
	public static readonly Nav WindmillRanch = new WindmillRanchSE();
	public static readonly Nav TorahTuesday = new TorahTuesdaySE();
	public static readonly Nav IndepthStudy = new IndepthStudySE();
	public static readonly Nav About = new AboutSE();
	public static readonly Nav BloodMoons = new BloodMoonsSE();
	public static readonly Nav Articles = new ArticlesSE();
	public static readonly Nav FurtherStudies = new FurtherStudiesSE();	
	public static readonly Nav ImportantLinks = new ImportantLinksSE();	 

	#endregion


	private Nav(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract int Sort { get; }
	public abstract string HomeTitleSuffix { get; }
	public abstract string HomeFloatRightHebrew { get; }
	public abstract bool IsPartOfList(PageListType pageListType);
	public abstract PageListType PageListType { get; }
	public abstract bool Disabled { get; }

	public string DisabledHtml
	{
		get
		{
			return $"{(Disabled ? " disabled" : "")}";
		}
	}

	public string TextColor
	{
		get
		{
			return $"{(Disabled ? " text-black-50" : "text-primary")}";
		}
	}

	#endregion

	#region Private Instantiation

	private sealed class HomeSE : Nav
	{
		public HomeSE() : base($"{nameof(Id.Home)}", Id.Home) { }
		public override string Index => "/";
		public override string Title => "Home";
		public override string Icon => "fas fa-home";
		public override int Sort => Id.Home;
		public override string HomeTitleSuffix => " bayit H1004";
		public override string HomeFloatRightHebrew => "בַּיִת";
		public override PageListType PageListType => PageListType.Footer;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class CalendarSE : Nav
	{
		public CalendarSE() : base($"{nameof(Id.Calendar)}", Id.Calendar) { }
		public override string Index => "/Calendar";
		public override string Title => "Calendar";
		public override string Icon => "far fa-calendar-alt";
		public override int Sort => Id.Calendar;
		public override string HomeTitleSuffix => " ";
		public override string HomeFloatRightHebrew => "";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Footer | PageListType.Layout | PageListType.LayoutMd;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class FeastTableSE : Nav
	{
		public FeastTableSE() : base($"{nameof(Id.FeastTable)}", Id.FeastTable) { }
		public override string Index => "/FeastTable";
		public override string Title => "Feast Table";
		public override string Icon => "fas fa-glass-cheers";
		public override int Sort => Id.FeastTable;
		public override string HomeTitleSuffix => " ";
		public override string HomeFloatRightHebrew => "";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Footer | PageListType.Layout | PageListType.LayoutMd;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => true;
	}

	private sealed class CalendarHealthCheckSE : Nav
	{
		public CalendarHealthCheckSE() : base($"{nameof(Id.CalendarHealthCheck)}", Id.CalendarHealthCheck) { }
		public override string Index => "/Calendar/HealthCheck";
		public override string Title => "Calendar Health Check";
		public override string Icon => "fas fa-heartbeat";
		public override int Sort => Id.CalendarHealthCheck;
		public override string HomeTitleSuffix => " ";
		public override string HomeFloatRightHebrew => "";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.HealthCheck;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class DonateSE : Nav
	{
		public DonateSE() : base($"{nameof(Id.Donate)}", Id.Donate) { }
		public override string Index => "/Donate";
		public override string Title => "Donate";
		public override string Icon => "fas fa-donate";
		public override int Sort => Id.Donate;
		public override string HomeTitleSuffix => " Tsadik H6662";
		public override string HomeFloatRightHebrew => "צַדִּיק";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Footer | PageListType.Layout | PageListType.LayoutMd;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class DonateReplyConfirmSE : Nav
	{
		public DonateReplyConfirmSE() : base($"{nameof(Id.DonateReplyConfirm)}", Id.DonateReplyConfirm) { }
		public override string Index => "/confirm_donation.html";
		public override string Title => "Donation Confirmed";
		public override string Icon => "fab fa-cc-stripe";
		public override int Sort => Id.DonateReplyConfirm;
		public override string HomeTitleSuffix => "";
		public override string HomeFloatRightHebrew => "";

		public override PageListType PageListType => PageListType.Reply;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false; // N/A
	}

	private sealed class SitemapSE : Nav
	{
		public SitemapSE() : base($"{nameof(Id.Sitemap)}", Id.Sitemap) { }
		public override string Index => "/Sitemap";
		public override string Title => "Sitemap";
		public override string Icon => "fas fa-sitemap";
		public override int Sort => Id.Sitemap;
		public override string HomeTitleSuffix => " nahal H5095";
		public override string HomeFloatRightHebrew => "נָהַל";
		public override PageListType PageListType => PageListType.Footer | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class HealthCheckTestLoggerSE : Nav
	{
		public HealthCheckTestLoggerSE() : base($"{nameof(Id.HealthCheckTestLogger)}", Id.HealthCheckTestLogger) { }
		public override string Index => "HealthCheck/TestLogger";
		public override string Title => "Test Logger (HC)";
		public override string Icon => "fas fa-bomb";  //fas fa-wrench
		public override int Sort => Id.HealthCheckTestLogger;
		public override string HomeTitleSuffix => " ";
		public override string HomeFloatRightHebrew => "";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.HealthCheck;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class FeastsSE : Nav
	{
		public FeastsSE() : base($"{nameof(Id.Feasts)}", Id.Feasts) { }
		public override string Index => "/Feasts";
		public override string Title => "Feasts"; 	//public override string Description: "Feasts of YHWH";	
		public override string Icon => "fas fa-pizza-slice";
		public override int Sort => Id.Feasts;
		public override string HomeTitleSuffix => " moed H4150";
		public override string HomeFloatRightHebrew => "מוֹעֵד";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Footer | PageListType.Layout | PageListType.LayoutMd;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class LunarMonthSE : Nav
	{
		public LunarMonthSE() : base($"{nameof(Id.LunarMonth)}", Id.LunarMonth) { }
		public override string Index => "/LunarMonth";  // /LunarMonthV2
		public override string Title => "Lunar Month";
		public override string Icon => "fas fa-cloud-moon";
		public override int Sort => Id.LunarMonth;
		public override string HomeTitleSuffix => " yerach H3394"; // "Month yerach H3391";
		public override string HomeFloatRightHebrew => "יָרֵחַ";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout | PageListType.LayoutMd;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class LeadershipSE : Nav
	{
		public LeadershipSE() : base($"{nameof(Id.Leadership)}", Id.Leadership) { }
		public override string Index => "/Leadership";
		public override string Title => "Leadership";
		public override string Icon => "fas fa-user-tie";
		public override int Sort => Id.Leadership;
		public override string HomeTitleSuffix => " zaken H2205";  //nasi H5387
		public override string HomeFloatRightHebrew => "זָקֵן";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout | PageListType.LayoutMd;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class HeavensDeclareSE : Nav
	{
		public HeavensDeclareSE() : base($"{nameof(Id.HeavensDeclare)}", Id.HeavensDeclare) { }
		public override string Index => "/HeavensDeclare";
		public override string Title => "Heavens Declare";
		public override string Icon => "fas fa-cloud-moon"; //fas fa-star
		public override int Sort => Id.HeavensDeclare;
		public override string HomeTitleSuffix => " shamayim H8064";
		public override string HomeFloatRightHebrew => "שָׁמַיִם";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout | PageListType.LayoutMd;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class ThresholdCovenantSE : Nav
	{
		public ThresholdCovenantSE() : base($"{nameof(Id.ThresholdCovenant)}", Id.ThresholdCovenant) { }
		public override string Index => "/ThresholdCovenant";
		public override string Title => "Threshold Covenant";
		public override string Icon => "fas fa-broom";
		public override int Sort => Id.ThresholdCovenant;
		public override string HomeTitleSuffix => " saph H5592";
		public override string HomeFloatRightHebrew => "סַף";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout | PageListType.LayoutMd;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class PodcastSE : Nav
	{
		public PodcastSE() : base($"{nameof(Id.Podcast)}", Id.Podcast) { }
		public override string Index => "/Podcast";
		public override string Title => "Podcast";
		public override string Icon => "fas fa-podcast"; 
		public override int Sort => Id.Podcast;
		public override string HomeTitleSuffix => "";
		public override string HomeFloatRightHebrew => "";
		public override PageListType PageListType => PageListType.Footer;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class ParashaSE : Nav
	{
		public ParashaSE() : base($"{nameof(Id.Parasha)}", Id.Parasha) { }
		//public override string Index => ParashaEnums.Constants.GetUrl()! ?? this.Name;
		public override string Index => "/Parasha";
		public override string Title => "Parasha";
		public override string Icon => "far fa-bookmark";
		public override int Sort => Id.Parasha;
		public override string HomeTitleSuffix => " Parashat H6567";
		public override string HomeFloatRightHebrew => "פָּרָשַׁת";
		public override PageListType PageListType => PageListType.SitemapPage; // | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class IntroductionAndWelcomeSE : Nav
	{
		public IntroductionAndWelcomeSE() : base($"{nameof(Id.IntroductionAndWelcome)}", Id.IntroductionAndWelcome) { }
		public override string Index => "/IntroductionAndWelcome";
		public override string Title => "Welcome";
		public override string Icon => "far fa-handshake";
		public override int Sort => Id.IntroductionAndWelcome;
		public override string HomeTitleSuffix => " Shalom  H7695";
		public override string HomeFloatRightHebrew => "שָׁלוֹם";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => true;
	}

	// Liturgy
	private sealed class ShabbatServiceSE : Nav
	{
		public ShabbatServiceSE() : base($"{nameof(Id.ShabbatService)}", Id.ShabbatService) { }
		public override string Index => "/ShabbatService";
		public override string Title => "Shabbat Service";
		public override string Icon => "far fa-hand-spock";
		public override int Sort => Id.ShabbatService;
		public override string HomeTitleSuffix => " Shabbat H7676";
		public override string HomeFloatRightHebrew => "שַׁבָּת";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => true;
	}

	private sealed class UpcomingEventsSE : Nav
	{
		public UpcomingEventsSE() : base($"{nameof(Id.UpcomingEvents)}", Id.UpcomingEvents) { }
		public override string Index => "/UpcomingEvents";
		public override string Title => "Upcoming Events";
		public override string Icon => "far fa-clock";
		public override int Sort => Id.UpcomingEvents;
		public override string HomeTitleSuffix => "";
		public override string HomeFloatRightHebrew => "";
		public override PageListType PageListType => PageListType.SitemapPage; //  | PageListType.Layout
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => true;
	}

	private sealed class WindmillRanchSE : Nav
	{
		public WindmillRanchSE() : base($"{nameof(Id.WindmillRanch)}", Id.WindmillRanch) { }
		public override string Index => "/WindmillRanch/";
		public override string Title => "Windmill Ranch";
		public override string Icon => "fas fa-dharmachakra";
		//public override string Descr => "Landing page for the Windmill Ranch project";
		public override int Sort => Id.WindmillRanch;
		public override string HomeTitleSuffix => "";
		public override string HomeFloatRightHebrew => "";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout; 
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => true;
	}

	private sealed class TorahTuesdaySE : Nav
	{
		public TorahTuesdaySE() : base($"{nameof(Id.TorahTuesday)}", Id.TorahTuesday) { }
		public override string Index => "/TorahTuesday";
		public override string Title => "Torah Tuesday";
		public override string Icon => "fas fa-torah";
		public override int Sort => Id.TorahTuesday;
		public override string HomeTitleSuffix => " Torah H8451";
		public override string HomeFloatRightHebrew => "תּוֹרָה";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => true;
	}

	private sealed class IndepthStudySE : Nav
	{
		public IndepthStudySE() : base($"{nameof(Id.IndepthStudy)}", Id.IndepthStudy) { }
		public override string Index => "/IndepthStudy";
		public override string Title => "In-depth study";
		public override string Icon => "fas fa-graduation-cap";
		public override int Sort => Id.IndepthStudy;
		public override string HomeTitleSuffix => " Torah H8451";
		public override string HomeFloatRightHebrew => "תּוֹרָה";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => true;
	}

	private sealed class AboutSE : Nav
	{
		public AboutSE() : base($"{nameof(Id.About)}", Id.About) { }
		public override string Index => "/About";
		public override string Title => "About";
		public override string Icon => "fas fa-info";
		public override int Sort => Id.About;
		public override string HomeTitleSuffix => " Odot H182";
		public override string HomeFloatRightHebrew => "אודות";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => true;
	}

	private sealed class BloodMoonsSE : Nav
	{
		public BloodMoonsSE() : base($"{nameof(Id.BloodMoons)}", Id.BloodMoons) { }
		public override string Index => "/BloodMoons";
		public override string Title => "Blood Moons";
		public override string Icon => "far fa-moon";
		public override int Sort => Id.BloodMoons;
		public override string HomeTitleSuffix => " yareach H3394";
		public override string HomeFloatRightHebrew => "יָרֵחַ";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => true;
	}

	private sealed class ArticlesSE : Nav
	{
		public ArticlesSE() : base($"{nameof(Id.Articles)}", Id.Articles) { }
		public override string Index => "/Articles";
		public override string Title => "Articles";
		public override string Icon => "fas fa-pencil-alt";
		public override int Sort => Id.Articles;
		public override string HomeTitleSuffix => " Ketuvim H3789";
		public override string HomeFloatRightHebrew => "כְּתֻבִים";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => true;
	}

	private sealed class FurtherStudiesSE : Nav
	{
		public FurtherStudiesSE() : base($"{nameof(Id.FurtherStudies)}", Id.FurtherStudies) { }
		public override string Index => "/FurtherStudies";
		public override string Title => "Further Studies";
		public override string Icon => "fab fa-leanpub";
		public override int Sort => Id.FurtherStudies;
		public override string HomeTitleSuffix => " sepher H5612";
		public override string HomeFloatRightHebrew => "סֵפֶר";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => true;
	}
	
	private sealed class ImportantLinksSE : Nav
	{
		public ImportantLinksSE() : base($"{nameof(Id.ImportantLinks)}", Id.ImportantLinks) { }
		public override string Index => "/ImportantLinks";
		public override string Title => "Important Links";
		public override string Icon => "fas fa-external-link-square-alt";
		public override int Sort => Id.ImportantLinks;
		public override string HomeTitleSuffix => " rakad H7540";
		public override string HomeFloatRightHebrew => "רָקַד";
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => true;
	}

	#endregion

}

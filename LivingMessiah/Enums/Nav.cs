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
		public override bool Disabled => false;
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
		public override string Title => "Feasts";
		//public override string Description: "Feasts of YHWH";	
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
		public override string Index => "/LunarMonthV2";
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
	#endregion
}

using Ardalis.SmartEnum;

namespace LivingMessiahAdmin.Enums;

[Flags]
public enum PageListType
{
	None = 0,
	SitemapPage = 1, // Delete, or make it a cool search feature
	Footer = 2, // Delete
	Layout = 4,
	HealthCheck = 8,
	Reply = 16,
	LayoutXs = 32,
	LayoutSm = 64,
	LayoutMd = 128,
	LayoutLg = 256,
	LayoutXl = 512
	// SampleCode ??? 
}

/*
- [ ] Calendar\ManageKeyDates
- [ ] Calendar\ManageParashaCalendar
- [ ] Contacts
- [ ] SpecialEvents
 */

public abstract class Nav : SmartEnum<Nav>
{

	#region Id's
	private static class Id
	{
		internal const int Home = 1; // The content is Sitemap
		internal const int KeyDates = 2;
		internal const int Database = 3;
		internal const int HealthCheckCalendar = 4;
		internal const int HealthCheckTestLogger = 5;
		internal const int PsalmsAndProverbs = 6;
		internal const int SpecialEvents = 7;
		internal const int Sukkot = 8;
		internal const int SukkotNotes = 9;
		//		internal const int SukkotDelete = 10;
		internal const int SukkotAttendanceReport = 11;
	}
	#endregion

	#region Declared Public Instances
	public static readonly Nav Home = new HomeSE();
	public static readonly Nav KeyDates = new KeyDatesSE();
	public static readonly Nav Database = new DatabaseSE();
	public static readonly Nav HealthCheckCalendar = new HealthCheckCalendarSE();
	public static readonly Nav HealthCheckTestLogger = new HealthCheckTestLoggerSE();
	public static readonly Nav PsalmsAndProverbs = new PsalmsAndProverbsSE();
	public static readonly Nav Sukkot = new SukkotSE();
	public static readonly Nav SukkotNotes = new SukkotNotesSE();
	//	public static readonly Nav SukkotDelete = new SukkotDeleteSE();
	public static readonly Nav SukkotAttendanceReport = new SukkotAttendanceReportSE();
	public static readonly Nav SpecialEvents = new SpecialEventsSE();
	#endregion


	private Nav(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract int Sort { get; }
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
		public override PageListType PageListType => PageListType.Footer;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class KeyDatesSE : Nav
	{
		public KeyDatesSE() : base($"{nameof(Id.KeyDates)}", Id.KeyDates) { }
		public override string Index => "/KeyDates";
		public override string Title => "Key Dates";
		public override string Icon => "far fa-calendar-check";
		public override int Sort => Id.KeyDates;
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Footer | PageListType.Layout | PageListType.LayoutMd;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class DatabaseSE : Nav
	{
		public DatabaseSE() : base($"{nameof(Id.Database)}", Id.Database) { }
		public override string Index => "/Database"; // /ErrorLog
		public override string Title => "Database Error Logs";
		public override string Icon => "fas fa-bomb";
		public override int Sort => Id.Database;
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Footer | PageListType.Layout | PageListType.LayoutMd;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class HealthCheckCalendarSE : Nav
	{
		public HealthCheckCalendarSE() : base($"{nameof(Id.HealthCheckCalendar)}", Id.HealthCheckCalendar) { }
		public override string Index => "/HealthCheck/Calendar";
		public override string Title => "Health Check | Calendar";
		public override string Icon => "fas fa-heartbeat";
		public override int Sort => Id.HealthCheckCalendar;
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.HealthCheck;
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
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.HealthCheck;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class PsalmsAndProverbsSE : Nav
	{
		public PsalmsAndProverbsSE() : base($"{nameof(Id.PsalmsAndProverbs)}", Id.PsalmsAndProverbs) { }
		public override string Index => "/PsalmsAndProverbs"; // Index2 = "/PandP";
		public override string Title => "Upcoming Psalms And Proverbs";
		public override string Icon => "fab fa-readme";
		public override int Sort => Id.PsalmsAndProverbs;
		public override PageListType PageListType => PageListType.SitemapPage;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class SpecialEventsSE : Nav
	{
		public SpecialEventsSE() : base($"{nameof(Id.SpecialEvents)}", Id.SpecialEvents) { }
		public override string Index => "/SpecialEvents";
		public override string Title => "Special Events";
		public override string Icon => "far fa-clock";
		public override int Sort => Id.SpecialEvents;
		public override PageListType PageListType => PageListType.SitemapPage; //  | PageListType.Layout
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	private sealed class SukkotSE : Nav
	{
		public SukkotSE() : base($"{nameof(Id.Sukkot)}", Id.Sukkot) { }
		public override string Index => "/Sukkot";  // Sukkot/ManageRegistration
		public override string Title => "Manage Registration";
		public override string Icon => "fas fa-mask";
		public override int Sort => Id.Sukkot;
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	// This is a sub-page of Sukkot, do I want this here?
	private sealed class SukkotNotesSE : Nav
	{
		public SukkotNotesSE() : base($"{nameof(Id.SukkotNotes)}", Id.SukkotNotes) { }
		public override string Index => "/Sukkot/Notes";
		public override string Title => "Sukkot Notes";
		public override string Icon => "fas fa-sticky-note";
		public override int Sort => Id.SukkotNotes;
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	//public const string DeleteConfirmation = "/Sukkot/DeleteConfirmation";
	//public const string DeleteConfirmationTitle = "Delete Sukkot Registration?";
	//public const string DeleteConfirmationSubTitle = "Delete Registration? | ";

	//private sealed class SukkotDeleteSE : Nav
	//{
	//	public SukkotDeleteSE() : base($"{nameof(Id.SukkotDelete)}", Id.SukkotDelete) { }
	//	public override string Index => "/Sukkot/Delete";
	//	public override string Title => "Sukkot Delete";
	//	public override string Icon => "fas fa-times";
	//	public override int Sort => Id.SukkotDelete;
	//	public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
	//	public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
	//	public override bool Disabled => false;
	//}

	//public const string AttendanceChart = "/SukkotAdmin/AttendanceChart";

	private sealed class SukkotAttendanceReportSE : Nav
	{
		public SukkotAttendanceReportSE() : base($"{nameof(Id.SukkotAttendanceReport)}", Id.SukkotAttendanceReport) { }
		public override string Index => "/Sukkot/AttendanceAllFeastDays";
		public override string Title => "Attendance all Feast Days";
		public override string Icon => "fas fa-sticky-note";
		public override int Sort => Id.SukkotAttendanceReport;
		public override PageListType PageListType => PageListType.SitemapPage | PageListType.Layout;
		public override bool IsPartOfList(PageListType pageListType) => (PageListType & pageListType) == pageListType;
		public override bool Disabled => false;
	}

	#endregion

}

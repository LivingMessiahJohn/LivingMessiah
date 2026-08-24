using Admin.Features.Sukkot.Dashboard.Enums;
using SukkotEnumsMenuBar = Admin.Features.Sukkot.Enums.MenuBar;
using Ardalis.SmartEnum;

using RoleEnum = Admin.Security.Enums.Role;

namespace Admin.Enums;

public abstract class Nav : SmartEnum<Nav>
{
	#region Id's
	private static class Id
	{
		//internal const int Home = 1; // The content is Sitemap
		internal const int SukkotHome = 2;
		internal const int SukkotDashboard = 3; // ToDo: Delete for the same reason SukkotCRUD was
	//internal const int SukkotCRUD = 4;
		internal const int KeyDates = 5;
		internal const int Database = 6;
		internal const int HealthCheckCalendar = 7;
		internal const int FeastTable = 8;
		internal const int SpecialEvents = 9;
		//internal const int Profile = 10;
		internal const int WeeklyDownload = 11;
		internal const int SukkotSchedule = 12;
	}
	#endregion

	#region Declared Public Instances
	//public static readonly Nav Home = new HomeSE();
	public static readonly Nav SukkotHome = new SukkotHomeSE();
	public static readonly Nav SukkotDashboard = new SukkotDashboardSE(); 
	//public static readonly Nav SukkotCRUD = new SukkotCRUDSE(); 
	public static readonly Nav KeyDates = new KeyDatesSE();
	public static readonly Nav Database = new DatabaseSE();
	public static readonly Nav HealthCheckCalendar = new HealthCheckCalendarSE();
	public static readonly Nav FeastTable = new FeastTableSE();
	public static readonly Nav SpecialEvents = new SpecialEventsSE();
	//public static readonly Nav Profile = new ProfileSE();
	public static readonly Nav WeeklyDownload = new WeeklyDownloadSE();
	public static readonly Nav SukkotSchedule = new SukkotScheduleSE();
	#endregion


	private Nav(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract int Parent { get; }
	public abstract int Sort { get; }
	public abstract int RequiredRoles { get; } // New property for bitwise role checking

	#endregion

	#region Private Instantiation


	private sealed class SukkotHomeSE : Nav
	{
		public SukkotHomeSE() : base($"{nameof(Id.SukkotHome)}", Id.SukkotHome) { }
		public override string Index => SukkotEnumsMenuBar.Home.Index;
		public override string Title => SukkotEnumsMenuBar.Home.Title;
		public override string Icon => SukkotEnumsMenuBar.Home.Icon;
		public override int Parent => NavGroup.Sukkot.Value;
		public override int Sort => 1;
		public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value | RoleEnum.Admin.Value;
	}


	private sealed class SukkotDashboardSE : Nav
	{
		public SukkotDashboardSE() : base($"{nameof(Id.SukkotDashboard)}", Id.SukkotDashboard) { }
		public override string Index => SukkotEnumsMenuBar.Dashboard.Index;
		public override string Title => SukkotEnumsMenuBar.Dashboard.Title;
		public override string Icon => SukkotEnumsMenuBar.Dashboard.Icon;
		public override int Parent => NavGroup.Sukkot.Value;
		public override int Sort => 1;
		public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value | RoleEnum.Admin.Value;
	}
	/*
	private sealed class SukkotCRUDSE : Nav
	{
		public SukkotCRUDSE() : base($"{nameof(Id.SukkotCRUD)}", Id.SukkotCRUD) { }
		public override string Index => SukkotEnumsMenuBar.CRUD.Index;
		public override string Title => SukkotEnumsMenuBar.CRUD.Title;
		public override string Icon => SukkotEnumsMenuBar.CRUD.Icon;
		public override int Parent => NavGroup.Sukkot.Value;
		public override int Sort => 2;
		public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value;
	}
	*/
	private sealed class SukkotScheduleSE : Nav
	{
		public SukkotScheduleSE() : base($"{nameof(Id.SukkotSchedule)}", Id.SukkotSchedule) { }
		public override string Index => "/SukkotSchedule";
		public override string Title => "Sukkot Daily Schedule";
		public override string Icon => "far fa-calendar-alt";
		public override int Parent => NavGroup.Sukkot.Value;
		public override int Sort => 3;
		public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value | RoleEnum.Admin.Value;
	}

	private sealed class WeeklyDownloadSE : Nav
	{
		public WeeklyDownloadSE() : base($"{nameof(Id.WeeklyDownload)}", Id.WeeklyDownload) { }
		public override string Index => "/WeeklyDownload";
		public override string Title => "Weekly Download";
		public override string Icon => "fas fa-file-pdf";
		public override int Parent => NavGroup.WeeklyDownload.Value;
		public override int Sort => 1;
		public override int RequiredRoles => RoleEnum.WeeklyDownload.Value | RoleEnum.Admin.Value;
	}

	private sealed class SpecialEventsSE : Nav
	{
		public SpecialEventsSE() : base($"{nameof(Id.SpecialEvents)}", Id.SpecialEvents) { }
		public override string Index => "/SpecialEvents";
		public override string Title => "Special Events";
		public override string Icon => "far fa-clock";
		public override int Parent => NavGroup.SpecialEvents.Value;
		public override int Sort => 1;
		public override int RequiredRoles => RoleEnum.Announcements.Value | RoleEnum.Admin.Value; // Announcements or Admin role
	}

	private sealed class KeyDatesSE : Nav
	{
		public KeyDatesSE() : base($"{nameof(Id.KeyDates)}", Id.KeyDates) { }
		public override string Index => "/KeyDates";
		public override string Title => "Key Dates";
		public override string Icon => "far fa-calendar-check";
		public override int Parent => NavGroup.KeyDates.Value;
		public override int Sort => 1;
		public override int RequiredRoles => RoleEnum.KeyDates.Value | RoleEnum.Admin.Value; // KeyDates or Admin role
	}

	private sealed class DatabaseSE : Nav
	{
		public DatabaseSE() : base($"{nameof(Id.Database)}", Id.Database) { }
		public override string Index => "/Database"; // /ErrorLog
		public override string Title => "Database Error Logs";
		public override string Icon => "fas fa-bomb";
		public override int Parent => NavGroup.Alerts.Value;
		public override int Sort => 1;
		public override int RequiredRoles => RoleEnum.Admin.Value; // Admin only
	}

	private sealed class HealthCheckCalendarSE : Nav
	{
		public HealthCheckCalendarSE() : base($"{nameof(Id.HealthCheckCalendar)}", Id.HealthCheckCalendar) { }
		public override string Index => "/HealthCheck/Calendar";
		public override string Title => "Health Check | Calendar";
		public override string Icon => "fas fa-heartbeat";
		public override int Parent => NavGroup.HealthChecks.Value;
		public override int Sort => 1;
		public override int RequiredRoles => RoleEnum.Admin.Value; // Admin only
	}

	private sealed class FeastTableSE : Nav
	{
		public FeastTableSE() : base($"{nameof(Id.FeastTable)}", Id.FeastTable) { }
		public override string Index => "/FeastTable";
		public override string Title => "Feast Table";
		public override string Icon => "fas fa-glass-cheers";
		public override int Parent => NavGroup.HealthChecks.Value;
		public override int Sort => 2;
		public override int RequiredRoles => RoleEnum.Admin.Value; 
	}


	#endregion

}



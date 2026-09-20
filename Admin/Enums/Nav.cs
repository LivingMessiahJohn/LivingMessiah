using Ardalis.SmartEnum;
using RoleEnum = Admin.Security.Enums.Role;
using SukkotHomeTab = Admin.Features.Sukkot.Home.Enums.Tab;

namespace Admin.Enums;

public record FolderConstants
{
	public const string FolderIcon = "far fa-folder";
}

public abstract class Nav : SmartEnum<Nav>
{
	#region Id's
	private static class Id
	{
		internal const int Home = 1;
		internal const int SukkotFolder = 2;
		internal const int SukkotDashboard = 3;
		internal const int SukkotCRUD = 4;
		internal const int SukkotSchedule = 5;
		internal const int WeeklyDownload = 6;
		internal const int SpecialEvents = 7;
		internal const int KeyDates = 8;
		internal const int Users = 15;
		internal const int Alerts = 9;
		internal const int HealthChecks = 10;
		internal const int Profile = 11;
		internal const int Database = 12;
		internal const int HealthCheckCalendar = 13;
		internal const int HealthCheckDailySchedule = 14;
	}
	#endregion

	#region Declared Public Instances
	public static readonly Nav Home = new HomeSE();
	public static readonly Nav SukkotFolder = new SukkotFolderSE();
	public static readonly Nav SukkotDashboard = new SukkotDashboardSE();
	public static readonly Nav SukkotCRUD = new SukkotCRUDSE();
	public static readonly Nav SukkotSchedule = new SukkotScheduleSE();
	public static readonly Nav WeeklyDownload = new WeeklyDownloadSE();
	public static readonly Nav SpecialEvents = new SpecialEventsSE();
	public static readonly Nav KeyDates = new KeyDatesSE();
	public static readonly Nav Users = new UsersSE();
	public static readonly Nav Alerts = new AlertsSE();
	public static readonly Nav HealthChecks = new HealthChecksSE();
	public static readonly Nav Profile = new ProfileSE();
	public static readonly Nav Database = new DatabaseSE();
	public static readonly Nav HealthCheckCalendar = new HealthCheckCalendarSE();
	public static readonly Nav HealthCheckDailySchedule = new HealthCheckDailyScheduleSE();
	#endregion

	private Nav(string name, int value) : base(name, value)
	{
	}

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract int Parent { get; }
	public abstract int Sort { get; }
	public abstract int RequiredRoles { get; }

	public virtual bool ExpandByDefault => false;

	public bool IsFolder => List.Any(n => n.Parent == Value);
	#endregion

	#region Private Instantiation

	private sealed class HomeSE : Nav
	{
		public HomeSE() : base($"{nameof(Id.Home)}", Id.Home) { }
		public override string Index => "/";
		public override string Title => "Home";
		public override string Icon => "fas fa-home";
		public override int Parent => 0;
		public override int Sort => 0;
		public override int RequiredRoles => 0;
		public override bool ExpandByDefault => true;
	}

	private sealed class SukkotFolderSE : Nav
	{
		public SukkotFolderSE() : base($"{nameof(Id.SukkotFolder)}", Id.SukkotFolder) { }
		public override string Index => ""; // Not Applicable as it is a subfolder
		public override string Title => "Sukkot";
		public override string Icon => "fas fa-campground";
		public override int Parent => Home.Value;
		public override int Sort => 3;
		public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value | RoleEnum.Admin.Value;
		public override bool ExpandByDefault => true;
	}

	private sealed class SukkotDashboardSE : Nav
	{
		public SukkotDashboardSE() : base($"{nameof(Id.SukkotDashboard)}", Id.SukkotDashboard) { }
		public override string Index => SukkotHomeTab.Dashboard.Index;
		public override string Title => SukkotHomeTab.Dashboard.Title;
		public override string Icon => SukkotHomeTab.Dashboard.Icon;
		public override int Parent => SukkotFolder.Value;
		public override int Sort => 1;
		public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value | RoleEnum.Admin.Value;
	}

	private sealed class SukkotCRUDSE : Nav
	{
		public SukkotCRUDSE() : base($"{nameof(Id.SukkotCRUD)}", Id.SukkotCRUD) { }
		public override string Index => SukkotHomeTab.CRUD.Index;
		public override string Title => SukkotHomeTab.CRUD.Title;
		public override string Icon => SukkotHomeTab.CRUD.Icon;
		public override int Parent => SukkotFolder.Value;
		public override int Sort => 2;
		public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value | RoleEnum.Admin.Value;
	}

	private sealed class SukkotScheduleSE : Nav
	{
		public SukkotScheduleSE() : base($"{nameof(Id.SukkotSchedule)}", Id.SukkotSchedule) { }
		public override string Index => SukkotHomeTab.DailySchedule.Index;
		public override string Title => SukkotHomeTab.DailySchedule.Title;
		public override string Icon => SukkotHomeTab.DailySchedule.Icon;
		public override int Parent => SukkotFolder.Value;
		public override int Sort => 3;
		public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value | RoleEnum.Admin.Value;
	}

	private sealed class WeeklyDownloadSE : Nav
	{
		public WeeklyDownloadSE() : base($"{nameof(Id.WeeklyDownload)}", Id.WeeklyDownload) { }
		public override string Index => "/WeeklyDownload";
		public override string Title => "Weekly Download";
		public override string Icon => "fas fa-file-pdf";
		public override int Parent => Home.Value;
		public override int Sort => 1;
		public override int RequiredRoles => RoleEnum.WeeklyDownload.Value | RoleEnum.Admin.Value;
	}

	private sealed class SpecialEventsSE : Nav
	{
		public SpecialEventsSE() : base($"{nameof(Id.SpecialEvents)}", Id.SpecialEvents) { }
		public override string Index => "/SpecialEvents";
		public override string Title => "Special Events";
		public override string Icon => "far fa-clock";
		public override int Parent => Home.Value;
		public override int Sort => 2;
		public override int RequiredRoles => RoleEnum.Announcements.Value | RoleEnum.Admin.Value;
	}

	private sealed class KeyDatesSE : Nav
	{
		public KeyDatesSE() : base($"{nameof(Id.KeyDates)}", Id.KeyDates) { }
		public override string Index => "/KeyDates";
		public override string Title => "Key Dates";
		public override string Icon => "far fa-calendar-check";
		public override int Parent => Home.Value;
		public override int Sort => 4;
		public override int RequiredRoles => RoleEnum.KeyDates.Value | RoleEnum.Admin.Value;
	}

	private sealed class UsersSE : Nav
	{
		public UsersSE() : base($"{nameof(Id.Users)}", Id.Users) { }
		public override string Index => "/Users";
		public override string Title => "Auth0 Users";
		public override string Icon => "fas fa-users";
		public override int Parent => Home.Value;
		public override int Sort => 5;
		public override int RequiredRoles => RoleEnum.Admin.Value;
	}

	private sealed class AlertsSE : Nav
	{
		public AlertsSE() : base($"{nameof(Id.Alerts)}", Id.Alerts) { }
		public override string Index => "";
		public override string Title => "Logs and Alerts";
		public override string Icon => "fas fa-bell";
		public override int Parent => Home.Value;
		public override int Sort => 6;
		public override int RequiredRoles => RoleEnum.Admin.Value;
	}

	private sealed class HealthChecksSE : Nav
	{
		public HealthChecksSE() : base($"{nameof(Id.HealthChecks)}", Id.HealthChecks) { }
		public override string Index => "";
		public override string Title => "Health Checks";
		public override string Icon => "fas fa-heartbeat";
		public override int Parent => Home.Value;
		public override int Sort => 7;
		public override int RequiredRoles => RoleEnum.Admin.Value;
	}

	private sealed class ProfileSE : Nav
	{
		public ProfileSE() : base($"{nameof(Id.Profile)}", Id.Profile) { }
		public override string Index => "/Profile";
		public override string Title => "Profile";
		public override string Icon => "fab fa-superpowers";
		public override int Parent => 0;
		public override int Sort => 0;
		public override int RequiredRoles => 0;
	}

	private sealed class DatabaseSE : Nav
	{
		public DatabaseSE() : base($"{nameof(Id.Database)}", Id.Database) { }
		public override string Index => "/Database";
		public override string Title => "Database Error Logs";
		public override string Icon => "fas fa-bomb";
		public override int Parent => Alerts.Value;
		public override int Sort => 1;
		public override int RequiredRoles => RoleEnum.Admin.Value;
	}

	private sealed class HealthCheckCalendarSE : Nav
	{
		public HealthCheckCalendarSE() : base($"{nameof(Id.HealthCheckCalendar)}", Id.HealthCheckCalendar) { }
		public override string Index => "/HealthCheck/Calendar/Index";
		public override string Title => "Health Check | Calendar";
		public override string Icon => "fas fa-heartbeat";
		public override int Parent => HealthChecks.Value;
		public override int Sort => 1;
		public override int RequiredRoles => RoleEnum.Admin.Value;
	}

	private sealed class HealthCheckDailyScheduleSE : Nav
	{
		public HealthCheckDailyScheduleSE() : base($"{nameof(Id.HealthCheckDailySchedule)}", Id.HealthCheckDailySchedule) { }
		public override string Index => "/HealthCheck/DailySchedule/Index";
		public override string Title => "Health Check | Daily Schedule";
		public override string Icon => "fas fa-heartbeat";
		public override int Parent => HealthChecks.Value;
		public override int Sort => 2;
		public override int RequiredRoles => RoleEnum.Admin.Value;
	}
	#endregion
}

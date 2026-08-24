using Ardalis.SmartEnum;

using RoleEnum = Admin.Security.Enums.Role;
using SukkotEnumsMenuBar = Admin.Features.Sukkot.Enums.MenuBar;

namespace Admin.Enums;

//public static class Constants
public record FolderConstants
{
	public const string FolderIcon = "far fa-folder";
}

// ToDo: rename this to NavTree
public abstract class NavGroup : SmartEnum<NavGroup>
{
	#region Id's
	private static class Id
	{
		internal const int Home = 1; 
		
		internal const int SukkotHome = 2;
		
		internal const int SukkotDashboard = 3;
		internal const int SukkotCRUD = 4;
		internal const int SukkotSchedule = 5;

		internal const int WeeklyDownload = 6;
		internal const int SpecialEvents = 7;
		internal const int KeyDates = 8;

		internal const int Alerts = 9;
		
		internal const int HealthChecks = 10;
		
		internal const int Profile = 11;
	}
	#endregion

	#region Declared Public Instances
	public static readonly NavGroup Home = new HomeSE();
	public static readonly NavGroup Sukkot = new SukkotSE();
	public static readonly NavGroup SukkotDashboard = new SukkotDashboardSE();
	public static readonly NavGroup SukkotCRUD = new SukkotCRUDSE();

	public static readonly NavGroup WeeklyDownload = new WeeklyDownloadSE();
	public static readonly NavGroup SpecialEvents = new SpecialEventsSE();
	public static readonly NavGroup KeyDates = new KeyDatesSE();
	public static readonly NavGroup Alerts = new AlertsSE();
	public static readonly NavGroup HealthChecks = new HealthChecksSE();
	public static readonly NavGroup Profile = new ProfileSE();
	#endregion

	private NavGroup(string name, int value) : base(name, value)
	{
	}

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }

	public abstract int Parent { get; }
	public abstract string? FolderIcon { get; }
	public abstract string? FolderTitle { get; }
	//public abstract List<Nav>? Children { get; }  ToDo: see if this works

	public abstract int RequiredRoles { get; }
	#endregion

	#region Private Instantiation

	private sealed class HomeSE : NavGroup
	{
		public HomeSE() : base($"{nameof(Id.Home)}", Id.Home) { }
		public override string Index => "/";
		public override string Title => "Home";
		public override string Icon => "fas fa-home";

		public override int Parent => 0;
		public override string? FolderIcon => FolderConstants.FolderIcon; 
		public override string? FolderTitle => "Home";
		
		public override int RequiredRoles => 0; // No specific role required for Home
	}


	private sealed class SukkotSE : NavGroup
	{
		public SukkotSE() : base($"{nameof(Id.SukkotHome)}", Id.SukkotHome) { }
		public override string Index => "/SukkotHome";
		public override string Title => "Sukkot";
		public override string Icon => "fas fa-campground";

		public override int Parent => 0;
		public override string? FolderIcon => FolderConstants.FolderIcon;
		public override string FolderTitle => Title; 
	
		public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value | RoleEnum.Admin.Value; 
	}

	private sealed class SukkotDashboardSE : NavGroup
	{
		public SukkotDashboardSE() : base(Nav.SukkotDashboard.Name, Nav.SukkotDashboard.Value) { }
		public override string Index => SukkotEnumsMenuBar.Dashboard.Index;
		public override string Title => SukkotEnumsMenuBar.Dashboard.Title;
		public override string Icon => SukkotEnumsMenuBar.Dashboard.Icon;

		public override int Parent => NavGroup.Sukkot.Value;
		public override string? FolderIcon => null;
		public override string? FolderTitle => null;

		public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value | RoleEnum.Admin.Value;
	}

	private sealed class SukkotCRUDSE : NavGroup
	{
		public SukkotCRUDSE() : base($"{nameof(Id.SukkotCRUD)}", Id.SukkotCRUD) { }
		public override string Index => SukkotEnumsMenuBar.CRUD.Index;
		public override string Title => SukkotEnumsMenuBar.CRUD.Title;
		public override string Icon => SukkotEnumsMenuBar.CRUD.Icon;

		public override int Parent => NavGroup.Sukkot.Value;
		public override string? FolderIcon => null;
		public override string? FolderTitle => null;

		public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value;
	}


	private sealed class WeeklyDownloadSE : NavGroup
	{
		public WeeklyDownloadSE() : base($"{nameof(Id.WeeklyDownload)}", Id.WeeklyDownload) { }
		public override string Index => "/WeeklyDownload";
		public override string Title => "Weekly Download";
		public override string Icon => "";

		public override int Parent => 0;
		public override string? FolderIcon => null;
		public override string? FolderTitle => null;
		
		public override int RequiredRoles => RoleEnum.WeeklyDownload.Value | RoleEnum.Admin.Value;
	}

	private sealed class SpecialEventsSE : NavGroup
	{
		public SpecialEventsSE() : base($"{nameof(Id.SpecialEvents)}", Id.SpecialEvents) { }
		public override string Index => "/SpecialEvents";
		public override string Title => "Special Events";
		public override string Icon => "far fa-clock";

		public override int Parent => 0;
		public override string? FolderIcon => null;
		public override string? FolderTitle => null;	

		public override int RequiredRoles => RoleEnum.Announcements.Value | RoleEnum.Admin.Value; 
	}

	private sealed class KeyDatesSE : NavGroup
	{
		public KeyDatesSE() : base($"{nameof(Id.KeyDates)}", Id.KeyDates) { }
		public override string Index => "/KeyDates";
		public override string Title => "Key Dates";
		public override string Icon => "far fa-calendar-check";

		public override int Parent => 0;
		public override string? FolderIcon => FolderTitle;
		public override string? FolderTitle => Title;
		public override int RequiredRoles => RoleEnum.KeyDates.Value | RoleEnum.Admin.Value; // KeyDates or Admin role
	}

	private sealed class AlertsSE : NavGroup
	{
		public AlertsSE() : base($"{nameof(Id.Alerts)}", Id.Alerts) { }
		public override string Index => "/Alerts";
		public override string Title => "Logs and Alerts";
		public override string Icon => "fas fa-bell";

		public override int Parent => 0;
		public override string? FolderIcon => FolderTitle;
		public override string? FolderTitle => Title;
		public override int RequiredRoles => RoleEnum.Admin.Value; // Admin only
	}

	private sealed class HealthChecksSE : NavGroup
	{
		public HealthChecksSE() : base($"{nameof(Id.HealthChecks)}", Id.HealthChecks) { }
		public override string Index => "/HealthChecks";
		public override string Title => "Health Checks";
		public override string Icon => "fas fa-heartbeat";

		public override int Parent => 0;
		public override string? FolderIcon => FolderTitle;
		public override string? FolderTitle => Title;

		public override int RequiredRoles => RoleEnum.Admin.Value; // Admin only
	}

	private sealed class ProfileSE : NavGroup
	{
		public ProfileSE() : base($"{nameof(Id.Profile)}", Id.Profile) { }
		public override string Index => "/Profile";
		public override string Title => "Profile";
		public override string Icon => "fab fa-superpowers";

		public override int Parent => 0;
		public override string? FolderIcon => FolderTitle;
		public override string? FolderTitle => Title;

		//public override int Sort => Id.Profile;
		//public override bool Disabled => false;
		public override int RequiredRoles => 0; // No specific role required
	}


	#endregion
}

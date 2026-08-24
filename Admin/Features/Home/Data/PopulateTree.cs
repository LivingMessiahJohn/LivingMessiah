using Admin.Enums;
using Admin.Features.Home;
using MenuBar = Admin.Features.Sukkot.Enums.MenuBar;

namespace Admin.Features.Home.Data;

public static class PopulateTree
{
	public static List<NavTreeItem> Get() =>
	[
		new NavTreeItem
		{
			Text = NavGroup.Home.Title,
			Title = NavGroup.Home.Title,
			Index = NavGroup.Home.Index,
			IconClass = FolderConstants.FolderIcon,
			OpenIconClass = "far fa-folder-open",
			IsExpanded = true,
			RequiredRoles = NavGroup.Home.RequiredRoles,
			Children =
			{
				new NavTreeItem
				{
					Text = NavGroup.WeeklyDownload.Title,
					Title = NavGroup.WeeklyDownload.Title,
					Index = NavGroup.WeeklyDownload.Index,
					IconClass = "fas fa-file-pdf",
					RequiredRoles = NavGroup.WeeklyDownload.RequiredRoles
				},
				new NavTreeItem
				{
					Text = NavGroup.SpecialEvents.Title,
					Title = NavGroup.SpecialEvents.Title,
					Index = NavGroup.SpecialEvents.Index,
					IconClass = NavGroup.SpecialEvents.Icon,
					RequiredRoles = NavGroup.SpecialEvents.RequiredRoles
				},
				new NavTreeItem
				{
					Text = NavGroup.Sukkot.Title,
					Title = NavGroup.Sukkot.Title,
					Index = NavGroup.Sukkot.Index,
					IconClass = FolderConstants.FolderIcon,
					OpenIconClass = "far fa-folder-open",
					IsExpanded = true,
					RequiredRoles = NavGroup.Sukkot.RequiredRoles,
					Children =
					{
						new NavTreeItem
						{
							Text = MenuBar.Dashboard.Title,
							Title = MenuBar.Dashboard.Title,
							Index = MenuBar.Dashboard.Index,
							IconClass = MenuBar.Dashboard.Icon,
							RequiredRoles = NavGroup.SukkotDashboard.RequiredRoles
						},
						new NavTreeItem
						{
							Text = MenuBar.CRUD.Title,
							Title = MenuBar.CRUD.Title,
							Index = MenuBar.CRUD.Index,
							IconClass = MenuBar.CRUD.Icon,
							RequiredRoles = NavGroup.SukkotCRUD.RequiredRoles
						},
						new NavTreeItem
						{
							Text = MenuBar.DailySchedule.Title,
							Title = MenuBar.DailySchedule.Title,
							Index = MenuBar.DailySchedule.Index,
							IconClass = MenuBar.DailySchedule.Icon,
							RequiredRoles = NavGroup.Sukkot.RequiredRoles
						}
					}
				},
				new NavTreeItem
				{
					Text = NavGroup.KeyDates.Title,
					Title = NavGroup.KeyDates.Title,
					Index = NavGroup.KeyDates.Index,
					IconClass = NavGroup.KeyDates.Icon,
					RequiredRoles = NavGroup.KeyDates.RequiredRoles
				},
				new NavTreeItem
				{
					Text = NavGroup.Alerts.Title,
					Title = NavGroup.Alerts.Title,
					Index = NavGroup.Alerts.Index,
					IconClass = FolderConstants.FolderIcon,
					OpenIconClass = "far fa-folder-open",
					RequiredRoles = NavGroup.Alerts.RequiredRoles,
					Children =
					{
						new NavTreeItem
						{
							Text = Nav.Database.Title,
							Title = Nav.Database.Title,
							Index = Nav.Database.Index,
							IconClass = Nav.Database.Icon,
							RequiredRoles = NavGroup.Alerts.RequiredRoles
						}
					}
				},
				new NavTreeItem
				{
					Text = NavGroup.HealthChecks.Title,
					Title = NavGroup.HealthChecks.Title,
					Index = NavGroup.HealthChecks.Index,
					IconClass = FolderConstants.FolderIcon,
					OpenIconClass = "far fa-folder-open",
					RequiredRoles = NavGroup.HealthChecks.RequiredRoles,
					Children =
					{
						new NavTreeItem
						{
							Text = Nav.HealthCheckCalendar.Title,
							Title = Nav.HealthCheckCalendar.Title,
							Index = Nav.HealthCheckCalendar.Index,
							IconClass = Nav.HealthCheckCalendar.Icon,
							RequiredRoles = NavGroup.HealthChecks.RequiredRoles
						},
						new NavTreeItem
						{
							Text = Nav.FeastTable.Title,
							Title = Nav.FeastTable.Title,
							Index = Nav.FeastTable.Index,
							IconClass = Nav.FeastTable.Icon,
							RequiredRoles = NavGroup.HealthChecks.RequiredRoles
						}
					}
				}
			}
		}
	];
}

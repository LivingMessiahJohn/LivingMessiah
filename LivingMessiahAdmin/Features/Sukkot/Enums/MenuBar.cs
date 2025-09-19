using Ardalis.SmartEnum;

namespace LivingMessiahAdmin.Features.Sukkot.Enums;


/*
By using `RoleFlag` can I make MenuBar more declaritve with regard to who can see what?

	
Extra Fields
	public abstract bool HasAccess(PageListType pageListType);

Example
  public ReportSE() : base($"{nameof(Id.Report)}", Id.Report) { }
	public override bool HasAccess(bool Role) => (aaa) || (bbb);

[Flags]
public enum RoleFlag
{
	//All = -1,
	//None = 0,
	Admin = 1,
	Announcements = 2,
	KeyDates = 4,
	Sukkot = 8,
	SukkotHost = 16
}
*/

public abstract class MenuBar : SmartEnum<MenuBar>
{
	#region Id's
	private static class Id
	{
		internal const int Home = 1;
		internal const int Notes = 2;
		internal const int Report = 3;  // or AttendanceReport
		internal const int LegalAgreementVerbiage = 4;
		internal const int Dashboard = 5;
		//internal const int DeleteConfirmation = 2;

	}
	#endregion

	private MenuBar(string name, int value) : base(name, value) { } // Constructor

	#region  Declared Public Instances
	public static readonly MenuBar Home = new HomeSE();
	public static readonly MenuBar Notes = new NotesSE();
	public static readonly MenuBar Report = new ReportSE();
	public static readonly MenuBar LegalAgreementVerbiage = new LegalAgreementVerbiageSE();
	public static readonly MenuBar Dashboard = new DashboardSE();
	// SE=SmartEnum
	#endregion

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract string Css { get; }
	#endregion

	#region Private Instantiation

	private sealed class HomeSE : MenuBar
	{
		public HomeSE() : base($"{nameof(Id.Home)}", Id.Home) { }
		public override string Index => "/Sukkot";
		public override string Title => "Registration CrUD";
		public override string Icon => "fas fa-campground"; //fas fa-home fas fa-mask
		public override string Css => "badge bg-dark";
	}
	private sealed class NotesSE : MenuBar
	{
		public NotesSE() : base($"{nameof(Id.Notes)}", Id.Notes) { }
		public override string Index => "/Sukkot/Notes";
		public override string Title => "Sukkot Registration Notes";
		public override string Icon => "far fa-sticky-note";
		public override string Css => "badge bg-info text-black";
	}

	private sealed class ReportSE : MenuBar
	{
		public ReportSE() : base($"{nameof(Id.Report)}", Id.Report) { }
		public override string Index => "/Sukkot/AttendanceAllFeastDays";
		public override string Title => "Attendance Count";
		public override string Icon => "fas fa-calculator";   // ToDo: use this with Chart fas fa-chart-line
		public override string Css => "badge bg-warning text-black";
	}

	private sealed class LegalAgreementVerbiageSE : MenuBar
	{
		public LegalAgreementVerbiageSE() : base($"{nameof(Id.LegalAgreementVerbiage)}", Id.LegalAgreementVerbiage) { }
		public override string Index => "/Sukkot/LegalAgreementVerbiage";
		public override string Title => "Legal Agreement Verbiage";
		public override string Icon => "fas fa-balance-scale";   // "fas fa-handshake" "far fa-handshake"
		public override string Css => "badge bg-secondary";
	}

	private sealed class DashboardSE : MenuBar
	{
		public DashboardSE() : base($"{nameof(Id.Dashboard)}", Id.Dashboard) { }
		public override string Index => "/SukkotDashboard";
		public override string Title => "Registration Dashboard";
		public override string Icon => "fas fa-tachometer-alt";
		public override string Css => "badge bg-danger text-black";
	}

	#endregion


	/*
	ToDo: See C:\Source\LivingeMessiahBackup\999-Delete-Registration\Notes.md
	public static readonly NavButton DeleteConfirmation = new DeleteConfirmationSE(); 
	*/


	/*
	private sealed class DeleteConfirmationSE : NavButton
	{
		public DeleteConfirmationSE() : base($"{nameof(Id.DeleteConfirmation)}", Id.DeleteConfirmation) { }
		public override string Route => Pages.DeleteConfirmation.Index; 
		public override string Title => "Delete";
		public override string Icon => "fas fa-times";
		//public override string Css => "btn btn-outline-danger";
	}
	*/
}
// Ignore Spelling: Css 
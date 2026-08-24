using Ardalis.SmartEnum;

namespace Admin.Features.Sukkot.Dashboard.Enums;


public abstract class MenuBarDB : SmartEnum<MenuBarDB>
{
	#region Id's
	private static class Id
	{
		//internal const int Home = 1;
		//internal const int Dashboard = 2;
		//internal const int CRUD = 3;
		internal const int Notes = 4;
		internal const int Report = 5;  // or AttendanceReport
		//internal const int LegalAgreementVerbiage = 6;
		internal const int StripeTable = 7;

	}
	#endregion

	private MenuBarDB(string name, int value) : base(name, value) { } // Constructor

	#region  Declared Public Instances
	//public static readonly MenuBarDB Home = new HomeSE();
	//public static readonly MenuBarDB Dashboard = new DashboardSE();
	//public static readonly MenuBarDB CRUD = new CRUDSE();
	public static readonly MenuBarDB Notes = new NotesSE();
	public static readonly MenuBarDB Report = new ReportSE();
	//public static readonly MenuBarDB LegalAgreementVerbiage = new LegalAgreementVerbiageSE();
	public static readonly MenuBarDB StripeTable = new StripeTableSE();

	// SE=SmartEnum
	#endregion

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract string Css { get; }
	#endregion

	#region Private Instantiation

	/*
	private sealed class HomeSE : MenuBarDB
	{
		public HomeSE() : base($"{nameof(Id.Home)}", Id.Home) { }
		public override string Index => "/SukkotHome";
		public override string Title => "Registration Home";
		public override string Icon => "fas fa-campground";
		public override string Css => "badge bg-danger text-black";
	}

	private sealed class DashboardSE : MenuBarDB
	{
		public DashboardSE() : base($"{nameof(Id.Dashboard)}", Id.Dashboard) { }
		public override string Index => "/SukkotDashboard";
		public override string Title => "Registration Dashboard";
		public override string Icon => "fas fa-tachometer-alt";
		public override string Css => "badge bg-danger text-black";
	}

	private sealed class CRUDSE : MenuBarDB
	{
		public CRUDSE() : base($"{nameof(Id.CRUD)}", Id.CRUD) { }
		public override string Index => "/SukkotCRUD";
		public override string Title => "Registration CRUD";
		public override string Icon => "fas fa-hammer"; //fas fa-home fas fa-mask  
		public override string Css => "badge bg-dark";
	}

	*/

	private sealed class NotesSE : MenuBarDB
	{
		public NotesSE() : base($"{nameof(Id.Notes)}", Id.Notes) { }
		public override string Index => "/SukkotDashboard/Notes";
		public override string Title => "Registration Notes";
		public override string Icon => "far fa-sticky-note";
		public override string Css => "badge bg-info text-black";
	}

	private sealed class ReportSE : MenuBarDB
	{
		public ReportSE() : base($"{nameof(Id.Report)}", Id.Report) { }
		public override string Index => "/SukkotDashboard/AttendanceAllFeastDays";
		public override string Title => "Attendance Count";
		public override string Icon => "fas fa-calculator";   // ToDo: use this with Chart fas fa-chart-line
		public override string Css => "badge bg-warning text-black";
	}

	private sealed class StripeTableSE : MenuBarDB
	{
		public StripeTableSE() : base($"{nameof(Id.StripeTable)}", Id.StripeTable) { }
		public override string Index => "/SukkotDashboard/StripeTable";
		public override string Title => "Stripe Table";
		public override string Icon => "fab fa-cc-stripe"; 
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
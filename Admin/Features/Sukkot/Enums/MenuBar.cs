using Ardalis.SmartEnum;

namespace Admin.Features.Sukkot.Enums;


public abstract class MenuBar : SmartEnum<MenuBar>
{
	#region Id's
	private static class Id
	{
		internal const int Home = 1;
		internal const int Dashboard = 2;
		internal const int CRUD = 3;
		internal const int Notes = 4;
		internal const int Report = 5;  // or AttendanceReport
		internal const int StripeTable = 6;
		internal const int DailySchedule = 7;
		internal const int AddAgreement = 8;
		internal const int AgreementVerbiage = 9;

	}
	#endregion

	private MenuBar(string name, int value) : base(name, value) { } // Constructor

	#region  Declared Public Instances
	public static readonly MenuBar Home = new HomeSE();
	public static readonly MenuBar Dashboard = new DashboardSE(); // ToDo: Delete, as it's a Tab under SukkotHome
	public static readonly MenuBar CRUD = new CRUDSE();           // ToDo: Delete, as it's a Tab under SukkotHome
	public static readonly MenuBar Notes = new NotesSE();
	public static readonly MenuBar Report = new ReportSE();
	public static readonly MenuBar StripeTable = new StripeTableSE();
	public static readonly MenuBar DailySchedule = new DailyScheduleSE();
	public static readonly MenuBar AddAgreement = new AddAgreementSE();
	public static readonly MenuBar AgreementVerbiage = new AgreementVerbiageSE();
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
		public override string Index => "/SukkotHome";
		public override string Title => "Registration Home";
		public override string Icon => "fas fa-campground";
		public override string Css => "badge bg-danger text-black";
	}

	private sealed class DashboardSE : MenuBar
	{
		public DashboardSE() : base($"{nameof(Id.Dashboard)}", Id.Dashboard) { }
		public override string Index => "/SukkotDashboard";  // ToDo: Delete, as it's a Tab under SukkotHome
		public override string Title => "Registration Dashboard";
		public override string Icon => "fas fa-tachometer-alt";
		public override string Css => "badge bg-primary text-white";
	}

	private sealed class NotesSE : MenuBar
	{
		public NotesSE() : base($"{nameof(Id.Notes)}", Id.Notes) { }
		public override string Index => "/SukkotDashboard/Notes";
		public override string Title => "Registration Notes";
		public override string Icon => "far fa-sticky-note";
		public override string Css => "badge bg-info text-black";
	}

	private sealed class ReportSE : MenuBar
	{
		public ReportSE() : base($"{nameof(Id.Report)}", Id.Report) { }
		public override string Index => "/SukkotDashboard/AttendanceAllFeastDays";
		public override string Title => "Attendance Count";
		public override string Icon => "fas fa-calculator";   // ToDo: use this with Chart fas fa-chart-line
		public override string Css => "badge bg-warning text-black";
	}

	private sealed class StripeTableSE : MenuBar
	{
		public StripeTableSE() : base($"{nameof(Id.StripeTable)}", Id.StripeTable) { }
		public override string Index => "/SukkotDashboard/StripeTable";
		public override string Title => "Stripe Table";
		public override string Icon => "fab fa-cc-stripe"; 
		public override string Css => "badge bg-dark";
	}

	private sealed class CRUDSE : MenuBar
	{
		public CRUDSE() : base($"{nameof(Id.CRUD)}", Id.CRUD) { }
		public override string Index => "/SukkotCRUD";  // ToDo: Delete, as it's a Tab under SukkotHome
		public override string Title => "Registration CRUD";
		public override string Icon => "fas fa-hammer"; //fas fa-home fas fa-mask  
		public override string Css => "badge bg-danger text-white";
	}

	private sealed class DailyScheduleSE : MenuBar
	{
		public DailyScheduleSE() : base($"{nameof(Id.DailySchedule)}", Id.DailySchedule) { }
		public override string Index => "/SukkotSchedule";
		public override string Title => "Sukkot Daily Schedule";
		public override string Icon => "fas fa-tasks";
		public override string Css => "badge bg-warning text-black";
		//public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value | RoleEnum.Admin.Value;
	}

	private sealed class AddAgreementSE : MenuBar
	{
		public AddAgreementSE() : base($"{nameof(Id.AddAgreement)}", Id.AddAgreement) { }
		public override string Index => ""; // ToDo: this isn't a thing because it's related VisibleComponent.AddAgreement
		public override string Title => "Add Agreement";
		public override string Icon => "fas fa-plus";
		public override string Css => "badge bg-warning text-black";
		//public override int RequiredRoles => RoleEnum.Sukkot.Value | RoleEnum.SukkotHost.Value | RoleEnum.Admin.Value;
	}
	
	private sealed class AgreementVerbiageSE : MenuBar
	{
		public AgreementVerbiageSE() : base($"{nameof(Id.AgreementVerbiage)}", Id.AgreementVerbiage) { }
		public override string Index => ""; // ToDo: this isn't a thing because it's related VisibleComponent.AgreementVerbiage
		public override string Title => "Legal Agreement Verbiage";
		public override string Icon => "fas fa-balance-scale";   // "fas fa-handshake" "far fa-handshake"
		public override string Css => "badge bg-secondary";
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
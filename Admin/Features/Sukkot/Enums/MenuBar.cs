using Ardalis.SmartEnum;
using TabEnum = Admin.Features.Sukkot.Home.Enums.Tab;

namespace Admin.Features.Sukkot.Enums;


public abstract class MenuBar : SmartEnum<MenuBar>
{
	#region Id's
	private static class Id
	{
		internal const int Home = 1;
		internal const int Dashboard = 2;
		internal const int CRUD = 3;
		internal const int DailySchedule = 4;
		internal const int AddAgreement = 5;
		internal const int AgreementVerbiage = 6;
		internal const int AddRegistration = 7;
		internal const int EditRegistration = 8;

	}
	#endregion

	private MenuBar(string name, int value) : base(name, value) { } // Constructor

	#region  Declared Public Instances
	public static readonly MenuBar Home = new HomeSE();
	public static readonly MenuBar Dashboard = new DashboardSE(); // ToDo: Delete, as it's a Tab under SukkotHome
	public static readonly MenuBar CRUD = new CRUDSE();           // ToDo: Delete, as it's a Tab under SukkotHome
	public static readonly MenuBar DailySchedule = new DailyScheduleSE();
	public static readonly MenuBar AddAgreement = new AddAgreementSE();
	public static readonly MenuBar AgreementVerbiage = new AgreementVerbiageSE();
	public static readonly MenuBar AddRegistration = new AddRegistrationSE();
	public static readonly MenuBar EditRegistration = new EditRegistrationSE();
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
		public override string Index => TabEnum.Dashboard.Index;
		public override string Title => "Registration Home";
		public override string Icon => "fas fa-campground";
		public override string Css => "badge bg-danger text-black";
	}

	private sealed class DashboardSE : MenuBar
	{
		public DashboardSE() : base($"{nameof(Id.Dashboard)}", Id.Dashboard) { }
		public override string Index => TabEnum.Dashboard.Index;  // ToDo: Delete, as it's a Tab under SukkotHome
		public override string Title => "Registration Dashboard";
		public override string Icon => "fas fa-tachometer-alt";
		public override string Css => "badge bg-primary text-white";
	}

	private sealed class CRUDSE : MenuBar
	{
		public CRUDSE() : base($"{nameof(Id.CRUD)}", Id.CRUD) { }
		public override string Index => TabEnum.CRUD.Index;  // ToDo: Delete, as it's a Tab under SukkotHome
		public override string Title => "Registration CRUD";
		public override string Icon => "fas fa-hammer"; //fas fa-home fas fa-mask  
		public override string Css => "badge bg-danger text-white";
	}

	private sealed class DailyScheduleSE : MenuBar
	{
		public DailyScheduleSE() : base($"{nameof(Id.DailySchedule)}", Id.DailySchedule) { }
		public override string Index => TabEnum.DailySchedule.Index;
		public override string Title => "Daily Schedule";
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

	private sealed class AddRegistrationSE : MenuBar
	{
		public AddRegistrationSE() : base($"{nameof(Id.AddRegistration)}", Id.AddRegistration) { }
		public override string Index => "";
		public override string Title => "Add Registration";
		public override string Icon => "fas fa-plus";
		public override string Css => "badge bg-success text-white";
	}

	private sealed class EditRegistrationSE : MenuBar
	{
		public EditRegistrationSE() : base($"{nameof(Id.EditRegistration)}", Id.EditRegistration) { }
		public override string Index => "";
		public override string Title => "Edit Registration";
		public override string Icon => "fas fa-pencil-alt";
		public override string Css => "badge bg-primary text-white";
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
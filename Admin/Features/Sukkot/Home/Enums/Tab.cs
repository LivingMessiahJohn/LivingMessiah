using Ardalis.SmartEnum;

namespace Admin.Features.Sukkot.Home.Enums;


public abstract class Tab : SmartEnum<Tab>
{
	#region Id's
	private static class Id
	{
		internal const int Dashboard = 1;
		internal const int CRUD = 2;
		internal const int DailySchedule = 3;

	}
	#endregion

	private Tab(string name, int value) : base(name, value) { } // Constructor

	#region  Declared Public Instances
	public static readonly Tab Dashboard = new DashboardSE();
	public static readonly Tab CRUD = new CRUDSE();
	public static readonly Tab DailySchedule = new DailyScheduleSE();
	// SE=SmartEnum
	#endregion

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract string Css { get; }
	#endregion

	#region Private Instantiation

	private sealed class DashboardSE : Tab
	{
		public DashboardSE() : base($"{nameof(Id.Dashboard)}", Id.Dashboard) { }
		public override string Index => $"/SukkotHome/{Name}";
		public override string Title => "Registration Dashboard";
		public override string Icon => "fas fa-tachometer-alt";
		public override string Css => "badge bg-primary text-white";
	}

	private sealed class CRUDSE : Tab
	{
		public CRUDSE() : base($"{nameof(Id.CRUD)}", Id.CRUD) { }
		public override string Index => $"/SukkotHome/{Name}";
		public override string Title => "Registration CRUD";
		public override string Icon => "fas fa-hammer";
		public override string Css => "badge bg-danger text-white";
	}

	private sealed class DailyScheduleSE : Tab
	{
		public DailyScheduleSE() : base($"{nameof(Id.DailySchedule)}", Id.DailySchedule) { }
		public override string Index => $"/SukkotHome/{Name}";
		public override string Title => "Daily Schedule";
		public override string Icon => "fas fa-tasks";
		public override string Css => "badge bg-warning text-black";
	}

	#endregion

}
// Ignore Spelling: Css

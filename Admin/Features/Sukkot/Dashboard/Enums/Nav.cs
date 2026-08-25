using Ardalis.SmartEnum;

namespace Admin.Features.Sukkot.Dashboard.Enums;


public abstract class Nav : SmartEnum<Nav>
{
	#region Id's
	private static class Id
	{
		internal const int Notes = 1;
		internal const int Report = 2; 
		internal const int StripeTable = 3;

	}
	#endregion

	private Nav(string name, int value) : base(name, value) { } // Constructor

	#region  Declared Public Instances
	public static readonly Nav Notes = new NotesSE();
	public static readonly Nav Report = new ReportSE();
	public static readonly Nav StripeTable = new StripeTableSE();

	// SE=SmartEnum
	#endregion

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract string Css { get; }
	#endregion

	#region Private Instantiation

	private sealed class NotesSE : Nav
	{
		public NotesSE() : base($"{nameof(Id.Notes)}", Id.Notes) { }
		public override string Index => "/SukkotDashboard/Notes";
		public override string Title => "Registration Notes";
		public override string Icon => "far fa-sticky-note";
		public override string Css => "badge bg-info text-black";
	}

	private sealed class ReportSE : Nav
	{
		public ReportSE() : base($"{nameof(Id.Report)}", Id.Report) { }
		public override string Index => "/SukkotDashboard/AttendanceAllFeastDays";
		public override string Title => "Attendance Count";
		public override string Icon => "fas fa-calculator";   // ToDo: use this with Chart fas fa-chart-line
		public override string Css => "badge bg-warning text-black";
	}

	private sealed class StripeTableSE : Nav
	{
		public StripeTableSE() : base($"{nameof(Id.StripeTable)}", Id.StripeTable) { }
		public override string Index => "/SukkotDashboard/StripeTable";
		public override string Title => "Stripe Table";
		public override string Icon => "fab fa-cc-stripe";
		public override string Css => "badge bg-dark";
	}
	#endregion

}
// Ignore Spelling: Css 
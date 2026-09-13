using Ardalis.SmartEnum;

namespace Admin.Features.Sukkot.DailySchedule.Enums;


public abstract class Nav : SmartEnum<Nav>
{
	#region Id's
	private static class Id
	{
		internal const int PocketModBuild = 1;
		internal const int PocketModPrintPreview8PagePDF = 2;
		internal const int MarkdownEdit = 3;
	}
	#endregion

	private Nav(string name, int value) : base(name, value) { } // Constructor

	#region  Declared Public Instances
	public static readonly Nav PocketModBuild = new PocketModBuildSE();
	public static readonly Nav PocketModPrintPreview8PagePDF = new PocketModPrintPreview8PagePDFSE();
	public static readonly Nav MarkdownEdit = new MarkdownEditSE();

	// SE=SmartEnum
	#endregion

	#region Extra Fields
	public abstract string Index { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract string Css { get; }
	#endregion

	#region Private Instantiation

	private sealed class PocketModBuildSE : Nav
	{
		public PocketModBuildSE() : base($"{nameof(Id.PocketModBuild)}", Id.PocketModBuild) { }
		public override string Index => "/SukkotSchedule/PocketMod/Build";
		public override string Title => "PocketMod Build";
		public override string Icon => "fas fa-layer-group";
		public override string Css => "badge bg-primary text-white";
	}

	private sealed class PocketModPrintPreview8PagePDFSE : Nav
	{
		public PocketModPrintPreview8PagePDFSE() : base($"{nameof(Id.PocketModPrintPreview8PagePDF)}", Id.PocketModPrintPreview8PagePDF) { }
		public override string Index => "/SukkotSchedule/PocketMod/PrintPreview8PagePDF";
		public override string Title => "PocketMod Print";
		public override string Icon => "fas fa-print";
		public override string Css => "badge bg-secondary text-white";
	}

	private sealed class MarkdownEditSE : Nav
	{
		public MarkdownEditSE() : base($"{nameof(Id.MarkdownEdit)}", Id.MarkdownEdit) { }
		public override string Index => "/SukkotSchedule/MarkdownEdit";
		public override string Title => "MarkDown Edit";
		public override string Icon => "fas fa-edit";
		public override string Css => "badge bg-info text-black";
	}
	#endregion

}
// Ignore Spelling: Css

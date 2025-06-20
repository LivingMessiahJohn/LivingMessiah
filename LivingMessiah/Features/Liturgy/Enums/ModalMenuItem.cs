using Ardalis.SmartEnum;

namespace LivingMessiah.Features.Liturgy.Enums;

public abstract class ModalMenuItem : SmartEnum<ModalMenuItem>
{
	#region Id's
	private static class Id
	{
		internal const int Instructions = 1;
		internal const int ShowImages = 2;
	}
	#endregion

	#region Declared Public Instances
	public static readonly ModalMenuItem Instructions = new InstructionsSE();
	public static readonly ModalMenuItem ShowImages = new ShowImagesSE();
	#endregion

	private ModalMenuItem(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Title { get; }
	public abstract string AntiTitle { get; }
	//public abstract string Category { get; }
	//public abstract int CategorySort { get; }

	//Properties
	//public string AntiTitle => this.Value == ShowImages.Value &&  ? "" : this.Title;

	#endregion

	private sealed class InstructionsSE : ModalMenuItem
	{
		public InstructionsSE() : base(nameof(Instructions), Id.Instructions) { }
		public override string Title => "Instructions";
		public override string AntiTitle => "Instructions";
		//public override string Category => "Instructions";
		//public override int CategorySort => 1;
	}


	private sealed class ShowImagesSE : ModalMenuItem
	{
		public ShowImagesSE() : base(nameof(ShowImages), Id.ShowImages) { }
		public override string Title => "Show Images";
		public override string AntiTitle => "Hide Images";
		//public override string Category => "Appendix";
		//public override int CategorySort => 2;
	}
	
}

using Ardalis.SmartEnum;

namespace LivingMessiah.Features.Liturgy.Enums;

public abstract class Direction : SmartEnum<Direction>
{
	#region Id's
	private static class Id
	{
		internal const int Previous = 1;
		internal const int Next = 2;
	}
	#endregion

	#region Declared Public Instances
	public static readonly Direction Previous = new PreviousSE();
	public static readonly Direction Next = new NextSE();
	#endregion

	private Direction(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string Icon { get; }
	public abstract string KeyboardKey { get; }
	public abstract string Margin { get; }

	#endregion

	private sealed class PreviousSE : Direction
	{
		public PreviousSE() : base(nameof(Previous), Id.Previous) { }
		public override string Icon => " fas fa-arrow-left";
		public override string KeyboardKey => "ArrowLeft";
		public override string Margin => " ms-1";
	}


	private sealed class NextSE : Direction
	{
		public NextSE() : base(nameof(Next), Id.Next) { }
		public override string Icon => " fas fa-arrow-right";
		public override string KeyboardKey => "ArrowRight";
		public override string Margin => " me-1";
	}

}

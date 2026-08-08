using Ardalis.SmartEnum;

namespace Admin.Data.Enums;

public abstract class Database : SmartEnum<Database>
{
	#region Id's
	private static class Id
	{
		internal const int LivingMessiah = 1;
		internal const int Sukkot = 2;
		internal const int SpecialEvent = 3;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Database LivingMessiah = new LivingMessiahSE();
	public static readonly Database Sukkot = new SukkotSE();
	public static readonly Database SpecialEvent = new SpecialEventSE();
	// SE=SmartEnum
	#endregion

	private Database(string name, int value) : base(name, value) { }  // Constructor

	#region Extra Fields
	public abstract string Title { get; }
	public abstract string ConnectionStringKey { get; }
	#endregion

	#region Private Instantiation

	private sealed class LivingMessiahSE : Database
	{
			public LivingMessiahSE() : base("LivingMessiah", Id.LivingMessiah) { }
			public override string Title => "Living Messiah";
			public override string ConnectionStringKey => "ConnectionStrings:LivingMessiah";
	}

	private sealed class SukkotSE : Database
	{
		public SukkotSE() : base("Sukkot", Id.Sukkot) { }
		public override string Title => "Sukkot";
		// Config key remains ConnectionStrings:Sukkot; catalog is SukkotRegistration (dbo objects).
		public override string ConnectionStringKey => "ConnectionStrings:Sukkot";
	}

	private sealed class SpecialEventSE : Database
	{
		public SpecialEventSE() : base("SpecialEvent", Id.SpecialEvent) { }
		public override string Title => "Special Event";
		public override string ConnectionStringKey => "ConnectionStrings:SpecialEvent";
	}

	#endregion

}
// Ignore Spelling: Abrv

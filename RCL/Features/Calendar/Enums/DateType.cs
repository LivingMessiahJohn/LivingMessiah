using Ardalis.SmartEnum;
using RCL.Features.Calendar.Constants;

namespace RCL.Features.Calendar.Enums;

public abstract class DateType : SmartEnum<DateType>
{
	#region Id's
	private static class Id
	{
		internal const int All = -1;
		internal const int None = 0;

		internal const int Month = 1;
		internal const int Feast = 2;
		internal const int Season = 3;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly DateType All = new AllSE();
	public static readonly DateType Month = new MonthSE();
	public static readonly DateType Feast = new FeastSE();
	public static readonly DateType Season = new SeasonSE();
	// SE=SmartEnum
	#endregion

	private DateType(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	#endregion

  #region Private Instantiation

  private sealed class AllSE : DateType
	{
		public AllSE() : base($"{nameof(Id.All)}", Id.All) { }
  }

	private sealed class FeastSE : DateType
	{
		public FeastSE() : base($"{nameof(Id.Feast)}", Id.Feast) { }
  }

	private sealed class MonthSE : DateType
	{
		public MonthSE() : base($"{nameof(Id.Month)}", Id.Month) { }
  }

	private sealed class SeasonSE : DateType
	{
		public SeasonSE() : base($"{nameof(Id.Season)}", Id.Season) { }
  }
	#endregion

}



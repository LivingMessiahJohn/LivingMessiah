using Ardalis.SmartEnum;
using RCL.Features.Calendar.Constants;

namespace RCL.Features.Calendar.Enums;

public abstract class Filter : SmartEnum<Filter>
{
	#region Id's
	private static class Id
	{
		internal const int All = -1;
		internal const int None = 0;

		internal const int Month = 1;
		internal const int Feast = 2;
		internal const int Season = 3;
		internal const int Parasha = 4;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Filter All = new AllSE();
	public static readonly Filter Month = new MonthSE();
	public static readonly Filter Feast = new FeastSE();
	public static readonly Filter Season = new SeasonSE();
	public static readonly Filter Parasha = new ParashaSE();
	// SE=SmartEnum
	#endregion

	private Filter(string name, int value) : base(name, value) { } // Constructor

	//#region Extra Fields
	//#endregion

  #region Private Instantiation

  private sealed class AllSE : Filter
	{
		public AllSE() : base($"{nameof(Id.All)}", Id.All) { }
  }

	private sealed class FeastSE : Filter
	{
		public FeastSE() : base($"{nameof(Id.Feast)}", Id.Feast) { }
  }

	private sealed class MonthSE : Filter
	{
		public MonthSE() : base($"{nameof(Id.Month)}", Id.Month) { }
  }

	private sealed class SeasonSE : Filter
	{
		public SeasonSE() : base($"{nameof(Id.Season)}", Id.Season) { }
  }

  private sealed class ParashaSE : Filter
  {
    public ParashaSE() : base($"{nameof(Id.Parasha)}", Id.Parasha) { }
  }

  #endregion

}

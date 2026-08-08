using Ardalis.SmartEnum;

namespace RCL.Features.Sukkot.Enums;

public abstract class AttendanceDate : SmartFlagEnum<AttendanceDate>
{
	#region Id's
	// This is a SmartEnum the leverages Bitwise, therefor all the Id values need to be powers of two
	private static class BitwiseId 
	{
		//internal const int All = -1;

		internal const int None = 0;
		internal const int Fri_09_25 = 1;
		internal const int Sat_09_26 = 2;
		internal const int Sun_09_27 = 4;
		internal const int Mon_09_28 = 8;
		internal const int Tue_09_29 = 16;
		internal const int Wed_09_30 = 32;
		internal const int Thu_10_01 = 64;
		internal const int Fri_10_02 = 128;
		internal const int Sat_10_03 = 256;
		internal const int Sun_10_04 = 512;
	}
	#endregion


	#region  Declared Public Instances
	//public static readonly AttendanceDate All = new AllSE();
	public static readonly AttendanceDate None = new NoneSE();

	public static readonly AttendanceDate Fri_09_25 = new Fri_09_25_SE();
	public static readonly AttendanceDate Sat_09_26 = new Sat_09_26_SE();
	public static readonly AttendanceDate Sun_09_27 = new Sun_09_27_SE();
	public static readonly AttendanceDate Mon_09_28 = new Mon_09_28_SE();
	public static readonly AttendanceDate Tue_09_29 = new Tue_09_29_SE();
	public static readonly AttendanceDate Wed_09_30 = new Wed_09_30_SE();
	public static readonly AttendanceDate Thu_10_01 = new Thu_10_01_SE();
	public static readonly AttendanceDate Fri_10_02 = new Fri_10_02_SE();
	public static readonly AttendanceDate Sat_10_03 = new Sat_10_03_SE();
	public static readonly AttendanceDate Sun_10_04 = new Sun_10_04_SE();
	// SE=SmartEnum
	#endregion

	private AttendanceDate(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Title { get; }
	public abstract DateTime Date { get; }
	public abstract DateRangeType DateRangeType { get; }
	public abstract int Week { get; }
  public abstract int Day { get; } // ToDo: is there a smarter way to do this? I already know the Dates

  // Properties

  #endregion


  #region Private Instantiation

  /*
	private sealed class AllSE : AttendanceDate
	{
		public AllSE() : base($"{nameof(Id.All)}", Id.All) { }
		public override string Title => "All";
		public override DateTime Date => DateTime.MaxValue;
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1; // N/A
	}
	*/


  private sealed class NoneSE : AttendanceDate
	{
		public NoneSE() : base($"{nameof(BitwiseId.None)}", BitwiseId.None) { }
		public override string Title => "None";
		public override DateTime Date => DateTime.MinValue;
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1; // N/A
    public override int Day => 0;
  }

	private sealed class Fri_09_25_SE : AttendanceDate
	{
		public Fri_09_25_SE() : base($"{nameof(BitwiseId.Fri_09_25)}", BitwiseId.Fri_09_25) { }
		public override string Title => "Fri 09/25";
		public override DateTime Date => Convert.ToDateTime("2026-09-25");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
    public override int Day => 25;
  }
	private sealed class Sat_09_26_SE : AttendanceDate
	{
		public Sat_09_26_SE() : base($"{nameof(BitwiseId.Sat_09_26)}", BitwiseId.Sat_09_26) { }
		public override string Title => "Sat 09/26";
		public override DateTime Date => Convert.ToDateTime("2026-09-26");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
    public override int Day => 26;
  }
	private sealed class Sun_09_27_SE : AttendanceDate
	{
		public Sun_09_27_SE() : base($"{nameof(BitwiseId.Sun_09_27)}", BitwiseId.Sun_09_27) { }
		public override string Title => "Sun 09/27";
		public override DateTime Date => Convert.ToDateTime("2026-09-27");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
    public override int Day => 27;
  }
	private sealed class Mon_09_28_SE : AttendanceDate
	{
		public Mon_09_28_SE() : base($"{nameof(BitwiseId.Mon_09_28)}", BitwiseId.Mon_09_28) { }
		public override string Title => "Mon 09/28";
		public override DateTime Date => Convert.ToDateTime("2026-09-28");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
    public override int Day => 28;
  }
	private sealed class Tue_09_29_SE : AttendanceDate
	{
		public Tue_09_29_SE() : base($"{nameof(BitwiseId.Tue_09_29)}", BitwiseId.Tue_09_29) { }
		public override string Title => "Tue 09/29";
		public override DateTime Date => Convert.ToDateTime("2026-09-29");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
    public override int Day => 29;
  }
	private sealed class Wed_09_30_SE : AttendanceDate
	{
		public Wed_09_30_SE() : base($"{nameof(BitwiseId.Wed_09_30)}", BitwiseId.Wed_09_30) { }
		public override string Title => "Wed 09/30";
		public override DateTime Date => Convert.ToDateTime("2026-09-30");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
    public override int Day => 30;
  }
	private sealed class Thu_10_01_SE : AttendanceDate
	{
		public Thu_10_01_SE() : base($"{nameof(BitwiseId.Thu_10_01)}", BitwiseId.Thu_10_01) { }
		public override string Title => "Thu 10/01";
		public override DateTime Date => Convert.ToDateTime("2026-10-01");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
    public override int Day => 1;
  }
	private sealed class Fri_10_02_SE : AttendanceDate
	{
		public Fri_10_02_SE() : base($"{nameof(BitwiseId.Fri_10_02)}", BitwiseId.Fri_10_02) { }
		public override string Title => "Fri 10/02";
		public override DateTime Date => Convert.ToDateTime("2026-10-02");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
    public override int Day => 2;
  }
	private sealed class Sat_10_03_SE : AttendanceDate
	{
		public Sat_10_03_SE() : base($"{nameof(BitwiseId.Sat_10_03)}", BitwiseId.Sat_10_03) { }
		public override string Title => "Sat 10/03";
		public override DateTime Date => Convert.ToDateTime("2026-10-03");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
    public override int Day => 3;
  }
	private sealed class Sun_10_04_SE : AttendanceDate
	{
		public Sun_10_04_SE() : base($"{nameof(BitwiseId.Sun_10_04)}", BitwiseId.Sun_10_04) { }
		public override string Title => "Sun 10/04";
		public override DateTime Date => Convert.ToDateTime("2026-10-04");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
    public override int Day => 4;
  }

	#endregion
}


/*
	DECLARE @RC int
	EXEC @RC = dbo.stpAttendanceDateCodeGen 

	-- 2026 note: when AttendanceMinDate is not Sunday, stpAttendanceDateCodeGen's
	-- join to tvfAttendanceTwoWeeks can omit mid-range days. Fill those classes
	-- from vwAttendanceDateSmartFlagEnumCodeGen (Week: first 7 days = 1, rest = 2).
 */

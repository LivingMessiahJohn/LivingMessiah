using Ardalis.SmartEnum;

namespace LivingMessiahSukkot.Features.Enums;

//  Used by: NormalUser\AttendanceDateList.razor
public abstract class AttendanceDate : SmartFlagEnum<AttendanceDate>
{
	#region Id's
	// This is a SmartEnum the leverages Bitwise, therefor all the Id values need to be powers of two
	private static class BitwiseId 
	{
		//internal const int All = -1;

		internal const int None = 0;
		internal const int Sun_10_05 = 1;
		internal const int Mon_10_06 = 2;
		internal const int Tue_10_07 = 4;
		internal const int Wed_10_08 = 8;
		internal const int Thu_10_09 = 16;
		internal const int Fri_10_10 = 32;
		internal const int Sat_10_11 = 64;
		internal const int Sun_10_12 = 128;
		internal const int Mon_10_13 = 256;
		internal const int Tue_10_14 = 512;
	}
	#endregion


	#region  Declared Public Instances
	//public static readonly AttendanceDate All = new AllSE();
	public static readonly AttendanceDate None = new NoneSE();

	public static readonly AttendanceDate Sun_10_05 = new Sun_10_05_SE();
	public static readonly AttendanceDate Mon_10_06 = new Mon_10_06_SE();
	public static readonly AttendanceDate Tue_10_07 = new Tue_10_07_SE();
	public static readonly AttendanceDate Wed_10_08 = new Wed_10_08_SE();
	public static readonly AttendanceDate Thu_10_09 = new Thu_10_09_SE();
	public static readonly AttendanceDate Fri_10_10 = new Fri_10_10_SE();
	public static readonly AttendanceDate Sat_10_11 = new Sat_10_11_SE();
	public static readonly AttendanceDate Sun_10_12 = new Sun_10_12_SE();
	public static readonly AttendanceDate Mon_10_13 = new Mon_10_13_SE();
	public static readonly AttendanceDate Tue_10_14 = new Tue_10_14_SE();
	// SE=SmartEnum
	#endregion

	private AttendanceDate(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Title { get; }
	public abstract DateTime Date { get; }
	public abstract DateRangeType DateRangeType { get; }
	public abstract int Week { get; }

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
	}

	private sealed class Sun_10_05_SE : AttendanceDate
	{
		public Sun_10_05_SE() : base($"{nameof(BitwiseId.Sun_10_05)}", BitwiseId.Sun_10_05) { }
		public override string Title => "Sun 10/05";
		public override DateTime Date => Convert.ToDateTime("2025-10-05");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Mon_10_06_SE : AttendanceDate
	{
		public Mon_10_06_SE() : base($"{nameof(BitwiseId.Mon_10_06)}", BitwiseId.Mon_10_06) { }
		public override string Title => "Mon 10/06";
		public override DateTime Date => Convert.ToDateTime("2025-10-06");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Tue_10_07_SE : AttendanceDate
	{
		public Tue_10_07_SE() : base($"{nameof(BitwiseId.Tue_10_07)}", BitwiseId.Tue_10_07) { }
		public override string Title => "Tue 10/07";
		public override DateTime Date => Convert.ToDateTime("2025-10-07");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Wed_10_08_SE : AttendanceDate
	{
		public Wed_10_08_SE() : base($"{nameof(BitwiseId.Wed_10_08)}", BitwiseId.Wed_10_08) { }
		public override string Title => "Wed 10/08";
		public override DateTime Date => Convert.ToDateTime("2025-10-08");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Thu_10_09_SE : AttendanceDate
	{
		public Thu_10_09_SE() : base($"{nameof(BitwiseId.Thu_10_09)}", BitwiseId.Thu_10_09) { }
		public override string Title => "Thu 10/09";
		public override DateTime Date => Convert.ToDateTime("2025-10-09");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Fri_10_10_SE : AttendanceDate
	{
		public Fri_10_10_SE() : base($"{nameof(BitwiseId.Fri_10_10)}", BitwiseId.Fri_10_10) { }
		public override string Title => "Fri 10/10";
		public override DateTime Date => Convert.ToDateTime("2025-10-10");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Sat_10_11_SE : AttendanceDate
	{
		public Sat_10_11_SE() : base($"{nameof(BitwiseId.Sat_10_11)}", BitwiseId.Sat_10_11) { }
		public override string Title => "Sat 10/11";
		public override DateTime Date => Convert.ToDateTime("2025-10-11");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 1;
	}
	private sealed class Sun_10_12_SE : AttendanceDate
	{
		public Sun_10_12_SE() : base($"{nameof(BitwiseId.Sun_10_12)}", BitwiseId.Sun_10_12) { }
		public override string Title => "Sun 10/12";
		public override DateTime Date => Convert.ToDateTime("2025-10-12");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
	}
	private sealed class Mon_10_13_SE : AttendanceDate
	{
		public Mon_10_13_SE() : base($"{nameof(BitwiseId.Mon_10_13)}", BitwiseId.Mon_10_13) { }
		public override string Title => "Mon 10/13";
		public override DateTime Date => Convert.ToDateTime("2025-10-13");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
	}
	private sealed class Tue_10_14_SE : AttendanceDate
	{
		public Tue_10_14_SE() : base($"{nameof(BitwiseId.Tue_10_14)}", BitwiseId.Tue_10_14) { }
		public override string Title => "Tue 10/14";
		public override DateTime Date => Convert.ToDateTime("2025-10-14");
		public override DateRangeType DateRangeType => DateRangeType.Attendance;
		public override int Week => 2;
	}

	#endregion
}


/*
	DECLARE @RC int
	EXEC @RC = Sukkot.stpAttendanceDateCodeGen 

 */
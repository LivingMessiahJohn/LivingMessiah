using Ardalis.SmartEnum;

namespace LivingMessiahSukkot.Features.Enums;

public abstract class DateRangeType : SmartEnum<DateRangeType>
{
	#region Id's
	private static class Id
	{
		internal const int Attendance = 1;
		internal const int Lodging = 2; // Not being used
	}
	#endregion

	#region  Declared Public Instances
	public static readonly DateRangeType Attendance = new AttendanceSE();
	public static readonly DateRangeType Lodging = new LodgingSE();
	// SE=SmartEnum
	#endregion

	private DateRangeType(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Title { get; }
	public abstract DateRange Range { get; }
	public abstract DateRange? Range2ndMonth { get; }
	public abstract bool HasSecondMonth { get; }
	//public abstract List<DayRecord>? DayList { get; }
	#endregion


	#region Private Instantiation
	// Configuration with NO second month
	private sealed class AttendanceSE : DateRangeType
	{
		public AttendanceSE() : base($"{nameof(Id.Attendance)}", Id.Attendance) { }
		public override string Title => "Attendance Date";
		public override DateRange Range => new DateRange(Constants.DateRange.Attendance.Start, Constants.DateRange.Attendance.Finish);
		public override DateRange? Range2ndMonth => null;
		public override bool HasSecondMonth => false;
	}

	private sealed class LodgingSE : DateRangeType
	{
		public LodgingSE() : base($"{nameof(Id.Lodging)}", Id.Lodging) { }
		public override string Title => "Lodging Date";
		public override DateRange Range => new DateRange(Constants.DateRange.Lodging.Start, Constants.DateRange.Lodging.Finish);
		public override DateRange? Range2ndMonth => null;
		public override bool HasSecondMonth => false;
		/*
		public override List<DayRecord> DayList => [
		new DayRecord(1, 1, "Sun 10/5", 1),
		new DayRecord(2, 2, "Mon 10/6", 1),
		new DayRecord(3, 4, "Tue 10/7", 1),
		//
		new DayRecord(3, 512, "Tue 10/14", 1),
		];
		*/
	}

	/*
	# Configuration WITH second month

	private sealed class AttendanceSE : DateRangeType
	{
		public AttendanceSE() : base($"{nameof(Id.Attendance)}", Id.Attendance) { }
		public override string Title => "Attendance Date";
		public override DateRange Range => new DateRange(Convert.ToDateTime("2023-09-29"), Convert.ToDateTime("2023-09-30"));
		public override DateRange Range2ndMonth => new DateRange(Convert.ToDateTime("2023-10-01"), Convert.ToDateTime("2023-10-07"));
		public override bool HasSecondMonth => true;
	}
	

	private sealed class LodgingSE : DateRangeType
	{
		public LodgingSE() : base($"{nameof(Id.Lodging)}", Id.Lodging) { }
		public override string Title => "Lodging Date";
		public override DateRange Range => new DateRange(Convert.ToDateTime("2023-09-29"), Convert.ToDateTime("2023-09-30"));
		public override DateRange Range2ndMonth => new DateRange(Convert.ToDateTime("2023-10-01"), Convert.ToDateTime("2023-10-07"));
		public override bool HasSecondMonth => true;
	}
*/
	#endregion
}

public record DateRange(DateTime Min, DateTime Max);

/*
	SELECT DateRangeCodeGen FROM Sukkot.vwDateRangeTypeCodeGen
*/
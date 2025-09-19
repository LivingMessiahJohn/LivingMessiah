using Ardalis.SmartEnum;

namespace LivingMessiahAdmin.Features.Sukkot.Dashboard.Enums;

public abstract class ShowColumn : SmartEnum<ShowColumn>
{
	// This is a SmartEnum the leverages Bitwise, therefor all the Id values need to be powers of two
	
	#region Id's
	private static class BitwiseId
	{
		//internal const int All = -1;
		//internal const int None = 0;
		internal const int Email = 1;
		internal const int Phone = 2;
		internal const int People = 4;
		internal const int Paid = 8;
		internal const int Notes = 16;
		internal const int Attendance = 32;
	}
	#endregion

	#region Declared Public Instances
	public static readonly ShowColumn Email = new EmailSE();
	public static readonly ShowColumn Phone = new PhoneSE();
	public static readonly ShowColumn People = new PeopleSE();
	public static readonly ShowColumn Paid = new PaidSE();
	public static readonly ShowColumn Notes = new NotesSE();
	public static readonly ShowColumn Attendance = new AttendanceSE();
	#endregion

	private ShowColumn(string name, int value) : base(name, value) { }

	#region Extra Fields
	#endregion

	#region Private Instantiation
	private sealed class EmailSE : ShowColumn
	{
		public EmailSE() : base(nameof(BitwiseId.Email), BitwiseId.Email) { }
	}
	private sealed class PhoneSE : ShowColumn
	{
		public PhoneSE() : base(nameof(BitwiseId.Phone), BitwiseId.Phone) { }
	}

	private sealed class PeopleSE : ShowColumn
	{
		public PeopleSE() : base(nameof(BitwiseId.People), BitwiseId.People) { }
	}

	private sealed class PaidSE : ShowColumn
	{
		public PaidSE() : base(nameof(BitwiseId.Paid), BitwiseId.Paid) { }
	}

	private sealed class NotesSE : ShowColumn
	{
		public NotesSE() : base(nameof(BitwiseId.Notes), BitwiseId.Notes) { }
	}

	private sealed class AttendanceSE : ShowColumn
	{
		public AttendanceSE() : base(nameof(BitwiseId.Attendance), BitwiseId.Attendance) { }
	}
	#endregion
}
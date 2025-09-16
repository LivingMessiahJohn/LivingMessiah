using Ardalis.SmartEnum;
using StepEnums = LivingMessiahAdmin.Features.Sukkot.Enums.Step;

namespace LivingMessiahAdmin.Features.Sukkot.Dashboard.Enums;

public abstract class Filter : SmartEnum<Filter>
{
	#region Id's
	private static class Id
	{
		internal const int RegisteredOrPaid = 1; 
		internal const int Registered = 2; 
		internal const int RegisteredAndPaid = 3; 
		internal const int NotRegistered = 4; 
	}
	#endregion

	#region Declared Public Instances
	public static readonly Filter RegisteredOrPaid = new RegisteredOrPaidSE();
	public static readonly Filter Registered = new RegisteredSE();
	public static readonly Filter RegisteredAndPaid = new RegisteredAndPaidSE();
	public static readonly Filter NotRegistered = new NotRegisteredSE();
	// Note: SE=SmartEnum
	#endregion

	private Filter(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string ButtonLabel { get; }
	public abstract string ButtonTitle { get; }
	public abstract StepEnums Step { get; }
	public abstract StepEnums? Step2 { get; }
	#endregion

	#region Private Instantiation
	private sealed class RegisteredOrPaidSE : Filter
	{
		public RegisteredOrPaidSE() : base($"{nameof(Id.RegisteredOrPaid)}", Id.RegisteredOrPaid) { }
		public override string ButtonLabel => "Registered or Paid";
		public override string ButtonTitle => "List either those registered OR PAID";
		public override StepEnums Step => StepEnums.Payment;
		public override StepEnums? Step2 => StepEnums.Complete;
	}

	private sealed class RegisteredSE : Filter
	{
		public RegisteredSE() : base($"{nameof(Id.Registered)}", Id.Registered) { }
		public override string ButtonLabel => "Not Yet Paid";
		public override string ButtonTitle => "List only those registered BUT NOT YET PAID";
		public override StepEnums Step => StepEnums.Payment;
		public override StepEnums? Step2 => null;
	}

	private sealed class RegisteredAndPaidSE : Filter
	{
		public RegisteredAndPaidSE() : base($"{nameof(Id.RegisteredAndPaid)}", Id.RegisteredAndPaid) { }
		public override string ButtonLabel => "Completed";
		public override string ButtonTitle => "List only those registered AND PAID";
		public override StepEnums Step => StepEnums.Complete;
		public override StepEnums? Step2 => null;
	}

	private sealed class NotRegisteredSE : Filter
	{
		public NotRegisteredSE() : base($"{nameof(Id.NotRegistered)}", Id.NotRegistered) { }
		public override string ButtonLabel => "Not Yet Registered";
		public override string ButtonTitle => "List only those NOT registered";
		public override StepEnums Step => StepEnums.Registration;
		public override StepEnums? Step2 => null;
	}
	#endregion
}

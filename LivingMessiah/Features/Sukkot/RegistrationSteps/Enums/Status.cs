using Ardalis.SmartEnum;

namespace LivingMessiah.Features.Sukkot.RegistrationSteps.Enums;

//ToDo: this can be deleted after removing reference from Sukkot\Domain\vwRegistration.cs
public abstract class Status : SmartEnum<Status>
{
	#region Id's
	private static class Id
	{
		internal const int AgreementNotSigned = 3;
		internal const int StartRegistration = 4;
		internal const int Payment = 5;
		internal const int Complete = 6;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Status AgreementNotSigned = new AgreementNotSignedSE();
	public static readonly Status StartRegistration = new StartRegistrationSE();
	public static readonly Status Payment = new PaymentSE();
	public static readonly Status Complete = new CompleteSE();
	// SE=SmartEnum
	#endregion

	private Status(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields

	public abstract string Heading { get; }
	public abstract string Text { get; }
	public abstract string Icon { get; }
	public abstract bool UsedByUI { get; }
	public abstract bool DisplayRegistrationToggleButton { get; }

	#endregion

	#region Private Instantiation

	private sealed class AgreementNotSignedSE : Status
	{
		public AgreementNotSignedSE() : base($"{nameof(Id.AgreementNotSigned)}", Id.AgreementNotSigned) { }
		public override string Heading => "Sign Agreement";
		public override string Text => "Sign Agreement";
		public override string Icon => "fas fa-check";
		public override bool UsedByUI => true;
		public override bool DisplayRegistrationToggleButton => false;
	}

	private sealed class StartRegistrationSE : Status
	{
		public StartRegistrationSE() : base($"{nameof(Id.StartRegistration)}", Id.StartRegistration) { }
		public override string Heading => "Registration Form";
		public override string Text => "Start Registration";
		public override string Icon => "fas fa-check";
		public override bool UsedByUI => true;
		public override bool DisplayRegistrationToggleButton => false;
	}

	private sealed class PaymentSE : Status
	{
		public PaymentSE() : base($"{nameof(Id.Payment)}", Id.Payment) { }
		public override string Heading => "Payment";
		public override string Text => "Payment";
		public override string Icon => "fas fa-check";  //fas fa-star-half
		public override bool UsedByUI => true;
		public override bool DisplayRegistrationToggleButton => true;

	}

	private sealed class CompleteSE : Status
	{
		public CompleteSE() : base($"{nameof(Id.Complete)}", Id.Complete) { }
		public override string Heading => "Registration Complete";
		public override string Text => "Registered";
		public override string Icon => "fas fa-check";
		public override bool UsedByUI => true;
		public override bool DisplayRegistrationToggleButton => true;

	}
	#endregion
}


/*
SELECT * FROM Sukkot.Status
*/
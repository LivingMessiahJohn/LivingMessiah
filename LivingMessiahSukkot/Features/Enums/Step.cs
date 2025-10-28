using Ardalis.SmartEnum;

namespace LivingMessiahSukkot.Features.Enums;

public abstract class Step : SmartEnum<Step>
{
	#region Id's
	private static class Id
	{
		internal const int SignAgreement = 1;
		internal const int RegistrationForm = 2;
		internal const int Payment = 3;
		internal const int Complete = 4;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly Step SignAgreement = new SignAgreementSE();
	public static readonly Step Registration = new RegistrationSE();
	public static readonly Step Payment = new PaymentSE();
	public static readonly Step Complete = new CompleteSE();
	// SE=SmartEnum
	#endregion

	private Step(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields

	public abstract string Heading { get; }
	public abstract string Text { get; }
	public abstract string Icon { get; }
	public abstract string CardHeaderCSS { get; }
	public abstract string CardBodyCSS { get; }
	public abstract bool RegistrationExists { get; }

	#endregion

	#region Private Instantiation

	private sealed class SignAgreementSE : Step
	{
		public SignAgreementSE() : base($"{nameof(Id.SignAgreement)}", Id.SignAgreement) { }
		public override string Heading => "Agreement";
		public override string Text => "Sign Agreement";
		public override string Icon => "far fa-handshake"; 
		public override string CardHeaderCSS => "bg-success-subtle";  
		public override string CardBodyCSS => "bg-success-subtle";
		public override bool RegistrationExists => false;
	}

	private sealed class RegistrationSE : Step
	{
		public RegistrationSE() : base($"{nameof(Id.RegistrationForm)}", Id.RegistrationForm) { }
		public override string Heading => "Registration";
		public override string Text => "Registration Form";
		public override string Icon => "far fa-keyboard";
		public override string CardHeaderCSS => "bg-warning-subtle";
		public override string CardBodyCSS => "bg-warning-subtle";
		public override bool RegistrationExists => false;
	}

	private sealed class PaymentSE : Step
	{
		public PaymentSE() : base($"{nameof(Id.Payment)}", Id.Payment) { }
		public override string Heading => "Donation";   // Payment
		public override string Text => "Make Donation"; // Make Payment
		public override string Icon => "fas fa-dollar-sign";   
		public override string CardHeaderCSS => "bg-danger-subtle";
		public override string CardBodyCSS => "bg-danger-subtle";
		public override bool RegistrationExists => true;
	}

	private sealed class CompleteSE : Step
	{
		public CompleteSE() : base($"{nameof(Id.Complete)}", Id.Complete) { }
		public override string Heading => "Complete";
		public override string Text => "Registration Complete";
		public override string Icon => "fas fa-flag-checkered"; 
		public override string CardHeaderCSS => "bg-info-subtle";
		public override string CardBodyCSS => "bg-info-subtle";
		public override bool RegistrationExists => true;
	}
	#endregion

}


/*
SELECT * FROM Sukkot.Step
*/
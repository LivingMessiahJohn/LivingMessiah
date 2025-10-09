using Ardalis.SmartEnum;

namespace LivingMessiahAdmin.Features.Sukkot.Home.Enums;

public abstract class VisibleComponent : SmartEnum<VisibleComponent>
{
	#region Id's
	private static class Id
	{
		internal const int Agreement = 1;
		internal const int Registrant = 2;
		internal const int RegistrationList = 3;  // ToDo: will this be split between RgistrationList and RegistrationCard?
		internal const int RegistrationDetail = 4;
		internal const int Donation = 5;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly VisibleComponent Agreement = new AgreementSE();
	public static readonly VisibleComponent Registrant = new RegistrantSE();
	public static readonly VisibleComponent RegistrationList = new RegistrationListSE();
	public static readonly VisibleComponent RegistrationDetail = new RegistrationDetailSE();  
	public static readonly VisibleComponent Donation = new DonationSE();
	#endregion

	private VisibleComponent(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	//public abstract string Title { get; }
	#endregion


	#region Private Instantiation

	private sealed class AgreementSE : VisibleComponent
	{
		public AgreementSE() : base($"{nameof(Id.Agreement)}", Id.Agreement) { }
	}

	private sealed class RegistrantSE : VisibleComponent
	{
		public RegistrantSE() : base($"{nameof(Id.Registrant)}", Id.Registrant) { }
	}

	private sealed class RegistrationListSE : VisibleComponent
	{
		public RegistrationListSE() : base($"{nameof(Id.RegistrationList)}", Id.RegistrationList) { }
	}

	private sealed class RegistrationDetailSE : VisibleComponent
	{
		public RegistrationDetailSE() : base($"{nameof(Id.RegistrationDetail)}", Id.RegistrationDetail) { }
	}

	private sealed class DonationSE : VisibleComponent
	{
		public DonationSE() : base($"{nameof(Id.Donation)}", Id.Donation) { }
	}


	#endregion
}

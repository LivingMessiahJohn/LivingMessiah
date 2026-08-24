using Ardalis.SmartEnum;

namespace Admin.Features.Sukkot.CRUD.Enums;

public abstract class VisibleComponent : SmartEnum<VisibleComponent>
{
	#region Id's
	private static class Id
	{
		internal const int AddAgreement = 1; // ToDo: rename AddAgreement or ShowAddAgreement
		internal const int AgreementVerbiage = 2;
		internal const int Registrant = 3;
		internal const int RegistrationList = 4;  // ToDo: will this be split between RgistrationList and RegistrationCard?
		internal const int RegistrationDetail = 5;
		internal const int Donation = 6;

	}
	#endregion

	#region  Declared Public Instances
	public static readonly VisibleComponent AddAgreement = new AddAgreementSE();
	public static readonly VisibleComponent AgreementVerbiage = new AgreementVerbiageSE();
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

	private sealed class AddAgreementSE : VisibleComponent
	{
		public AddAgreementSE() : base($"{nameof(Id.AddAgreement)}", Id.AddAgreement) { } 
	}

	private sealed class AgreementVerbiageSE : VisibleComponent
	{
		public AgreementVerbiageSE() : base($"{nameof(Id.AgreementVerbiage)}", Id.AgreementVerbiage) { }
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

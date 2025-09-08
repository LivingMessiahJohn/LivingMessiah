using Ardalis.SmartEnum;

namespace LivingMessiahAdmin.Features.Sukkot.Home.Enums;

public abstract class VisibleComponent : SmartEnum<VisibleComponent>
{
	#region Id's
	private static class Id
	{
		internal const int RegistrationList = 1;  // ToDo: will this be split between RgistrationList and RegistrationCard?
		internal const int RegistrationDetail = 2;
		internal const int Registrant = 3;
	}
	#endregion

	#region  Declared Public Instances
	public static readonly VisibleComponent RegistrationList = new RegistrationListSE();
	public static readonly VisibleComponent RegistrationDetail = new RegistrationDetailSE();  
	public static readonly VisibleComponent Registrant = new RegistrantSE();
	#endregion

	private VisibleComponent(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	//public abstract string Title { get; }
	#endregion


	#region Private Instantiation

	private sealed class RegistrationListSE : VisibleComponent
	{
		public RegistrationListSE() : base($"{nameof(Id.RegistrationList)}", Id.RegistrationList) { }
	}

	private sealed class RegistrationDetailSE : VisibleComponent
	{
		public RegistrationDetailSE() : base($"{nameof(Id.RegistrationDetail)}", Id.RegistrationDetail) { }
	}

	private sealed class RegistrantSE : VisibleComponent
	{
		public RegistrantSE() : base($"{nameof(Id.Registrant)}", Id.Registrant) { }
	}

	#endregion
}

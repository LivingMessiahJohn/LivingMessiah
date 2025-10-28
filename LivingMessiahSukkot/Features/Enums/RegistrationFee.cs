using Ardalis.SmartEnum;

namespace LivingMessiahSukkot.Features.Enums;

public abstract class RegistrationFee : SmartEnum<RegistrationFee>
{
	#region Id's
	private static class Id
	{
		internal const int Single = 1;
		internal const int Family = 2;
	}
	#endregion

	#region Declared Public Instances
	public static readonly RegistrationFee Single = new SingleSE();
	public static readonly RegistrationFee Family = new FamilySE();
	#endregion

	private RegistrationFee(string name, int value) : base(name, value) { }

	#region Extra Fields
	public abstract long Fee { get; }
	public long Pennies => Fee * 100;
	public string CurrencyFormat => $"${Fee:0.00}"; //this.Fee * 100

	#endregion

	#region Private Instantiation

	private sealed class SingleSE : RegistrationFee
	{
		public SingleSE() : base($"{nameof(Id.Single)}", Id.Single) { }
		public override long Fee => 50;
	}

	private sealed class FamilySE : RegistrationFee
	{
		public FamilySE() : base($"{nameof(Id.Family)}", Id.Family) { }
		public override long Fee => 100;
	}

	#endregion

}

public class RegistrationFeeHelper
{
	public static RegistrationFee AdultsToRegistrationFee(int adults)
	{
		return adults > 1 ? RegistrationFee.Family : RegistrationFee.Single;
	}
}

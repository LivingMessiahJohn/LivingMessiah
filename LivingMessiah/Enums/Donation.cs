using Ardalis.SmartEnum;
using LivingMessiah.Helpers.Constants;

namespace LivingMessiah.Enums;

public abstract class Donation : SmartEnum<Donation>
{

	#region Id's
	private static class Id
	{
		internal const int OneTime = 1;
		internal const int Subscription = 2;
	}
	#endregion

	#region Declared Public Instances
	public static readonly Donation OneTime = new OneTimeSE();
	public static readonly Donation Subscription = new SubscriptionSE();
	#endregion


	private Donation(string name, int value) : base(name, value)  // Constructor
	{
	}

	#region Extra Fields
	public abstract string WebhookUrl { get; }
	public abstract string Title { get; }

	public string SessionUrl => $"{Actions.BaseSessionUrl}-{Name.ToLower()}";
	#endregion

	#region Private Instantiation

	private sealed class OneTimeSE : Donation
	{
		public OneTimeSE() : base($"{nameof(Id.OneTime)}", Id.OneTime) { }
		public override string WebhookUrl => "/webhook/striperegulardonation";
		public override string Title => "Home";
	}

	private sealed class SubscriptionSE : Donation
	{
		public SubscriptionSE() : base($"{nameof(Id.Subscription)}", Id.Subscription) { }
		public override string WebhookUrl => "/webhook/stripesubscription";
		public override string Title => "Donate";
	}

	#endregion
}



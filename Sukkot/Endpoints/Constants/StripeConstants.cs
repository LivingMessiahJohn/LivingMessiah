namespace Sukkot.Endpoints.Constants;

public static class StripeConstants
{
	public const string ApiKey = "Stripe:ApiKey";
	public const string WebhookSecret = "Stripe:WebhookSecret";
	public const string RequestHeaders = "Stripe-Signature";
	public const string EventType = "checkout.session.completed";

	/// <summary>Committed sample values in appsettings.json — not real secrets.</summary>
	public const string PlaceholderApiKey = "sk_test_your_stripe_api_key";
	public const string PlaceholderWebhookSecret = "whsec_...";
}

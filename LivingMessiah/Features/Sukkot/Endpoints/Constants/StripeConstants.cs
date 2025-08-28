namespace LivingMessiah.Features.Sukkot.Endpoints.Constants;

public static class StripeConstants
{
	public const string ApiKey = "Stripe:ApiKey"; 
	public const string WebhookSecret = "Stripe:WebhookSecret";
	public const string RequestHeaders = "Stripe-Signature";
	public const string EventType = "checkout.session.completed";
}

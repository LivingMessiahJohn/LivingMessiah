namespace Admin.HealthChecks.Sukkot.Endpoints.Constants;

public static class StripeConstants
{
	public const string ApiKey = "Stripe:ApiKey"; 
	public const string WebhookSecret = "Stripe:WebhookSecret";
	/// <summary>
	/// Production Sukkot App Service origin (not the PWA at livingmessiah.com).
	/// Stripe Dashboard endpoint must match this host + path.
	/// </summary>
	public const string WebhookUrl = "https://sukkot.livingmessiah.com/webhook/stripesukkotdonation";
	public const string HealthCheckUrl = "/health/sukkot/stripe";
	public const string HealthCheckName = "Is Stripe Webhook Enabled"; 
}

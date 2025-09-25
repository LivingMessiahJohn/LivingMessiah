namespace LivingMessiahAdmin.HealthChecks.Sukkot.Endpoints.Constants;

public static class StripeConstants
{
	public const string ApiKey = "Stripe:ApiKey"; 
	public const string WebhookSecret = "Stripe:WebhookSecret";
	public const string WebhookUrl = "https://livingmessiah.com/webhook/stripesukkotdonation";
	public const string HealthCheckUrl = "/health/sukkot/stripe";
	public const string HealthCheckName = "Is Stripe Webhook Enabled"; 
}

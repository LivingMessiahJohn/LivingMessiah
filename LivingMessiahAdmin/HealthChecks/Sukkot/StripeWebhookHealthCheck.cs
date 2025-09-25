using System.Text;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using HealthChecksSukkot = LivingMessiahAdmin.HealthChecks.Sukkot.Endpoints;

namespace LivingMessiahAdmin.HealthChecks.Sukkot;

public class StripeWebhookHealthCheck : IHealthCheck
{
	private readonly Settings.Stripe _stripeSettings;
	private readonly ILogger<StripeWebhookHealthCheck> Logger;
	private readonly IHttpClientFactory _httpClientFactory;

	public StripeWebhookHealthCheck(
		IOptions<Settings.Stripe> stripeOptions,
		ILogger<StripeWebhookHealthCheck> logger,
		IHttpClientFactory httpClientFactory)
	{
		_stripeSettings = stripeOptions.Value;
		Logger = logger;
		_httpClientFactory = httpClientFactory;
	}

	public async Task<HealthCheckResult> CheckHealthAsync(
		HealthCheckContext context,
		CancellationToken cancellationToken = default)
	{

		var webhookUrl = HealthChecksSukkot.Constants.StripeConstants.WebhookUrl; 
		var client = _httpClientFactory.CreateClient();

		var testPayload = "{}"; // Optionally, use a more realistic payload
		var request = new HttpRequestMessage(HttpMethod.Post, webhookUrl)
		{
			Content = new StringContent(testPayload, Encoding.UTF8, "application/json")
		};
		// Optionally, add Stripe signature header if your endpoint requires it

		try
		{
			var response = await client.SendAsync(request, cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				Logger!.LogDebug("{Method} {Message}", nameof(CheckHealthAsync), "Stripe Webhook endpoint is reachable.");
				return HealthCheckResult.Healthy("Stripe Webhook endpoint is reachable.");
			}
			else
			{
				Logger!.LogWarning("{Method} {Message}"
					, nameof(CheckHealthAsync)
					, $"Stripe Webhook endpoint returned status code {response.StatusCode}.");
				return HealthCheckResult.Unhealthy($"Stripe Webhook endpoint returned status code {response.StatusCode}.");
			}
		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "{Method}, {Message}", nameof(CheckHealthAsync), "Error: {ex.Message}");
			return HealthCheckResult.Unhealthy("Error calling Stripe Webhook endpoint.", ex);

		}
	}
}
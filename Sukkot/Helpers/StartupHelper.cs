using Serilog;
using Sukkot.Endpoints.Constants;

namespace Sukkot.Helpers;

public static class StartupHelper
{
	public static void DumpSectionConfiguration(IConfigurationSection config, string section)
	{
		Log.Debug("{Class}, {Method} {Message}", nameof(StartupHelper), nameof(DumpSectionConfiguration), $"{section} items...");
		foreach (var kvp in config.GetChildren())
		{
			//Log.Debug("{Message}", $"...Key: {kvp.Key}; Value: {kvp.Value}");
			Log.Debug("...: {Key} / {Value}", kvp.Key, kvp.Value);
		}
	}

	/// <summary>
	/// Fails fast when Stripe secrets are missing or still the committed appsettings placeholders.
	/// Does not log secret values.
	/// </summary>
	public static void EnsureStripeSecretsConfigured(IConfiguration configuration)
	{
		var apiKey = configuration[StripeConstants.ApiKey];
		if (IsMissingOrPlaceholder(apiKey, StripeConstants.PlaceholderApiKey))
		{
			const string message =
				"Stripe:ApiKey is missing or still the appsettings placeholder. " +
				"Set a real key via user-secrets, environment variables, or Azure App Settings " +
				"(see docs/Sukkot-Stripe-Endpoints.md and SECRETS-QUICK-REF.md). " +
				"Never commit live keys.";
			Log.Fatal("{Class}, {Method}, {Message}", nameof(StartupHelper), nameof(EnsureStripeSecretsConfigured), message);
			throw new InvalidOperationException(message);
		}

		var webhookSecret = configuration[StripeConstants.WebhookSecret];
		if (IsMissingOrPlaceholder(webhookSecret, StripeConstants.PlaceholderWebhookSecret))
		{
			const string message =
				"Stripe:WebhookSecret is missing or still the appsettings placeholder. " +
				"Set the webhook signing secret (Stripe CLI whsec_… for local, Dashboard secret for deployed). " +
				"See docs/Sukkot-Stripe-Endpoints.md.";
			Log.Fatal("{Class}, {Method}, {Message}", nameof(StartupHelper), nameof(EnsureStripeSecretsConfigured), message);
			throw new InvalidOperationException(message);
		}

		Log.Information("{Class}, {Method}, Stripe secrets are present (values not logged)",
			nameof(StartupHelper), nameof(EnsureStripeSecretsConfigured));
	}

	private static bool IsMissingOrPlaceholder(string? value, string placeholder) =>
		string.IsNullOrWhiteSpace(value)
		|| string.Equals(value.Trim(), placeholder, StringComparison.Ordinal)
		|| value.Contains("your_stripe", StringComparison.OrdinalIgnoreCase)
		|| value.TrimEnd().EndsWith("...", StringComparison.Ordinal);
}

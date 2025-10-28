using Stripe;
using Stripe.Checkout;

using LivingMessiahSukkot.Endpoints.Constants;
using LivingMessiahSukkot.Features.Data;
using RegistrationFeeEnums = LivingMessiahSukkot.Features.Enums.RegistrationFee;
using static LivingMessiahSukkot.Features.Constants.FormFields;

namespace LivingMessiahSukkot.Endpoints;

public static class Webhook
{
	public static void WebhookConfig(this IEndpointRouteBuilder endpoints, string webhookUrl)
	{
		endpoints.MapPost(webhookUrl, async (
				HttpContext context,
				IConfiguration config,
				ILoggerFactory loggerFactory,
				IRepository db) =>
		{
			var Logger = loggerFactory.CreateLogger(nameof(Webhook));
			var json = await new StreamReader(context.Request.Body).ReadToEndAsync();

			//LogSecret(webhookUrl, config, Logger); // ToDo: comment out in production

			try
			{
				var stripeEvent = EventUtility.ConstructEvent(
									json,
									context.Request.Headers[StripeConstants.RequestHeaders],
									config[StripeConstants.WebhookSecret],
									throwOnApiVersionMismatch: false);

				// Logger.LogInformation("...ET: {ET}", stripeEvent.Type);

				if (stripeEvent.Type == StripeConstants.EventType)
				{
					Logger.LogDebug("{Method}, {Message}, {EventType}", nameof(WebhookConfig), $"webhookUrl: {webhookUrl}", stripeEvent.Type);
					var session = stripeEvent.Data.Object as Session;

					Logger.LogInformation("{Method}, {Message}", nameof(WebhookConfig), $"Email: {session!.CustomerEmail}"); 		

					var (amountError, amount) = ValidateAmount(session);
					if (!string.IsNullOrEmpty(amountError))
					{
						Logger.LogError("{Method}, Validation failed: {ErrorMessage}", nameof(WebhookConfig), amountError);
						return Results.BadRequest($"{amountError}".Trim());
					}

					var (registrationIdError, registrationId) = ValidateRegistrationId(session);
					if (!string.IsNullOrEmpty(registrationIdError))
					{
						Logger.LogError("{Method}, Validation failed: {ErrorMessage}", nameof(WebhookConfig), registrationIdError);
						return Results.BadRequest($"{registrationIdError}".Trim());
					}

					var (insertError, newId) = await InsertDonation(db, Logger, session, amount, registrationId);
					if (!string.IsNullOrEmpty(insertError))
					{
						Logger.LogError("{Method}, DB Insertion failed: {ErrorMessage}", nameof(WebhookConfig), insertError);
						return Results.BadRequest($"{insertError}".Trim());
					}
				}

				return Results.Ok();
			}
			catch (StripeException ex)
			{
				Logger!.LogError(ex, "{Method}, {Message}", nameof(WebhookConfig), "Stripe error: {ex.Message}");
				return Results.BadRequest();
			}
			catch (Exception ex)
			{
				Logger!.LogError(ex, "{Method}, {Message}", nameof(WebhookConfig), "Error: {ex.Message}");
				return Results.BadRequest();
			}
		});
	}

	private static async Task<(string ErrorMsg, int NewId)> InsertDonation(IRepository db, ILogger Logger, Session session, decimal amount, int registrationId)
	{
		DonationRecord donation = new()
		{
			RegistrationId = registrationId,
			Amount = amount,
			Notes = "Stripe Checkout Session Completed",
			Email = session.CustomerEmail,
			ReferenceId = session.Id,
			CreatedBy = $"Endpoint: {StripeConstants.EventType}",
			CreateDate = DateTime.UtcNow
		};

		var (newId, errorMsg) = await db.DonationInsert(donation);

		if (!string.IsNullOrEmpty(errorMsg)) 
		{ 
			Logger.LogWarning("{Method}, {Message}", nameof(WebhookConfig), $"Error message from {nameof(db.DonationInsert)}: {errorMsg}");
		}
		else
		{
			Logger.LogInformation("{Method}, {Message}", nameof(WebhookConfig), $"Returned id: {newId}, Email: {session.CustomerEmail}");
		}

		return (errorMsg ?? "", newId);
	}

	private static (string ReturnMsg, decimal amount) ValidateAmount(Session session)
	{
		string returnMsg = string.Empty;
		decimal amount = 0;
		if (session.AmountTotal.HasValue)
		{
			amount = session.AmountTotal.Value / 100m;
			if (amount == RegistrationFeeEnums.Single.Fee || amount == RegistrationFeeEnums.Family.Fee)
			{
				//returnMsg = "amount is valid";
			}
			else
			{
				//returnMsg = $"amount is invalid: {session.AmountTotal.Value}";
				returnMsg = $"amount is invalid: {amount}";
			}
		}
		else
		{
			returnMsg = "amount has no value";
		}
		return (returnMsg, amount);
	}

	private static (string ReturnMsg, int Id) ValidateRegistrationId(Session session)
	{
		var str = session.Metadata?[RegistrationId];
		_ = int.TryParse(str, out int registrationId);

		string returnMsg = registrationId > 0 ? "" : "RegistrationId NOT FOUND";
		return (returnMsg, registrationId);
	}

	// ToDo: once I get this working, I need to remove it.
	private static void LogSecret(string webhookUrl, IConfiguration config, ILogger Logger)
	{
		string? secret = config[StripeConstants.WebhookSecret] ?? string.Empty;
		if (!string.IsNullOrEmpty(secret))
		{
			if (secret.Length >= 10)
			{
				secret = $"Length >=10; secret.Substring(0, 10)Value: {secret.Substring(0, 10)}";  // secret.Substring(0, 10);
			}
			else
			{
				secret = $"Length less than 10; secret: {secret}";
			}
		}
		else
		{
			secret = "IsNullOrEmpty";
		}
		Logger.LogWarning("{Method}, {Message}, {Secret}", nameof(WebhookConfig), webhookUrl, secret);
	}

}


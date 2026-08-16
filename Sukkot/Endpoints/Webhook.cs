using Stripe;
using Stripe.Checkout;
using RegistrationFeeEnums = RCL.Features.Sukkot.Enums.RegistrationFee;
using static Sukkot.Features.Constants.FormFields;
using Sukkot.Endpoints.Constants;
using Sukkot.Endpoints.Data;

namespace Sukkot.Endpoints;

public static class Webhook
{
	/// <summary>Fits dbo.Donation.CreatedBy nvarchar(25).</summary>
	private const string DonationCreatedBy = "Stripe Webhook";

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

			if (string.IsNullOrWhiteSpace(json))
			{
				Logger.LogWarning("{Method}, Empty request body", nameof(WebhookConfig));
				return Results.BadRequest("Empty body");
			}

			try
			{
				var webhookSecret = config[StripeConstants.WebhookSecret];
				if (string.IsNullOrWhiteSpace(webhookSecret))
				{
					Logger.LogError("{Method}, Stripe:WebhookSecret is not configured", nameof(WebhookConfig));
					return Results.BadRequest("Webhook secret not configured");
				}

				var signature = context.Request.Headers[StripeConstants.RequestHeaders].ToString();
				if (string.IsNullOrWhiteSpace(signature))
				{
					Logger.LogWarning("{Method}, Missing Stripe-Signature header", nameof(WebhookConfig));
					return Results.BadRequest("Missing Stripe-Signature");
				}

				var stripeEvent = EventUtility.ConstructEvent(
									json,
									signature,
									webhookSecret,
									throwOnApiVersionMismatch: false);

				Logger.LogInformation("{Method}, Stripe event accepted: {EventId} type={EventType}",
					nameof(WebhookConfig), stripeEvent.Id, stripeEvent.Type);

				if (stripeEvent.Type != StripeConstants.EventType)
				{
					// Acknowledge other event types so Stripe does not retry forever.
					Logger.LogDebug("{Method}, Ignoring event type {EventType}", nameof(WebhookConfig), stripeEvent.Type);
					return Results.Ok();
				}

				if (stripeEvent.Data.Object is not Session session)
				{
					Logger.LogError("{Method}, Event data is not a Checkout Session", nameof(WebhookConfig));
					return Results.BadRequest("Expected checkout Session payload");
				}

				Logger.LogInformation("{Method}, checkout.session.completed SessionId={SessionId} Email={Email}",
					nameof(WebhookConfig), session.Id, session.CustomerEmail);

				var (amountError, amount) = ValidateAmount(session);
				if (!string.IsNullOrEmpty(amountError))
				{
					Logger.LogError("{Method}, Validation failed: {ErrorMessage}", nameof(WebhookConfig), amountError);
					return Results.BadRequest(amountError.Trim());
				}

				var (registrationIdError, registrationId) = ValidateRegistrationId(session);
				if (!string.IsNullOrEmpty(registrationIdError))
				{
					Logger.LogError("{Method}, Validation failed: {ErrorMessage} SessionId={SessionId}",
						nameof(WebhookConfig), registrationIdError, session.Id);
					return Results.BadRequest(registrationIdError.Trim());
				}

				var (insertError, newId, alreadyExists) = await InsertDonation(db, Logger, session, amount, registrationId);
				if (alreadyExists)
				{
					// Idempotent: payment already recorded — tell Stripe success so it stops retrying.
					Logger.LogInformation("{Method}, Donation already present for RegistrationId={RegistrationId}; treating as success",
						nameof(WebhookConfig), registrationId);
					return Results.Ok();
				}

				if (!string.IsNullOrEmpty(insertError))
				{
					Logger.LogError("{Method}, DB Insertion failed: {ErrorMessage}", nameof(WebhookConfig), insertError);
					return Results.BadRequest(insertError.Trim());
				}

				Logger.LogInformation("{Method}, Donation inserted NewId={NewId} RegistrationId={RegistrationId}",
					nameof(WebhookConfig), newId, registrationId);
				return Results.Ok();
			}
			catch (StripeException ex)
			{
				Logger.LogError(ex, "{Method}, Stripe signature/event error: {Message}", nameof(WebhookConfig), ex.Message);
				return Results.BadRequest();
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "{Method}, Unhandled webhook error: {Message}", nameof(WebhookConfig), ex.Message);
				return Results.BadRequest();
			}
		})
		.AllowAnonymous()
		.DisableAntiforgery();
	}

	private static async Task<(string ErrorMsg, int NewId, bool AlreadyExists)> InsertDonation(
		IRepository db,
		ILogger Logger,
		Session session,
		decimal amount,
		int registrationId)
	{
		DonationRecord donation = new()
		{
			RegistrationId = registrationId,
			Amount = amount,
			Notes = "Stripe Checkout Session Completed",
			Email = session.CustomerEmail ?? string.Empty,
			ReferenceId = session.Id,
			CreatedBy = DonationCreatedBy,
			CreateDate = DateTime.UtcNow
		};

		var (newId, errorMsg) = await db.DonationInsert(donation);

		if (!string.IsNullOrEmpty(errorMsg))
		{
			bool alreadyExists = errorMsg.Contains("already exists", StringComparison.OrdinalIgnoreCase);
			Logger.LogWarning("{Method}, {Message}", nameof(InsertDonation),
				$"Error message from {nameof(db.DonationInsert)}: {errorMsg}");
			return (errorMsg, newId, alreadyExists);
		}

		Logger.LogInformation("{Method}, Returned id: {NewId}, Email: {Email}",
			nameof(InsertDonation), newId, session.CustomerEmail);
		return (string.Empty, newId, false);
	}

	private static (string ReturnMsg, decimal amount) ValidateAmount(Session session)
	{
		string returnMsg = string.Empty;
		decimal amount = 0;
		if (session.AmountTotal.HasValue)
		{
			amount = session.AmountTotal.Value / 100m;
			if (amount != RegistrationFeeEnums.Single.Fee && amount != RegistrationFeeEnums.Family.Fee)
			{
				returnMsg = $"amount is invalid: {amount} (expected {RegistrationFeeEnums.Single.Fee} or {RegistrationFeeEnums.Family.Fee})";
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

		string returnMsg = registrationId > 0
			? string.Empty
			: $"RegistrationId NOT FOUND in session metadata (raw='{str ?? "(null)"}')";
		return (returnMsg, registrationId);
	}
}

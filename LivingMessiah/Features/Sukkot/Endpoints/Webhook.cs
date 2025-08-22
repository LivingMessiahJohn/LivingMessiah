using Stripe;
using LivingMessiah.Features.Sukkot.Data;
using Stripe.Checkout;

namespace LivingMessiah.Features.Sukkot.Endpoints;

public static class Webhook
{
	public static void Configure(this IEndpointRouteBuilder endpoints, string webhookUrl)
	{
		endpoints.MapPost(webhookUrl, async (
				HttpContext context,
				IConfiguration config,
				ILoggerFactory loggerFactory,
				IRepository db) =>
		{
			var Logger = loggerFactory.CreateLogger(nameof(Configure));
			var json = await new StreamReader(context.Request.Body).ReadToEndAsync();

			Logger.LogDebug("{Method}, {Message}", nameof(Configure), webhookUrl);

			try
			{
				var stripeEvent = EventUtility.ConstructEvent(
									json,
									context.Request.Headers["Stripe-Signature"],
									config["Stripe:WebhookSecret"],
									throwOnApiVersionMismatch: false);

				string eventType = "checkout.session.completed";
				if (stripeEvent.Type == eventType)
				{
					var session = stripeEvent.Data.Object as Session;

					Logger.LogInformation("{Method}, {Message}", nameof(Configure)
						, $"EventType: {eventType}, SessionId: {session!.Id}, Email: {session!.CustomerEmail}");

					decimal amount = session.AmountTotal.HasValue ? (decimal)(session.AmountTotal.Value / 100m) : 0m;

					var registrationIdStr = session.Metadata?["registrationId"];
					int registrationId = 0;
					int.TryParse(registrationIdStr, out registrationId);

					DonationRecord donation = new()
					{
						RegistrationId = registrationId,
						Amount = amount,
						Notes = "Stripe Checkout Session Completed",
						Email = session.CustomerEmail,
						ReferenceId = session.Id,
						CreatedBy = $"Endpoint: {eventType}",
						CreateDate = DateTime.UtcNow
					};
					int newId = await db.DonationInsert(donation);
					Logger.LogInformation("{Method}, {Message}", nameof(Configure), $"Returned id: {newId}, Email: {session.CustomerEmail}");
				}
				// Unhandled event types can be logged if needed

				return Results.Ok();
			}
			catch (StripeException ex)
			{
				Logger!.LogError(ex, "{Method}, {Message}", nameof(Configure), "Stripe error: {ex.Message}");
				return Results.BadRequest();
			}
			catch (Exception ex)
			{
				Logger!.LogError(ex, "{Method}, {Message}", nameof(Configure), "Error: {ex.Message}");
				return Results.BadRequest();
			}
		});
	}

}


using Stripe.Checkout;
using LivingMessiah.Features.Sukkot.ConstantsFOLDER;

namespace LivingMessiah.Features.Sukkot.Endpoints;

public static class CheckoutSession
{
	private static class ReturnUrl
	{
		public const string SuccessUrl = "Sukkot/PaymentConfirm";
		public const string CancelUrl = "Sukkot/PaymentCanceled";
	}

	public static void Configure(this IEndpointRouteBuilder endpoints, string sessionUrl, string domain)
	{
		endpoints.MapPost(sessionUrl,
			async (HttpContext context,
			IConfiguration config,
			ILoggerFactory loggerFactory) =>
			{
				var Logger = loggerFactory.CreateLogger(nameof(CheckoutSession));
				var form = await context.Request.ReadFormAsync();
				var registrationId = int.Parse(form[FormFields.RegistrationId]);
				var amount = int.Parse(form[FormFields.Amount]); // amount in cents
				var email = form[FormFields.Email];

				Logger.LogDebug("{Method} {Message}", nameof(CheckoutSession), $"amount: {amount}");

				SessionCreateOptions options = new SessionCreateOptions
				{
					PaymentMethodTypes = new List<string> { "card" },
					LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							Currency = "usd",
							UnitAmount = amount,
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = "Registration Fee",
							},
						},
						Quantity = 1,
					},
				},
					Mode = "payment",
					SuccessUrl = $"{domain}/{ReturnUrl.SuccessUrl}",
					CancelUrl = $"{domain}/{ReturnUrl.CancelUrl}",
					CustomerEmail = email,
					Metadata = new Dictionary<string, string>
					{
						{ FormFields.RegistrationId, registrationId.ToString() },
						{ FormFields.Email, email! }
					}
				};

				var service = new SessionService();
				var session = await service.CreateAsync(options);
				Logger.LogInformation("Stripe session created with ID: {SessionId}", session.Id);
				context.Response.Redirect(session.Url);
			});
	}

}

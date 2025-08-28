using Stripe.Checkout;
using LivingMessiah.Features.Sukkot.Enums;
using LivingMessiah.Features.Sukkot.Constants;

namespace LivingMessiah.Features.Sukkot.Endpoints;

public static class CheckoutSession
{
	private static class ReturnUrl
	{
		public const string SuccessUrl = "Sukkot/PaymentConfirm";
		public const string CancelUrl = "Sukkot/PaymentCanceled";
	}

	public static void CheckoutSessionConfig(this IEndpointRouteBuilder endpoints, string sessionUrl, string domain)
	{
		endpoints.MapPost(sessionUrl,
			async (HttpContext context,
			IConfiguration config,
			ILoggerFactory loggerFactory) =>
			{
				var Logger = loggerFactory.CreateLogger(nameof(CheckoutSession));

				IFormCollection form = await context.Request.ReadFormAsync();

				var (registrationIdError, registrationId) = ValidateRegistrationId(form);
				if (!string.IsNullOrEmpty(registrationIdError))
				{
					Logger.LogError("{Method}, Validation failed: {ErrorMessage}", nameof(CheckoutSessionConfig), registrationIdError);
					return Results.BadRequest($"{registrationIdError}".Trim());
				}

				var (feeEnumIdValueError, registrationFee) = ValidateFeeEnumValue(form);
				if (!string.IsNullOrEmpty(feeEnumIdValueError))
				{
					Logger.LogError("{Method}, Validation failed: {ErrorMessage}", nameof(CheckoutSessionConfig), feeEnumIdValueError);
					return Results.BadRequest($"{feeEnumIdValueError}".Trim());
				}

				var (emailError, email) = ValidateEmail(form);
				if (!string.IsNullOrEmpty(emailError))
				{
					Logger.LogError("{Method}, Validation failed: {ErrorMessage}", nameof(CheckoutSessionConfig), emailError);
					return Results.BadRequest($"{emailError}".Trim());
				}

				Logger.LogDebug("{Method} {Message}", nameof(CheckoutSession), $"amount: {registrationFee.Fee}");
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
							UnitAmount = registrationFee.Pennies,
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
				return Results.Redirect(session.Url);
			});
	}

	private static (string ReturnMsg, int registrationId) ValidateRegistrationId(IFormCollection form)
	{
		if (!form.TryGetValue(FormFields.RegistrationId, out var registrationIdValue) || string.IsNullOrEmpty(registrationIdValue))
		{
			return ("RegistrationId is required and cannot be null or empty.", 0);
		}

		if (!int.TryParse(registrationIdValue, out int registrationId))
		{
			return ("RegistrationId must be a valid integer.", 0);
		}

		return (string.Empty, registrationId);
	}

	private static (string ReturnMsg, RegistrationFee registrationFee) ValidateFeeEnumValue(IFormCollection form)
	{
		if (!form.TryGetValue(FormFields.FeeEnumValue, out var feeEnumIdValue) || string.IsNullOrEmpty(feeEnumIdValue))
		{
			return ("FeeEnumId is required and cannot be null or empty.", null!);
		}

		if (!int.TryParse(feeEnumIdValue, out int feeEnumId))
		{
			return ("FeeEnumId must be a valid integer.", null!);
		}

		if (!RegistrationFee.TryFromValue(feeEnumId, out var registrationFee))
		{
			return ($"RegistrationFee fee, gotten by TryFromValue using feeEnumId: {feeEnumId} is unknown", null!);
		}

		return (string.Empty, registrationFee);
	}

	private static (string ReturnMsg, string Email) ValidateEmail(IFormCollection form)
	{
		if (!form.TryGetValue(FormFields.Email, out var emailValue) || string.IsNullOrEmpty(emailValue))
		{
			return ("Email is required and cannot be null or empty.", string.Empty);
		}


		string email = emailValue.ToString();
		try
		{
			var addr = new System.Net.Mail.MailAddress(email);
			if (addr.Address != email)
			{
				return ("Email format is invalid.", string.Empty);
			}
		}
		catch
		{
			return ("Email format is invalid.", string.Empty);
		}

		return (string.Empty, email);

	}

}

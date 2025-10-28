using Stripe.Checkout;
using LivingMessiahSukkot.Features.Enums;
using LivingMessiahSukkot.Features.Constants;
using LivingMessiahSukkot.Features.Data;

namespace LivingMessiahSukkot.Endpoints;

public static class CheckoutSession
{
	private static class ReturnUrl
	{
		public const string SuccessUrl = "PaymentConfirm";
		public const string CancelUrl = "PaymentCanceled";
	}

	public static void CheckoutSessionConfig(this IEndpointRouteBuilder endpoints, string sessionUrl, string domain)
	{
		endpoints.MapPost(sessionUrl,
			async (HttpContext context,
			IConfiguration config,
			ILoggerFactory loggerFactory,
			IRepository db) =>
			{
				var Logger = loggerFactory.CreateLogger(nameof(CheckoutSession));

				IFormCollection form = await context.Request.ReadFormAsync();

				var validationResult = CheckValidation(form, Logger, out int registrationId, out RegistrationFee registrationFee, out string email);
				if (validationResult != null)
				{
					return validationResult;
				}

				Logger.LogDebug("{Method} {Message}", nameof(CheckoutSession), $"Passed validation, amount: {registrationFee.Fee}");

				try
				{
					var sprocTuple = await db!.StripeMerge(email, registrationId);
				}
				catch (Exception ex)
				{
					Logger!.LogError(ex, "{Method}, {Message} {Called}", nameof(CheckoutSessionConfig)
						, "Error: {ex.Message}", $"just called {nameof(db.StripeMerge)}, keep moving");
					//return Results.BadRequest();
				}

				try
				{
					SessionCreateOptions options = GetSessionOptions(registrationId, registrationFee, email, domain);

					var service = new SessionService();
					var session = await service.CreateAsync(options);
					Logger.LogInformation("Stripe session created with ID: {SessionId}", session.Id);
					return Results.Redirect(session.Url);

				}
				catch (Exception ex)
				{
					Logger!.LogError(ex, "{Method}, {Message} {Called}", nameof(CheckoutSessionConfig)
						, "Error: {ex.Message}", $"just called {nameof(GetSessionOptions)}");
					return Results.BadRequest();
				}

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
			return ($"Registration donation, gotten by TryFromValue using feeEnumId: {feeEnumId} is unknown", null!);
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

	private static IResult? CheckValidation(
		IFormCollection form,
		ILogger Logger,
		out int registrationId,
		out RegistrationFee registrationFee,
		out string email)
	{
		registrationId = 0;
		registrationFee = null!;
		email = string.Empty;

		try
		{
			var (registrationIdError, regId) = ValidateRegistrationId(form);
			if (!string.IsNullOrEmpty(registrationIdError))
			{
				Logger.LogError("{Method}, Validation failed: {ErrorMessage}", nameof(CheckoutSessionConfig), registrationIdError);
				return Results.BadRequest($"{registrationIdError}".Trim());
			}
			registrationId = regId;

			var (feeEnumIdValueError, regFee) = ValidateFeeEnumValue(form);
			if (!string.IsNullOrEmpty(feeEnumIdValueError))
			{
				Logger.LogError("{Method}, Validation failed: {ErrorMessage}", nameof(CheckoutSessionConfig), feeEnumIdValueError);
				return Results.BadRequest($"{feeEnumIdValueError}".Trim());
			}
			registrationFee = regFee;

			var (emailError, emailValue) = ValidateEmail(form);
			if (!string.IsNullOrEmpty(emailError))
			{
				Logger.LogError("{Method}, Validation failed: {ErrorMessage}", nameof(CheckoutSessionConfig), emailError);
				return Results.BadRequest($"{emailError}".Trim());
			}
			email = emailValue;

			return null;

		}
		catch (Exception ex)
		{
			Logger!.LogError(ex, "{Method}, {Message} {Variables}"
			, nameof(CheckValidation), "Error: {ex.Message}"
			, $"registrationId: {registrationId}; email: {email}; ");
			return Results.BadRequest();
		}
	}

	private static SessionCreateOptions GetSessionOptions(
		int registrationId,
		RegistrationFee registrationFee,
		string email,
		string domain)
	{
		return new SessionCreateOptions
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
	}
}

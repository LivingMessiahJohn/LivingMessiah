using Serilog;
using Blazored.Toast;
using LivingMessiahSukkot.Components;

using Auth0.AspNetCore.Authentication;
using static LivingMessiahSukkot.Constants.Auth0;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

using Stripe;
using EndpointsSetting = LivingMessiahSukkot.Settings.EndpointsSetting;
using EndpointsCheckoutSession = LivingMessiahSukkot.Endpoints.CheckoutSession;
using EndpointsWebhook = LivingMessiahSukkot.Endpoints.Webhook;

using static LivingMessiahSukkot.Enums.DonationConstants; 

using AccountEnum = LivingMessiahSukkot.Enums.Account;
using LivingMessiahSukkot.Features.Data;
using LivingMessiahSukkot.Endpoints;
using LivingMessiahSukkot.Endpoints.Constants;

using LifeCyclePhaseSetting = LivingMessiahSukkot.Settings.LifeCyclePhaseSetting;

using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;  // ToDo: remove after configuring with LivingMessia.ServiceDefaults.cs

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

string appSettingJson =
	Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development
	? appSettingJson = "appsettings.Development.json"
	: "appsettings.Production.json";

var configuration = new ConfigurationBuilder().AddJsonFile(appSettingJson).Build();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

Log.Warning("{Class}, {Environment}, AppSettingJsonFile: {AppSettingJsonFile}; "
			, nameof(Program), Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), appSettingJson);

try
{
	builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(configuration));

	//ToDo: Why put this here?
	//LivingMessiahSukkot.Helpers.StartupHelper.DumpSectionConfiguration(builder.Configuration.GetSection(nameof(StripeSettings)), nameof(StripeSettings));

	// Services.Add
	builder.Services.AddBlazoredToast();

	builder.Services.AddAuthorizationBuilder()
			.AddPolicy(Policy.Name, policy =>
				policy.RequireClaim(Policy.Claim, "true"));

	builder.Services.AddApplicationInsightsTelemetry();
	builder.Services.AddSukkotData();

	// Services for Auth0
	builder.Services.AddAuth0WebAppAuthentication(options =>
	{
		options.Domain = builder.Configuration[Configuration.Domain] ?? "";
		options.ClientId = builder.Configuration[Configuration.ClientId] ?? "";
		options.ClientSecret = builder.Configuration[Configuration.ClientSecret] ?? "";
		options.Scope = "openid profile email roles";
		options.OpenIdConnectEvents = new OpenIdConnectEvents
		{
			OnRemoteFailure = ctx =>
			{
				// When the user cancels / denies consent Auth0 returns error=access_denied
				var error = ctx.Request.Query["error"].ToString();
				var errorDesc = ctx.Request.Query["error_description"].ToString();

				if (string.Equals(error, "access_denied", StringComparison.OrdinalIgnoreCase))
				{
					// Prevent the middleware from rethrowing the exception
					ctx.HandleResponse();

					// Redirect to a friendly Blazor page and include details if you want
					var redirectUrl = "/auth-failed";
					if (!string.IsNullOrEmpty(error) || !string.IsNullOrEmpty(errorDesc))
					{
						var q = $"?error={Uri.EscapeDataString(error ?? "")}&error_description={Uri.EscapeDataString(errorDesc ?? "")}";
						redirectUrl += q;
					}

					ctx.Response.Redirect(redirectUrl);
				}
				else
				{
					// Generic fallback for other remote failures
					ctx.HandleResponse();
					ctx.Response.Redirect("/auth-failed?error=remote_failure");
				}

				return Task.CompletedTask;
			}
		};

	});

	builder.Services.Configure<EndpointsSetting>(options => configuration.GetSection(nameof(EndpointsSetting)).Bind(options));
	//LivingMessiahSukkot.Helpers.StartupHelper.DumpSectionConfiguration(builder.Configuration.GetSection(nameof(EndpointsSetting)), nameof(EndpointsSetting));
	builder.Services.Configure<StripeSettings>(options => configuration.GetSection(nameof(StripeSettings)).Bind(options));
	builder.Services.AddRazorComponents().AddInteractiveServerComponents();
	builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

	builder.Services.Configure<LifeCyclePhaseSetting>(options => configuration.GetSection(nameof(LifeCyclePhaseSetting)).Bind(options));
	//LivingMessiahSukkot.Helpers.StartupHelper.DumpSectionConfiguration(builder.Configuration.GetSection(nameof(LifeCyclePhaseSetting)), nameof(LifeCyclePhaseSetting));

	

	var stripeApiKey = builder.Configuration[StripeConstants.ApiKey];
	StripeConfiguration.ApiKey = stripeApiKey;

	var app = builder.Build();

	app.MapDefaultEndpoints();

	// app.Use
	if (!app.Environment.IsDevelopment())
	{
		app.UseExceptionHandler("/Error", createScopeForErrors: true);
		app.UseHsts();
	}
	else
	{
		app.UseDeveloperExceptionPage();
	}

	app.UseSerilogRequestLogging();
	app.UseHttpsRedirection();
	app.UseAntiforgery();

	app.MapStaticAssets(); // new for .Net 9; 

	// Auth0 login/logout MapEndpoints
	app.MapGet(AccountEnum.Login.Index, async (HttpContext httpContext, string returnUrl = "/") =>
	{
		var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
						.WithRedirectUri(returnUrl)
						.Build();
		await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
	});

	app.MapGet(AccountEnum.Logout.Index, async (HttpContext httpContext) =>
	{
		var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
						.WithRedirectUri("/")
						.Build();
		await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
		await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
	});


	app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

	// Stripe Endpoints
	EndpointsSetting? endpointsSetting = configuration.GetSection(nameof(EndpointsSetting)).Get<EndpointsSetting>();
	EndpointsCheckoutSession.CheckoutSessionConfig(app, BaseSessionUrl, endpointsSetting!.Domain!);
	EndpointsWebhook.WebhookConfig(app, WebHookUrl);


	app.Run();
	Log.Information("{Class}, Stopped cleanly", nameof(Program));
	return 0;
}
catch (Exception ex)
{
	Log.Fatal(ex, "{Class}, An unhandled exception occurred during bootstrapping", nameof(Program));
	return 1;
}
finally
{
	Log.CloseAndFlush();
}

/*
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
*/
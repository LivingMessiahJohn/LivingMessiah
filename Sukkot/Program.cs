using Serilog;
using Blazored.Toast;
using Sukkot.Components;

using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using NavEnums = Sukkot.Enums.Nav;

using Stripe;
using EndpointsSetting = Sukkot.Settings.EndpointsSetting;
using EndpointsCheckoutSession = Sukkot.Endpoints.CheckoutSession;
using EndpointsWebhook = Sukkot.Endpoints.Webhook;

using static Sukkot.Enums.DonationConstants;

using static Sukkot.Features.Components.LifeCycleAuthority.ServiceCollectionExtensions;

using Microsoft.Extensions.Hosting;
using Sukkot.Endpoints;
using Sukkot.Security;
using Sukkot.Security.Constants;
using Sukkot.Features.Components.LifeCycleAuthority;
using Sukkot.Endpoints.Constants;
using Sukkot.Features.Data;
using Sukkot.Features.LandingPage.Data;

// OpenTelemetry
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Azure.Monitor.OpenTelemetry.Exporter;
using Sukkot.Endpoints.Data;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

string appSettingJson =
	Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development
	? "appsettings.Development.json"
	: "appsettings.Production.json";

var configuration = new ConfigurationBuilder().AddJsonFile(appSettingJson).Build();

// Application Insights connection string (env var preferred)
var aiConnectionString = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING")
	?? configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];

Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(configuration)
	.CreateLogger();

Log.Warning("{Class}, {Environment}, AppSettingJsonFile: {AppSettingJsonFile}; ApplicationInsightsConfigured: {ApplicationInsightsConfigured}",
	nameof(Program),
	Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
	appSettingJson,
	!string.IsNullOrWhiteSpace(aiConnectionString));

try
{
	builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(configuration));

	builder.Logging.AddOpenTelemetry(options =>
	{
		options.IncludeScopes = true;
		options.IncludeFormattedMessage = true;
		options.ParseStateValues = true;
		options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Sukkot"));
		if (!string.IsNullOrWhiteSpace(aiConnectionString))
		{
			options.AddAzureMonitorLogExporter(o => o.ConnectionString = aiConnectionString);
		}
		else
		{
			options.AddConsoleExporter();
		}
	});

	builder.Services.AddOpenTelemetry()
			.WithTracing(tracerProviderBuilder =>
			{
				tracerProviderBuilder
					.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Sukkot"))
					.AddAspNetCoreInstrumentation()
					.AddHttpClientInstrumentation();

				if (!string.IsNullOrWhiteSpace(aiConnectionString))
				{
					tracerProviderBuilder.AddAzureMonitorTraceExporter(o => o.ConnectionString = aiConnectionString);
				}
				else
				{
					tracerProviderBuilder.AddConsoleExporter(); // from OpenTelemetry.Exporter.Console
				}
			});

	// Services.Add
	builder.Services.AddBlazoredToast();

	builder.Services.AddAuthorizationBuilder()
			.AddPolicy(Policy.Name, policy =>
				policy.RequireClaim(Policy.Claim, "true"));

	builder.Services.AddSukkotData();
	builder.Services.AddEndpointsData();
	builder.Services.AddSukkotDailyScheduleData();	


	builder.Services.AddAuth0Authentication(builder.Configuration);

	builder.Services.Configure<EndpointsSetting>(options => configuration.GetSection(nameof(EndpointsSetting)).Bind(options));
	builder.Services.Configure<StripeSettings>(options => configuration.GetSection(nameof(StripeSettings)).Bind(options));
	builder.Services.AddRazorComponents().AddInteractiveServerComponents();
	builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

	builder.Services.AddLifeCyclePhaseSetting(configuration);

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

	app.MapGet(NavEnums.Login.Index, async (HttpContext httpContext, string returnUrl = "/") =>
	{
		var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
						.WithRedirectUri(returnUrl)
						.Build();
		await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
	});

	app.MapGet(NavEnums.Logout.Index, async (HttpContext httpContext) =>
	{
		var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
						.WithRedirectUri("/")
						.Build();
		await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
		await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
	});


	app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

	// Stripe Endpoints
	// using static Sukkot.Enums.DonationConstants;
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

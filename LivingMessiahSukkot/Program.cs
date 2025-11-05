using Serilog;
using Blazored.Toast;
using LivingMessiahSukkot.Components;

using Auth0.AspNetCore.Authentication;

using LivingMessiahSukkot.Security; 
using LivingMessiahSukkot.Security.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using NavEnums = LivingMessiahSukkot.Enums.Nav;

using Stripe;
using EndpointsSetting = LivingMessiahSukkot.Settings.EndpointsSetting;
using EndpointsCheckoutSession = LivingMessiahSukkot.Endpoints.CheckoutSession;
using EndpointsWebhook = LivingMessiahSukkot.Endpoints.Webhook;

using static LivingMessiahSukkot.Enums.DonationConstants;

using LivingMessiahSukkot.Features.Data;
using LivingMessiahSukkot.Endpoints;
using LivingMessiahSukkot.Endpoints.Constants;

using static LivingMessiahSukkot.Features.Components.LifeCycleAuthority.ServiceCollectionExtensions;

using Microsoft.Extensions.Hosting;

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
	//LivingMessiahSukkot.Helpers.StartupHelper.DumpSectionConfiguration(builder.Configuration.GetSection(nameof(StripeSettings)), nameof(StripeSettings);

	// Services.Add
	builder.Services.AddBlazoredToast();

	builder.Services.AddAuthorizationBuilder()
			.AddPolicy(Policy.Name, policy =>
				policy.RequireClaim(Policy.Claim, "true"));

	builder.Services.AddApplicationInsightsTelemetry();
	builder.Services.AddSukkotData();
		
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
	// using static LivingMessiahSukkot.Enums.DonationConstants;
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

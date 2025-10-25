using Serilog;
using Syncfusion.Blazor;
using Blazored.Toast;
using Blazored.LocalStorage;
using LivingMessiah.Components;
using LivingMessiah.Features.Calendar;
using LivingMessiah.Features.FeastDayPlanner.Data;
using LivingMessiah.Settings;
using LivingMessiah.State;

using Auth0.AspNetCore.Authentication;
using static LivingMessiah.SecurityRoot.Auth0;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using Stripe;
using LivingMessiah.Enums;
//using OneTimeEndpoints = LivingMessiah.Features.OneTime.Endpoints;

using SukkotSettings = LivingMessiah.Features.Sukkot.Settings;
using SukkotEndpointsCheckoutSession = LivingMessiah.Features.Sukkot.Endpoints.CheckoutSession;
using SukkotEndpointsWebhook = LivingMessiah.Features.Sukkot.Endpoints.Webhook;

using AccountEnum = LivingMessiah.Enums.Account;
using LivingMessiah.Features.Sukkot.Data;
using LivingMessiah.Features.Sukkot.Endpoints;
using LivingMessiah.Features.Sukkot.Endpoints.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

string appSettingJson =
	Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development
	? appSettingJson = "appsettings.Development.json"
	: "appsettings.Production.json";

var configuration = new ConfigurationBuilder()
	.AddJsonFile(appSettingJson)
	.Build();

Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(configuration)
	.CreateLogger();

Log.Warning("{Class}, {Environment}, AppSettingJsonFile: {AppSettingJsonFile}; "
			, nameof(Program), Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), appSettingJson);

try
{
	builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(configuration));
	//LivingMessiah.Helpers.LogHelper.DumpSectionConfiguration(builder.Configuration.GetSection("Stripe"), "Stripe");

	/*
	Log.Warning("{Class}, SyncfusionLicense: {SyncfusionLicense}",
		nameof(Program),
		string.IsNullOrEmpty(builder.Configuration["SyncfusionLicense"])
				? "IsNullOrEmpty"
				: builder.Configuration["SyncfusionLicense"]);
	*/
	Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration["SyncfusionLicense"] ?? "");

	builder.Services.AddBlazoredToast();
	builder.Services.AddBlazoredLocalStorage();

	/*
	builder.Services.AddSingleton<AppState>(); 
	==> Cannot consume scoped service 'Blazored.LocalStorage.ILocalStorageService' from singleton 'LivingMessiah.State.AppState'. 
	 */

	builder.Services.AddAuthorizationBuilder()
			.AddPolicy(Policy.Name, policy =>
				policy.RequireClaim(Policy.Claim, "true"));

	builder.Services.AddScoped<AppState>(); // Scoped is more common for Blazor Server apps	

	builder.Services.AddApplicationInsightsTelemetry();


	//Services
	//builder.Services.AddOneTimeData(); ToDo: add
	builder.Services.AddSukkotData();
	builder.Services.AddCalendar();
	builder.Services.AddFeastDayPlanner();

	builder.Services.AddAuth0WebAppAuthentication(options =>
	{
		options.Domain = builder.Configuration[Configuration.Domain] ?? "";
		options.ClientId = builder.Configuration[Configuration.ClientId] ?? "";
		options.ClientSecret = builder.Configuration[Configuration.ClientSecret] ?? "";
		options.Scope = "openid profile email roles";
	});

	//builder.Services.AddScoped<TokenProvider>();
	//TokenProvider used by _Host App


	builder.Services.Configure<AppSettings>(options => configuration.GetSection(nameof(AppSettings)).Bind(options));
	//LivingMessiah.Helpers.LogHelper.DumpSectionConfiguration(builder.Configuration.GetSection("AppSettings"), "AppSettings");
	
	builder.Services.Configure<SukkotSettings>(options => configuration.GetSection(nameof(SukkotSettings)).Bind(options));
	//LivingMessiah.Helpers.LogHelper.DumpSectionConfiguration(builder.Configuration.GetSection(nameof(SukkotSettings)), nameof(SukkotSettings));

	builder.Services.Configure<StripeSettings>(options => configuration.GetSection(nameof(StripeSettings)).Bind(options));

	builder.Services.AddRazorComponents()
			.AddInteractiveServerComponents();

	builder.Services.AddSyncfusionBlazor();

	builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

	/*
	 ToDo: this doesn't work but the code after it does.
	 var sukkotStripeSettings = configuration.GetSection(nameof(Settings)).Get<Settings>();
	 StripeConfiguration.ApiKey = sukkotStripeSettings?.ApiKey ?? "";
  */
	
	var stripeApiKey = builder.Configuration[StripeConstants.ApiKey];
	StripeConfiguration.ApiKey = stripeApiKey;

	var app = builder.Build();
	app.MapDefaultEndpoints();

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

	#region Auth0 login/logout MapEndpoints
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
	#endregion

	app.MapRazorComponents<App>()
			.AddInteractiveServerRenderMode();

	#region Sukkot Stripe Endpoints
	SukkotSettings? sukkotSettings = configuration.GetSection(nameof(SukkotSettings)).Get<SukkotSettings>();
	SukkotEndpointsCheckoutSession.CheckoutSessionConfig(app, Donation.SukkotRegistration.SessionUrl, sukkotSettings!.Domain!);
	SukkotEndpointsWebhook.WebhookConfig(app, Donation.SukkotRegistration.WebhookUrl);

	#endregion

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


using Auth0.AspNetCore.Authentication;
using Blazored.Toast;
using LivingMessiahAdmin.Components;
using LivingMessiahAdmin.Features.Database;
using LivingMessiahAdmin.Features.KeyDates.Data;
using LivingMessiahAdmin.Features.Sukkot.Dashboard.Data;
using LivingMessiahAdmin.Features.Sukkot.Home.Data;
using LivingMessiahAdmin.Features.Sukkot.Home.Donations.Data;
using LivingMessiahAdmin.Features.Sukkot.Notes.Data;
using LivingMessiahAdmin.Features.Sukkot.Reports.Data;

using LivingMessiahAdmin.Features.WeeklyDownloads.Data;
using LivingMessiahAdmin.SecurityRoot;
//using LivingMessiahAdmin.Features.FeastDayPlanner.Data;
using LivingMessiahAdmin.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using static LivingMessiahAdmin.SecurityRoot.Auth0;
using AccountEnum = LivingMessiahAdmin.Enums.Account;

using HealthChecksSukkot = LivingMessiahAdmin.HealthChecks.Sukkot;
using HealthChecksSukkotEndPoint = LivingMessiahAdmin.HealthChecks.Sukkot.Endpoints.Constants.StripeConstants;

using WeeklyDownloadsSettings = LivingMessiahAdmin.Features.WeeklyDownloads.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
Log.Warning("{Class}, Environment: {Environment}; ", nameof(Program), Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

try
{
	builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(builder.Configuration));

	builder.Services.AddBlazoredToast();
	builder.Services.AddAuthorizationPolicies();

	//Services
	builder.Services.AddDatabase();
	builder.Services.AddSukkotGridData();
	builder.Services.AddSukkotData(); // CrUD
	builder.Services.AddSukkotDonationsData();
	builder.Services.AddManageNotes();
	builder.Services.AddReports();
	builder.Services.AddManageKeyDates();

	builder.Services.AddRazorComponents().AddInteractiveServerComponents();

	builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));


	//builder.Services.AddFeastDayPlanner();
	//builder.Services.AddApplicationInsightsTelemetry();

	builder.Services.Configure<HealthChecksSukkot.Settings.Stripe>(builder.Configuration.GetSection(nameof(HealthChecksSukkot.Settings.Stripe)));
	builder.Services.AddHealthChecks().AddCheck<HealthChecksSukkot.StripeWebhookHealthCheck>(HealthChecksSukkotEndPoint.HealthCheckName);

	builder.Services.Configure<WeeklyDownloadsSettings.AzureBlob>(builder.Configuration.GetSection(nameof(WeeklyDownloadsSettings.AzureBlob)));
	builder.Services.AddAzureBlobService();

	builder.Services.AddAuth0WebAppAuthentication(options =>
	{
		options.Domain = builder.Configuration[Configuration.Domain] ?? string.Empty;
		options.ClientId = builder.Configuration[Configuration.ClientId] ?? string.Empty;
		options.ClientSecret = builder.Configuration[Configuration.ClientSecret] ?? string.Empty;
		options.Scope = "openid profile email roles";
	});


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
	app.UseStatusCodePagesWithReExecute("/Error"); // Suggested by Grok
	app.UseHttpsRedirection();
	app.UseAntiforgery();
	app.MapStaticAssets();

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

	app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
	app.MapHealthChecks(HealthChecksSukkot.Endpoints.Constants.StripeConstants.HealthCheckUrl);

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

static void ValidateAppSettings(IConfigurationRoot configuration, bool displayValues = false)
{
	const string err1 = "Configuration error: AppSettings section is missing or invalid.";
	const string err2 = "Configuration error: AppSettings:YearId must be set to a non-zero value.";

	var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
	if (appSettings is null)
	{
		Log.Warning("{Class}, {Method}, {Message}, ", nameof(Program), nameof(ValidateAppSettings), err1);
		throw new InvalidOperationException(err1);
	}
	if (appSettings.YearId == 0)
	{
		Log.Warning("{Class}, {Method}, {Message}, ", nameof(Program), nameof(ValidateAppSettings), err2);
		throw new InvalidOperationException(err2);
	}

	if(displayValues) 
	{
		LivingMessiahAdmin.Helpers.StartupHelper.DumpSectionConfiguration(configuration.GetSection("AppSettings"), "AppSettings");
	}

}
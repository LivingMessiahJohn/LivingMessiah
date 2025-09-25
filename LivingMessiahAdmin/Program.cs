using Serilog;
//using Syncfusion.Blazor;
using Blazored.Toast;
using Blazored.LocalStorage;
using LivingMessiahAdmin.Components;
using LivingMessiahAdmin.Features.KeyDates.Data;
//using LivingMessiahAdmin.Features.FeastDayPlanner.Data;
using LivingMessiahAdmin.Settings;
using LivingMessiahAdmin.State;

using Auth0.AspNetCore.Authentication;
using static LivingMessiahAdmin.SecurityRoot.Auth0;
using LivingMessiahAdmin.SecurityRoot;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


//using System.Text.Json;
using AccountEnum = LivingMessiahAdmin.Enums.Account;
using LivingMessiahAdmin.Features.Database;
using LivingMessiahAdmin.Features.Sukkot.Dashboard.Data;
using LivingMessiahAdmin.Features.Sukkot.Home.Data;
using LivingMessiahAdmin.Features.Sukkot.Notes.Data;
using LivingMessiahAdmin.Features.Sukkot.Reports.Data;

//using Stripe; 
using HealthChecksSukkot = LivingMessiahAdmin.HealthChecks.Sukkot;



var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

string appSettingJson =
	Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development
	? appSettingJson = "appsettings.Development.json"
	: "appsettings.Production.json";

var configuration = new ConfigurationBuilder()
	.AddJsonFile(appSettingJson)
	.Build();

builder.Services.AddRazorComponents()
		.AddInteractiveServerComponents();

Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(configuration)
	.CreateLogger();

Log.Warning("{Class}, {Environment}, AppSettingJsonFile: {AppSettingJsonFile}; "
			, nameof(Program), Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), appSettingJson);

try
{
	builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(configuration));

	builder.Services.AddBlazoredToast();
	builder.Services.AddBlazoredLocalStorage();

	builder.Services.AddAuthorizationPolicies();

	builder.Services.AddScoped<AppState>(); // Scoped is more common for Blazor Server apps	

	//Services
	builder.Services.AddDatabase();
	builder.Services.AddSukkotGridData();
	builder.Services.AddSukkotData(); // CrUD
	builder.Services.AddManageNotes();
	builder.Services.AddReports();
	builder.Services.AddManageKeyDates();
	builder.Services.Configure<AppSettings>(options => configuration.GetSection("AppSettings").Bind(options));
	//builder.Services.AddFeastDayPlanner();
	//builder.Services.AddApplicationInsightsTelemetry();
	builder.Services.Configure<HealthChecksSukkot.Settings.Stripe>(options => configuration.GetSection(nameof(Stripe)).Bind(options));
	builder.Services.AddHealthChecks()
    .AddCheck<HealthChecksSukkot.StripeWebhookHealthCheck>(HealthChecksSukkot.Endpoints.Constants.StripeConstants.HealthCheckName); 

	builder.Services.AddAuth0WebAppAuthentication(options =>
	{
		options.Domain = builder.Configuration[Configuration.Domain] ?? "";
		options.ClientId = builder.Configuration[Configuration.ClientId] ?? "";
		options.ClientSecret = builder.Configuration[Configuration.ClientSecret] ?? "";
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


using Serilog;
//using Syncfusion.Blazor;
using Blazored.Toast;
using Blazored.LocalStorage;
using LivingMessiahAdmin.Components;
//using LivingMessiahAdmin.Features.Calendar;
//using LivingMessiahAdmin.Features.FeastDayPlanner.Data;
using LivingMessiahAdmin.Settings;
using LivingMessiahAdmin.State;

using Auth0.AspNetCore.Authentication;
using static LivingMessiahAdmin.SecurityRoot.Auth0;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

//using Stripe;
//using System.Text.Json;
using AccountEnum = LivingMessiahAdmin.Enums.Account;
using LivingMessiahAdmin.Features.Sukkot.Data;


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

	builder.Services.AddAuthorizationBuilder()
		.AddPolicy(Policy.Name, policy =>
			policy.RequireClaim(Policy.Claim, "true"));

	builder.Services.AddScoped<AppState>(); // Scoped is more common for Blazor Server apps	
																					
	//Services
	builder.Services.AddSukkotData();
	//builder.Services.AddCalendar();
	//builder.Services.AddFeastDayPlanner();

	//builder.Services.AddApplicationInsightsTelemetry();

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

	app.MapRazorComponents<App>()
			.AddInteractiveServerRenderMode();

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


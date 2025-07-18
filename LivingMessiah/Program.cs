using Serilog;
using Syncfusion.Blazor;
using Blazored.Toast;
using Blazored.LocalStorage;

using LivingMessiah;
using LivingMessiah.Components;
using LivingMessiah.Features.Calendar;
using LivingMessiah.Features.FeastDayPlanner.Data;
using LivingMessiah.Settings;
using LivingMessiah.State;

using Auth0.AspNetCore.Authentication;
using static LivingMessiah.SecurityRoot.Auth0;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AccountEnum = LivingMessiah.Enums.Account;

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
	builder.Services.AddDataStores();
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

	builder.Services.Configure<AppSettings>(options => configuration.GetSection("AppSettings").Bind(options));
	builder.Services.Configure<SukkotSettings>(options => configuration.GetSection("SukkotSettings").Bind(options));

	builder.Services.AddRazorComponents()
			.AddInteractiveServerComponents();

	builder.Services.AddSyncfusionBlazor();

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
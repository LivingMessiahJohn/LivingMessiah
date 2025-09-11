using Serilog;
//using Syncfusion.Blazor;
using Blazored.Toast;
using Blazored.LocalStorage;
using LivingMessiahAdmin.Components;
//using LivingMessiahAdmin.Features.Calendar;
//using LivingMessiahAdmin.Features.FeastDayPlanner.Data;
using LivingMessiahAdmin.Settings;
using LivingMessiahAdmin.State;

using RoleEnum = LivingMessiahAdmin.Enums.Role;
using RoleGroup = LivingMessiahAdmin.Enums.RoleGroup;

using Auth0.AspNetCore.Authentication;
using static LivingMessiahAdmin.SecurityRoot.Auth0;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

//using Stripe;
//using System.Text.Json;
using AccountEnum = LivingMessiahAdmin.Enums.Account;
using LivingMessiahAdmin.Features.Database;
using LivingMessiahAdmin.Features.Sukkot.Home.Data;
using LivingMessiahAdmin.Features.Sukkot.Notes.Data;
using LivingMessiahAdmin.Features.Sukkot.Reports.Data;

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
		.AddPolicy(RoleGroup.EmailVerified.Name, policy => policy.RequireClaim(RoleGroup.EmailVerified.Claim, "true"))

		.AddPolicy(RoleGroup.Announcements, policy =>
			policy.RequireAssertion(context =>
				context.User.IsInRole(RoleEnum.Announcements.Claim) ||
				context.User.IsInRole(RoleEnum.Admin.Claim)))

		.AddPolicy(RoleGroup.KeyDates, policy =>
			policy.RequireAssertion(context =>
				context.User.IsInRole(RoleEnum.KeyDates.Claim) ||
				context.User.IsInRole(RoleEnum.Admin.Claim)))

		.AddPolicy(RoleGroup.Sukkot, policy =>
			policy.RequireAssertion(context =>
				context.User.IsInRole(RoleEnum.Sukkot.Claim) ||
				context.User.IsInRole(RoleEnum.SukkotHost.Claim) ||
				context.User.IsInRole(RoleEnum.Admin.Claim)))

		.AddPolicy(RoleGroup.SukkotHost, policy =>
			policy.RequireAssertion(context =>
				context.User.IsInRole(RoleEnum.SukkotHost.Claim) ||
				context.User.IsInRole(RoleEnum.Admin.Claim)))


		//.AddPolicy(RoleEnum.Announcements.Name, policy => policy.RequireRole(RoleEnum.Announcements.Claim, "true"))
		//.AddPolicy(RoleEnum.KeyDates.Name, policy => policy.RequireRole(RoleEnum.KeyDates.Claim, "true"))
		//.AddPolicy(RoleEnum.SukkotHost.Name, policy => policy.RequireRole(RoleEnum.SukkotHost.Claim, "true"))

		//.AddPolicy(RoleEnum.Sukkot.Name, policy => policy.RequireRole(RoleEnum.Sukkot.Claim, "true"))
		//.AddPolicy(RoleEnum.SukkotHost.Name, policy => policy.RequireRole(RoleEnum.SukkotHost.AndAdminClaim, "true"))
		//.AddPolicy(RoleEnum.SukkotOrSukkotHost.Name, policy => policy.RequireRole(RoleEnum.SukkotOrSukkotHost.AndAdminClaim, "true"))

		/*
		.AddPolicy(RoleGroup.SukkotHost, policy =>
			policy.RequireAssertion(context =>
				context.User.IsInRole(RoleEnum.Elder.Claim) ||
				context.User.IsInRole(RoleEnum.SukkotHost.Claim) ||
				context.User.IsInRole(RoleEnum.Admin.Claim)))
		*/

		.AddPolicy(RoleEnum.Admin.Name, policy => policy.RequireRole(RoleEnum.Admin.Claim, "true"));

	builder.Services.AddScoped<AppState>(); // Scoped is more common for Blazor Server apps	

	//Services
	builder.Services.AddDatabase();
	builder.Services.AddSukkotData();
	builder.Services.AddManageNotes();
	builder.Services.AddReports();
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


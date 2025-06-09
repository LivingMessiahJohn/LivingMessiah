using Microsoft.Extensions.DependencyInjection;

using Serilog;

using LivingMessiah.Components;
using LivingMessiah.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

string appSettingJson = string.Empty;
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
{
	appSettingJson = "appsettings.Development.json";
}
else
{
	appSettingJson = "appsettings.Production.json";
}

var configuration = new ConfigurationBuilder()
	.AddJsonFile(appSettingJson)
	.Build();

Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(configuration)
	.CreateLogger();

Log.Warning("{Class}, {Environment}, {AppSettings}; save to Serilog console and file sinks."
			, nameof(Program), Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), appSettingJson);

try
{

	builder.Host.UseSerilog((ctx, lc) =>
	lc.ReadFrom.Configuration(configuration));
	builder.Services.Configure<AppSettings>(options => configuration.GetSection("AppSettings").Bind(options));

	builder.Services.AddRazorComponents()
			.AddInteractiveServerComponents();

	builder.Configuration.AddUserSecrets<Program>();

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
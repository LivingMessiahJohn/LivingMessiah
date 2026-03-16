using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PWA;
using PWA.Features.Home;
using PWA.Features.Home.WeeklyDownload.Settings;
using PWA.Features.FeastDayPlanner.Data;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configure Serilog for WebAssembly (logs to browser console)
/*
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Is(builder.HostEnvironment.IsDevelopment()
		? Serilog.Events.LogEventLevel.Debug
		: Serilog.Events.LogEventLevel.Information)
	.WriteTo.BrowserConsole()
	.CreateLogger();
*/

Log.Logger = new LoggerConfiguration()
		.MinimumLevel.Information()
		.WriteTo.BrowserConsole()
		.CreateLogger();

builder.Logging.AddSerilog(Log.Logger);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.Configure<AzureBlob>(builder.Configuration.GetSection("AzureBlob"));

/*
ToDo: Move this logic to an Azure Function solution
builder.Services.AddAzureBlobService();
*/

builder.Services.AddFeastDayPlanner();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped<MarkdownService>();
builder.Services.AddPWAUpdater();

Log.Information("PWA WebAssembly App Starting...");

try
{
	await builder.Build().RunAsync();
	Log.Information("PWA WebAssembly App Stopped cleanly");
}
catch (Exception ex)
{
	Log.Fatal(ex, "PWA WebAssembly App terminated unexpectedly");
	throw;
}
finally
{
	await Log.CloseAndFlushAsync();
}

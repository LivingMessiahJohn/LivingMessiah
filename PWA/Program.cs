using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PWA;
using PWA.Features.Home;
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

// Use Azure Function API for blob operations
// When running under Aspire, the API endpoint is provided via service discovery
// In production (Azure Static Web Apps), the API is at the same base address
var apiBaseAddress = builder.Configuration["services:api:https:0"] 
	?? builder.Configuration["services:api:http:0"]
	?? (builder.HostEnvironment.IsDevelopment() 
		? "http://localhost:7071"  // Fallback for running PWA standalone
		: builder.HostEnvironment.BaseAddress);  // Same as app base address in production

Log.Information("API Base Address: {ApiBaseAddress}", apiBaseAddress);

builder.Services.AddBlobApiService(apiBaseAddress);

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

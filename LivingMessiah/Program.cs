using LivingMessiah.Components;

using Microsoft.Extensions.DependencyInjection;
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

builder.Services.Configure<DonationSettings>(
	options => configuration.GetSection("DonationSettings").Bind(options));

builder.Services.AddRazorComponents()
		.AddInteractiveServerComponents();

var app = builder.Build();
app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets(); // new for .Net 9; 
app.MapRazorComponents<App>()
		.AddInteractiveServerRenderMode();

app.Run();

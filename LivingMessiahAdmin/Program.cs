using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using LivingMessiahAdmin.Components;
using Microsoft.AspNetCore.Authentication; // Added later per IntelliSense suggestions

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
		.AddInteractiveServerComponents();

// Add Microsoft Entra ID authentication
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
		.AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("RequireAuthenticatedUser", policy =>
			policy.RequireAuthenticatedUser());
});

var app = builder.Build();

app.MapDefaultEndpoints();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error"); // Suggested by Grok
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map authentication endpoints for sign-in and sign-out
app.MapGet("/auth/signin", async (HttpContext context) =>
{
	if (!context.User.Identity!.IsAuthenticated)
	{
		await context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
	}
	return Results.Redirect("/");
});

app.MapPost("/auth/signout", async (HttpContext context) =>
{
	await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
	return Results.Redirect("/");
});

app.MapRazorComponents<App>()
		.AddInteractiveServerRenderMode();

app.Run();

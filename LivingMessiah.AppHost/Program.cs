var builder = DistributedApplication.CreateBuilder(args);

// ToDo: Lot's of redundancy here with the blob configuration.

// Azure Storage + SpecialEvent DB configuration from User Secrets
var storageConnectionString = builder.Configuration["AzureStorageConnectionString"]
	?? throw new InvalidOperationException("AzureStorageConnectionString not configured in secrets");
var blobContainerName = builder.Configuration["BlobContainerName"]
	?? throw new InvalidOperationException("BlobContainerName not configured in secrets");
var specialEventConnectionString = builder.Configuration["SpecialEventConnectionString"]
	?? throw new InvalidOperationException("SpecialEventConnectionString not configured in secrets");

// Add Azure Functions API with configuration
var apiService = builder.AddProject<Projects.Api>("api")
	.WithEnvironment("AzureStorageConnectionString", storageConnectionString)
	.WithEnvironment("BlobContainerName", blobContainerName)
	.WithEnvironment("SpecialEventConnectionString", specialEventConnectionString);

#region custom URLs column
// Customize Aspire dashboard link labels only.
// Do NOT set ExcludeLaunchProfile = true here: that drops each project's launchSettings.json
// (applicationUrl → ASPNETCORE_URLS and ASPNETCORE_ENVIRONMENT=Development). Dashboard tabs
// still open, but Admin/Sukkot often fail to bind/load. Ports live in each project's https profile.
//
// PWA  → https://localhost:7211
// Admin → https://localhost:7191
// Sukkot → https://localhost:7201

const int PwaHttpsPort = 7211;
builder.AddProject<Projects.PWA>("pwa")
	.WithReference(apiService)
	.WithUrlForEndpoint("https", url =>
	{
		url.DisplayText = $"PWA ({PwaHttpsPort})";
	});

const int AdminHttpsPort = 7191;
builder.AddProject<Projects.Admin>("admin")
	.WithUrlForEndpoint("https", url =>
	{
		url.DisplayText = $"Admin ({AdminHttpsPort})";
	});

const int SukkotHttpsPort = 7201;
builder.AddProject<Projects.Sukkot>("sukkot")
	.WithUrlForEndpoint("https", url =>
	{
		url.DisplayText = $"Sukkot ({SukkotHttpsPort})";
	});
#endregion

builder.Build().Run();

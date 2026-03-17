var builder = DistributedApplication.CreateBuilder(args);

// Azure Storage configuration from User Secrets
var storageConnectionString = builder.Configuration["AzureStorageConnectionString"] 
    ?? throw new InvalidOperationException("AzureStorageConnectionString not configured in secrets");
var blobContainerName = builder.Configuration["BlobContainerName"] 
    ?? throw new InvalidOperationException("BlobContainerName not configured in secrets");

// Add Azure Functions API with configuration
var apiService = builder.AddProject<Projects.Api>("api")
    .WithEnvironment("AzureStorageConnectionString", storageConnectionString)
    .WithEnvironment("BlobContainerName", blobContainerName);

// Add PWA with reference to API
builder.AddProject<Projects.PWA>("pwa")
    .WithReference(apiService);

builder.AddProject<Projects.LivingMessiah>("livingmessiah");

builder.AddProject<Projects.LivingMessiahAdmin>("livingmessiahadmin");

builder.AddProject<Projects.LivingMessiahSukkot>("livingmessiahsukkot");

builder.Build().Run();

# Secret Management Strategy for Living Messiah

## Overview

This project uses **different secret management strategies** depending on the environment:

| Environment | PWA (Blazor WASM) | API (Azure Functions) | Secrets Location |
|-------------|-------------------|----------------------|------------------|
| **Local Dev (Aspire)** | No secrets | Env vars from Aspire | `dotnet user-secrets` in AppHost |
| **Local Dev (Standalone)** | No secrets | `local.settings.json` | `Api/local.settings.json` (gitignored) |
| **Azure Production** | No secrets | Env vars from Azure | Azure Portal > Environment Variables |

## Why This Approach?

### ❌ Blazor WASM Cannot Have Secrets
- Runs entirely in the browser
- All code is downloaded and visible
- **Never** put connection strings in PWA

### ✅ Azure Functions API Holds Secrets
- Runs on server (Azure or local)
- Can securely access Azure Storage
- PWA calls the API, which uses secrets

---

## Local Development with Aspire ⭐ **RECOMMENDED**

### Benefits
- ✅ Single place to manage secrets (User Secrets)
- ✅ All projects run together with proper configuration
- ✅ Service discovery between PWA and API
- ✅ Aspire dashboard for monitoring

### Setup

#### 1. Store Secrets Using User Secrets
```powershell
# Navigate to AppHost project
cd LivingMessiah.AppHost

# Set your Azure Storage connection string
dotnet user-secrets set "AzureStorageConnectionString" "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT;AccountKey=YOUR_KEY;EndpointSuffix=core.windows.net"

# Set your blob container name
dotnet user-secrets set "BlobContainerName" "your-container-name"

# List secrets to verify
dotnet user-secrets list
```

#### 2. Run from Aspire AppHost
```powershell
# Set AppHost as startup project in Visual Studio
# OR run from command line:
cd LivingMessiah.AppHost
dotnet run
```

This will:
- Start all projects (PWA, API, other Blazor apps)
- Inject secrets into the API as environment variables
- Configure PWA to reference the API
- Open the Aspire dashboard

### How It Works

**`LivingMessiah.AppHost/Program.cs`:**
```csharp
// Reads from User Secrets
var storageConnectionString = builder.Configuration["AzureStorageConnectionString"];
var blobContainerName = builder.Configuration["BlobContainerName"];

// Injects into API as environment variables
var apiService = builder.AddProject<Projects.Api>("api")
    .WithEnvironment("AzureStorageConnectionString", storageConnectionString)
    .WithEnvironment("BlobContainerName", blobContainerName);

// PWA references API
builder.AddProject<Projects.PWA>("pwa")
    .WithReference(apiService);
```

**User Secrets Location:**
```
%APPDATA%\Microsoft\UserSecrets\7e978837-0419-41d5-8485-cca5326c844a\secrets.json
```

---

## Local Development WITHOUT Aspire (Standalone)

If you need to run the API standalone (e.g., debugging just the function):

### Setup

#### 1. Update `Api/local.settings.json`
```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "AzureStorageConnectionString": "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT;AccountKey=YOUR_KEY;EndpointSuffix=core.windows.net",
    "BlobContainerName": "your-container-name"
  }
}
```

⚠️ **This file is gitignored** - your real secrets stay local.

#### 2. Run Azure Functions
```powershell
cd Api
func start
```

The function will be at: `http://localhost:7071/api/blob-info`

#### 3. Update PWA to use standalone API
The PWA is already configured to use `http://localhost:7071` in development mode.

---

## Azure Production Deployment

### Where Secrets Live in Azure

**NOT** in your code or Git repository. They live in **Azure Portal**:

1. Navigate to your **Static Web App** in Azure Portal
2. Go to **Settings** > **Environment variables**
3. Add **Application settings**:

| Name | Value |
|------|-------|
| `AzureStorageConnectionString` | Your Azure Storage connection string |
| `BlobContainerName` | Your container name (e.g., "pdfs") |

### How to Get Your Connection String

**Option 1: Azure Portal**
1. Go to your **Storage Account**
2. **Security + networking** > **Access keys**
3. Click **Show** next to Key1
4. Copy the **Connection string**

**Option 2: Azure CLI**
```powershell
az storage account show-connection-string `
  --name your-storage-account-name `
  --resource-group your-resource-group
```

### Deployment Process

1. Push code to GitHub
2. GitHub Actions deploys PWA + API together
3. API reads secrets from Azure Environment Variables
4. PWA calls API at: `https://your-app.azurestaticapps.net/api/*`

---

## Security Checklist

### ✅ Files That ARE Committed to Git
- `Api/local.settings.json` - **WITH DUMMY VALUES**
- All source code
- Project files

### ❌ Files That ARE NOT Committed (gitignored)
- `Api/local.settings.json` - **WITH REAL SECRETS** (overwrite locally)
- User Secrets (`%APPDATA%\Microsoft\UserSecrets\...`)
- `bin/`, `obj/`, build outputs

### 🔒 Secret Storage Locations

| Location | What | Protected? |
|----------|------|------------|
| **Git Repository** | No secrets | ✅ Public/Private repo |
| **Local Machine (User Secrets)** | Real secrets | ✅ Your machine only |
| **Local Machine (`local.settings.json`)** | Real secrets | ✅ Gitignored |
| **Azure Portal (Env Variables)** | Real secrets | ✅ Azure RBAC |
| **PWA (Browser)** | NO SECRETS | ⚠️ Everything is visible |

---

## Comparison to Other .NET Project Types

| Project Type | Local Secrets | Production Secrets |
|--------------|--------------|-------------------|
| **Blazor Server** | `secrets.json` via User Secrets | Azure App Settings / Key Vault |
| **ASP.NET Core API** | `secrets.json` via User Secrets | Azure App Settings / Key Vault |
| **Azure Functions** | `local.settings.json` | Azure App Settings / Key Vault |
| **Blazor WASM** | ❌ No secrets (browser-based) | ❌ No secrets |
| **Aspire AppHost** | User Secrets (for all projects) | N/A (dev only) |

---

## Common Tasks

### View Your User Secrets
```powershell
cd LivingMessiah.AppHost
dotnet user-secrets list
```

### Update a Secret
```powershell
cd LivingMessiah.AppHost
dotnet user-secrets set "AzureStorageConnectionString" "new-value"
```

### Remove a Secret
```powershell
cd LivingMessiah.AppHost
dotnet user-secrets remove "AzureStorageConnectionString"
```

### Clear All Secrets
```powershell
cd LivingMessiah.AppHost
dotnet user-secrets clear
```

### Find User Secrets File Location
```powershell
cd LivingMessiah.AppHost
dotnet user-secrets list --verbose
```

---

## Troubleshooting

### ❌ "AzureStorageConnectionString not configured in secrets"
**Cause:** User Secrets not set in AppHost
**Solution:**
```powershell
cd LivingMessiah.AppHost
dotnet user-secrets set "AzureStorageConnectionString" "your-connection-string"
```

### ❌ API returns "Azure Storage configuration is missing"
**Running via Aspire:**
- Check User Secrets in AppHost (see above)

**Running API standalone:**
- Check `Api/local.settings.json` has real values

**Azure Production:**
- Check Azure Portal > Static Web App > Environment variables

### ❌ PWA can't reach API locally
**Check:**
1. Is the API running? (`func start` or via Aspire)
2. Is it on the right port? (7071 for standalone, Aspire assigns dynamically)
3. Check browser console for errors

### ❌ Secrets showing up in Git
**Solution:**
```powershell
# Remove from tracking
git rm --cached Api/local.settings.json

# Verify .gitignore
cat .gitignore | Select-String "local.settings.json"
```

---

## Best Practices

1. ✅ **NEVER** commit real secrets to Git
2. ✅ Use Aspire for local development (simplifies multi-project scenarios)
3. ✅ Keep `local.settings.json` with dummy values in Git (documentation)
4. ✅ Use User Secrets for sensitive data during development
5. ✅ Use Azure Environment Variables for production
6. ✅ Consider Azure Key Vault for enterprise scenarios
7. ✅ Rotate your keys regularly
8. ❌ **NEVER** put secrets in PWA (Blazor WASM) - it runs in the browser!

---

## Next Steps

1. ✅ Set up User Secrets (see "Local Development with Aspire" section)
2. ✅ Run from Aspire AppHost to test locally
3. ✅ Configure Azure Environment Variables when ready to deploy
4. ✅ Push to GitHub to trigger automatic deployment

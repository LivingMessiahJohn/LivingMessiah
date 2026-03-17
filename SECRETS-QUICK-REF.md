# Secret Management Quick Reference

## TL;DR

```powershell
# 1. Set secrets (ONE TIME SETUP)
cd LivingMessiah.AppHost
dotnet user-secrets set "AzureStorageConnectionString" "YOUR_CONNECTION_STRING"
dotnet user-secrets set "BlobContainerName" "YOUR_CONTAINER_NAME"

# 2. Run everything via Aspire
dotnet run

# Done! Secrets automatically flow to API
```

## Where Secrets Live

| What | Where | Safe? |
|------|-------|-------|
| **Development (Aspire)** | User Secrets in AppHost | ✅ Not in Git |
| **Development (Standalone)** | `Api/local.settings.json` | ✅ Gitignored |
| **Production (Azure)** | Environment Variables in Portal | ✅ Azure RBAC |

## Key Commands

```powershell
# Set a secret
dotnet user-secrets set "KEY" "VALUE"

# List all secrets
dotnet user-secrets list

# Remove a secret
dotnet user-secrets remove "KEY"

# Clear all secrets
dotnet user-secrets clear
```

## Azure Portal Setup (Production)

1. Go to **Static Web App** > **Settings** > **Environment variables**
2. Add Application settings:
   - `AzureStorageConnectionString`
   - `BlobContainerName`

## Remember

- ✅ PWA (Blazor WASM) = NO SECRETS (runs in browser)
- ✅ API (Azure Functions) = HAS SECRETS (runs on server)
- ✅ Aspire AppHost User Secrets = Best for local dev
- ❌ NEVER commit `local.settings.json` with real values

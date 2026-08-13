# Secret Management Quick Reference

## TL;DR

```powershell
# 1. Set secrets (ONE TIME SETUP)
cd LivingMessiah.AppHost
dotnet user-secrets set "AzureStorageConnectionString" "YOUR_CONNECTION_STRING"
dotnet user-secrets set "BlobContainerName" "YOUR_CONTAINER_NAME"
dotnet user-secrets set "SpecialEventConnectionString" "YOUR_SPECIAL_EVENT_SQL_CONNECTION_STRING"

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
   - `SpecialEventConnectionString` (Azure SQL connection string for the SpecialEvent database)

## Sukkot daily schedule blob (Admin + Sukkot apps)

Schedule markdown lives in private container `sukkot-content` (see `ScheduleBlob` constants). Only the connection string is secret.

```powershell
# Admin (user-secrets)
cd Admin
dotnet user-secrets set "AzureBlob:ConnectionString" "YOUR_STORAGE_CONNECTION_STRING"

# Sukkot (user-secrets)
cd ../Sukkot
dotnet user-secrets set "AzureBlob:ConnectionString" "YOUR_STORAGE_CONNECTION_STRING"
```

- Config key: `AzureBlob:ConnectionString`
- Not secret: container `sukkot-content`, blob path `sukkot/scheduled-events.md`, metadata key `lastrevised`
- Placeholders may appear in `appsettings*.json`; real values go in user-secrets or Azure app settings

## Remember

- ✅ PWA (Blazor WASM) = NO SECRETS (runs in browser)
- ✅ API (Azure Functions) = HAS SECRETS (runs on server)
- ✅ Admin / Sukkot Blazor Server = connection strings in user-secrets or Azure settings
- ✅ Aspire AppHost User Secrets = Best for local dev (Api storage today)
- ❌ NEVER commit `local.settings.json` with real values

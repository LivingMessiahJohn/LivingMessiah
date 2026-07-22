# Api — Agent Instructions

Azure **Functions** project that performs privileged Azure Blob operations for the PWA (and Aspire-hosted local dev).

## Role

- Server-side blob checks/operations so the WASM client never holds storage credentials
- Consumed by PWA via HTTP; under Aspire, wired from `LivingMessiah.AppHost` with env vars for storage
- See `docs/PWA-and-Api-Relationship.md`, `README.md`, `QUICK-START.md`

## Layout

- Functions: `Functions/`
- Request/response models: `Models/`
- Host config: `host.json`, `Program.cs`
- Local secrets: `local.settings.json` (gitignored) — not user-secrets in this project by default

## Conventions

- Keep the surface small: blob info / existence style operations matching existing functions
- Configuration keys: `AzureStorageConnectionString`, `BlobContainerName` (must stay aligned with AppHost)
- Do not expose raw connection strings to clients or logs
- Prefer updating models + function together when the PWA contract changes; update PWA `BlobApiService` / models in the same change set when possible

## Safety

- Never commit `local.settings.json` with real secrets
- Confirm before changing production function auth, CORS, or storage account wiring

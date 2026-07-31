# Api — Agent Instructions

Azure **Functions** project that performs privileged Azure Blob operations for the PWA (and Aspire-hosted local dev).

## Role

- Server-side privileged operations so the WASM client never holds storage or SQL credentials
- Blob checks for teaching PDFs; `GetSpecialEvents` reads the SpecialEvent database for public display
- Consumed by PWA via HTTP; under Aspire, wired from `LivingMessiah.AppHost` with env vars
- See `docs/PWA-and-Api-Relationship.md`, `README.md`, `QUICK-START.md`

## Layout

- Functions: `Functions/`
- Request/response models: `Models/`
- Host config: `host.json`, `Program.cs`
- Local secrets: `local.settings.json` (gitignored) — not user-secrets in this project by default

## Conventions

- Keep the surface small: HTTP functions matching existing patterns (`GetBlobInfo`, `GetSpecialEvents`)
- Configuration keys (must stay aligned with AppHost / SWA app settings):
  - `AzureStorageConnectionString`, `BlobContainerName`
  - `SpecialEventConnectionString` — SQL connection for SpecialEvent DB (read-only use from API)
- Do not expose raw connection strings to clients or logs
- Prefer updating models + function together when the PWA contract changes; update PWA client services in the same change set when possible

## Safety

- Never commit `local.settings.json` with real secrets
- Confirm before changing production function auth, CORS, or storage account wiring

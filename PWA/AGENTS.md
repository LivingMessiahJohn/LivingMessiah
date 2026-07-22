# PWA — Agent Instructions

Public Blazor **WebAssembly** client (`net10.0`). Runs in the browser; keep secrets and privileged logic out of this project.

## Layout

- Features: `Features/<Name>/` (Home, Feasts, Haggadah, Parasha, Liturgy, etc.)
- Shared layout/nav: `Layout/`, `Components/`
- Shared domain/UI: project reference to `RCL` — prefer RCL over local duplicates
- Static assets: `wwwroot/`

## API & blobs

- Blob existence / storage checks go through the **Api** project, not Azure Storage from the client
- Wiring: `Program.cs` resolves `services:api:https:0` / `http:0` under Aspire, falls back to `http://localhost:7071` in standalone dev, and base address in production
- Client service pattern: `AddBlobApiService` and related types under feature `Data/` (e.g. Home Shabbat teaching)
- Background: `docs/PWA-and-Api-Relationship.md`, `docs/ShabbatCard-and-BlobApiService.md`

## Conventions

- Register new services in `Program.cs` the same way existing `Add*` extensions are registered
- Prefer co-located feature folders; keep page-level components under the feature that owns the route
- Do not add Auth0 / server-only packages or connection strings here
- Logging uses Serilog browser console; keep log noise reasonable

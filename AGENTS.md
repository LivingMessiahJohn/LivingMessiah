# LivingMessiah — Agent Instructions

.NET / Blazor rewrite of LivingMessiahBlazor. Multi-project solution with Aspire orchestration. Prefer matching existing patterns over inventing new ones.

## Solution layout

| Project | Role |
|---------|------|
| `PWA/` | Public Blazor WebAssembly app |
| `Admin/` | Admin Blazor Server app (Auth0, Sukkot admin, KeyDates, etc.) |
| `Sukkot/` | Public/member Sukkot Blazor Server app (registration, Stripe) |
| `RCL/` | Shared Razor Class Library (enums, calendar, components, blob abstractions) |
| `Api/` | Azure Functions API for blob operations (used by PWA) |
| `Database/` | SDK-style SQL projects (`Microsoft.Build.Sql`), one folder per Azure DB (`SpecialEvent/`, `SukkotRegistration/`) |
| `LivingMessiah.AppHost/` | .NET Aspire host — preferred local run entry |
| `LivingMessiah.ServiceDefaults/` | Shared Aspire service defaults (health, OpenTelemetry, service discovery) |
| `ShabbatPdf/` | Parses Living Messiah Shabbat service agenda PDFs |

Feature UI and feature-local code live under each app’s `Features/` folder (e.g. `PWA/Features/Home/`, `Admin/Features/Sukkot/`). Shared domain types and reusable UI belong in `RCL/`.

## Build & run

- Solution: `LivingMessiah.sln`
- Prefer Aspire for full stack: `cd LivingMessiah.AppHost` then `dotnet run` (starts Api, PWA, Admin, Sukkot)
- Single project: `dotnet build <Project>/<Project>.csproj` or run from that folder
- Most apps target `net10.0`; check the project’s `.csproj` if a build fails
- Do not commit `bin/`, `obj/`, publish output, or secret-bearing local config

## Secrets

- Never commit connection strings, Auth0 secrets, Stripe keys, or Azure keys
- Dev secrets via Aspire: user-secrets on AppHost (see `SECRETS-QUICK-REF.md`, `SECRETS-MANAGEMENT.md`)
- Standalone API: `Api/local.settings.json` (gitignored)
- If a change needs a new secret, document the **key name** only; do not invent or paste real values

## Coding conventions

- Match neighboring files: namespaces, folder depth, DI registration style
- New feature work: put it under the correct app’s `Features/<FeatureName>/` with co-located `.razor`, `.cs`, and `Data/` as existing features do
- Prefer `RCL` for shared enums/helpers over duplicating across PWA / Admin / Sukkot
- Blazor interactive modes and auth differ by host (WASM vs Server) — do not copy auth/DI patterns between apps without checking that app’s `Program.cs`
- Use existing components (`RCL/Components/`, layout pieces) before adding new ones
- Common packages in this solution: Ardalis.SmartEnum, Dapper, Serilog, Blazored.* where already used
- C#: follow the style of the file you are editing (indentation, naming, nullable)

## Architecture notes

- PWA talks to `Api` for blob checks; see `docs/PWA-and-Api-Relationship.md`
- Shabbat / teaching blob flow: `docs/ShabbatCard-and-BlobApiService.md`
- Admin and Sukkot use Auth0 + cookie auth; policies live under each app’s `Security/`
- Data access often uses repository-style services and `ServiceCollectionExtensions` under feature `Data/` folders
- Aspire wiring (projects + env for storage): `LivingMessiah.AppHost/Program.cs`

## Git & safety

- **Ship process** (issues → PR → deploy → human gates): [`docs/SHIP-WORKFLOW.md`](docs/SHIP-WORKFLOW.md)
- Do not force-push `main` or rewrite published history without an explicit ask
- Do not change production config or Azure deploy settings unless requested
- Prefer small, focused changes; avoid drive-by refactors
- Confirm before: `git push`, opening/closing PRs, deleting branches, or changing secrets
- Prefer issue-linked PRs (`Fixes #N`); human merges and smokes preview/prod per ship workflow

## When unsure

- Read an existing similar feature end-to-end before scaffolding something new
- Prefer extending `RCL` over copying enums/components into an app
- Ask before large cross-project moves or auth/security changes
- Deeper `AGENTS.md` files under app folders refine these rules for that project

When writing Markdown lists, **always** use standard list markers:
- Use `- ` (hyphen + space) for unordered lists
- Never use the Unicode bullet `•`

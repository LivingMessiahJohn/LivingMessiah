# Admin — Agent Instructions

Blazor **Server** admin app (`net10.0`) with interactive server components, Auth0, and SQL-backed features.

## Layout

- Features: `Features/<Name>/` (Sukkot, KeyDates, SpecialEvents, WeeklyDownloads, Database, Profile, etc.)
- Auth / policies: `Security/` (`ServiceCollectionExtensions`, roles, policies)
- Data base helpers: `Data/` (`BaseRepositoryAsync`, etc.)
- Shared UI/types: `RCL` project reference
- Aspire defaults: `LivingMessiah.ServiceDefaults`

## Feature pattern

Match existing Sukkot / KeyDates style when adding or extending admin features:

- UI: `.razor` pages/components under the feature folder
- Queries/repos: `Data/` with repository + `ServiceCollectionExtensions`
- Register the feature in `Program.cs` via `builder.Services.Add…()`
- Validation often uses FluentValidation / Blazored patterns already present
- Settings: options types under feature `Settings/` bound with `Configure<T>`

## Auth & safety

- Auth0 + cookie authentication; do not weaken policies or skip authorization without an explicit ask
- Roles/policies: `Security/Enums`, `Security/Policies`
- Never log or commit secrets; use configuration / user-secrets / environment as elsewhere in the solution
- SQL access via Dapper and existing repository patterns — avoid ad-hoc connection handling

## Conventions

- Prefer extending an existing feature’s `Data` layer over a new global data stack
- Co-locate VM / DTO / validator next to the form that uses them
- Health checks live under `HealthChecks/` when relevant

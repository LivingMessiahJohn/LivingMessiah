# RCL — Agent Instructions

Shared **Razor Class Library** (`net10.0`). Consumed by PWA, Admin, and Sukkot. Keep this package host-agnostic.

## What belongs here

- Reusable components: `Components/`
- Shared features: `Features/` (Calendar, Parasha, Feasts, FeastDayPlanner, Sukkot enums/helpers, Storage abstractions)
- Cross-app constants/helpers: `Constants/`, `Helpers/`, `Enums/`

## What does not belong here

- App-specific `Program.cs` DI for a single host (register in the app; expose extension methods only when multiple apps need the same registration)
- Auth0 / cookie / policy setup (lives in each server app’s `Security/`)
- PWA-only or Admin-only pages and routes
- Secrets or environment-specific connection strings

## Conventions

- Prefer SmartEnum / existing enum patterns already used in Calendar and Parasha
- Namespaces follow folder structure (e.g. `RCL.Features.Calendar.Enums`)
- Components should stay presentational or lightly logic-bound; heavy data access that needs SQL stays in the host app unless already centralized here (e.g. blob abstractions under `Features/Storage/`)
- Breaking changes here ripple to all apps — prefer additive APIs; check callers in PWA/Admin/Sukkot when renaming or removing public types

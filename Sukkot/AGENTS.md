# Sukkot — Agent Instructions

Blazor **Server** app for Sukkot registration and related flows (`net10.0`). Includes Auth0, Stripe checkout/webhooks, and feature-local data access.

## Layout

- Features: `Features/` (registration, donations, lifecycle authority, etc.)
- Minimal APIs / Stripe: `Endpoints/` (checkout session, webhook)
- Auth / policies: `Security/`
- Data helpers: `Data/`, feature `Data/` folders
- Shared enums/helpers for Sukkot dates/fees: prefer `RCL/Features/Sukkot/` when shared; app-specific UI stays here
- Aspire defaults: `LivingMessiah.ServiceDefaults`

## Patterns

- Register services and endpoints in `Program.cs` consistently with existing `Add*` / endpoint mapping
- Stripe and donation constants: `Enums/DonationConstants`, `Endpoints/Constants` — extend carefully; money and webhooks are high-risk
- Forms often use VM + validator + co-located components under the feature
- OpenTelemetry / Application Insights wiring may be present — do not remove diagnostics without reason

## Auth, payments & safety

- Auth0 + cookie auth; keep authorization on protected pages and endpoints
- Webhooks and Stripe secrets come from configuration/environment — never hardcode keys
- Confirm before changing checkout, webhook signature validation, or donation fee logic
- Prefer small, testable changes around payment paths

## Relation to Admin

- **Admin** manages registrations, notes, reports, dashboard
- **This app** is the participant-facing Sukkot experience
- Shared domain (dates, fees, steps) should trend toward `RCL`; do not fork the same enum in both apps without need

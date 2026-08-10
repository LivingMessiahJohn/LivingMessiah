# Sukkot Stripe Endpoints & Admin Health Check

How the **Sukkot** app creates Stripe Checkout sessions, receives webhooks, and persists payment data into **SukkotRegistration**. Also covers the **Admin** health check that probes the production webhook URL.

## Overview

```text
Participant (Blazor UI)
    │  POST form (registrationId, feeEnumValue, email)
    ▼
Sukkot POST /api/stripe/create-session     ← CheckoutSession
    │  1. dbo.stpStripeMerge  → dbo.Stripe (audit / in-flight)
    │  2. Stripe SessionService.CreateAsync
    ▼
Stripe Checkout (hosted)
    │  card payment
    ▼
Stripe → POST /webhook/stripesukkotdonation  ← Webhook
    │  signature verify + checkout.session.completed
    │  dbo.stpDonationInsert → dbo.Donation
    │  Registration.StatusId → Complete; delete dbo.Stripe row
    ▼
SukkotRegistration database

Admin GET /health/sukkot/stripe
    │  StripeWebhookHealthCheck
    ▼
HTTP POST https://livingmessiah.com/webhook/stripesukkotdonation
```

| Piece | Project | Role |
|-------|---------|------|
| Checkout session Minimal API | `Sukkot` | Starts payment; redirects to Stripe |
| Webhook Minimal API | `Sukkot` | Stripe → DB donation insert |
| Stripe endpoint data layer | `Sukkot/Endpoints/Data` | `stpStripeMerge`, `stpDonationInsert` |
| Registration / fee UI | `Sukkot/Features` | Form that posts into create-session |
| Webhook reachability check | `Admin/HealthChecks/Sukkot` | Probes production webhook URL |

Database objects live under `Database/SukkotRegistration/dbo/` (tables, sprocs, views).

---

## Configuration & secrets

### Sukkot app

| Config key | Constant / type | Purpose |
|------------|-----------------|---------|
| `Stripe:ApiKey` | `Sukkot.Endpoints.Constants.StripeConstants.ApiKey` | Server-side Stripe secret; set on `StripeConfiguration.ApiKey` in `Program.cs` |
| `Stripe:WebhookSecret` | `StripeConstants.WebhookSecret` | Signing secret for `EventUtility.ConstructEvent` |
| `EndpointsSetting:Domain` | `Sukkot.Settings.EndpointsSetting` | Public origin for success/cancel URLs (e.g. `https://localhost:7201`) |

Routes (not secrets):

| Constant | Value | File |
|----------|-------|------|
| `DonationConstants.BaseSessionUrl` | `/api/stripe/create-session` | `Sukkot/Enums/DonationConstants.cs` |
| `DonationConstants.WebHookUrl` | `/webhook/stripesukkotdonation` | same |

Placeholder values appear in `Sukkot/appsettings.json` (`sk_test_your_stripe_api_key`, `whsec_...`). Real keys come from user-secrets / Azure app settings — never commit live secrets. See `SECRETS-QUICK-REF.md` / `SECRETS-MANAGEMENT.md`.

**Startup guard:** `StartupHelper.EnsureStripeSecretsConfigured` runs before the app hosts. If `Stripe:ApiKey` or `Stripe:WebhookSecret` is missing or still a known placeholder, startup **fails fast** with a clear `Log.Fatal` / `InvalidOperationException` (secret values are never logged). Set real values locally before `dotnet run` / Aspire, or the process will exit during bootstrap.

### Admin health check

| Config / constant | Value | Notes |
|-------------------|-------|-------|
| `Stripe` section → `Admin.HealthChecks.Sukkot.Settings.Stripe` | `ApiKey`, `WebhookSecret` | Bound in Admin `Program.cs`; **currently unused** by the health check implementation |
| `StripeConstants.WebhookUrl` | `https://livingmessiah.com/webhook/stripesukkotdonation` | Hard-coded production probe target |
| `StripeConstants.HealthCheckUrl` | `/health/sukkot/stripe` | Mapped health endpoint on Admin |
| `StripeConstants.HealthCheckName` | `Is Stripe Webhook Enabled` | Name registered with `AddHealthChecks` |

---

## Code map (Sukkot)

```text
Sukkot/
  Enums/DonationConstants.cs          # Route path constants
  Features/Constants/FormFields.cs    # Form / metadata field names
  Features/Steps/PaymentStep/
    StripeCard.razor                  # HTML form → create-session
  Endpoints/
    CheckoutSession.cs                # MapPost create-session
    Webhook.cs                        # MapPost webhook
    StripeSettings.cs                 # Options type (ApiKey, WebhookSecret)
    Constants/StripeConstants.cs      # Config key names + event type
    Data/
      DonationRecord.cs               # Insert DTO
      Repository.cs                   # IRepository: StripeMerge, DonationInsert
      ServiceCollectionExtensions.cs  # AddEndpointsData()
  Program.cs                          # DI, StripeConfiguration, Map* endpoints
```

Registration wiring in `Sukkot/Program.cs`:

1. `builder.Services.AddEndpointsData()` — registers `Sukkot.Endpoints.Data.IRepository`
2. `Configure<StripeSettings>` + `StripeConfiguration.ApiKey = configuration[StripeConstants.ApiKey]`
3. `CheckoutSession.CheckoutSessionConfig(app, BaseSessionUrl, endpointsSetting.Domain)`
4. `Webhook.WebhookConfig(app, WebHookUrl)`

> **Note:** Feature registration data uses a *different* `Sukkot.Features.Data.IRepository`. Endpoint handlers inject `Sukkot.Endpoints.Data.IRepository` only.

---

## End-to-end payment flow

### 1. UI posts the checkout form

`StripeCard.razor` (payment step) posts to `DonationConstants.BaseSessionUrl` with antiforgery and three hidden fields:

| Form field (`FormFields`) | Meaning |
|---------------------------|---------|
| `registrationId` | Existing `dbo.Registration.Id` |
| `feeEnumValue` | `RegistrationFee` SmartEnum value (`1` Single, `2` Family) |
| `email` | Customer email (also used as Stripe customer email) |

Fee amounts come from `RCL.Features.Sukkot.Enums.RegistrationFee` (`Fee` in dollars, `Pennies` for Stripe `UnitAmount`).

### 2. Create session — `POST /api/stripe/create-session`

**Handler:** `Sukkot.Endpoints.CheckoutSession.CheckoutSessionConfig`

1. **Validate form** — registration id (int), fee enum (`RegistrationFee.TryFromValue`), email format.
2. **`StripeMerge`** — `dbo.stpStripeMerge` upserts `dbo.Stripe` by email (in-flight payment marker). Failures are logged; the handler still continues to create the Stripe session.
3. **Create Stripe Checkout Session** via `SessionService.CreateAsync`:
   - Mode `payment`, card only
   - Line item: “Registration Donation”, amount = `registrationFee.Pennies` (USD)
   - `CustomerEmail` = form email
   - Metadata: `registrationId`, `email`
   - Success → `{Domain}/PaymentConfirm`
   - Cancel → `{Domain}/PaymentCanceled`
4. **Redirect** browser to `session.Url` (Stripe hosted Checkout).

### 3. Customer pays on Stripe

Stripe owns card entry and 3DS. On success, Stripe emits `checkout.session.completed` to the configured webhook endpoint.

### 4. Webhook — `POST /webhook/stripesukkotdonation`

**Handler:** `Sukkot.Endpoints.Webhook.WebhookConfig`

1. Read raw body; construct event with **`Stripe-Signature`** header and `Stripe:WebhookSecret` (`throwOnApiVersionMismatch: false`).
2. Handle only `checkout.session.completed` (`StripeConstants.EventType`).
3. Cast `stripeEvent.Data.Object` to Checkout `Session`.
4. **Validate amount** — `AmountTotal / 100` must equal `RegistrationFee.Single.Fee` or `RegistrationFee.Family.Fee`.
5. **Validate registration id** — parse `session.Metadata["registrationId"]` (must be &gt; 0).
6. **`DonationInsert`** — app-level guard then `dbo.stpDonationInsert`.

Insert payload (`DonationRecord`):

| Field | Source |
|-------|--------|
| `RegistrationId` | Session metadata |
| `Amount` | Validated dollars from `AmountTotal` |
| `Notes` | `"Stripe Checkout Session Completed"` |
| `Email` | `session.CustomerEmail` |
| `ReferenceId` | `session.Id` (Stripe Checkout Session id) |
| `CreatedBy` | `"Endpoint: checkout.session.completed"` |
| `CreateDate` | `DateTime.UtcNow` |

On signature or processing failure → `400 Bad Request` (Stripe may retry). On success → `200 OK`.

---

## Database persistence

Schema project: `Database/SukkotRegistration/`.

### Tables

**`dbo.Stripe`** — short-lived in-flight record keyed uniquely by `Email`.

| Column | Role |
|--------|------|
| `Email` | Unique index; merge key |
| `RegistrationId` | Linked registration |
| `ModificationCount` / `LastModifiedDate` | How many times checkout was re-attempted |

**`dbo.Donation`** — permanent payment row(s) for a registration.

| Column | Role |
|--------|------|
| `RegistrationId` | FK → `dbo.Registration` |
| `Detail` | Per-registration sequence (1, 2, …); unique with `RegistrationId` |
| `Amount` | Money |
| `ReferenceId` | Stripe session id (or manual reference) |
| `Email`, `Notes`, `CreatedBy`, `CreateDate` | Audit / display |

### Stored procedures

#### `dbo.stpStripeMerge` (`@Email`, `@RegistrationId`, `@NewId OUT`)

- `MERGE` into `dbo.Stripe` on `Email`
- **Matched:** update `RegistrationId`, bump `ModificationCount`, set `LastModifiedDate` (UTC); `@NewId = 0`
- **Not matched:** insert new row; `@NewId` = new identity

Called from `Endpoints.Data.Repository.StripeMerge` during create-session.

#### `dbo.stpDonationInsert` (donation fields + `@NewId OUT`)

Business rules inside the sproc:

1. **Insert** into `dbo.Donation` with next `Detail` for that registration.
2. **No-partial-payments rule:** set `dbo.Registration.StatusId` to complete (`Constants.StatusCompleteId`).
3. **Cleanup:** `DELETE dbo.Stripe WHERE Email = @Email`.

Called from `Repository.DonationInsert` after an app-level check:

```text
SELECT COUNT(Id) FROM dbo.Donation WHERE RegistrationId = @RegistrationId
```

If a row already exists, the repository **does not** call the sproc and returns an error string (`Donation already exists for registrationId …`). That is an application guard in addition to the unique index on `(RegistrationId, Detail)`.

On FK / insert failure, `NewId` may be null; the sproc logs via `dbo.stpLogError` / `ErrorLog`.

### Related read paths

- Registration print / detail UI reads donations via feature queries (e.g. `DonationQuery` from `dbo.Donation`).
- Admin reports use donation views (`vwDonationDetail`, `vwDonationReport`, …).
- `dbo.vwStripe` joins in-flight Stripe rows to registration names for ops.

---

## Admin.HealthChecks.Sukkot

Namespace root: `Admin.HealthChecks.Sukkot`.

```text
Admin/HealthChecks/Sukkot/
  StripeWebhookHealthCheck.cs     # IHealthCheck implementation
  Settings/Stripe.cs              # Options (ApiKey, WebhookSecret) — unused by check today
  Endpoints/Constants/
    StripeConstants.cs            # WebhookUrl, HealthCheckUrl, HealthCheckName, config key names
```

### What it does

`StripeWebhookHealthCheck.CheckHealthAsync`:

1. Builds `HttpClient` from `IHttpClientFactory`.
2. `POST` body `{}` as `application/json` to the **hard-coded** production webhook URL (`https://livingmessiah.com/webhook/stripesukkotdonation`).
3. Does **not** send a valid `Stripe-Signature` header.
4. Interprets **any success status code** as Healthy; non-success or exception as Unhealthy.

### Registration (`Admin/Program.cs`)

```csharp
builder.Services.Configure<HealthChecksSukkot.Settings.Stripe>(
    builder.Configuration.GetSection(nameof(HealthChecksSukkot.Settings.Stripe)));
builder.Services.AddHealthChecks()
    .AddCheck<HealthChecksSukkot.StripeWebhookHealthCheck>(
        HealthChecksSukkotEndPoint.HealthCheckName);

// later
app.MapHealthChecks(HealthChecksSukkot.Endpoints.Constants.StripeConstants.HealthCheckUrl);
// → /health/sukkot/stripe
```

Aspire / default health endpoints (`/health`, `/alive` via ServiceDefaults) are separate from this named check.

### Interpreting results

Because the probe posts an unsigned empty JSON body:

- The Sukkot webhook will typically fail signature validation and return **400**.
- The health check treats non-success as **Unhealthy**.
- A **Healthy** result means the URL accepted the POST with a success status (unusual for the real webhook without a valid Stripe signature).
- Network failures / DNS / TLS errors also yield **Unhealthy**.

So this check is closer to a **reachability / deployment smoke** of the public webhook path than a proof that Stripe signing secrets and donation inserts are correct. For full confidence, use Stripe CLI / Dashboard webhook delivery logs and a real or test `checkout.session.completed` event.

`Settings.Stripe` is bound but not read by `StripeWebhookHealthCheck` (code comments note possible removal).

---

## Local development tips

1. **Domain:** set `EndpointsSetting:Domain` to the Sukkot HTTPS origin so success/cancel redirects work.
2. **API key:** test mode `sk_test_…` via secrets.
3. **Webhooks:** use [Stripe CLI](https://stripe.com/docs/stripe-cli) to forward events:
   ```bash
   stripe listen --forward-to https://localhost:<port>/webhook/stripesukkotdonation
   ```
   Put the CLI `whsec_…` into `Stripe:WebhookSecret`.
4. **Do not** enable `LogSecret` in `Webhook.cs` in production (debug helper; currently commented at call site).
5. **Fees:** webhook rejects amounts that are not exactly Single or Family fees from `RegistrationFee`.

---

## Failure modes (quick reference)

| Stage | Failure | Behavior |
|-------|---------|----------|
| Create-session validation | Bad form fields | `400` with message |
| `stpStripeMerge` | DB error | Logged; checkout still attempted |
| Stripe session create | API / network | Logged; `400` |
| Webhook signature | Bad secret / body | `StripeException` → `400` |
| Amount / registration metadata | Invalid | `400`; no insert |
| Donation already exists | App guard | `400` with error string |
| `stpDonationInsert` FK / error | `NewId` null | Error string; check `dbo.ErrorLog` |

---

## Related issues & code

- Issue **#212** — this documentation
- Issue **#210** — form action must match mapped create-session route (`DonationConstants.BaseSessionUrl`)
- SQL project: `Database/SukkotRegistration/`
- Shared fees: `RCL/Features/Sukkot/Enums/RegistrationFee.cs`
- Ship process: `docs/SHIP-WORKFLOW.md`

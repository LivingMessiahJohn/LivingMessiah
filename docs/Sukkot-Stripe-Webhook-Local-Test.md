# Sukkot Stripe webhook — local test runbook

How to prove end-to-end that a test card payment inserts `dbo.Donation` on a **local** Sukkot app.

Architecture and production URLs: [`Sukkot-Stripe-Endpoints.md`](Sukkot-Stripe-Endpoints.md).

---

## Why this is multi-step

| Piece | Role |
|-------|------|
| **Sukkot app** | Creates Checkout sessions; receives webhook POSTs |
| **Stripe hosted Checkout** | Takes the test card; does **not** call `localhost` by itself |
| **Stripe CLI** (`stripe listen`) | Forwards Stripe events to `https://localhost:7201/...` |
| **`whsec_…` from the CLI** | Signing secret for **this** listen session only — must match Sukkot’s `Stripe:WebhookSecret` |

Stripe’s servers never reach your machine. Without `stripe listen`, you get `dbo.Stripe` (create-session) but **no** `dbo.Donation` (webhook).

**Secrets are not shared with Azure:**

| Secret | Where | Used for |
|--------|--------|----------|
| CLI `whsec_…` | Local user-secrets only | Verifying events forwarded by `stripe listen` |
| Dashboard endpoint `whsec_…` | Azure `Stripe__WebhookSecret` | Verifying events POSTed to `sukkot.livingmessiah.com` |
| `sk_test_…` | Local secrets / Azure | Creating Checkout sessions (API key) |

Do **not** put the local CLI `whsec_` into Azure, and do not expect the Azure secret to work with `stripe listen`.

---

## Prerequisites

- [Stripe CLI](https://stripe.com/docs/stripe-cli) installed
- Stripe **test mode** account access
- Sukkot runs on HTTPS **https://localhost:7201** (see `Sukkot/Properties/launchSettings.json`)
- Local secrets have a valid test API key, e.g. `Stripe:ApiKey` = `sk_test_…` (Checkout uses this; the CLI uses its own login)
- Seq (or console logs) available if you want log verification

---

## Runbook (successful path)

Keep **two terminals** open: one for Stripe CLI, one for Aspire/Sukkot (or use the IDE for the app).

### 1. Log in the Stripe CLI

```powershell
stripe login
```

- CLI prints a **pairing code** and a URL.
- Open the link in a browser, confirm the code, authorize.
- If you see `api_key_expired` / 401, run `stripe logout` then `stripe login` again.  
  The CLI does **not** read Sukkot’s `secrets.json`; expired CLI auth is separate from app secrets.

### 2. Forward webhooks to local Sukkot

```powershell
stripe listen --forward-to https://localhost:7201/webhook/stripesukkotdonation
```

Leave this running. When ready you should see something like:

```text
Ready! You are using Stripe API Version [...]. Your webhook signing secret is whsec_...
```

### 3. Put that `whsec_` into Sukkot local secrets

Copy the **full** `whsec_…` from the listen output (same terminal session).

**Preferred (CLI):**

```powershell
cd C:\Source\repos\LivingMessiah\Sukkot
dotnet user-secrets set "Stripe:WebhookSecret" "whsec_paste_exactly_from_listen"
```

**Or** edit the user-secrets `secrets.json` for the Sukkot project and set:

```json
"Stripe:WebhookSecret": "whsec_..."
```

If you stop and restart `stripe listen`, you may get a **new** `whsec_`. Update secrets again and restart the app.

### 4. Reset DB rows for the email under test

In **SukkotRegistration**, for the test participant email (example):

```sql
-- Inspect first
SELECT * FROM dbo.Stripe WHERE Email = N'you@example.com';
SELECT d.*
FROM dbo.Donation d
INNER JOIN dbo.Registration r ON r.Id = d.RegistrationId
WHERE r.EMail = N'you@example.com';  -- column name as in your schema

-- Then delete (order: donations that block re-test; Stripe is in-flight audit)
-- Adjust joins/filters to your email / RegistrationId
DELETE d
FROM dbo.Donation d
INNER JOIN dbo.Registration r ON r.Id = d.RegistrationId
WHERE r.EMail = N'you@example.com';

DELETE FROM dbo.Stripe WHERE Email = N'you@example.com';
```

Also ensure the registration is still in a state that can open payment (if StatusId is already Complete from a prior test, set it back as needed for your scenario).

Why: the app refuses a second donation for the same registration (`Donation already exists…`), and leftover `dbo.Stripe` rows confuse “in-flight vs paid” checks.

### 5. Launch the app and sign in

1. Start **Aspire** (or run Sukkot alone) so Sukkot is on **https://localhost:7201**.
2. **Restart Sukkot after any `WebhookSecret` change** (secrets load at startup).
3. Log in to the Sukkot app (Auth0) as the test user.
4. Reach the payment step for that registration.

### 6. Pay with Stripe test card

1. Click the Stripe / pay button (posts to `/api/stripe/create-session`).
2. On Stripe Checkout, use a [test card](https://docs.stripe.com/testing#cards), e.g.:
   - Number: `4242 4242 4242 4242`
   - Any future expiry, any CVC, any postal code
3. Complete payment. Browser should return to **PaymentConfirm** / registration complete UI.

### 7. Verification checklist

All of these should pass:

| # | Check | Expected |
|---|--------|----------|
| 1 | **UI** | Registration **Complete** (paid / finished step) |
| 2 | **`stripe listen` terminal** | `checkout.session.completed` with **`[200]`** on `POST …/webhook/stripesukkotdonation` |
| 3 | **Database** | Row in **`dbo.Donation`** for that registration; **no** row left in **`dbo.Stripe`** for that email (sproc deletes Stripe after successful insert) |
| 4 | **Seq** (or app logs) | Events for webhook handling / **`DonationInsert`** / “Donation inserted” |

Other events (`charge.succeeded`, `payment_intent.succeeded`) may appear; after a correct secret they should also return **200** (handler ignores non-`checkout.session.completed`). The donation write is only on **`checkout.session.completed`**.

---

## What “success” looks like in the CLI

```text
--> checkout.session.completed [evt_...]
<-- [200] POST https://localhost:7201/webhook/stripesukkotdonation [evt_...]
```

If you see **`[400]`** on every event (including `charge.succeeded`):

1. `Stripe:WebhookSecret` does not match this listen session’s `whsec_`.
2. Re-copy `whsec_`, set user-secrets, **restart Sukkot**, pay again.

---

## Common failures

| Symptom | Likely cause |
|---------|----------------|
| `dbo.Stripe` yes, `dbo.Donation` no; no webhook lines in Seq | `stripe listen` not running, or wrong port/path |
| CLI: `Authorization failed` / `api_key_expired` | CLI login expired → `stripe logout` then `stripe login` (not necessarily app secrets) |
| CLI: all events `[400]` | Wrong/stale `whsec_` in secrets, or app not restarted |
| Seq: signature / Stripe error | Same as above |
| Seq: `Donation already exists` | Prior donation for that `RegistrationId` — delete test donation rows |
| Seq: `RegistrationId NOT FOUND` | Session metadata missing; check create-session path |
| Seq: `amount is invalid` | Amount not exactly Single/Family fee from `RegistrationFee` |
| UI complete but DB incomplete | Rare race; re-check webhook 200 and Seq for insert errors |

---

## Optional commands

Trigger a generic event (proves endpoint + secret only; may not insert a real registration donation):

```powershell
stripe trigger checkout.session.completed
```

Prefer a real Checkout payment for full metadata (`registrationId`, fee amount).

Update CLI if prompted:

```text
A newer version of the Stripe CLI is available...
```

---

## Production reminder (not this runbook)

- Webhook URL: `https://sukkot.livingmessiah.com/webhook/stripesukkotdonation` (Sukkot host, **not** livingmessiah.com PWA).
- Azure `Stripe__WebhookSecret` = Dashboard signing secret for that endpoint.
- Local CLI `whsec_` stays on the machine only.

---

## Related

- Issue **#224** — webhook not writing donations
- [`Sukkot-Stripe-Endpoints.md`](Sukkot-Stripe-Endpoints.md) — full create-session / webhook / DB map
- `Sukkot/Endpoints/Webhook.cs` — handler
- `DonationConstants.WebHookUrl` → `/webhook/stripesukkotdonation`

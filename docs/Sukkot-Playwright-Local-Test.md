# Sukkot Playwright — local test runbook

How to run the `Sukkot.Playwright` tests against a local Sukkot app.

Playwright is **not** a service you start by itself. You start **Sukkot**, then `dotnet test` launches Playwright (Microsoft Edge) for each run.

Project: [`Sukkot.Playwright/`](../Sukkot.Playwright/). Tests do **not** log in to Auth0 and do **not** start the app.

---

## Prerequisites

- Repo root: `C:\Source\repos\LivingMessiah`
- .NET SDK that can build `net10.0`
- **Microsoft Edge** installed (this machine has no Chrome; tests use Edge via `channel: msedge`)
- Sukkot can run with the `http` launch profile on **http://localhost:5000**

---

## Runbook (two terminals)

Keep **two terminals** open from the repo root. Leave Sukkot running while the tests execute.

### 1. Start Sukkot

```powershell
cd C:\Source\repos\LivingMessiah
dotnet run --project Sukkot/Sukkot.csproj --launch-profile http
```

Wait until it is listening. In a browser, http://localhost:5000 should show the Sukkot landing page.

Leave this terminal running.

### 2. Run the Playwright tests

In a **second** terminal:

```powershell
cd C:\Source\repos\LivingMessiah
dotnet test Sukkot.Playwright/Sukkot.Playwright.csproj
```

That command:

1. Builds `Sukkot.Playwright`
2. Pings http://localhost:5000 (fails fast if Sukkot is down)
3. Starts Playwright and opens **headless Edge**
4. Runs the smoke tests against `/` and `/Steps`

A passing run looks like:

```text
Passed!  - Failed:     0, Passed:     7, Skipped:     0, Total:     7
```

### 3. Watch the browser (optional)

Same as step 2, but show Edge while tests click through the page:

```powershell
dotnet test Sukkot.Playwright/Sukkot.Playwright.csproj -- Playwright.LaunchOptions.Headless=false
```

---

## What the tests cover

| Test | What it checks |
|------|----------------|
| `HasTitleAndHeading` | `/` title and H1 **Sukkot** |
| `ShowsBannerDatesAndFees` | 2026 banner, Sep 25–Oct 3, $100 / $50 |
| `ShowsDocumentPacketLinks` | Waiver, House Rules, PocketMod PDF URLs |
| `ExpandsRegistrationWalkThrough` | Walk-through accordion opens |
| `AdvancesScheduleToNextDay` | Schedule **Next day** goes Day 0 → Day 1 |
| `DesktopNavShowsAboutAndSteps` | About, Steps, Login in the desktop nav |
| `ShowsNotAuthorizedWhenLoggedOut` | `/Steps` shows **Not Authorized** |

---

## Other ways to run

**One test:**

```powershell
dotnet test Sukkot.Playwright/Sukkot.Playwright.csproj --filter "HasTitleAndHeading"
```

**Visual Studio:** open `LivingMessiah.sln`, start Sukkot (`http` profile), then Test Explorer → `Sukkot.Playwright`.

**Different base URL** (if Sukkot is not on port 5000):

```powershell
$env:SUKKOT_BASE_URL = "http://localhost:5000"
dotnet test Sukkot.Playwright/Sukkot.Playwright.csproj
```

---

## Troubleshooting

| Symptom | What to do |
|---------|------------|
| `Sukkot is not running at http://localhost:5000` | Start step 1 and wait until the site loads in a browser, then re-run tests |
| Browser launch fails / Chrome not found | Tests are set to Edge in `Sukkot.Playwright/playwright.runsettings` (`Channel=msedge`). Do not use the Chrome channel on this PC |
| `AdvancesScheduleToNextDay` fails | Landing-page schedule comes from blob markdown. Confirm `/` shows **Day 0** and a **Next day** button |
| Tests pass headless but fail headed | Wait for Sukkot to finish starting; first load after `dotnet run` can be slow |
| Port is already in use | Another Sukkot instance is running. Use that one, or stop it and start step 1 again |

Settings live in [`Sukkot.Playwright/playwright.runsettings`](../Sukkot.Playwright/playwright.runsettings): Edge, headless, 15s expect timeout.

Signed-in Admin checks use a different Edge profile and are not these tests: [Admin-Playwright-Login.md](Admin-Playwright-Login.md).

---

## Optional: Playwright’s Chromium instead of Edge

Only needed on a machine without Edge (for example CI). This PC should keep Edge.

```powershell
dotnet build Sukkot.Playwright/Sukkot.Playwright.csproj
pwsh Sukkot.Playwright/bin/Debug/net10.0/playwright.ps1 install chromium
```

Then remove `<Channel>msedge</Channel>` from `playwright.runsettings`.

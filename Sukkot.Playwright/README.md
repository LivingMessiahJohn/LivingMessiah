# Sukkot.Playwright

Playwright (NUnit) smoke tests for the public Sukkot Blazor Server app.

**How to start Sukkot and run these tests:** [`docs/Sukkot-Playwright-Local-Test.md`](../docs/Sukkot-Playwright-Local-Test.md)

Short version (two terminals, repo root):

```powershell
dotnet run --project Sukkot/Sukkot.csproj --launch-profile http
```

```powershell
dotnet test Sukkot.Playwright/Sukkot.Playwright.csproj
```

Watch the browser:

```powershell
dotnet test Sukkot.Playwright/Sukkot.Playwright.csproj -- Playwright.LaunchOptions.Headless=false
```

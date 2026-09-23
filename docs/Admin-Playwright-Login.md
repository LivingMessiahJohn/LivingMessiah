# Admin Playwright login — local runbook

How to sign in to local Admin so an agent can open a protected page in the Playwright Edge window.

This is not the `Sukkot.Playwright` test project. Those tests stay logged out. See [Sukkot-Playwright-Local-Test.md](Sukkot-Playwright-Local-Test.md).

The agent drives Microsoft Edge through the Playwright profile at `C:\Users\JohnM\.grok\playwright-profile`. That profile is separate from the daily Edge window. A login in daily Edge does not show up for the agent.

Do not put the Auth0 password in chat, in this doc, or in the repo. Type it only in the Auth0 page inside the Playwright window.

---

## Pick one Admin address

Use the address that matches how Admin was started. The Auth0 cookie is stored for that exact origin. A login on one address is not sent to the other.

| How Admin is started | Open this | Auth0 callback |
| --- | --- | --- |
| Aspire, startup project `LivingMessiah.AppHost` | https://localhost:7191 | `https://localhost:7191/callback` (already allowed) |
| HTTP-only launch profile | http://localhost:5026 | `http://localhost:5026/callback` (add on Auth0.com if it is missing) |

Aspire uses Admin's **https** launch profile. That profile listens on both `https://localhost:7191` and `http://localhost:5026`. Admin calls `UseHttpsRedirection()`, so a request to port 5026 is sent to `https://localhost:7191`.

The HTTP-only command listens only on port 5026:

```powershell
cd C:\Source\repos\LivingMessiah
dotnet run --project Admin/Admin.csproj --launch-profile http
```

Do not run Aspire and that HTTP-only command at the same time. Both want port 5026.

Leave the terminal that started Admin running for the whole check.

---

## Auth0 allow list (HTTP origin only)

`https://localhost:7191/callback` is already allowed. No Auth0 change is required for an Aspire login.

`http://localhost:5026/callback` is not in appsettings or user secrets. Admin builds the callback from the address in the browser. Auth0 checks it against the application settings on auth0.com.

To allow the HTTP-only origin, open the regular web application whose client id is Admin's `auth0:ClientId` (not the Management API machine-to-machine app) and add:

- Allowed Callback URLs: `http://localhost:5026/callback`
- Allowed Logout URLs: `http://localhost:5026`

Leave the existing `https://localhost:7191` entries in place. Save, then try http://localhost:5026 again.

`appsettings.Development.json` and `secrets.json` only identify which Auth0 application Admin calls (`auth0:Domain`, `auth0:ClientId`, `auth0:ClientSecret`). They do not hold the callback allow list.

---

## Runbook

1. Start Admin on one address from the table above. Wait until that address loads.
2. Ask the agent to open that same address in the Playwright window.
3. In the window the agent opened, click **Login** and finish Auth0. The window title can say **Personal** even though it is the Playwright profile.
4. Wait until the address is back on that same origin and **Log out** is visible.
5. Leave that window open. Tell the agent which page to check, for example `/SukkotHome/Dashboard`.

The agent uses that same window. A signed-in dashboard shows the account email in the header, the page content, and no **Not Authorized** alert.

---

## Why the window has to stay open

Admin's Auth0 cookie lasts for the browser session. Closing the Playwright window deletes it. The next open of the same profile returns to **Not Authorized, Login Required**, and the login has to be done again in the new window.

Opening Edge yourself with `--user-data-dir=C:\Users\JohnM\.grok\playwright-profile` locks that profile. The agent cannot attach until that window is closed, and closing it drops the Admin session. Let the agent open the window, then sign in there.

The Edge page **You're almost set up** is the new profile syncing a Microsoft account. It is not Admin and it is not the Auth0 login.

---

## Troubleshooting

| Symptom | What to do |
| --- | --- |
| `https://localhost:7191` says localhost refused to connect | Admin was started with the HTTP-only profile, which listens only on port 5026. Open http://localhost:5026. To use 7191, stop that `dotnet run` (Ctrl+C) and start Aspire. |
| `http://localhost:5026` says localhost refused to connect | Nothing is listening on 5026. Start the HTTP-only command, or use https://localhost:7191 while Aspire is running. |
| The address changes from 5026 to 7191 | Aspire is running and HTTPS redirection is doing that on purpose. Sign in at https://localhost:7191. |
| Auth0: **Callback URL mismatch** | Add `http://localhost:5026/callback` on auth0.com, as above. Do not change appsettings or secrets for this error. |
| **Browser is already in use** for `playwright-profile` | An Edge window still has that profile open. Close the window titled with **localhost** (it may say **Personal**). Daily Edge can stay open. Then ask the agent to open Admin again and sign in in that new window. |
| Login succeeded, then the agent sees **Login** again | The Playwright window was closed, so the session cookie is gone. Ask the agent to open the URL and sign in again in that window. Leave it open. |
| Signed in in the normal browser, agent still sees **Login** | The agent only reads the Playwright profile. Sign in in the window the agent opened. |

# Admin: Auth0 Users page (read-only)

Admin route **`/Users`** (nav: **Auth0 Users**) lists Auth0 tenant users in a QuickGrid, including assigned RBAC roles.

## Access

- ASP.NET policy: `Admin` (`RoleEnum.Admin.Name`) — users with the Auth0 `admin` role claim
- Nav visibility: `Nav.Users.RequiredRoles` = Admin only
- Role assignment and other mutations stay in the **Auth0 Dashboard** (this page is read-only)

## Data source

- Auth0 **Management API** via HttpClient + M2M client credentials (token cached in-process)
- Scopes required on the M2M app (Auth0 Management API → Machine to Machine):
  - `read:users` (user list)
  - **and** `read:roles` (per-user roles via `/users/{id}/roles`)  
    — Auth0 also accepts `read:role_members` as an alternative to the pair above
- Credentials: `Auth0M2M:Domain`, `Auth0M2M:ClientId`, `Auth0M2M:ClientSecret`  
  (see `SECRETS-QUICK-REF.md` — separate from login `auth0:*` settings)
- After changing M2M scopes, **restart Admin** so the cached access token is refreshed
- Note: Auth0 returns a JSON **array** unless `include_totals=true` (object with `users`). The page requests totals and also accepts either shape.
- If role scopes are missing, the grid still loads users; the Roles column stays empty and a warning is logged

## How roles are joined

User list payloads do not include RBAC roles. The feature:

1. Lists the **20** most recently logged-in users (`sort=last_login:-1`)
2. For each of those users, lists assigned roles via `/users/{id}/roles`
3. Joins role names in memory for the Roles column

## Limits

- The grid shows at most **20** users, ordered by Auth0 `last_login` descending
- Users who have never logged in sort after those with a `last_login` value

## Related

- Adding app roles / claims: [`Admin-Adding-A-Role.md`](Admin-Adding-A-Role.md)
- Issue: [#237](https://github.com/LivingMessiahJohn/LivingMessiah/issues/237)

# Admin: adding a new role

How to add a role-gated feature in the **Admin** app, using the **WeeklyDownload** work ([#208](https://github.com/LivingMessiahJohn/LivingMessiah/pull/209)) as the template.

Roles come from **Auth0** as claim values (e.g. `weeklydownload`). The app maps those claims into:

1. **ASP.NET authorization policies** — gate whole pages with `<AuthorizeView Policy=...>`
2. **Bitwise flags** — show/hide nav items via `Nav.RequiredRoles`

Both must stay in sync. Missing either step usually means “page works but no menu link” or “menu shows but page says Not Authorized.”

---

## Checklist (copy for each new role)

Use a short **PascalCase** name (e.g. `WeeklyDownload`) and a lowercase **claim string** with no spaces (e.g. `weeklydownload`). Keep them consistent everywhere.

| # | Where | What to do |
|---|--------|------------|
| 1 | Auth0 (outside the repo) | Create the role / assign users; claim value must match `Role.Claim` |
| 2 | `Admin/Security/Enums/Role.cs` | Bitwise id, public instance, private `*SE` class with `Claim` |
| 3 | `Admin/Security/Enums/RoleGroup.cs` | Policy name constant (usually same string as claim) |
| 4 | `Admin/Security/ServiceCollectionExtensions.cs` | Dedicated policy + include in `EmailVerifiedWithAtLeastOneRole` |
| 5 | `Admin/Enums/Nav.cs` | `RequiredRoles` on the nav item (role \| Admin) |
| 6 | (automatic) | `NavRoleAuthorization` maps claims via `Role.List` — no Home tree edit for a new role |
| 7 | Feature page(s) | `<AuthorizeView Policy=@RoleGroup.YourRole>` |
| 8 | Verify | User with only the new claim; Admin; user with neither |

Optional / only if needed:

| When | Where |
|------|--------|
| Multi-role page access | OR extra roles in both the **policy** and `Nav.RequiredRoles` (see Sukkot + SukkotHost) |

---

## Step-by-step (WeeklyDownload pattern)

### 1. Auth0 (ops, not code)

1. Create a role whose **name/value** matches the claim string you will use in code (example: `weeklydownload`).
2. Assign the role to the users who should get access (not everyone needs `admin`).
3. Confirm the Admin Auth0 app still requests roles in the token (`Scope` includes `roles` in `AddAuth0Authentication`).
4. After login, open **Admin → Profile** (or whatever shows claims) and confirm the role claim appears.

If Auth0 and code disagree on the claim string, every policy check fails.

### 2. Define the role — `Admin/Security/Enums/Role.cs`

Bitwise flags must be **powers of two** so nav can OR them: `1, 2, 4, 8, 16, 32, 64, ...`.

Add all of the following:

```csharp
// RoleFlag enum (if still maintained) — next free power of two
WeeklyDownload = 32

// BitwiseId nested class
internal const int WeeklyDownload = 32;

// Public SmartEnum instance
public static readonly Role WeeklyDownload = new WeeklyDownloadSE();

// Private sealed class
private sealed class WeeklyDownloadSE : Role
{
    public WeeklyDownloadSE() : base($"{nameof(BitwiseId.WeeklyDownload)}", BitwiseId.WeeklyDownload) { }
    public override string Claim => "weeklydownload"; // must match Auth0
}
```

**Rules:**

- `BitwiseId` value must match the flag value.
- `Claim` is the string Auth0 puts on the user; use it in policies and nav mapping.
- Prefer consistent casing for the C# name (`WeeklyDownload`). The claim string is usually lowercase (`weeklydownload`).

### 3. Policy name — `Admin/Security/Enums/RoleGroup.cs`

```csharp
public const string WeeklyDownload = "weeklydownload";
```

This string is the **policy name** used by `AddPolicy` and by `<AuthorizeView Policy=...>`. It does not have to equal the claim, but matching them reduces mistakes.

### 4. Register policies — `Admin/Security/ServiceCollectionExtensions.cs`

**A. Feature policy** (typical: this role **or** Admin):

```csharp
.AddPolicy(RoleGroup.WeeklyDownload, policy =>
    policy.RequireAssertion(context =>
        context.User.IsInRole(RoleEnum.WeeklyDownload.Claim) ||
        context.User.IsInRole(RoleEnum.Admin.Claim)))
```

**B. “Has at least one role” policy** — add the new claim to `RoleGroup.EmailVerifiedWithAtLeastOneRole` so users who only have this role can still use the authenticated home area:

```csharp
// inside the RequireAssertion for EmailVerifiedWithAtLeastOneRole:
context.User.IsInRole(RoleEnum.WeeklyDownload.Claim) ||
// ... existing roles ...
```

**Multi-role example (Sukkot):** a policy may accept more than one non-admin role:

```csharp
// RoleGroup.Sukkot allows sukkot OR sukkothost OR admin
```

Mirror any multi-role rule in `Nav.RequiredRoles` (step 5).

### 5. Nav visibility — `Admin/Enums/Nav.cs`

On the nav SmartEnum entry for the feature:

```csharp
public override int RequiredRoles => RoleEnum.WeeklyDownload.Value | RoleEnum.Admin.Value;
```

- `0` = visible without a special role (public-within-app items).
- Use `|` for “any of these roles.”
- Keep the same set of roles as the page policy when possible.

### 6. Home tree filtering — automatic via `NavRoleAuthorization`

`Admin/Features/Home/NavTree.razor` filters nodes with `Admin/Security/NavRoleAuthorization`:

- Builds a user bitmask from role claims by matching each claim to `Role.List` / `Role.Claim`
- Keeps items where `HasRequiredRole(RequiredRoles, userRoles)` is true (`RequiredRoles == 0` or bitwise overlap)
- Hides empty folders after filtering

Adding a role to `Role.cs` (step 2) and setting `Nav.RequiredRoles` (step 5) is enough — **do not** add a hardcoded claim→bitmask branch in the tree. If the home link is missing, check that Auth0’s claim string equals `Role.Claim` exactly and that `RequiredRoles` ORs the new flag.

### 7. Gate the page — feature `Index.razor` (etc.)

```razor
<AuthorizeView Policy=@RoleGroup.WeeklyDownload>
  <Authorized>
    @* feature UI *@
  </Authorized>
  <NotAuthorized>
    <div class="alert alert-warning" role="alert">
      <small>Not Authorized; Role Group Policy: @RoleGroup.WeeklyDownload</small>
    </div>
    <LoginRedirectCard Nav="Nav.WeeklyDownload" ReturnUrl=@Nav.WeeklyDownload.Index />
  </NotAuthorized>
</AuthorizeView>
```

Use the same `RoleGroup.*` constant as in `AddPolicy`. Point `LoginRedirectCard` / return URLs at the correct `Nav` entry.

### 8. Verify

| User | Expected |
|------|----------|
| Auth0 role = new claim only | Sees nav item; page Authorized |
| Auth0 role = `admin` only | Same (Admin is included in policy and nav) |
| Logged in, no relevant role | No nav item (or hidden); direct URL → Not Authorized |
| Logged out | Login / redirect behavior as today |

Also re-check **Profile claims** so the claim string matches `Role.Claim` exactly (case-sensitive string compare in `NavRoleAuthorization` and `IsInRole`).

---

## File map (Admin only)

```text
Auth0 (dashboard)
    claim string  ─────────────────────────────┐
                                               ▼
Admin/Security/Enums/Role.cs          Claim + bitwise value
Admin/Security/Enums/RoleGroup.cs     policy name constant
Admin/Security/ServiceCollectionExtensions.cs
        ├─ EmailVerifiedWithAtLeastOneRole  (include new claim)
        └─ RoleGroup.YourRole               (feature policy)

Admin/Enums/Nav.cs                    RequiredRoles bitmask
Admin/Security/NavRoleAuthorization.cs  claim → bitmask via Role.List
Admin/Features/Home/NavTree.razor     filters tree with NavRoleAuthorization
Admin/Features/<Feature>/Index.razor  AuthorizeView Policy=...
```

---

## What is *not* required for a simple new role

- Changing `Program.cs` if policies already register via `AddAuthorizationPolicies`
- Database changes (roles are Auth0 + app policy, not SQL)
- PWA or Sukkot app role enums (this guide is **Admin**-only unless those apps get their own roles)
- A new branch of `RoleFlag` usage if that enum is unused — still keep `BitwiseId` correct for `Nav.RequiredRoles`

---

## Common mistakes

| Symptom | Likely miss |
|---------|-------------|
| Always Not Authorized | Auth0 claim ≠ `Role.Claim`, or policy not registered |
| Page OK by URL, no home link | Auth0 claim ≠ `Role.Claim`, or forgot `Nav.RequiredRoles` |
| Home shows link, page Not Authorized | Policy uses different claim/policy name than nav |
| User with only this role bounced from home | Not added to `EmailVerifiedWithAtLeastOneRole` |
| Wrong users can open the page | Policy too broad (e.g. reused KeyDates) or Admin-only intended but role ORed in |
| Bitmask never matches | Used a non-power-of-two id, or forgot `\|` Admin in nav |

---

## Reference: WeeklyDownload touch list (#208)

Concrete files updated when introducing `weeklydownload`:

1. `Admin/Security/Enums/Role.cs` — flag `32`, instance, `Claim => "weeklydownload"`
2. `Admin/Security/Enums/RoleGroup.cs` — `WeeklyDownload = "weeklydownload"`
3. `Admin/Security/ServiceCollectionExtensions.cs` — policy + at-least-one-role
4. `Admin/Enums/Nav.cs` — `RequiredRoles` for Weekly Download nav
5. `Admin/Features/WeeklyDownloads/Index.razor` — `AuthorizeView` policy (was KeyDates)

Home tree visibility uses `NavRoleAuthorization` + `Role.List` (no per-role edit in `NavTree`).

Unrelated polish in the same PR (logout icon) is **not** part of the role recipe.

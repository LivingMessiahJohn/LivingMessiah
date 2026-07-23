# Ship workflow (Issues → PR → Deploy)

How work gets from idea to production for LivingMessiah. Prefer this over ad-hoc notes folders or IDE-only merge rituals. Agents should follow this when shipping code.

## Pipeline

```text
Issue → Branch → Implement → Verify → PR → Preview/CI → Smoke → Merge → Deploy → Prod smoke → Cleanup
```

| Step | Who | Notes |
|------|-----|--------|
| Issue | Human | Intent + acceptance criteria (even a short issue) |
| Branch | Agent or human | From up-to-date `main` |
| Implement | Agent (scoped) | Match `AGENTS.md`; no drive-by refactors |
| Verify | Both | Build, `/review` or `/check-work`, manual UI smoke |
| Open PR / push | Human approves | Agent drafts body with `Fixes #N` |
| Preview / CI | GitHub Actions | See [Deploy map](#deploy-map) |
| Staging / preview smoke | **Human** | Click critical paths |
| Merge | **Human** | Prefer GitHub UI after green checks |
| Production smoke | **Human** | Confirm live app |
| Close issue / delete branch | Auto + human | `Fixes #N` on merge; delete remote branch |

## Branch naming

```text
john-<issue#>-short-slug
```

Example: `john-161-pwa-installed-error`

## Issue (required)

Minimum body:

```markdown
## Problem
## Desired outcome
## Acceptance criteria
- [ ] ...
## Out of scope
## Risk (auth, payments, deploy, data)
## How I'll verify
```

Paste the issue number or URL into the agent prompt when starting work.

## PR body (replaces external commit notes)

```markdown
## Summary
What changed and why.

## Linked issue
Fixes #NNN

## Test plan
- [ ] Local / Aspire
- [ ] Preview or staging (if applicable)
- [ ] Prod smoke after merge

## Risk
Auth, Stripe, deploy, data, PWA offline, etc.
```

- Link the issue with `Fixes #NNN` so merge auto-closes it.
- Prefer one issue ↔ one PR. Rare multi-issue PRs may list multiple `Fixes #` lines.

## Human-in-the-loop (do not automate away)

| Gate | Human must |
|------|------------|
| Scope | Accept issue criteria |
| Secrets | Never invent or commit secrets; key **names** only |
| Push / open or close PR/issue | Explicit approval (unless already granted for the session) |
| Preview / staging smoke | Exercise critical paths |
| Merge to `main` | Human decision |
| Production smoke | Confirm live |
| Product / content ambiguity | Decide “done enough” |

Agents must **not** merge production-bound PRs unless the human explicitly asks.

## Deploy map

Workflows live under `.github/workflows/`.

| App | Workflow | Triggers today | Staging / preview |
|-----|----------|----------------|-------------------|
| **PWA** (+ Api for SWA) | `deploy-pwa.yml` | Push to `main`; PR open/sync/reopen/close targeting `main` | Azure Static Web Apps **PR preview** environments |
| **Admin** | `deploy-admin.yml` | Push to `main` with `Admin/**` changes; `workflow_dispatch` | No separate staging yet — merge deploys production App Service |
| **Sukkot** | `deploy-sukkot.yml` | Push to `main` with `Sukkot/**` changes; `workflow_dispatch` | Same as Admin |

### PWA / SWA environment limit

SWA allows a small number of concurrent environments (often **3**). Prefer few open PWA PRs. Closing or merging a PR should run the workflow’s close job and free a preview slot. Do not rely on manual Azure portal cleanup as the primary process.

### After merge

1. Watch Actions for the affected app(s).
2. Smoke production.
3. Confirm the issue closed (if `Fixes #` was used).
4. Delete the remote feature branch (enable “delete branch on merge” in GitHub if available).
5. Locally: `git checkout main`, `git pull`, delete the local feature branch.

Do **not** “merge the feature branch into main again” in the IDE after GitHub already merged the PR — pull `main` instead.

## Grok / agent habits

| Habit | Guidance |
|-------|----------|
| Start session | From solution root; put the **issue** in the first prompt |
| Implement | Stay in the right app `Features/` and/or `RCL/` |
| Before push | `/review` (local or branch) and a build |
| After PR open | Optional `/pr-babysit` for CI failures; human still merges |
| Secrets / Azure | See `SECRETS-QUICK-REF.md`; confirm before changing deploy YAML or production config |

## Testing (pragmatic)

There is little formal test CI yet. Prefer:

1. **New pure logic** (helpers, mappers, SmartEnum helpers in `RCL`) — unit tests with the PR that introduces it.
2. **Production bugs** — a regression test that would have caught them.
3. **Content / layout pages** — manual smoke, not a unit-test tax.
4. **Auth / Stripe / money paths** — careful manual checklist; add automation later for thin critical slices.

When a test project and CI job exist, PR should run **build + tests** before deploy.

## Git safety (summary)

- No force-push to `main`; no history rewrite without an explicit ask.
- Confirm before: `git push`, opening/closing PRs or issues, deleting branches, changing secrets or deploy settings.
- Small, focused changes; match existing patterns.

## Related docs

- Root and app `AGENTS.md` — coding and architecture rules
- `docs/PWA-and-Api-Relationship.md` — PWA ↔ Api blob checks
- `docs/ShabbatCard-and-BlobApiService.md` — teaching / blob flow
- `SECRETS-QUICK-REF.md` / `SECRETS-MANAGEMENT.md` — secrets (key names only in git)

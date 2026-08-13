# Phase 0 decisions — Sukkot schedule → blob (#215)

Decided 2026-08-11. Updated: **LastRevised via blob metadata** (not YAML front matter).

## Locked decisions

| Decision | Choice |
|----------|--------|
| Container strategy | **Option A — dedicated container** |
| Container name | `sukkot-content` (constant: `ScheduleBlob.ContainerName`) |
| Public access on container | **Private** (no anonymous public read) |
| Blob path | `sukkot/scheduled-events.md` (`ScheduleBlob.BlobName`) |
| Read model | **Private container + server-side SDK** |
| Write model | Admin only, same SDK, overwrite single blob |
| Content-Type | `text/markdown; charset=utf-8` |
| `LastRevised` | **Blob metadata** key `lastrevised` (`ScheduleBlob.LastRevisedMetadataKey`); fallback = blob `LastModified` |

### Why private + SDK (not public URL)

- Sukkot and Admin are **Blazor Server** apps; they already hold connection strings server-side (same pattern as WeeklyDownloads / SpecialEvents).
- Read and write use one code path (`IAzureBlobService`), not a public `HttpClient` GET.
- Landing-page HTML still renders the schedule to visitors; they never need direct blob access.

### Config / constants

| Item | Value |
|------|--------|
| Secret | `AzureBlob:ConnectionString` only |
| Container / blob path | Code constants on `ScheduleBlob` (not secrets) |
| Metadata key | `lastrevised` — ISO-8601 datetime string on save |

## Baseline export

| Field | Value |
|-------|--------|
| Source | Local SQL `JohnsDellDT\SQLEXPRESS` / `SukkotRegistration` |
| SQL `LastRevised` | `2026-08-05T19:15:00` (set as blob metadata on first upload) |
| Repo baseline file | [`docs/sukkot/scheduled-events.md`](scheduled-events.md) — **markdown body only** |

**Prod note:** If Azure Free DB differs, re-export body and set metadata before/at first upload.

## Human ops (Azure)

1. Create private container **`sukkot-content`**.
2. Upload `docs/sukkot/scheduled-events.md` → blob `sukkot/scheduled-events.md`.
3. Set metadata **`lastrevised`** = `2026-08-05T19:15:00` (or use Admin Save once Phase 4 exists).

## Phase 1–2 (platform + read path)

- `DownloadTextAsync` → `BlobTextContent(Text, LastRevised)` from metadata / LastModified
- `UploadStreamAsync` optional `metadata` dictionary
- `IScheduleQueryWriter` contract (Admin save later)
- `ScheduleBlobQueryLoader` + `AddSukkotScheduleFromBlob()` in RCL
- Admin / Sukkot: `AddSukkotDailyScheduleData()` → blob loader (SQL schedule repos removed)

Phase 3: seed blob (body only; LastModified OK).  
Phase 4: Admin `/SukkotSchedule` edit + save via `IScheduleQueryWriter`.  
Phase 6: no C# references to `ScheduledEventsMarkdown`; seed/copy scripts retired.  
Phase 7: table removed from SQL project; drop on DBs via `docs/sql/sukkot-registration/03-Drop-ScheduledEventsMarkdown.sql`.

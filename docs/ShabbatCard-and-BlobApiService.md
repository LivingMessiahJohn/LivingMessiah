# ShabbatCard, children, and BlobApiService

Documentation of how `Triennial` is defined and populated for the home-page Shabbat card, how it is passed to child components, how those children use it, and how `BlobApiService.GetParasha()` relates to `Triennial`.

**Primary paths**

| Item | Path |
|------|------|
| Parent | `PWA/Features/Home/Shabbat/ShabbatCard.razor` |
| Host page | `PWA/Features/Home/Index.razor` (`<ShabbatCard />`) |
| `Triennial` type | `RCL/Features/Parasha/Enums/Triennial.cs` |
| Population helper | `RCL/Features/Parasha/Helpers.GetCurrentReading()` |
| Blob service | `PWA/Features/Home/Shabbat/Teaching/Data/BlobApiService.cs` |

---

## Product rules (current week vs archive)

| Concern | Rule |
|---------|------|
| **Current week buttons** (`ButtonWrapper`) | Each PdfType button is shown **only** when that blob exists on Azure. Null reading, offline, API errors, or missing blob → **render nothing** for that button. |
| **PDF Archive** (`Grid`) | **Always available**, including when parent `Triennial` is null. |
| **Older archive rows** | Always listed with static blob URLs. **No** existence checks for historical weeks. |
| **Current week on the list** | Included **only if either** Teaching Only **or** Complete Service exists on Azure. That is why `Triennial` is passed into `Grid` — to identify the current week and check **only that week** (two `GetParasha` calls in parallel), not the whole archive. |

---

## 1. What is `Triennial`?

`Triennial` is an Ardalis `SmartEnum` (`SmartEnum<Enums.Triennial>`) that models each weekly Torah reading in the congregation’s triennial cycle.

- **Defined in:** `RCL/Features/Parasha/Enums/Triennial.cs`
- **Shape (high level):** each entry has an id/value, Torah (and related) verse ranges, and computed properties such as:
  - `Date` / `DateOnly` — calendar date for that reading (derived from a seed date + week offset)
  - `BCV` — book + chapter/verse display string
  - `TorahAbrv` — abbreviated Torah reference
  - PDF-related helpers live on `Helpers.GetPdfFile(Triennial, PdfType)`, not on the enum itself

There are many static instances (`Gen_01`, `Gen_02`, … through Deuteronomy). This document does **not** detail the full enum catalog or the internals of `GetCurrentReading()` beyond how the card uses it.

---

## 2. How `ShabbatCard` defines and populates `Triennial`

```csharp
// ShabbatCard.razor @code
Triennial? Triennial;

protected override void OnInitialized()
{
    Triennial = RCL.Features.Parasha.Helpers.GetCurrentReading();
}
```

- Field type: `Triennial?` (nullable).
- Population: single call to `Helpers.GetCurrentReading()` during `OnInitialized`.
- That helper returns the `Triennial` whose `Date` matches the next Shabbat (Arizona timezone), or **`null`** if none matches.
- Init is synchronous (date lookup only). Blob checks stay async inside `ButtonWrapper`.

`ShabbatCard` itself does **not** call `BlobApiService`. It only resolves `Triennial` and passes it down.

### Component tree (relevant portion)

```
ShabbatCard
├── ParashaHeader          Triennial="@Triennial"
├── ButtonWrapper          PdfType=CompleteService, Triennial="@Triennial"  → show only if blob Exists
├── ButtonWrapper          PdfType=TeachingOnly,    Triennial="@Triennial"  → show only if blob Exists
├── MarksWebsite           (no Triennial)
└── Grid                   Triennial="@Triennial"   → always builds archive of older PDFs
```

Also rendered on the card but **not** related to `Triennial`: liturgy link, worship time text.

---

## 3. Children that receive `Triennial`

| # | Component | File | Receives `Triennial`? |
|---|-----------|------|----------------------|
| 1 | `ParashaHeader` | `Teaching/ParashaHeader.razor` | Yes |
| 2 | `ButtonWrapper` (×2) | `Teaching/ButtonWrapper.razor` | Yes |
| 3 | `Grid` | `Teaching/Archives/Grid.razor` | Yes (older cutoff + current-week existence gate) |

`MarksWebsite` is a sibling under the same row but does **not** take `Triennial`.

---

## 4. Per-component: `Triennial` and `GetParasha`

### 4.1 `ParashaHeader`

**Parameter**

```csharp
[Parameter, EditorRequired] public ParashaEnums.Triennial? Triennial { get; set; }
```

**How `Triennial` is used**

| State | Behavior |
|-------|----------|
| `null` | Header uses danger-subtle styling; `Parasha` text = `"NOT FOUND! "` |
| non-null | Primary header styling; `Parasha` = `"{Date:yyyy MMMM dd} \| {BCV}"` |

Logic runs in `OnParametersSet()` from the parameter only (no re-fetch of current reading).

**`BlobApiService.GetParasha()`**

- **Not used.** Display-only component. Header still shows even when PDF buttons are hidden.

---

### 4.2 `ButtonWrapper` (used twice: Complete Service + Teaching Only)

**Parameters**

```csharp
[Parameter, EditorRequired] public ParashaEnums.PdfType PdfType { get; set; }
[Parameter, EditorRequired] public ParashaEnums.Triennial? Triennial { get; set; }
```

**How `Triennial` is used**

| State | Behavior |
|-------|----------|
| `null` | **Renders nothing.** Does not call the blob API. |
| non-null + loading | Brief disabled spinner button (“Checking PDF availability”). |
| non-null + offline / error / `Exists == false` | **Renders nothing.** |
| non-null + `Dto.Exists == true` | Renders `<PdfDownloadButton Dto="@Dto" />`. |

**`BlobApiService.GetParasha()`**

- **Used** only when `Triennial` is non-null and the browser appears online:

```csharp
Dto = await BlobApiService.GetParasha(Triennial, PdfType);
```

- Passes the parent’s `Triennial` and this instance’s `PdfType`.
- Only proceeds to UI when `Dto.Exists` is true (Azure confirmed the blob).

**Child note:** `PdfDownloadButton` does not take `Triennial`; it only uses `BlobDTO` and itself only renders when `Exists` is true.

---

### 4.3 `Grid` (PDF Archive)

**Parameter**

```csharp
[Parameter, EditorRequired] public ParashaEnums.Triennial? Triennial { get; set; }
```

**Why pass `Triennial` (and alternatives)**

| Approach | Pros | Cons |
|----------|------|------|
| **Pass `Triennial` into `Grid` (chosen)** | Grid owns list rules; parent stays thin; only current week needs blob checks | Overlaps HTTP with `ButtonWrapper` (same week checked again per PdfType) |
| Parent checks existence once, passes `bool includeCurrent` + optional DTOs | Single pair of blob calls shared by buttons + grid | Parent grows orchestration; more parameters |
| Cascading/shared cache on `IBlobApiService` | Dedupes identical blob-name lookups | Requires service-level cache/invalidation |

Passing `Triennial` is enough for the product rule: “older rows always; current row only if either PDF exists.” A later optimization is to share the two existence results between `ButtonWrapper` and `Grid` so Azure is hit at most twice per load for the current week.

**How `Triennial` is used**

| State | Archive rows |
|-------|----------------|
| `null` | All readings in `Triennial.List` (static URLs). No blob API. |
| non-null | All **older** rows (`DateOnly < Triennial.DateOnly`), static URLs. **Plus** current week at the top **only if** `GetParasha` says TeachingOnly **or** CompleteService `Exists`. |

```csharp
var olderOrAll = BuildArchiveRows(olderThanExclusive: Triennial?.DateOnly);

if (Triennial is not null && await CurrentWeekHasAnyPdfAsync(Triennial))
    allItems = olderOrAll.Prepend(ToProjection(Triennial)).AsQueryable();
else
    allItems = olderOrAll.AsQueryable();
```

`CurrentWeekHasAnyPdfAsync` runs both PdfTypes with `Task.WhenAll` — **only for the current `Triennial`**.

**`BlobApiService.GetParasha()`**

- **Used only for the current week** (0 or 2 calls).
- Historical rows: static `{Blob.BaseUrl}{Helpers.GetPdfFile(...)}` with **no** existence check.

**Related children of `Grid` (no `Triennial` parameter)**

- `ShowHideGridToggle` — UI toggle only  
- `ArchiveGridAnchor` — renders `href` / text / title from `Projection`  
- `Projection` — DTO for grid rows  

---

### 4.4 `MarksWebsite` (sibling; no `Triennial`)

Does not receive `Triennial` and does not call `BlobApiService`. Shows a placeholder “Under Construction” modal.

---

## 5. `BlobApiService.GetParasha()`

**Location:** `PWA/Features/Home/Shabbat/Teaching/Data/BlobApiService.cs`  
**Registration:** `AddBlobApiService` in `Teaching/Data/ServiceCollectionExtensions.cs` (typed `HttpClient` with app base address).  
**Sole production caller from this feature:** `ButtonWrapper`.

### Signature

```csharp
Task<BlobDTO> GetParasha(Triennial? triennial, PdfType pdfType, CancellationToken ct = default);
```

### How it uses `Triennial`

Flow:

1. Build a default `BlobDTO` (`Exists: false`, empty Url/Parasha, given `PdfType`).
2. Call private `GetCurrentParasha(triennial, pdfType)`:
   - **`resolved = triennial ?? Helpers.GetCurrentReading()`**  
     If the caller passes `null`, the service **re-fetches** the current reading (fallback).
   - If `resolved` is still `null`: log warning, return empty DTO with `ExceptionOccurred: false` and empty blob name (early exit; **no HTTP**).
   - If non-null:
     - `blobName = Helpers.GetPdfFile(resolved, pdfType)`
     - DTO `Parasha` display string = `"{resolved.Date:yyyy MMMM dd} | {resolved.BCV}"`
3. If no exception flag from step 2, POST `BlobInfoRequest(blobName)` to `AzureFunctionAPI.HttpClientUri` (`/api/blob-info`).
4. Map HTTP / JSON result to `Exists` and `Url` (or `ExceptionOccurred`).

`Triennial` is therefore used to:

- Choose the PDF **blob file name** (`GetPdfFile`)
- Fill the human-readable **Parasha** string on the DTO

It is **not** sent to the Azure Function; only `blobName` is.

### Fallback vs. `ButtonWrapper` null handling

| Layer | When `Triennial` is null |
|-------|-------------------------|
| `ButtonWrapper` | Renders nothing; **never calls** `GetParasha` |
| `BlobApiService.GetCurrentParasha` | Falls back to `GetCurrentReading()` again |

For the **current** Shabbat card path, the service’s `triennial ?? GetCurrentReading()` fallback is **unreachable** from `ButtonWrapper`, because the wrapper never invokes the service when its parameter is null. The fallback only matters if some other caller passes `null` deliberately (none in this feature today).

### Possible unnecessary / redundant code in `GetParasha`

| Observation | Detail |
|-------------|--------|
| Null fallback re-resolution | `triennial ?? GetCurrentReading()` duplicates work already done by `ShabbatCard` and is unused by `ButtonWrapper`’s null path. |
| Nullable parameter | Defensive for a shared API; Shabbat path always passes non-null or does not call. |
| `ExceptionOccurred: false` when no reading | Soft miss (`Exists: false`); `ButtonWrapper` treats that the same as missing blob (hide button). |

---

## 6. Null `Triennial` and missing-blob behavior

### When `GetCurrentReading()` returns null

| Path | Behavior |
|------|----------|
| `ParashaHeader` | “NOT FOUND!” header |
| `ButtonWrapper` ×2 | **Hidden** (no API, no disabled “No Parasha” button) |
| `BlobApiService.GetParasha` | Not called |
| `MarksWebsite` | Unchanged |
| **`Grid` archive** | **Still shown** — full historical list (no current-week blob check) |

### When `Triennial` is non-null but neither / one blob exists

| Path | Behavior |
|------|----------|
| `ParashaHeader` | Still shows current date / BCV |
| Each `ButtonWrapper` | Shown only if **that** PdfType’s blob exists |
| `Grid` current row | Shown if **either** PdfType exists; otherwise older rows only |
| Older archive rows | Always listed (static URLs) |

### What is intentionally **not** short-circuited when parent `Triennial` is null

- **PDF Archive (`Grid`)** — product requirement: always offer older PDFs.
- Liturgy / worship-time blocks.
- `MarksWebsite`.

### Remaining cleanup opportunities

1. **`BlobApiService` null fallback** — still redundant for the Shabbat card call path.
2. Archive **historical** rows use static URLs without existence checks (by design; only the **current** week is checked).
3. **Duplicate HTTP for current week:** `ButtonWrapper` ×2 plus `Grid` ×2 can mean up to four blob-info calls for the same week. A shared result (parent orchestration or service cache) would cut that to two.

### Diagram: data flow when `Triennial` is non-null

```
GetCurrentReading()
        │
        ▼
  ShabbatCard.Triennial
        │
        ├──► ParashaHeader  (Date + BCV display)
        │
        ├──► ButtonWrapper (CompleteService)
        │         └──► GetParasha → if Exists → PdfDownloadButton, else hide
        │
        ├──► ButtonWrapper (TeachingOnly)
        │         └──► GetParasha → if Exists → PdfDownloadButton, else hide
        │
        └──► Grid
                  ├──► older rows (DateOnly < current), static URLs
                  └──► GetParasha ×2 for current only
                        └──► if either Exists → prepend current row
```

### Diagram: data flow when `Triennial` is null

```
GetCurrentReading() → null
        │
        ▼
  ShabbatCard.Triennial = null
        │
        ├──► ParashaHeader → "NOT FOUND!"
        ├──► ButtonWrapper ×2 → hidden (no GetParasha)
        ├──► MarksWebsite (unchanged)
        └──► Grid → full archive list (no current-week blob check)
```

---

## 7. Quick reference

| Component / API | Uses `Triennial` param? | Calls `GetParasha`? |
|-----------------|-------------------------|---------------------|
| `ShabbatCard` | Owns / populates | No |
| `ParashaHeader` | Yes (display / null message) | No |
| `ButtonWrapper` | Yes; hide unless non-null + `Exists` | Yes (only if non-null & online) |
| `PdfDownloadButton` | No (uses `BlobDTO`; requires `Exists`) | No |
| `Grid` | Yes (older cutoff + include current if either PDF exists) | Yes (current week only, both PdfTypes) |
| `MarksWebsite` | N/A | No |
| `BlobApiService.GetParasha` | Uses arg or falls back to `GetCurrentReading()` | (is the implementation) |

---

## 8. Related files (inventory)

```
PWA/Features/Home/Shabbat/
  ShabbatCard.razor
  Teaching/
    ParashaHeader.razor
    ButtonWrapper.razor
    PdfDownloadButton.razor
    MarksWebsite.razor
    ShowHideGridToggle.razor
    Archives/
      Grid.razor
      ArchiveGridAnchor.razor
      Projection.cs
    Data/
      BlobApiService.cs
      BlobDTO.cs
      BlobApiModels.cs
      ServiceCollectionExtensions.cs
    Constants/
      Blob.cs
      AzureFunctionAPI.cs

RCL/Features/Parasha/
  Helpers.cs                    # GetCurrentReading, GetPdfFile, …
  Enums/Triennial.cs
```

# LMM Parse PDF

Parses Living Messiah Shabbat service agenda PDFs: compress the full service PDF, then save a teaching-only PDF.

| | |
|---|---|
| **Staging** | Admin uploads to Azure `shabbat-service-staging` (`YYYY-MM-DD-Citation.pdf`) |
| **Service** | Compressed full agenda in `shabbat-service` (same name) |
| **Teaching** | Teaching-only PDF in `shabbat-service-md` (same name) |
| **Stack** | .NET 8, Core + Console CLI + Azure Functions |

## Status

| Piece | Status |
|-------|--------|
| Models / options | Done |
| PdfPig line extract | Done (`PdfPig` **0.1.15**) |
| Anchors + intro skip | Done |
| Markdown builder | Done |
| CLI local mode | Done |
| **Azure blob I/O** | **Done** (`--blob` extracts teaching PDF) |
| **Teaching PDF slice** | **Done** (local `*-teaching.pdf`; Azure same name in `shabbat-service-md`) |
| **Azure Function Event Grid** | **Done** (`CompressStagingPdf` + `ProcessShabbatPdf`) |
| **Markdown from teaching PDF** | Local CLI `--output` only (not the Azure path) |
| **Shrink oversized service PDF** | **Done** (`CompressStagingPdf`; Ghostscript `/ebook`; target &lt; 65 MB) |

See [docs/design-lmm-parse-pdf.md](docs/design-lmm-parse-pdf.md) for the full design.

## Build & test

```powershell
dotnet build LivingMessiah.sln
dotnet test LivingMessiah.sln
```

## Configure Azure (one-time)

### 1. Create staging and teaching containers

```bash
az storage container create \
  --name shabbat-service-staging \
  --account-name livingmessiahstorage \
  --auth-mode login \
  --public-access off

az storage container create \
  --name shabbat-service-md \
  --account-name livingmessiahstorage \
  --auth-mode login \
  --public-access off
```

`shabbat-service` already exists (public Current Service downloads). Admin Weekly Downloads must upload to **`shabbat-service-staging`** (`AzureBlob:WeeklyDownloadContainer`).

### 2. Store the connection string (do not commit secrets)

```powershell
cd C:\Source\repos\LivingMessiah

dotnet user-secrets set "Blob:ConnectionString" "<your-storage-connection-string>" `
  --project ShabbatPdf\src\Cli
```

Or set environment variable: `Blob__ConnectionString`

`appsettings.json` holds non-secret defaults (container names). Connection string stays empty there on purpose.

## Run the CLI

### Local PDF → local Markdown

```powershell
 dotnet run --project ShabbatPdf\src\Cli -- `
  --input "C:\Users\JohnM\Downloads\2026-08-08-Lev-22-and-23.pdf" `
  --output ".\out\2026-08-08-Lev-22-and-23.md"
```

### Azure compressed agenda → teaching PDF in `shabbat-service-md`

```powershell
dotnet run --project ShabbatPdf\src\Cli -- `
  --blob "2026-08-08-Lev-22-and-23.pdf"
```

Reads `shabbat-service/2026-08-08-Lev-22-and-23.pdf` and writes the teaching-only PDF to `shabbat-service-md/2026-08-08-Lev-22-and-23.pdf` (same name).

### Batch teaching PDFs for all agendas

One-time (or rare) backfill of `*-teaching.pdf` only — **no Markdown**. Uses the same `Blob:ConnectionString` as a single CLI run (user secrets or `Blob__ConnectionString`) to list and process blobs. Skips existing `*-teaching.pdf` inputs and uses `--teaching-only --skip-existing` so you can re-run after failures.

```powershell
# Preview list only
.\scripts\batch-blob-parse.ps1 -WhatIf

# First 5 (smoke)
.\scripts\batch-blob-parse.ps1 -MaxCount 5

# Full container → uploads same-name teaching PDFs to shabbat-service-md only
.\scripts\batch-blob-parse.ps1
```

Single-blob equivalent:

```powershell
dotnet run --project ShabbatPdf\src\Cli -- `
  --blob "2026-08-08-Lev-22-and-23.pdf" --teaching-only
```

Logs go under `out\batch-blob-parse-*.log`. See the script header for more parameters.

Production chain (Azure Functions):

1. Admin uploads the full agenda to **`shabbat-service-staging`**.
2. **`CompressStagingPdf`:** if over 65 MB, Ghostscript `/ebook`; always publish the (possibly compressed) file to **`shabbat-service`** with the **same name**. Staging is not overwritten.
3. **`ProcessShabbatPdf`:** slice teaching pages → **`shabbat-service-md/{same-name}.pdf`**.

Local CLI still writes `*-teaching.pdf` next to the input (so it does not overwrite the agenda file). Azure uses the same name in a different container.

PWA Teaching Only buttons still look for `shabbat-service/*-teaching.pdf` until a follow-up retargets them.

### Flags

| Flag | Meaning |
|------|---------|
| `--input` / `-i` | Local PDF path |
| `--output` / `-o` | Local Markdown path (local mode) |
| `--blob` / `-b` | Compressed agenda name in `shabbat-service` |
| `--dry-run` | Parse only; no write/upload |
| `--skip-existing` | Skip if destination already exists |
| `--ensure-container` | Create `shabbat-service-md` if missing |
| `--allow-nonstandard-name` | Allow non `YYYY-MM-DD-…` names in blob mode |
| `--teaching-only` | Local: write `*-teaching.pdf` only. Blob mode already does this. |
| `--from-teaching` | Local: input is already a teaching PDF; Markdown only (no anchors/slice) |

Exactly one of `--input` or `--blob` is required.

### Exit codes

| Code | Meaning |
|------|---------|
| 0 | Success |
| 1 | Validation / anchors / invalid name |
| 2 | I/O / Azure / missing container |
| 3 | Unexpected |

### Visual Studio

1. Set **ShabbatPdf.Cli** as startup project  
2. Debug args examples:

```text
--blob 2026-07-04-Lev-16.pdf
```

```text
--input "C:\Users\JohnM\Downloads\agenda.pdf" --output "C:\Temp\out.md"
```

3. User Secrets (same as CLI): right-click project → **Manage User Secrets**, or the `dotnet user-secrets` command above.

## Azure Function (optional)

Two isolated-worker functions on Flex. Event Grid is required (classic blob triggers are not supported).

| | Compress | Extract |
|---|---|---|
| Function | `CompressStagingPdf` | `ProcessShabbatPdf` |
| Trigger | Event Grid `BlobCreated` on `shabbat-service-staging` | Event Grid `BlobCreated` on `shabbat-service` |
| Skips | Non-PDF and `*-teaching.pdf` | Non-PDF and `*-teaching.pdf` (legacy names in the service container) |
| Work | If &gt; 65 MB, Ghostscript `/ebook`; always publish to `shabbat-service` (copy if already small) | Anchors + teaching page slice |
| Outputs | Same name in `shabbat-service` | Same name teaching PDF in `shabbat-service-md` |

### PDF size limit (issue #50)

Weekly service decks can be 150–250+ MB (image-heavy). Mobile download needs them **under 65 MB**.

| | |
|---|---|
| **Where** | `CompressStagingPdf` only — not the CLI |
| **Engine** | [Ghostscript](https://www.ghostscript.com/) `pdfwrite` with `-dPDFSETTINGS=/ebook` |
| **License** | Ghostscript is **AGPL v3** (or Artifex commercial). Confirm that is acceptable for your deployment before enabling in production. |
| **Behavior** | Read staging. If blob size ≤ `PdfCompress:MaxBytes` (default 65 MiB), **copy** to `shabbat-service`. If larger: compress then **write to `shabbat-service`**. Staging is never overwritten. That write triggers extract. |
| **Local** | Install Ghostscript so `gswin64c` is on PATH, or set `PdfCompress__GhostscriptPath` |
| **Azure Flex** | Flex is Linux and does not ship Ghostscript. Mount a Linux `gs` binary (Azure Files OS mount is supported on Flex) and set `PdfCompress__GhostscriptPath` to that path. Raise function timeout if needed (250 MB decks can take 1–3 minutes). |

App settings (examples):

| Setting | Default | Meaning |
|---------|---------|---------|
| `PdfCompress__Enabled` | `true` | Master switch |
| `PdfCompress__MaxBytes` | `68157440` (65 MiB) | Skip when already under limit |
| `PdfCompress__GhostscriptPath` | *(auto-detect)* | Full path to `gs` / `gswin64c` |
| `PdfCompress__PdfSettings` | `/ebook` | Use `/screen` for more aggressive shrink |
| `PdfCompress__TimeoutSeconds` | `600` | Ghostscript wall-clock limit |

Smoke-test log lines to look for (Aspire / Application Insights):

```text
Published {Name}: compressed=True copied=False original=261.9 MB final=29.4 MB
OK {Name} teaching=… pages=…
```

### Local settings

```powershell
copy src\Functions\local.settings.json.example `
     src\Functions\local.settings.json
# Edit local.settings.json: set Blob and Blob__ConnectionString to your storage connection string
```

`local.settings.json` is gitignored. See `local.settings.json.example`.

### Run locally (needs Azure Functions Core Tools + Azurite or a real storage connection)

```powershell
cd src\Functions
func start
```

Or set the Functions project as startup in Visual Studio.

### Deployed app (current)

| | |
|---|---|
| **Name** | `lmm-shabbat-pdf` |
| **Resource group** | `LmmWebAppGroup` |
| **Plan** | Flex Consumption (West US) |
| **URL** | https://lmm-shabbat-pdf.azurewebsites.net |
| **Function** | `ProcessShabbatPdf` |
| **Storage** | `livingmessiahstorage` |

Redeploy after code changes:

```powershell
.\scripts\deploy-function.ps1
```

### Deploy notes

1. Prefer **Flex Consumption** or **Premium** (agendas can be tens of MB).  
2. App settings already configured on `lmm-shabbat-pdf` (connection string style for trigger + uploads):
   - `Blob` / `Blob__ConnectionString` → storage connection string  
   - `Blob__StagingContainer` = `shabbat-service-staging`  
   - `Blob__SourceContainer` = `shabbat-service`  
   - `Blob__DestinationContainer` = `shabbat-service-md`  
3. Later hardening: switch to Managed Identity (`Blob__UseDefaultAzureCredential=true` + RBAC) and remove keys from app settings.  
4. CLI remains fully supported for manual / batch runs.  
5. Smoke-test: upload a full agenda PDF to `shabbat-service-staging`, then confirm the same name in `shabbat-service` (compressed if it was large) and a teaching-only PDF of the same name in `shabbat-service-md`.
6. After deploying the new `CompressStagingPdf` function, run `.\scripts\setup-function-eventgrid.ps1` so staging has an Event Grid subscription.

## Operator checklist (first Azure success)

1. Create **private** `shabbat-service-staging` (and `shabbat-service-md` if missing)  
2. Set Admin `AzureBlob:WeeklyDownloadContainer` = `shabbat-service-staging`  
3. Set Function `Blob:ConnectionString` (read staging + write service + teaching)  
4. Upload a weekly PDF in Admin, or `--blob` against an existing `shabbat-service` file  
5. Confirm same-name blobs in `shabbat-service` and `shabbat-service-md`  

## Extract rules

1. **Start** after full lines `Welcome` + `Bienvenido` / `Bienvenidos`  
2. **Skip** intro pages (Fair Use / agenda title patterns)  
3. **End** before `The Avinu Prayer`  
4. **Text layer only** — no OCR, no images in v1  

## License / content

Agenda PDFs and extracted Scripture text are used for congregational study. Destination Markdown is intended to stay **private** until policy review.

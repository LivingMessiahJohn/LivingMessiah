# SukkotRegistration — SDK-style SQL Database project

Source of truth for the **SukkotRegistration** Azure SQL database (Free tier) and its local twin.

Objects were extracted from the existing **Sukkot** database and remapped from the `Sukkot` schema to **`dbo`**.

| | |
|--|--|
| Project | `Database/SukkotRegistration/SukkotRegistration.sqlproj` |
| SDK | `Microsoft.Build.Sql` 2.2.0 |
| Target platform | **Azure SQL Database** (`SqlAzureV12`) |
| Azure server | `lmm-azure-sql.database.windows.net` |
| Azure DB | `SukkotRegistration` (Free offer, same pattern as `SpecialEvent`) |
| Local (this machine) | `JohnsDellDT\SQLEXPRESS`, database `SukkotRegistration` |
| Source (legacy) | Local/Azure database `Sukkot`, schema `Sukkot.*` |

Tracked by [Issue #200](https://github.com/LivingMessiahJohn/LivingMessiah/issues/200).

Edit SQL here (or in VS Code **SQL Database Projects**). Keep Visual Studio for Blazor; build/publish SQL with CLI or VS Code.

Object layout:

```text
dbo/
  Tables/
  Views/
  Functions/
  StoredProcedures/
```

Includes registration tables, views, procs, ErrorLog helpers, and **`ScheduledEventsMarkdown`** (schedule markdown for #198).

---

## Prerequisites

```powershell
# Once per machine
dotnet tool install -g microsoft.sqlpackage
# Or update:
dotnet tool update -g microsoft.sqlpackage
```

VS Code: install **SQL Database Projects** + **SQL Server (mssql)** extensions.

---

## Build (dacpac)

**Prefer the CLI**, not Visual Studio Build Solution.

```powershell
dotnet build Database/SukkotRegistration/SukkotRegistration.sqlproj
```

Artifact:

`Database/SukkotRegistration/bin/Debug/SukkotRegistration.dacpac`

The project is in the solution for navigation; solution configurations do **not** build it (same as `SpecialEvent`).

---

## Publish → local SQLEXPRESS

Creates the database if missing (`CreateNewDatabase=True`).

```powershell
dotnet build Database/SukkotRegistration/SukkotRegistration.sqlproj

sqlpackage `
  /Action:Publish `
  /SourceFile:"Database/SukkotRegistration/bin/Debug/SukkotRegistration.dacpac" `
  /TargetServerName:"JohnsDellDT\SQLEXPRESS" `
  /TargetDatabaseName:"SukkotRegistration" `
  /TargetTrustServerCertificate:True `
  /p:CreateNewDatabase=True
```

### Copy data from legacy `Sukkot` schema (local)

After publish, optionally load rows from the existing local `Sukkot` database:

```powershell
sqlcmd -S "JohnsDellDT\SQLEXPRESS" -E -C -i docs/sql/sukkot-registration/01-Copy-Data-From-Legacy-Sukkot.sql
```

That script inserts into `SukkotRegistration.dbo.*` from `Sukkot.Sukkot.*` (and `dbo.ScheduledEventsMarkdown` when present).

Smoke check:

```sql
USE SukkotRegistration;
SELECT TOP 5 Id, FamilyName, EMail FROM dbo.Registration;
SELECT Markdown, LastRevised FROM dbo.ScheduledEventsMarkdown;
SELECT * FROM dbo.vwConstants;
```

---

## Publish → Azure SukkotRegistration (Free)

1. In Azure Portal, create database **`SukkotRegistration`** on `lmm-azure-sql` using the **Free** offer (same approach as `SpecialEvent`).
2. Publish the dacpac (do not commit passwords or `.pubxml` secrets):

```powershell
dotnet build Database/SukkotRegistration/SukkotRegistration.sqlproj

sqlpackage `
  /Action:Publish `
  /SourceFile:"Database/SukkotRegistration/bin/Debug/SukkotRegistration.dacpac" `
  /TargetConnectionString:"Server=tcp:lmm-azure-sql.database.windows.net,1433;Initial Catalog=SukkotRegistration;User ID=$env:AZURE_SQL_USER;Password=$env:AZURE_SQL_PASSWORD;Encrypt=True;TrustServerCertificate=False;"
```

Review the script first when the DB already has data:

```powershell
sqlpackage `
  /Action:Script `
  /SourceFile:"Database/SukkotRegistration/bin/Debug/SukkotRegistration.dacpac" `
  /TargetConnectionString:"..." `
  /OutputPath:"Database/SukkotRegistration/publish-preview.sql"
```

Data migration from Azure legacy **`Sukkot`** DB is a separate, intentional step (bacpac / insert scripts / SSIS). Prefer a named `.bacpac` backup of Azure `Sukkot` before any cutover.

---

## Schema remap (`Sukkot` → `dbo`)

| Legacy | New |
|--------|-----|
| `Sukkot.Registration` | `dbo.Registration` |
| `Sukkot.stpRegistrationInsert` | `dbo.stpRegistrationInsert` |
| `Sukkot.vwRegistration` | `dbo.vwRegistration` |
| … | all former `Sukkot.*` objects under `dbo` |

Constraint names may still contain the word `Sukkot` (e.g. `DF_Sukkot_Registration_FirstName`); only the **schema** was changed.

**App code still references `Sukkot.*`** until a follow-up cutover updates repositories and connection strings to `SukkotRegistration` / `dbo`.

---

## Extract / refresh from a live database

If Azure or local is ahead of git:

```powershell
$extract = "Database/SukkotRegistration/_extract-temp"

sqlpackage `
  /Action:Extract `
  /SourceConnectionString:"..." `
  /TargetFile:"$extract" `
  /p:ExtractTarget=SchemaObjectType

# Review, merge into dbo/..., then:
dotnet build Database/SukkotRegistration/SukkotRegistration.sqlproj
```

---

## Related

- Sibling project: `Database/SpecialEvent/`
- Annual startup scripts (still use legacy `Sukkot` schema until cutover): `docs/sql/sukkot-annual-startup/`
- Schedule table seed notes: issue #198 / #200 attachment `200-Create-ScheduledEventsMarkdown.md`

# SpecialEvent — SDK-style SQL Database project

Source of truth for the **SpecialEvent** Azure SQL database and its local twin.

| | |
|--|--|
| Project | `Database/SpecialEvent/SpecialEvent.sqlproj` |
| SDK | `Microsoft.Build.Sql` 2.2.0 |
| Target platform | **Azure SQL Database** (`SqlAzureV12`) |
| Azure server | `lmm-azure-sql.database.windows.net` |
| Azure DB | `SpecialEvent` |
| Local (this machine) | `JohnsDellDT\SQLEXPRESS`, database `SpecialEvent` |

Edit SQL here (or in VS Code **SQL Database Projects**). Keep Visual Studio for Blazor; build/publish SQL with CLI or VS Code.

Object layout (conventional, not required by the SDK — all `*.sql` under the project are included by default):

```text
dbo/
  Tables/
  Views/
  Functions/
  StoredProcedures/
```

Starter content: **ErrorLog** + dependencies (`zvwErrorLog`, `udfDateDiff_D_H_S_M`, `stpLogError*`, `stpPrintError`).  
If Azure already has more objects, **extract from Azure** (below) and merge into this tree.

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

`Microsoft.Build.Sql` restores as `netstandard2.1`. Classic SSDT / VS 2026 still often expects `net472`, which produces:

`01005: Assets file '...SpecialEvent\obj\project.assets.json' doesn't have a target for 'net472'`

That is a tooling mismatch, not a problem with Admin C# code. The `SpecialEvent` project is in the solution for navigation, but solution configurations do **not** build it. Build SQL with:

```powershell
dotnet build Database/SpecialEvent/SpecialEvent.sqlproj
```

Artifact:

`Database/SpecialEvent/bin/Debug/SpecialEvent.dacpac`

Optional: VS Code **SQL Database Projects** extension, or VS Installer component **SQL Server Data Tools SDK-Style** (when available for your VS version).

---

## Publish → local SQLEXPRESS

Creates the database if missing (`CreateNewDatabase=True`).

```powershell
dotnet build Database/SpecialEvent/SpecialEvent.sqlproj

sqlpackage `
  /Action:Publish `
  /SourceFile:"Database/SpecialEvent/bin/Debug/SpecialEvent.dacpac" `
  /TargetServerName:"JohnsDellDT\SQLEXPRESS" `
  /TargetDatabaseName:"SpecialEvent" `
  /TargetTrustServerCertificate:True `
  /p:CreateNewDatabase=True
```

Windows auth is the default for a local named instance when no user/password is passed.

Smoke check in SSMS / sqlcmd:

```sql
USE SpecialEvent;
EXEC dbo.stpLogErrorTest;
SELECT TOP 5 * FROM dbo.zvwErrorLog ORDER BY ErrorLogID DESC;
```

---

## Publish → Azure SpecialEvent

Prefer **Azure AD** or a SQL login via env vars — do not commit passwords or `.pubxml` with secrets (repo already gitignores `*.pubxml`).

```powershell
dotnet build Database/SpecialEvent/SpecialEvent.sqlproj

# Example: SQL auth (set env vars in your shell only)
sqlpackage `
  /Action:Publish `
  /SourceFile:"Database/SpecialEvent/bin/Debug/SpecialEvent.dacpac" `
  /TargetConnectionString:"Server=tcp:lmm-azure-sql.database.windows.net,1433;Initial Catalog=SpecialEvent;User ID=$env:AZURE_SQL_USER;Password=$env:AZURE_SQL_PASSWORD;Encrypt=True;TrustServerCertificate=False;"
```

Review the script first when the DB already has data:

```powershell
sqlpackage `
  /Action:Script `
  /SourceFile:"Database/SpecialEvent/bin/Debug/SpecialEvent.dacpac" `
  /TargetConnectionString:"..." `
  /OutputPath:"Database/SpecialEvent/publish-preview.sql"
```

---

## Extract from Azure → refresh project `.sql` files

Use this when Azure is ahead of git (objects created in portal/SSMS that are not in the project yet).

### Option A — VS Code (friendly)

1. Open the `LivingMessiah` folder in VS Code.
2. Command Palette → **Database Projects: Create Project from Database** (or **Update Project from Database** if the project already exists).
3. Connect to `lmm-azure-sql` → database `SpecialEvent`.
4. Target folder: `Database/SpecialEvent` (merge carefully so you keep `SpecialEvent.sqlproj`).
5. Prefer folder structure by **schema/object type** so files land under `dbo/Tables`, etc.

### Option B — SqlPackage extract to a temp folder, then copy

```powershell
$extract = "Database/SpecialEvent/_extract-temp"
New-Item -ItemType Directory -Path $extract -Force | Out-Null

sqlpackage `
  /Action:Extract `
  /SourceConnectionString:"Server=tcp:lmm-azure-sql.database.windows.net,1433;Initial Catalog=SpecialEvent;User ID=$env:AZURE_SQL_USER;Password=$env:AZURE_SQL_PASSWORD;Encrypt=True;" `
  /TargetFile:"$extract" `
  /p:ExtractTarget=SchemaObjectType

# Review $extract, then copy .sql files into dbo/... (do not overwrite SpecialEvent.sqlproj)
# Remove temp when done:
# Remove-Item -Recurse -Force $extract
```

`ExtractTarget=SchemaObjectType` yields paths like `dbo/Tables/ErrorLog.sql`.

### After extract

```powershell
dotnet build Database/SpecialEvent/SpecialEvent.sqlproj
```

Fix any build errors (unsupported options, order issues are rare with DacFx). Commit the new/changed `.sql` files.

---

## Sibling databases

Same pattern under `Database/`:

```text
Database/
  SpecialEvent/          ← this project
  SukkotRegistration/    ← Sukkot registration (dbo, Free Azure) — see #200
  LivingMessiah/         ← optional later
```

One SDK-style project per Azure database; add each to the **Database** solution folder.

---

## Related

- One-off deploy script (pre-project): `docs/sql/error-log/01-Create-ErrorLog-Objects.sql`
- Prefer **this project** as the long-term source of truth; keep docs scripts as runbooks only once the project is complete.

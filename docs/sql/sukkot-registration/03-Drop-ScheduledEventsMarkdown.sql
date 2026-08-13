/*
  Phase 7 of #215 — drop schedule table (content lives in Azure Blob).

  Apps use:
    container: sukkot-content
    blob:      sukkot/scheduled-events.md

  Local:
    sqlcmd -S "JohnsDellDT\SQLEXPRESS" -E -C -d SukkotRegistration -i docs/sql/sukkot-registration/03-Drop-ScheduledEventsMarkdown.sql

  Azure (example; set env credentials, do not commit secrets):
    sqlcmd -S tcp:lmm-azure-sql.database.windows.net,1433 -d SukkotRegistration -U $env:AZURE_SQL_USER -P $env:AZURE_SQL_PASSWORD -i docs/sql/sukkot-registration/03-Drop-ScheduledEventsMarkdown.sql

  Note: plain sqlpackage Publish without DropObjectsNotInSource does NOT remove
  tables that only exist on the target. Prefer this script (or a reviewed publish
  script that includes DROP TABLE).
*/

SET NOCOUNT ON;

IF OBJECT_ID(N'dbo.ScheduledEventsMarkdown', N'U') IS NULL
BEGIN
    PRINT 'dbo.ScheduledEventsMarkdown does not exist — nothing to drop.';
END
ELSE
BEGIN
    DROP TABLE dbo.ScheduledEventsMarkdown;
    PRINT 'Dropped dbo.ScheduledEventsMarkdown.';
END

-- Expect NULL
SELECT OBJECT_ID(N'dbo.ScheduledEventsMarkdown', N'U') AS ScheduledEventsMarkdown_ObjectId;

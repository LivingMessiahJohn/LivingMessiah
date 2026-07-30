/*
  Drop SpecialEvent schema objects from the LivingMessiah database.

  Context (issue #181 / #179):
  - Special events moved to their own Azure/local database: SpecialEvent (dbo.Event, dbo.EventType, ...).
  - Admin now uses ConnectionStrings:SpecialEvent, not LivingMessiah.SpecialEvent.*.
  - Old LivingMessiah data under SpecialEvent was outdated and not ported.

  Run against LivingMessiah only after Admin/PWA no longer reference SpecialEvent.* on this DB.

  Local example:
    sqlcmd -S "JOHNSDELLDT\SQLEXPRESS" -E -C -d LivingMessiah -i docs/sql/special-event/01-Drop-SpecialEvent-Schema-From-LivingMessiah.sql
*/

SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @DbName sysname = DB_NAME();
IF @DbName <> N'LivingMessiah'
BEGIN
    DECLARE @Msg nvarchar(400) = N'Connect to the LivingMessiah database before running this script. Current DB: ' + @DbName;
    RAISERROR(@Msg, 16, 1);
    RETURN;
END;

IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = N'SpecialEvent')
BEGIN
    PRINT 'Schema SpecialEvent does not exist — nothing to drop.';
    RETURN;
END;

BEGIN TRAN;

IF OBJECT_ID(N'SpecialEvent.vwSpecialEvent', N'V') IS NOT NULL
BEGIN
    DROP VIEW SpecialEvent.vwSpecialEvent;
    PRINT 'Dropped view SpecialEvent.vwSpecialEvent';
END;

IF OBJECT_ID(N'SpecialEvent.stpSpecialEventInsert', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE SpecialEvent.stpSpecialEventInsert;
    PRINT 'Dropped procedure SpecialEvent.stpSpecialEventInsert';
END;

IF OBJECT_ID(N'SpecialEvent.stpSpecialEventUpdate', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE SpecialEvent.stpSpecialEventUpdate;
    PRINT 'Dropped procedure SpecialEvent.stpSpecialEventUpdate';
END;

IF OBJECT_ID(N'SpecialEvent.PopulateEvent', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE SpecialEvent.PopulateEvent;
    PRINT 'Dropped procedure SpecialEvent.PopulateEvent';
END;

-- Drop any other procs remaining in the schema (safety net)
DECLARE @sql nvarchar(max) = N'';
SELECT @sql = @sql + N'DROP PROCEDURE ' + QUOTENAME(s.name) + N'.' + QUOTENAME(p.name) + N';'
FROM sys.procedures p
INNER JOIN sys.schemas s ON p.schema_id = s.schema_id
WHERE s.name = N'SpecialEvent';
IF LEN(@sql) > 0
BEGIN
    EXEC sp_executesql @sql;
    PRINT 'Dropped remaining SpecialEvent procedures.';
END;

IF OBJECT_ID(N'SpecialEvent.Event', N'U') IS NOT NULL
BEGIN
    DROP TABLE SpecialEvent.Event;
    PRINT 'Dropped table SpecialEvent.Event';
END;

IF OBJECT_ID(N'SpecialEvent.Type', N'U') IS NOT NULL
BEGIN
    DROP TABLE SpecialEvent.Type;
    PRINT 'Dropped table SpecialEvent.Type';
END;

-- Drop any other tables remaining in the schema (safety net)
SET @sql = N'';
SELECT @sql = @sql + N'DROP TABLE ' + QUOTENAME(s.name) + N'.' + QUOTENAME(t.name) + N';'
FROM sys.tables t
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE s.name = N'SpecialEvent';
IF LEN(@sql) > 0
BEGIN
    EXEC sp_executesql @sql;
    PRINT 'Dropped remaining SpecialEvent tables.';
END;

-- Drop any remaining views
SET @sql = N'';
SELECT @sql = @sql + N'DROP VIEW ' + QUOTENAME(s.name) + N'.' + QUOTENAME(v.name) + N';'
FROM sys.views v
INNER JOIN sys.schemas s ON v.schema_id = s.schema_id
WHERE s.name = N'SpecialEvent';
IF LEN(@sql) > 0
BEGIN
    EXEC sp_executesql @sql;
    PRINT 'Dropped remaining SpecialEvent views.';
END;

DROP SCHEMA SpecialEvent;
PRINT 'Dropped schema SpecialEvent.';

COMMIT TRAN;
PRINT 'Done.';
GO

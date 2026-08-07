/*
  Sukkot Annual Startup — Step 2: Rebuild AttendanceDate from Constants.

  Requires step 01 (Constants) first.
  SP prints delete/insert row counts.
*/

SET NOCOUNT ON;
USE SukkotRegistration;
GO

PRINT '=== BEFORE: AttendanceDate ===';
SELECT Id, [Date], [Value] FROM dbo.AttendanceDate ORDER BY Id;

DECLARE @RC int;
EXEC @RC = dbo.stpBuildAttendanceDate;
PRINT '=== stpBuildAttendanceDate return code: ' + CAST(@RC AS nvarchar(12)) + ' ===';

PRINT '=== AFTER: AttendanceDate ===';
SELECT Id, [Date], [Value] FROM dbo.AttendanceDate ORDER BY Id;

-- Optional markdown dump if dbo.Select2MD is installed:
-- EXEC dbo.Select2MD @table_name = 'AttendanceDate', @schema_name = 'Sukkot';
GO

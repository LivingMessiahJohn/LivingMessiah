/*
  Sukkot Annual Startup — Step 1: Update Constants for the new feast year.

  Server:  JohnsDellDT\SQLEXPRESS (local)
  Database: SukkotRegistration
  Schema:   dbo

  2026 attendance window aligns with FeastDayDates.Tabernacles = 2026-09-26:
    min = Tabernacles - 1 day  => 2026-09-25
    max = Tabernacles + 8 days => 2026-10-04

  EarlyRegistration* columns are deprecated but still updated for consistency.
*/

SET NOCOUNT ON;
USE SukkotRegistration;
GO

PRINT '=== BEFORE: dbo.Constants ===';
SELECT SingleRowId,
       EarlyRegistrationFee, EarlyRegistrationLastDay,
       RegistrationFee, RegistrationLastDay,
       AttendanceMinDate, AttendanceMaxDate,
       RegistrationFeeSingle
FROM dbo.Constants;

UPDATE dbo.Constants
SET EarlyRegistrationFee      = 100.0,
    EarlyRegistrationLastDay  = '2026-09-15',
    RegistrationFee           = 100.0,
    RegistrationLastDay       = '2026-09-15',
    AttendanceMinDate         = '2026-09-25',
    AttendanceMaxDate         = '2026-10-04';

PRINT '=== Constants rows updated: ' + CAST(@@ROWCOUNT AS nvarchar(12)) + ' ===';

PRINT '=== AFTER: dbo.vwConstants ===';
SELECT RegistrationLastDayMDY,
       RegistrationFee,
       RegistrationFeeSingle,
       RegistrationLastDay,
       AttendanceMinDateMDY,
       AttendanceMaxDateMDY,
       MinWeek,
       MaxWeek,
       FirstWeekStartDate,
       FirstWeekEndDate,
       SecondWeekStartDate,
       SecondWeekEndDate
FROM dbo.vwConstants;
GO

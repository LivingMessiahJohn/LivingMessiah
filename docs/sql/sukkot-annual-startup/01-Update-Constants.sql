/*
  Sukkot Annual Startup — Step 1: Update Constants for the new feast year.

  Server:  JohnsDellDT\SQLEXPRESS (local)
  Database: Sukkot
  Schema:   Sukkot

  2026 attendance window aligns with FeastDayDates.Tabernacles = 2026-09-26:
    min = Tabernacles - 1 day  => 2026-09-25
    max = Tabernacles + 8 days => 2026-10-04

  EarlyRegistration* columns are deprecated but still updated for consistency.
*/

SET NOCOUNT ON;
USE Sukkot;
GO

PRINT '=== BEFORE: Sukkot.Constants ===';
SELECT SingleRowId,
       EarlyRegistrationFee, EarlyRegistrationLastDay,
       RegistrationFee, RegistrationLastDay,
       AttendanceMinDate, AttendanceMaxDate,
       RegistrationFeeSingle
FROM Sukkot.Constants;

UPDATE Sukkot.Constants
SET EarlyRegistrationFee      = 100.0,
    EarlyRegistrationLastDay  = '2026-09-15',
    RegistrationFee           = 100.0,
    RegistrationLastDay       = '2026-09-15',
    AttendanceMinDate         = '2026-09-25',
    AttendanceMaxDate         = '2026-10-04';

PRINT '=== Constants rows updated: ' + CAST(@@ROWCOUNT AS nvarchar(12)) + ' ===';

PRINT '=== AFTER: Sukkot.vwConstants ===';
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
FROM Sukkot.vwConstants;
GO

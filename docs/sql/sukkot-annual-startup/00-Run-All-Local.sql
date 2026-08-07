/*
  Sukkot Annual Startup — master runner for LOCAL SQLEXPRESS only.

  Runs steps 01–03. Step 04 (CodeGen) is separate so you can review
  result grids in SSMS without noise.

  Review each step's PRINT metrics before deploying to Azure.
*/

SET NOCOUNT ON;
USE SukkotRegistration;
GO

:r 01-Update-Constants.sql
:r 02-Build-AttendanceDate.sql
:r 03-Delete-Prior-Year-Registrations.sql

PRINT '=== All local data steps finished. Run 04-CodeGen-Queries.sql next. ===';
GO

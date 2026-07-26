/*
  Sukkot Annual Startup — Step 4: CodeGen fragments for C# SmartEnums.

  Paste results into:
    - RCL/Features/Sukkot/Enums/Constants/DateRange.cs
    - RCL/Features/Sukkot/Enums/AttendanceDate.cs

  See docs/SukkotAnnualStartup.md for mapping details.
*/

SET NOCOUNT ON;
USE Sukkot;
GO

PRINT '=== vwDateRangeTypeCodeGen (DateRange.cs / DateRangeType) ===';
SELECT AttendanceMinDate, AttendanceMaxDate, DateRangeCodeGen
FROM Sukkot.vwDateRangeTypeCodeGen;

PRINT '=== vwAttendanceDateSmartFlagEnumCodeGen ===';
SELECT Id, [Date], Value, Title, DateYMD, DateRangeType, PermutationValue
FROM Sukkot.vwAttendanceDateSmartFlagEnumCodeGen
ORDER BY Id;

PRINT '=== stpAttendanceDateCodeGen (RegionId / Decl / Instantiation) ===';
EXEC Sukkot.stpAttendanceDateCodeGen;
GO

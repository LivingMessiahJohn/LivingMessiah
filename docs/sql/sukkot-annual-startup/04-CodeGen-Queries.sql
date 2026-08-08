/*
  Sukkot Annual Startup — Step 4: CodeGen fragments for C# SmartEnums.

  Paste results into:
    - RCL/Features/Sukkot/Enums/Constants/DateRange.cs
    - RCL/Features/Sukkot/Enums/AttendanceDate.cs

  See docs/SukkotAnnualStartup.md for mapping details.
*/

SET NOCOUNT ON;
USE SukkotRegistration;
GO

PRINT '=== vwDateRangeTypeCodeGen (DateRange.cs / DateRangeType) ===';
SELECT AttendanceMinDate, AttendanceMaxDate, DateRangeCodeGen
FROM dbo.vwDateRangeTypeCodeGen;

PRINT '=== vwAttendanceDateSmartFlagEnumCodeGen ===';
SELECT Id, [Date], Value, Title, DateYMD, DateRangeType, PermutationValue
FROM dbo.vwAttendanceDateSmartFlagEnumCodeGen
ORDER BY Id;

PRINT '=== stpAttendanceDateCodeGen (RegionId / Decl / Instantiation) ===';
EXEC dbo.stpAttendanceDateCodeGen;
GO

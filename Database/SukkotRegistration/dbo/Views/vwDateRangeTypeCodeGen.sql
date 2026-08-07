CREATE   VIEW dbo.vwDateRangeTypeCodeGen
AS

/*
SELECT * FROM dbo.vwDateRangeTypeCodeGen
SELECT AttendanceMinDateMDY, AttendanceMaxDateMDY  FROM dbo.vwConstants
*/

SELECT AttendanceMinDate, AttendanceMaxDate 
, '		public override DateRange Range => new DateRange(' 
+ 'Convert.ToDateTime("' + FORMAT(AttendanceMinDate, 'yyyy-MM-dd') + '"), ' 
+ 'Convert.ToDateTime("' + FORMAT(AttendanceMaxDate, 'yyyy-MM-dd') + '"));' 
AS DateRangeCodeGen
FROM dbo.Constants

GO


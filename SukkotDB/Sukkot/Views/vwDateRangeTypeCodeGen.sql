CREATE   VIEW Sukkot.vwDateRangeTypeCodeGen
AS

/*
SELECT * FROM Sukkot.vwDateRangeTypeCodeGen
SELECT AttendanceMinDateMDY, AttendanceMaxDateMDY  FROM Sukkot.vwConstants
*/

SELECT AttendanceMinDate, AttendanceMaxDate 
, '		public override DateRange Range => new DateRange(' 
+ 'Convert.ToDateTime("' + FORMAT(AttendanceMinDate, 'yyyy-MM-dd') + '"), ' 
+ 'Convert.ToDateTime("' + FORMAT(AttendanceMaxDate, 'yyyy-MM-dd') + '"));' 
AS DateRangeCodeGen
FROM Sukkot.Constants

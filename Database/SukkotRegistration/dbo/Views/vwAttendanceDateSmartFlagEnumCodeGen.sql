
CREATE       VIEW [dbo].[vwAttendanceDateSmartFlagEnumCodeGen]
AS

/*
SELECT * FROM dbo.vwAttendanceDateSmartFlagEnumCodeGen ORDER BY Id
SELECT AttendanceMinDateMDY, AttendanceMaxDateMDY  FROM dbo.vwConstants
*/

SELECT Id
, Date
, REPLACE( REPLACE(FORMAT([Date], N'ddd MM/dd'),' ','_'),'/','_') AS Value
, FORMAT([Date], N'ddd MM/dd') AS Title
, FORMAT([Date], 'yyyy-MM-dd') AS DateYMD
, '		public override DateRangeType DateRangeType => ' + 'Convert.ToDateTime("' + FORMAT([Date], 'yyyy-MM-dd') + '");' AS DateRangeType
, Value AS PermutationValue
FROM dbo.AttendanceDate

GO


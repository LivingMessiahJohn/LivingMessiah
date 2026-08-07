
CREATE   VIEW dbo.vwConstants
AS
/*

SELECT * FROM dbo.vwConstants

SELECT
  EarlyRegistrationLastDayMDY, EarlyRegistrationFee
, RegistrationLastDayMDY, RegistrationFee, RegistrationFeeSingle, RegistrationLastDay, RegistrationFeeAdjusted
, AttendanceMinDateMDY, AttendanceMaxDateMDY
FROM dbo.vwConstants

GRANT SELECT ON dbo.vwConstants TO InserUserName

*/

SELECT
  CONVERT(nvarchar(30), EarlyRegistrationLastDay, 101) AS EarlyRegistrationLastDayMDY
, EarlyRegistrationFee, EarlyRegistrationLastDay
, CONVERT(nvarchar(30), RegistrationLastDay, 101) AS RegistrationLastDayMDY
, RegistrationFee, RegistrationFeeSingle, RegistrationLastDay
, CASE WHEN  DATEDIFF(dd, EarlyRegistrationLastDay, GETDATE()) > 0  
    THEN (SELECT RegistrationFee FROM dbo.Constants)
		ELSE (SELECT EarlyRegistrationFee FROM dbo.Constants)
  END AS  RegistrationFeeAdjusted
, CONVERT(nvarchar(30), AttendanceMinDate, 101) AS AttendanceMinDateMDY
, CONVERT(nvarchar(30), AttendanceMaxDate, 101) AS AttendanceMaxDateMDY

, DATEPART(ww, AttendanceMinDate) AS MinWeek
, DATEPART(ww, AttendanceMaxDate) AS MaxWeek

, DATEADD(DAY, 1 - DATEPART(WEEKDAY, AttendanceMinDate), CAST(AttendanceMinDate AS DATE)) AS FirstWeekStartDate
, DATEADD(DAY, 7 - DATEPART(WEEKDAY, AttendanceMinDate), CAST(AttendanceMinDate AS DATE)) AS FirstWeekEndDate
, DATEADD(DAY, 1 - DATEPART(WEEKDAY, AttendanceMaxDate), CAST(AttendanceMaxDate AS DATE)) AS SecondWeekStartDate
, DATEADD(DAY, 7 - DATEPART(WEEKDAY, AttendanceMaxDate), CAST(AttendanceMaxDate AS DATE)) AS SecondWeekEndDate

/*
, DATEPART(dy, FirstWeekStartDate) FWS_DOY, DATEPART(dy, FirstWeekEndDate) FWE_DOY
, DATEPART(dy, SecondWeekStartDate) SWS_DOY, DATEPART(dy, SecondWeekEndDate) SWE_DOY
*/

FROM dbo.Constants

GO


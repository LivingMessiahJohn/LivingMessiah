CREATE   FUNCTION dbo.tvfAttendanceTwoWeeks (
@Wk1StartDate date, @Wk1EndDate date, @Wk2StartDate date, @Wk2EndDate date
)
RETURNS TABLE AS RETURN 
(
/*
SELECT RowId, Week, tw.Date, ad.Date AS AttendanceDate
FROM dbo.tvfAttendanceTwoWeeks(
		(SELECT FirstWeekStartDate FROM dbo.vwConstants)
	,	(SELECT FirstWeekEndDate FROM dbo.vwConstants)
	,	(SELECT SecondWeekStartDate FROM dbo.vwConstants)
	,	(SELECT SecondWeekEndDate FROM dbo.vwConstants)	
) tw
	LEFT OUTER JOIN dbo.AttendanceDate ad
		ON tw.Date = ad.Date
ORDER BY tw.RowId
*/

SELECT dbo.Numbers.Number+1 AS RowId, [Date] = DATEADD(DAY, Number, @Wk1StartDate), 1 AS Week
FROM dbo.Numbers
WHERE Number <= DATEDIFF(DAY, @Wk1StartDate, @Wk1EndDate)
UNION
SELECT dbo.Numbers.Number+8 AS RowId, [Date] = DATEADD(DAY, Number, @Wk2StartDate), 2 AS Week
FROM dbo.Numbers
WHERE Number <= DATEDIFF(DAY, @Wk2StartDate, @Wk2EndDate)

)

GO



CREATE   VIEW vwSpecialEvent
AS

/*

DECLARE @DaysAhead int = 100, @DaysPast int=-300

SELECT
  Id, EventDate
	, EventTypeId
--, EventType, EventTypeDescr
, Title, SubTitle, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId, Description
, DaysDiff, DaysDiffDescr
-- RowNum, YearId, DateId, EventType, DateType, EnumName
, ShowBeginDate, ShowEndDate
FROM vwSpecialEvent
WHERE DATEADD(d, @DaysAhead, GETUTCDATE()) >= EventDate
  AND DATEADD(d, @DaysPast, GETUTCDATE()) <= EventDate
ORDER BY EventDate
	
GRANT SELECT ON vwSpecialEvent TO [INSERT-USER-HERE]

*/

SELECT 
	e.Id
,	ROW_NUMBER() OVER(ORDER BY [DateTime] ) AS RowNum
, [DateTime] AS EventDate
, e.EventTypeId
, t.Descr AS EventTypeDescr
, CASE WHEN  GETUTCDATE()  > [DateTime]
		THEN DATEDIFF(DAY, [DateTime], GETUTCDATE())
		ELSE DATEDIFF(DAY, GETUTCDATE(), [DateTime])
	END AS DaysDiff
, CASE WHEN  GETUTCDATE()  > [DateTime]
		THEN 'Days Past'
		ELSE 'Days Ahead'
	END AS DaysDiffDescr
,  Title, SubTitle
, ShowBeginDate, ShowEndDate
, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId, Description
FROM Event	e
	INNER JOIN EventType t
		ON e.EventTypeId = t.Id
GO


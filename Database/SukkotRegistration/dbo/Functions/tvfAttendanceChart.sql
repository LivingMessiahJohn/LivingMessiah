CREATE   FUNCTION dbo.tvfAttendanceChart (@Day int, @Age int)
RETURNS TABLE AS RETURN 
(
/*
	SELECT * FROM dbo.tvfAttendanceChart(2, 1) WHERE Days <> 0 ORDER BY RegistrationId -- 2nd Day, Adult
	SELECT * FROM dbo.tvfAttendanceChart(2, 2) WHERE Days <> 0 ORDER BY RegistrationId -- 2nd Day, ChildBig
	SELECT * FROM dbo.tvfAttendanceChart(2, 3) WHERE Days <> 0 ORDER BY RegistrationId -- 2nd Day, ChildSmall
*/
SELECT Registration.Id AS RegistrationId, FamilyName, AttendanceBitwise
, @Age AS AgeSort
, CASE 
    WHEN @Age = 1 THEN 'Adults'
		WHEN @Age = 2 THEN 'ChildBig'
		              ELSE 'ChildSmall'
  END As AgeDesc
, CASE 
    WHEN @Age = 1 THEN Adults
		WHEN @Age = 2 THEN ChildBig
		              ELSE ChildSmall
  END As Days
, ad.Date
, ad.Id, LEFT(DATENAME(weekday, ad.Date), 3) + ' ' + CAST(DAY(ad.Date) AS VARCHAR(2)) AS FeastDay2
FROM dbo.Registration
CROSS JOIN dbo.AttendanceDate ad 
WHERE ad.Id = @Day  AND (AttendanceBitwise & ad.Value <> 0)
)

GO


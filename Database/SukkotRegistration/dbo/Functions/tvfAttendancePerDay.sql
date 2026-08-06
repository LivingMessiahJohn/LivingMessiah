CREATE   FUNCTION dbo.tvfAttendancePerDay (@Day int)
RETURNS TABLE AS RETURN 
(
/*
	SELECT * FROM dbo.tvfAttendancePerDay(2) ORDER BY RegistrationId
*/
SELECT Registration.Id AS RegistrationId, FamilyName, AttendanceBitwise
, Adults, ChildBig, ChildSmall,  Adults + ChildBig + ChildSmall AS TotalPeeps, ad.Date
, ad.Id, LEFT(DATENAME(weekday, ad.Date), 3) + ' ' + CAST(DAY(ad.Date) AS VARCHAR(2)) AS FeastDay2
FROM dbo.Registration
CROSS JOIN dbo.AttendanceDate ad 
WHERE ad.Id = @Day  AND (AttendanceBitwise & ad.Value <> 0)
)

GO


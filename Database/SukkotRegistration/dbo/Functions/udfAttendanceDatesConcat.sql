CREATE   FUNCTION dbo.udfAttendanceDatesConcat (@id AS int)
RETURNS  varchar(255)

/*
SELECT dbo.udfAttendanceDatesConcat(62) AS AttendanceDates

SELECT v.Id, v.Email
, dbo.udfAttendanceDatesConcat(v.Id) AS AttendanceDates
FROM dbo.vwRegistration v

GRANT EXECUTE ON dbo.udfAttendanceDatesConcat TO [INSERT-USER-HERE]

*/
BEGIN
	DECLARE @s varchar(255)
	SELECT @s =	
		STRING_AGG(CONVERT(nvarchar(30), ad.Date, 101), ',') 
		FROM dbo.vwRegistration v
		CROSS JOIN dbo.AttendanceDate ad
		WHERE v.Id=@id AND (v.AttendanceBitwise & ad.Value <> 0)
		GROUP BY v.Id

	RETURN @s

END

GO


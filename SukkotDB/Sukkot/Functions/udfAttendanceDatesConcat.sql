CREATE   FUNCTION Sukkot.udfAttendanceDatesConcat (@id AS int)
RETURNS  varchar(255)

/*
SELECT Sukkot.udfAttendanceDatesConcat(62) AS AttendanceDates

SELECT v.Id, v.Email
, Sukkot.udfAttendanceDatesConcat(v.Id) AS AttendanceDates
FROM Sukkot.vwRegistration v

GRANT EXECUTE ON Sukkot.udfAttendanceDatesConcat TO [INSERT-USER-HERE]

*/
BEGIN
	DECLARE @s varchar(255)
	SELECT @s =	
		STRING_AGG(CONVERT(nvarchar(30), ad.Date, 101), ',') 
		FROM Sukkot.vwRegistration v
		CROSS JOIN Sukkot.AttendanceDate ad
		WHERE v.Id=@id AND (v.AttendanceBitwise & ad.Value <> 0)
		GROUP BY v.Id

	RETURN @s

END

CREATE   FUNCTION Sukkot.udfSukkotAttendanceDaysCount (@bitwise AS int)
RETURNS  int
AS 
/*
SELECT Sukkot.udfSukkotAttendanceDaysCount(509) AS cnt509 --> 8
SELECT Sukkot.udfSukkotAttendanceDaysCount(256) AS cnt256 --> 1 @@Oct_17_Mon  INT = 256

SELECT Id
, AttendanceBitwise
, Sukkot.udfSukkotAttendanceDaysCount(AttendanceBitwise) AS AttendanceTotal
FROM Sukkot.Registration ORDER BY FamilyName

## Referenced by `vwRegistration`

SELECT Id, Email
, AttendanceBitwise,	AttendanceTotal,	AttendanceDatesCSV
FROM Sukkot.vwRegistration ORDER BY FirstName

# Relevante SmartEnums
- Folder Sukkot\Enums\

## `DateRangeType.cs`

## `AttendanceDate.cs`

*/

BEGIN
	DECLARE @i INT = 0

	DECLARE @Oct_09_Sun INT = 1
	DECLARE @Oct_10_Mon INT = 2
	DECLARE @Oct_11_Tue INT = 4
	DECLARE @Oct_12_Wed INT = 8
	DECLARE @Oct_13_Thu INT = 16
	DECLARE @Oct_14_Fri INT = 32
	DECLARE @Oct_15_Sat INT = 64
	DECLARE @Oct_16_Sun INT = 128
	DECLARE @Oct_17_Mon INT = 256
	--DECLARE @Oct_18_Tue INT = 512

	IF ( (@Oct_09_Sun & @bitwise) > 0 ) SET @i = @i + 1
	IF ( (@Oct_10_Mon & @bitwise) > 0 ) SET @i = @i + 1
	IF ( (@Oct_11_Tue & @bitwise) > 0 ) SET @i = @i + 1
	IF ( (@Oct_12_Wed & @bitwise) > 0 ) SET @i = @i + 1
	IF ( (@Oct_13_Thu & @bitwise) > 0 ) SET @i = @i + 1
	IF ( (@Oct_14_Fri & @bitwise) > 0 ) SET @i = @i + 1
	IF ( (@Oct_15_Sat & @bitwise) > 0 ) SET @i = @i + 1
	IF ( (@Oct_16_Sun & @bitwise) > 0 ) SET @i = @i + 1
	IF ( (@Oct_17_Mon & @bitwise) > 0 ) SET @i = @i + 1
	--IF ( (@Oct_10_Tue & @bitwise) > 0 ) SET @i = @i + 1

	RETURN @i
END

/*


SELECT 
Sukkot.udfSukkotAttendanceDaysCount(1) AS cnt1,
Sukkot.udfSukkotAttendanceDaysCount(2) AS cnt2,
Sukkot.udfSukkotAttendanceDaysCount(3) AS cnt3,
Sukkot.udfSukkotAttendanceDaysCount(4) AS cnt4,
Sukkot.udfSukkotAttendanceDaysCount(5) AS cnt5,
Sukkot.udfSukkotAttendanceDaysCount(6) AS cnt6,
Sukkot.udfSukkotAttendanceDaysCount(7) AS cnt7,
Sukkot.udfSukkotAttendanceDaysCount(8) AS cnt8,
Sukkot.udfSukkotAttendanceDaysCount(9) AS cnt9,
Sukkot.udfSukkotAttendanceDaysCount(10) AS cnt10

SELECT 
Sukkot.udfSukkotAttendanceDaysCount(501) AS cnt501,
Sukkot.udfSukkotAttendanceDaysCount(502) AS cnt502,
Sukkot.udfSukkotAttendanceDaysCount(503) AS cnt503,
Sukkot.udfSukkotAttendanceDaysCount(504) AS cnt504,
Sukkot.udfSukkotAttendanceDaysCount(505) AS cnt505,
Sukkot.udfSukkotAttendanceDaysCount(506) AS cnt506,
Sukkot.udfSukkotAttendanceDaysCount(507) AS cnt507,
Sukkot.udfSukkotAttendanceDaysCount(508) AS cnt508,
Sukkot.udfSukkotAttendanceDaysCount(509) AS cnt509,
Sukkot.udfSukkotAttendanceDaysCount(510) AS cnt510,
Sukkot.udfSukkotAttendanceDaysCount(511) AS cnt511,
Sukkot.udfSukkotAttendanceDaysCount(512) AS cnt512


https://docs.microsoft.com/en-us/sql/t-sql/language-elements/bitwise-operators-transact-sql?view=sql-server-2017

&  (Bitwise AND)							&= (Bitwise AND Assignment)					
|  (Bitwise OR)								|= (Bitwise OR Assignment)
^  (Bitwise Exclusive OR)			^= (Bitwise Exclusive OR Assignment)
~  (Bitwise NOT)							In other words, ones are changed to zeros and zeros are changed to ones. SELECT ~ a_int_value
 
 170 = 0000 0000 1010 1010 
& 75 = 0000 0000 0100 1011
	10 = 0000 0000 0000 1010

(A | B)  
 170 = 0000 0000 1010 1010
| 75 = 0000 0000 0100 1011
 235 = 0000 0000 1110 1011

(A ^ B)  
 170 = 0000 0000 1010 1010
^ 75 = 0000 0000 0100 1011
 225 = 0000 0000 1110 0001

https://stackoverflow.com/questions/8447/what-does-the-flags-enum-attribute-mean-in-c/8480#8480

*/
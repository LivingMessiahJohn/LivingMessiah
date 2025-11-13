
CREATE  PROCEDURE [Sukkot].[stpAttendanceDateCodeGen] 
AS
/*

DECLARE @RC int
EXEC @RC = Sukkot.stpAttendanceDateCodeGen 

SELECT * FROM Sukkot.vwAttendanceDateSmartFlagEnumCodeGen ORDER BY Id

SELECT * FROM Sukkot.AttendanceDate ORDER BY Id
*/

DECLARE @CRLF AS CHAR(2) = CHAR(13) + CHAR(10)
DECLARE @TAB AS CHAR(1) = CHAR(9)
DECLARE @Q AS CHAR(1) = '''' 
DECLARE @DQ AS CHAR(1) = CHAR(34) -- Double Quote
DECLARE @DQ2 AS CHAR(2) = CHAR(34)+CHAR(34) -- Double Quote
DECLARE @SQ AS CHAR(1) = CHAR(39) -- Single Quote
DECLARE @S1 AS varchar(255) = ''

DECLARE @ClassName AS VARCHAR(75) = 'AttendanceDate'
DECLARE @InstanceSuffix AS VARCHAR(25) = '_SE'

DECLARE @ExtraFields nvarchar(255) 

BEGIN

-- #region #region Id's
SELECT
'internal const int ' + Value + ' = ' + CAST(PermutationValue AS varchar(10)) + ';' AS RegionId
FROM vwAttendanceDateSmartFlagEnumCodeGen

--//	#endregion


-- #region  Declared Public Instances
	SELECT
	'public static readonly ' + @ClassName + ' ' + Value + ' = new ' + Value  + '_SE();' 
	AS CodeGenDeclPubInst
	FROM Sukkot.vwAttendanceDateSmartFlagEnumCodeGen
	ORDER BY Id
--//	SE=SmartEnum
--//	#endregion

-- 	private AttendanceDate(string name, int value) : base(name, value) { } // Constructor

-- nameof(Id.Fri_09_29)}", Id.Fri_09_29)
-- base($"{nameof(Id.Sun_10_01)}", Id.Sun_10_01) { }
	SELECT 
	'private sealed class ' + Value + @InstanceSuffix + ' : ' +  @ClassName  + @CRLF +  '{ ' 
	+ @CRLF + '	public ' + Value + @InstanceSuffix + '() : base($' + @DQ + '{nameof(BitwiseId.' + Value + ')}", BitwiseId.' + Value +  ') { }' 
	+ @CRLF + @TAB + 'public override string Title => ' + QUOTENAME(Title, CHAR(34)) + ';'
	+ @CRLF + @TAB + 'public override DateTime Date => Convert.ToDateTime("' + ad.DateYMD + @DQ + ');'
	+ @CRLF + @TAB + 'public override DateRangeType DateRangeType => DateRangeType.Attendance;' +
	+ @CRLF + @TAB + 'public override int Week => ' + CAST(Week AS VARCHAR(10)) + ';'
	+ @CRLF + ' } ' AS CodeGenInstantiation
	FROM Sukkot.tvfAttendanceTwoWeeks(
			(SELECT FirstWeekStartDate FROM Sukkot.vwConstants)
		,	(SELECT FirstWeekEndDate FROM Sukkot.vwConstants)
		,	(SELECT SecondWeekStartDate FROM Sukkot.vwConstants)
		,	(SELECT SecondWeekEndDate FROM Sukkot.vwConstants)	
	) tw
		INNER JOIN Sukkot.vwAttendanceDateSmartFlagEnumCodeGen ad
			ON tw.Date = ad.Date
	ORDER BY tw.RowId


END


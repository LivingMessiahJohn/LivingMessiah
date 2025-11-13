CREATE     PROCEDURE Sukkot.stpBuildAttendanceDate
AS
/*
	DECLARE @RC int
	EXEC @RC = Sukkot.stpBuildAttendanceDate

	SELECT * FROM Sukkot.AttendanceDate

*/

DECLARE @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  

DECLARE @StartDate date, @EndDate date;
SET @StartDate = (SELECT AttendanceMinDate FROM Sukkot.Constants)
SET @EndDate = (SELECT AttendanceMaxDate FROM Sukkot.Constants)

PRINT 'Start of ' + @ProcName
PRINT 'StartDate: ' + CAST(@StartDate AS NVARCHAR(12)) + ', EndDate: ' + CAST(@EndDate AS NVARCHAR(12))

BEGIN TRY

	PRINT 'Delete AttendanceDate rows'

	DELETE FROM Sukkot.AttendanceDate; 
	PRINT '...rows Deleted ' + CAST(@@ROWCOUNT AS NVARCHAR(12))

	INSERT INTO Sukkot.AttendanceDate (Id, [Date], [Value])
	SELECT dbo.Numbers.Number+1 
	, [Date] = DATEADD(DAY, Number, @StartDate)
	, POWER(2, dbo.Numbers.Number) AS Value
	FROM dbo.Numbers
	WHERE Number <= DATEDIFF(DAY, @StartDate, @EndDate)
	ORDER BY [Number];

	PRINT '...rows Inserted ' + CAST(@@ROWCOUNT AS NVARCHAR(12))

	PRINT 'End of ' + @ProcName

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
END CATCH;

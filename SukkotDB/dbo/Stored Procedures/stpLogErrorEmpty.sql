

CREATE PROCEDURE dbo.stpLogErrorEmpty
AS                              
/*		
	DECLARE @RC int
	EXECUTE @RC = dbo.stpLogErrorEmpty 

	SELECT * FROM zvwErrorLog

*/
	DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  
	DECLARE @MSG NVARCHAR(200) = 'Testing ErrorLog related objects.  Intentionally throwing an error. Inside:' + @ProcName
	PRINT 'Start of ' + @ProcName

	BEGIN TRY

		DELETE FROM dbo.ErrorLog

	END TRY

	BEGIN CATCH
		EXECUTE dbo.stpPrintError  
		EXECUTE dbo.stpLogError 
	END CATCH;
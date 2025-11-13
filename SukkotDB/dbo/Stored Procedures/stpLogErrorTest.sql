
CREATE PROCEDURE [dbo].[stpLogErrorTest]
AS                              
/*		
	DECLARE @RC int
	EXECUTE @RC = dbo.stpLogErrorTest 

	SELECT * FROM zvwErrorLog
	WHERE ErrorProcedure = 'Sukkot.stpLogErrorTest'
*/
	DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  
	DECLARE @MSG NVARCHAR(200) = 'Testing ErrorLog related objects.  Intentionally throwing an error. Inside:' + @ProcName
	PRINT 'Start of ' + @ProcName

	BEGIN TRY
    RAISERROR (@MSG, 
               16, -- Severity.
               1 -- State.
               );

	END TRY

	BEGIN CATCH
		EXECUTE dbo.stpPrintError  
 		IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
		EXECUTE dbo.stpLogError 
	END CATCH;

CREATE     PROCEDURE Sukkot.stpHRADelete ( @Id int )
/*
	DECLARE @RC int
	EXECUTE @RC = Sukkot.stpHRADelete 1
	
	SELECT * FROM Sukkot.HouseRulesAgreement WHERE ID = 1

	SELECT * FROM zvwErrorLog
	WHERE ErrorProcedure LIKE '%stpHRADelete'  -- might be prefaced with a Schema

	GRANT EXECUTE ON Sukkot.stpHRADelete TO _________  

*/

AS

DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  

PRINT 'Start of ' + @ProcName + ', @Id=' + CAST(@Id AS NVARCHAR(12))

BEGIN TRY

	DELETE FROM Sukkot.HouseRulesAgreement WHERE Id = @Id;   
	PRINT '...rows Deleted ' + CAST(@@ROWCOUNT AS NVARCHAR(12))

	PRINT 'End of ' + @ProcName

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
	RETURN Error_Number();
END CATCH;

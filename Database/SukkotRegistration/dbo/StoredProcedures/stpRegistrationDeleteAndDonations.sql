
CREATE PROCEDURE [dbo].[stpRegistrationDeleteAndDonations] ( @RegistrationId int)
/*
	DECLARE @RC int
	EXECUTE @RC = dbo.stpRegistrationDeleteAndDonations 22
	
	SELECT * FROM dbo.Registration WHERE FamilyName LIKE 'Marsing%'
	SELECT * FROM dbo.Registration WHERE ID = 22

	SELECT * FROM zvwErrorLog
	WHERE ErrorProcedure LIKE '%stpRegistrationDeleteAndDonations'  -- might be prefaced with a Schema
*/

AS
DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  
PRINT 'Start of ' + @ProcName + ', @RegistrationId=' + CAST(@RegistrationId AS NVARCHAR(12))

BEGIN TRY

	PRINT 'Third delete donation row(s) (if any)...'
	DELETE FROM dbo.Donation WHERE Donation.RegistrationId = @RegistrationId; 

	PRINT 'Finally delete the registration row...'
	DELETE FROM dbo.Registration WHERE Registration.Id = @RegistrationId;   

	PRINT 'End of ' + @ProcName + ', @RegistrationId=' + CAST(@RegistrationId AS NVARCHAR(12))

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
END CATCH;

GO


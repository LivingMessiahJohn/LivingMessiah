CREATE PROCEDURE dbo.stpHouseRulesAgreementDelete ( @EMail as nvarchar(75) )
/*
	DECLARE @RC int
	EXECUTE @RC = dbo.stpHouseRulesAgreementDelete 'aeaij@yahoo.com'
	
	SELECT * FROM dbo.Registration WHERE FamilyName LIKE 'Marsing%'
	SELECT * FROM dbo.Registration WHERE ID = 22

	SELECT * FROM zvwErrorLog
	WHERE ErrorProcedure LIKE '%stpRegistrationDeleteAndDonations'  -- might be prefaced with a Schema
*/

AS
DECLARE @RegistrationId int = 0
DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  

SELECT @RegistrationId = Id FROM dbo.Registration WHERE EMail = @Email
PRINT 'Start of ' + @ProcName + ', @@Email=' + @Email + ', @RegistrationId=' + CAST(@RegistrationId AS NVARCHAR(12))

BEGIN TRY

	PRINT 'Delete Donation row(s) (if any)...'
	DELETE FROM dbo.Donation WHERE Donation.RegistrationId = @RegistrationId; 
	PRINT '...rows Deleted ' + CAST(@@ROWCOUNT AS NVARCHAR(12))

	PRINT 'Delete Registration row(s) (if any)...'
	DELETE FROM dbo.Registration WHERE Id = @RegistrationId; 
	PRINT '...rows Deleted ' + CAST(@@ROWCOUNT AS NVARCHAR(12))

	PRINT 'Finally delete the HouseRulesAgreement row...'
	DELETE FROM dbo.HouseRulesAgreement WHERE EMail = @EMail;   
	PRINT '...rows Deleted ' + CAST(@@ROWCOUNT AS NVARCHAR(12))

	PRINT 'End of ' + @ProcName

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
END CATCH;

GO


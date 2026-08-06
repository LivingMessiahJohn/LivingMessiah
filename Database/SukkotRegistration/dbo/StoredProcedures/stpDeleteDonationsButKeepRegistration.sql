
CREATE PROCEDURE dbo.stpDeleteDonationsButKeepRegistration ( @RegistrationId int,  @StatusId int)
/*
	DECLARE @RC int
	EXECUTE @RC = dbo.stpDeleteDonationsButKeepRegistration 22
	
	SELECT * FROM dbo.Registration WHERE FamilyName LIKE 'Marsing%'
	SELECT * FROM dbo.Registration WHERE ID = 22

	SELECT * FROM zvwErrorLog
	WHERE ErrorProcedure LIKE '%stpDeleteDonationsButKeepRegistration' 

	This is setup so I can test the donation part without deleting the whole registration
*/

AS
DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  
PRINT 'Start of ' + @ProcName + ', @RegistrationId=' + CAST(@RegistrationId AS NVARCHAR(12))

BEGIN TRY

	PRINT 'Second delete donation row(s) (if any)...'
	DELETE FROM dbo.Donation WHERE Donation.RegistrationId = @RegistrationId; 

	UPDATE dbo.Registration SET StatusId=@StatusId WHERE Registration.Id = @RegistrationId;   

	PRINT 'End of ' + @ProcName + ', @RegistrationId=' + CAST(@RegistrationId AS NVARCHAR(12))

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
END CATCH;

GO


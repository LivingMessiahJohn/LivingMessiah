
CREATE PROCEDURE Sukkot.stpDeleteDonationsButKeepRegistration ( @RegistrationId int,  @StatusId int)
/*
	DECLARE @RC int
	EXECUTE @RC = Sukkot.stpDeleteDonationsButKeepRegistration 22
	
	SELECT * FROM Sukkot.Registration WHERE FamilyName LIKE 'Marsing%'
	SELECT * FROM Sukkot.Registration WHERE ID = 22

	SELECT * FROM zvwErrorLog
	WHERE ErrorProcedure LIKE '%stpDeleteDonationsButKeepRegistration' 

	This is setup so I can test the donation part without deleting the whole registration
*/

AS
DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  
PRINT 'Start of ' + @ProcName + ', @RegistrationId=' + CAST(@RegistrationId AS NVARCHAR(12))

BEGIN TRY

	PRINT 'Second delete donation row(s) (if any)...'
	DELETE FROM Sukkot.Donation WHERE Donation.RegistrationId = @RegistrationId; 

	UPDATE Sukkot.Registration SET StatusId=@StatusId WHERE Registration.Id = @RegistrationId;   

	PRINT 'End of ' + @ProcName + ', @RegistrationId=' + CAST(@RegistrationId AS NVARCHAR(12))

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
END CATCH;
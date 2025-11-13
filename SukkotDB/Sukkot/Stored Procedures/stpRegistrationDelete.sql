
CREATE   PROCEDURE [Sukkot].[stpRegistrationDelete] ( @RegistrationId int)
/*
	DECLARE @RC int
	EXECUTE @RC = Sukkot.stpRegistrationDelete 1016
	
	SELECT * FROM zvwErrorLog
	WHERE ErrorProcedure LIKE '%stpRegistrationDelete'  -- might be prefaced with a Schema
*/

AS
DECLARE @RC int, @DonationRowCount int=0, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  
PRINT 'Start of ' + @ProcName  + ', @RegistrationId=' + CAST(@RegistrationId AS NVARCHAR(12))

BEGIN TRY

	SELECT @DonationRowCount =  ISNULL(COUNT(*),0) FROM Sukkot.Donation WHERE RegistrationId = @RegistrationId;

	IF @DonationRowCount > 0 
		BEGIN
			DECLARE @MSG NVARCHAR(400) = 'Can not have donation rows when deleting registration, ProcName=' + @ProcName +
			 ', @RegistrationId=' + CAST(@RegistrationId AS NVARCHAR(12)) +
			 ', @DonationRowCount=' + CAST(@DonationRowCount AS NVARCHAR(12))
			PRINT @MSG;
			THROW 51000, @MSG, 1;  -- RAISERROR (@MSG, 16, 1); -- 16=Severity, 1=State
		END


	DELETE FROM Sukkot.Registration WHERE Registration.Id = @RegistrationId;   

	PRINT 'End of ' + @ProcName + ', @RegistrationId=' + CAST(@RegistrationId AS NVARCHAR(12))

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
	RETURN Error_Number();
END CATCH;

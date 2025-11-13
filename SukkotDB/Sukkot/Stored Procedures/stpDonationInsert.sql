CREATE PROCEDURE Sukkot.stpDonationInsert
(
	@RegistrationId int,
	@Amount MONEY,
	@Notes nvarchar(255),
	@Email nvarchar(100),
	@ReferenceId nvarchar(100),
	@CreatedBy nvarchar(25),
	@CreateDate smalldatetime	, 
	@NewId as int OUTPUT
)
AS
/*
<keyword>Insert Detail</keyword>
<keyword>SELECT Assignment</keyword>

SELECT * FROM zvwErrorLog WHERE ErrorProcedure = 'Sukkot.stpDonationInsert'

*/

DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  


DECLARE @StatusIdComplete int;
SELECT  @StatusIdComplete = StatusCompleteId FROM Sukkot.Constants;

PRINT 'Start of ' + @ProcName
PRINT 'RegistrationId=' + CAST(@RegistrationId AS NVARCHAR(12))

BEGIN TRY

	INSERT INTO Sukkot.Donation
	(RegistrationId, Detail, Amount, Notes, Email, ReferenceId, CreatedBy, CreateDate) -- Id, 
	SELECT
	 @RegistrationId
	 , ISNULL((SELECT MAX(Detail) FROM Sukkot.Donation WHERE @RegistrationId = Sukkot.Donation.RegistrationId), 0) + 1 AS Detail
	 , @Amount, @Notes, ISNULL(@Email,'NULL'), @ReferenceId, @CreatedBy, @CreateDate

	SET @NewId = SCOPE_IDENTITY() 

	/*
		#### No-Partial-Payments Business Rule
		A single payment must mean that the StatusId (Aka Step) in now 4 (Complete)
	*/
	PRINT 'Updating status to ' +  CAST(@StatusIdComplete AS nvarchar(10)) + ' (Fully Paid)'
	UPDATE Sukkot.Registration SET StatusId = @StatusIdComplete WHERE Id = @RegistrationId

	PRINT 'Delete from Sukkot.Stripe '
	DELETE Sukkot.Stripe WHERE Email=@Email

	RETURN 0;

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
 	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
	RETURN Error_Number();
END CATCH;

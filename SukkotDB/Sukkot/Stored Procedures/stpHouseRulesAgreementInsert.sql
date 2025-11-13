CREATE PROCEDURE [Sukkot].[stpHouseRulesAgreementInsert]
( @EMail as nvarchar(75)
, @TimeZone as nvarchar(50)
, @NewId as int OUTPUT) 
/*
	DECLARE @RC int
	EXECUTE @RC = Sukkot.stpHouseRulesAgreementInsert
*/

AS

DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  
PRINT 'Start of ' + @ProcName

BEGIN TRY

	INSERT INTO Sukkot.HouseRulesAgreement (
					EMail, AcceptedDate,	TimeZone)
	VALUES (@EMail,	GETUTCDATE(),	@TimeZone)   

	SET @NewId = SCOPE_IDENTITY() 

	PRINT 'End of ' + @ProcName + ', @NewId=' + CAST(@NewId AS NVARCHAR(12));

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
	RETURN Error_Number();
END CATCH;




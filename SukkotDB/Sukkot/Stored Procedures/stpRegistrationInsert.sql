CREATE     PROCEDURE Sukkot.stpRegistrationInsert
( @FamilyName as nvarchar(75), @FirstName as nvarchar(75)
, @SpouseName as nvarchar(75),	@OtherNames as nvarchar(255)
, @EMail as nvarchar(75), @Phone as nvarchar(15) = Null
, @Adults as int, @ChildBig as int, @ChildSmall as int
, @FeeEnumValue as int
, @StatusId as int
, @AttendanceBitwise as int
, @Notes as nvarchar(800) = Null
, @Avatar as nvarchar(255)
, @NewId as int OUTPUT) 
/*
	DECLARE @RC int
	EXECUTE @RC = Sukkot.stpRegistrationInsert
	
	SELECT * FROM zvwErrorLog
	WHERE ErrorProcedure LIKE '%stpRegistration'  -- might be prefaced with a Schema

	( @HouseRulesAgreementId as int

*/

AS

DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  
DECLARE @HouseRulesAgreementId as int
PRINT 'Start of ' + @ProcName

BEGIN TRY

	SELECT @HouseRulesAgreementId = Id FROM Sukkot.HouseRulesAgreement WHERE EMail = @EMail

	PRINT '@HouseRulesAgreementId=' + CAST(@HouseRulesAgreementId AS NVARCHAR(12));

	INSERT INTO Sukkot.Registration (
		HouseRulesAgreementId,
		FamilyName,
		FirstName,
		SpouseName,
		OtherNames,
		EMail,
		Phone,
		Adults,
		ChildBig,
		ChildSmall,
		FeeEnumValue,
		AttendanceBitwise,
		StatusId,
		Avatar,
		Notes)
	VALUES (
		@HouseRulesAgreementId,
		@FamilyName,
		@FirstName,
		@SpouseName,
		@OtherNames,
		@EMail,
		@Phone,
		@Adults,
		@ChildBig,
		@ChildSmall,
		@FeeEnumValue,
		@AttendanceBitwise,
		@StatusId,
		@Avatar,
		@Notes)   

	SET @NewId = SCOPE_IDENTITY() 

	PRINT 'End of ' + @ProcName + ', @NewId=' + CAST(@NewId AS NVARCHAR(12));

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
	RETURN Error_Number();
END CATCH;

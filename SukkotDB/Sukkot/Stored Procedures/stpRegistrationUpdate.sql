CREATE   PROCEDURE Sukkot.stpRegistrationUpdate
( @Id as int
, @FamilyName as nvarchar(75), @FirstName as nvarchar(75)
, @SpouseName as nvarchar(75), @OtherNames as nvarchar(255)
, @EMail as nvarchar(75), @Phone as nvarchar(15) = Null
, @Adults as int, @ChildBig as int, @ChildSmall as int, @FeeEnumValue as int
, @StatusId as int
, @AttendanceBitwise as int
, @Notes as nvarchar(800) = Null
, @Avatar as nvarchar(255)) 
/*
	DECLARE @RC int
	EXECUTE @RC = Sukkot.stpRegistrationUpdate
	
	SELECT * FROM zvwErrorLog
	WHERE ErrorProcedure LIKE '%stpRegistrationUpdate'  -- might be prefaced with a Schema

	GRANT EXECUTE ON Sukkot.stpRegistrationUpdate TO InserUserName

*/

AS

DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  
PRINT 'Start of ' + @ProcName

BEGIN TRY

	UPDATE Sukkot.Registration SET 
		FamilyName = @FamilyName,
		FirstName = @FirstName,
		SpouseName = @SpouseName,
		OtherNames = @OtherNames,
		EMail = @EMail,
		Phone = @Phone,
		Adults = @Adults,
		ChildBig = @ChildBig,
		ChildSmall = @ChildSmall,
		FeeEnumValue = @FeeEnumValue,
		AttendanceBitwise = @AttendanceBitwise,
		StatusId = @StatusId, 
		Notes = @Notes,
		Avatar = @Avatar
	WHERE Id = @Id

	PRINT 'End of ' + @ProcName + ', @@Id=' + CAST(@Id AS NVARCHAR(12));
	RETURN 0;

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
	RETURN Error_Number();
END CATCH;

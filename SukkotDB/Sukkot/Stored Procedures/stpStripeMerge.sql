
CREATE    PROCEDURE Sukkot.stpStripeMerge
(
    @Email nvarchar(75)
    , @RegistrationId int
    , @NewId int OUTPUT
)
AS
/*

DECLARE @RC int
DECLARE @Email nvarchar(75) = 'test02emailconfirmed@livingmessiah.com'
DECLARE @RegistrationId int = 78
DECLARE @NewId int

EXECUTE @RC = [Sukkot].[stpStripeMerge] 
   @Email
  ,@RegistrationId
  ,@NewId OUTPUT
GO

SELECT Id, Email, RegistrationId, ModificationCount, LastModifiedDate
FROM Sukkot.Stripe

--DELETE Sukkot.Stripe WHERE Id=1

SELECT * FROM zvwErrorLog WHERE ErrorProcedure = 'Sukkot.stpStripeMerge'

GRANT EXECUTE ON Sukkot.stpStripeMerge TO _________  

<keyword>Merge</keyword>

*/

DECLARE @RC int, @ProcName nvarchar(128) = OBJECT_NAME(@@PROCID);  

BEGIN TRY
    SET NOCOUNT ON;

    MERGE INTO Sukkot.Stripe AS target
    USING (
        SELECT 
            @Email AS Email,
            @RegistrationId AS RegistrationId
    ) AS source
    ON target.Email = source.Email
    WHEN MATCHED THEN
        UPDATE SET 
            RegistrationId = source.RegistrationId,
            ModificationCount = ModificationCount + 1,
            LastModifiedDate = GETUTCDATE(),
            @NewId = 0
    WHEN NOT MATCHED THEN
        INSERT (Email, RegistrationId, ModificationCount, LastModifiedDate)
        VALUES (source.Email, source.RegistrationId, 1, GETUTCDATE() );
         --OUTPUT inserted.Id INTO @NewId;
    SET @NewId = ISNULL(@NewId, SCOPE_IDENTITY());

    RETURN 0;

END TRY

BEGIN CATCH
	EXECUTE dbo.stpPrintError  
 	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
	EXECUTE dbo.stpLogError 
	RETURN Error_Number();
END CATCH;

-- stpLogErrorTest Stored Procedure
-- Test stored procedure for logging errors
CREATE PROCEDURE [dbo].[stpLogErrorTest]
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ErrorLog] 
    (
        [ErrorTime],
        [UserName],
        [ErrorNumber],
        [ErrorSeverity],
        [ErrorState],
        [ErrorProcedure],
        [ErrorLine],
        [ErrorMessage]
    )
    VALUES 
    (
        GETDATE(),
        SYSTEM_USER,
        50000,
        16,
        1,
        'stpLogErrorTest',
        1,
        'Test error log entry'
    );
    
    RETURN @@ROWCOUNT;
END;

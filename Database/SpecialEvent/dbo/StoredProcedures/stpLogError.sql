CREATE   PROCEDURE [dbo].[stpLogError]
    @BatchLogJobId INT = 0,
    @ErrorLogID INT = 0 OUTPUT -- ErrorLogID of the row inserted
AS
BEGIN
    SET NOCOUNT ON;

    -- Output parameter value of 0 indicates that error information was not logged
    SET @ErrorLogID = 0;

    BEGIN TRY
        -- Return if there is no error information to log
        IF ERROR_NUMBER() IS NULL
            RETURN;

        -- Return if inside an uncommittable transaction.
        IF XACT_STATE() = -1
        BEGIN
            PRINT 'Cannot log error since the current transaction is in an uncommittable state. '
                + 'Rollback the transaction before executing stpLogError in order to successfully log error information.';
            RETURN;
        END

        INSERT [dbo].[ErrorLog]
        (
            [UserName],
            [ErrorNumber],
            [ErrorSeverity],
            [ErrorState],
            [ErrorProcedure],
            [ErrorLine],
            [ErrorMessage],
            BatchLogJobId
        )
        VALUES
        (
            CONVERT(SYSNAME, CURRENT_USER),
            ERROR_NUMBER(),
            ERROR_SEVERITY(),
            ERROR_STATE(),
            ERROR_PROCEDURE(),
            ERROR_LINE(),
            ERROR_MESSAGE(),
            @BatchLogJobId
        );

        -- Pass back the ErrorLogID of the row inserted
        SET @ErrorLogID = @@IDENTITY;
    END TRY
    BEGIN CATCH
        PRINT 'An error occurred in stored procedure stpLogError: ';
        EXECUTE [dbo].[stpPrintError];
        RETURN -1;
    END CATCH
END;

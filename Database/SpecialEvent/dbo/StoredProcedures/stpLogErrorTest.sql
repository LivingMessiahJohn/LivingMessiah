CREATE   PROCEDURE [dbo].[stpLogErrorTest]
AS
/*
  DECLARE @RC int
  EXECUTE @RC = dbo.stpLogErrorTest
  SELECT * FROM zvwErrorLog WHERE ErrorProcedure = 'dbo.stpLogErrorTest'
*/
BEGIN
    DECLARE @ProcName NVARCHAR(128) = OBJECT_NAME(@@PROCID);
    DECLARE @MSG NVARCHAR(200) =
        N'Testing ErrorLog related objects.  Intentionally throwing an error. Inside:' + @ProcName;
    PRINT 'Start of ' + @ProcName;

    BEGIN TRY
        RAISERROR(@MSG, 16, 1);
    END TRY
    BEGIN CATCH
        EXECUTE dbo.stpPrintError;
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        EXECUTE dbo.stpLogError;
    END CATCH;
END;

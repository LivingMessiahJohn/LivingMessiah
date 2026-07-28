CREATE   PROCEDURE [dbo].[stpLogErrorEmpty]
AS
/*
  DECLARE @RC int
  EXECUTE @RC = dbo.stpLogErrorEmpty
  SELECT * FROM zvwErrorLog
*/
BEGIN
    DECLARE @ProcName NVARCHAR(128) = OBJECT_NAME(@@PROCID);
    PRINT 'Start of ' + @ProcName;

    BEGIN TRY
        DELETE FROM dbo.ErrorLog;
    END TRY
    BEGIN CATCH
        EXECUTE dbo.stpPrintError;
        EXECUTE dbo.stpLogError;
    END CATCH;
END;

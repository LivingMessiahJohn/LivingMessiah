CREATE   PROCEDURE [dbo].[stpPrintError]
    @BatchLogJobId INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    -- Print error information.
    PRINT 'Error ' + CONVERT(VARCHAR(50), ERROR_NUMBER()) +
          ', Severity ' + CONVERT(VARCHAR(5), ERROR_SEVERITY()) +
          ', State ' + CONVERT(VARCHAR(5), ERROR_STATE()) +
          ', Procedure ' + ISNULL(ERROR_PROCEDURE(), '-') +
          ', Line ' + CONVERT(VARCHAR(5), ERROR_LINE()) +
          ', [BatchLogJob.Id] ' + CONVERT(VARCHAR(5), @BatchLogJobId);
    PRINT ERROR_MESSAGE();
END;

-- zvwErrorLog View
-- View for querying error logs
CREATE VIEW [dbo].[zvwErrorLog]
AS
SELECT 
    [ErrorLogID],
    [ErrorTime],
    [UserName],
    [ErrorNumber],
    [ErrorSeverity],
    [ErrorState],
    [ErrorProcedure],
    [ErrorLine],
    [ErrorMessage]
FROM 
    [dbo].[ErrorLog];

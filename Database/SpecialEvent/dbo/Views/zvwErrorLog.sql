CREATE VIEW [dbo].[zvwErrorLog]
AS
/*
  SELECT * FROM zvwErrorLog
  WHERE ErrorProcedure LIKE '%stpRegistrationDelete'
*/
SELECT TOP (1000)
    ErrorProcedure,
    ErrorNumber,
    ErrorLine,
    ErrorMessage,
    ErrorLogID,
    ErrorTime,
    dbo.udfDateDiff_D_H_S_M(ErrorTime, GETDATE()) AS ErrorTime2
FROM ErrorLog
ORDER BY ErrorLogID DESC;

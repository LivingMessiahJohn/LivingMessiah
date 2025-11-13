
CREATE    VIEW dbo.zvwErrorLog
AS

/*
	SELECT *
	FROM zvwErrorLog
	WHERE ErrorProcedure LIKE '%stpRegistrationDelete'  -- might be prefaced with a Schema

SELECT DATEDIFF(day, '2023/08/14', GETDATE()) AS DaysAgo;
SELECT DATEDIFF(hours, '2023/08/14', GETDATE()) AS HoursAgo;

*/

SELECT TOP 1000
	ErrorProcedure, ErrorNumber, ErrorLine, ErrorMessage
, ErrorLogID
, ErrorTime
, dbo.udfDateDiff_D_H_S_M(ErrorTime, GetDate()) AS ErrorTime2
FROM ErrorLog 
ORDER BY ErrorLogID DESC

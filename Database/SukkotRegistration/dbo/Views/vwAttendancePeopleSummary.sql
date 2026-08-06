
CREATE VIEW [dbo].vwAttendancePeopleSummary
AS
/*
	SELECT Adults, ChildBig, ChildSmall, TotalPeeps FROM dbo.vwAttendancePeopleSummary
*/

SELECT SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM((Adults + ChildBig + ChildSmall)) AS TotalPeeps 
From dbo.Registration

GO


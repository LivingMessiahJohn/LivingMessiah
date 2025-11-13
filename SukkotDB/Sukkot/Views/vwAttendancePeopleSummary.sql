
CREATE VIEW [Sukkot].vwAttendancePeopleSummary
AS
/*
	SELECT Adults, ChildBig, ChildSmall, TotalPeeps FROM Sukkot.vwAttendancePeopleSummary
*/

SELECT SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM((Adults + ChildBig + ChildSmall)) AS TotalPeeps 
From Sukkot.Registration
CREATE VIEW dbo.vwAttendanceAllFeastDays
AS
/*
SELECT * FROM dbo.vwAttendanceAllFeastDays ORDER BY Id

SELECT MIN(TotalPeeps) AS MinPeeps, AVG(TotalPeeps) AS AvgPeeps, MAX(TotalPeeps) AS MaxPeeps, SUM(TotalPeeps) AS SumPeeps
FROM dbo.vwAttendanceAllFeastDays
*/

SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM dbo.tvfAttendancePerDay(1) 
GROUP BY FeastDay2, Id

UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM dbo.tvfAttendancePerDay(2) 
GROUP BY FeastDay2, Id

UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM dbo.tvfAttendancePerDay(3) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM dbo.tvfAttendancePerDay(4) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM dbo.tvfAttendancePerDay(5) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM dbo.tvfAttendancePerDay(6) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM dbo.tvfAttendancePerDay(7) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM dbo.tvfAttendancePerDay(8) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM dbo.tvfAttendancePerDay(9) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM dbo.tvfAttendancePerDay(10) 
GROUP BY FeastDay2, Id

GO


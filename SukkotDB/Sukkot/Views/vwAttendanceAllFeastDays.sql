CREATE VIEW Sukkot.vwAttendanceAllFeastDays
AS
/*
SELECT * FROM Sukkot.vwAttendanceAllFeastDays ORDER BY Id

SELECT MIN(TotalPeeps) AS MinPeeps, AVG(TotalPeeps) AS AvgPeeps, MAX(TotalPeeps) AS MaxPeeps, SUM(TotalPeeps) AS SumPeeps
FROM Sukkot.vwAttendanceAllFeastDays
*/

SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM Sukkot.tvfAttendancePerDay(1) 
GROUP BY FeastDay2, Id

UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM Sukkot.tvfAttendancePerDay(2) 
GROUP BY FeastDay2, Id

UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM Sukkot.tvfAttendancePerDay(3) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM Sukkot.tvfAttendancePerDay(4) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM Sukkot.tvfAttendancePerDay(5) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM Sukkot.tvfAttendancePerDay(6) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM Sukkot.tvfAttendancePerDay(7) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM Sukkot.tvfAttendancePerDay(8) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM Sukkot.tvfAttendancePerDay(9) 
GROUP BY FeastDay2, Id
	
UNION ALL
SELECT FeastDay2, Id, SUM(Adults) Adults, SUM(ChildBig) ChildBig, SUM(ChildSmall) ChildSmall, SUM(TotalPeeps) TotalPeeps
FROM Sukkot.tvfAttendancePerDay(10) 
GROUP BY FeastDay2, Id

CREATE VIEW dbo.vwAttendanceChart
AS
/*
	SELECT * FROM dbo.vwAttendanceChart ORDER BY Id, AgeSort

	SELECT *,
	'new ColumnPart () {' + 
	'DimensionOne="Adults", ' +
	'Days=' + CAST(Days AS nvarchar(3)) + ' },'
	+ ' // Day:' + CAST(Id AS nvarchar(3)) + '' AS CodeGen
	FROM dbo.vwAttendanceChart ORDER BY Id, AgeDesc

	SELECT * FROM dbo.tvfAttendanceChart(2, 2) 
	WHERE Days <> 0 
	ORDER BY RegistrationId -- 2nd Day, ChildBig
*/
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(1, 1) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(1, 2) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(1, 3) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL

SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(2, 1) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(2, 2) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(2, 3) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL

SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(3, 1) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(3, 2) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(3, 3) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL

SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(4, 1) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(4, 2) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(4, 3) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL

SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(5, 1) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(5, 2) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(5, 3) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL

SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(6, 1) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(6, 2) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(6, 3) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL

SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(7, 1) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(7, 2) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(7, 3) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL

SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(8, 1) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(8, 2) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(8, 3) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL

SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(9, 1) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(9, 2) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(9, 3) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL

SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(10, 1) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(10, 2) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc UNION ALL
SELECT Id, FeastDay2, AgeSort, AgeDesc, SUM(Days) AS Days FROM dbo.tvfAttendanceChart(10, 3) WHERE Days <> 0 GROUP BY Id, FeastDay2, AgeSort, AgeDesc 


--GRANT SELECT ON dbo.vwAttendanceChart TO [insert-user]

GO


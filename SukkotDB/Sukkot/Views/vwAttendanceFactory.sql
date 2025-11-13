

CREATE VIEW Sukkot.vwAttendanceFactory 
/*
	select * from Sukkot.vwAttendanceFactory ORDER BY Id, AgeSort
*/

AS

	SELECT Id, AgeSort,
	'new vwAttendanceChart { ' 
	 +	' Id = ' + CAST( Id AS VARCHAR(5))
	 + ', FeastDay2 = "' + FeastDay2  + '"'
	 + ', AgeDesc = "' + AgeDesc + '"'
	 + ', AgeSort = ' + CAST( AgeSort AS VARCHAR(5))
	 + ', Days = ' + CAST( Days AS VARCHAR(5)) + ' }, '
	 AS SQL
	FROM Sukkot.vwAttendanceChart 
	--ORDER BY Id, AgeSort
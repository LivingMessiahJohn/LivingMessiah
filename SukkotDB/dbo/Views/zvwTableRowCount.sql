CREATE VIEW dbo.zvwTableRowCount
AS
/*
	SELECT * FROM zvwTableRowCount
*/

SELECT TOP 500
	T.name AS TableName
, I.rows AS RowCnt
, SCHEMA_NAME(T.schema_id) AS SchemaName
 FROM sys.tables AS T 
 INNER JOIN sys.sysindexes AS I 
	ON T.object_id = I.id AND I.indid < 2 
 --ORDER BY I.rows DESC
 ORDER BY SchemaName, I.name
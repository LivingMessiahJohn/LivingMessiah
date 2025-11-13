-- =============================================
-- Stored Procedure: Select2MD
-- Author: Based on Tomaz Kastrun's implementation
-- Description: Converts table results to Markdown format
-- Usage: EXEC dbo.Select2MD @table_name = 'YourTable', @schema_name = 'dbo'
-- =============================================

CREATE   PROCEDURE [dbo].[Select2MD]
    @table_name VARCHAR(200),
    @schema_name VARCHAR(20) = 'dbo'
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Get the columns of the table
    SELECT 
        c.Column_name,
        c.Ordinal_position,
        c.is_nullable,
        c.Data_Type
    INTO #temp
    FROM INFORMATION_SCHEMA.TABLES AS t
    JOIN INFORMATION_SCHEMA.COLUMNS AS c 
        ON t.table_name = c.table_name 
        AND t.table_schema = c.table_schema 
        AND t.table_Catalog = c.table_Catalog
    WHERE t.table_type = 'BASE TABLE'
        AND t.Table_name = @table_name
        AND t.table_schema = @schema_name;
    
    DECLARE @MD NVARCHAR(MAX);
    
    -- Title
    DECLARE @title NVARCHAR(MAX) = (
        SELECT '## Result for table: _**' + CAST(@table_name AS NVARCHAR(MAX)) + 
               '**_' + CHAR(13) + CHAR(10) + 
               '### Schema Name: _' + CAST(@schema_name AS NVARCHAR(MAX)) + '_' + CHAR(13) + CHAR(10)
    );
    
    -- Header |name |name2 |name3 |name4 |name5 |name6
    DECLARE @header VARCHAR(MAX);
    SELECT @header = COALESCE(@header + '**|**', '') + column_name
    FROM #temp
    ORDER BY Ordinal_position ASC;
    
    SELECT @header = '|**' + @header + '**|';
    
    -- Delimiter |--- |--- |--- |--- |--- |---
    DECLARE @nof_columns INT = (SELECT MAX(Ordinal_position) FROM #temp);
    DECLARE @firstLine NVARCHAR(MAX) = (SELECT REPLICATE('|---', @nof_columns) + '|');
    
    SET @MD = @title + @header + CHAR(13) + CHAR(10) + @firstLine + CHAR(13) + CHAR(10);
    
    -- Body - Build dynamic SQL to get table data
    DECLARE @body NVARCHAR(MAX);
    SET @body = 'SELECT ''|'' + CAST(';
    
    DECLARE @i INT = 1;
    WHILE @i <= @nof_columns
    BEGIN
        DECLARE @w VARCHAR(1000) = (SELECT column_name FROM #temp WHERE Ordinal_position = @i);
        SET @body = @body + @w + ' AS VARCHAR(MAX)) + ''|'' + CAST(';
        SET @i = @i + 1;
    END
    
    SET @body = (SELECT SUBSTRING(@body, 1, LEN(@body) - 8));
    SET @body = @body + ' FROM ' + QUOTENAME(@schema_name) + '.' + QUOTENAME(@table_name);
    
    -- Execute dynamic SQL and capture results
    DECLARE @bodyTable TABLE(MD VARCHAR(MAX));
    INSERT INTO @bodyTable
    EXEC sp_executesql @body;
    
    DECLARE @body2 NVARCHAR(MAX);
    SELECT @body2 = COALESCE(@body2 + '', '') + MD + CHAR(13) + CHAR(10)
    FROM @bodyTable;
    
    SET @MD = @MD + @body2;
    
    -- Adding timestamp and user info
    DECLARE @Timestamp VARCHAR(100) = (SELECT CONVERT(VARCHAR, GETDATE(), 120));
    DECLARE @UserName VARCHAR(100) = (SELECT SUSER_SNAME());
    DECLARE @FootNote VARCHAR(200) = CHAR(13) + CHAR(10) + 
        '_Created on: ' + @Timestamp + ' by user: ' + @UserName + '_' + CHAR(13) + CHAR(10);
    
    SET @MD = @MD + @FootNote;
    
    SELECT @MD AS MarkdownOutput;
    
    DROP TABLE #temp;
END;

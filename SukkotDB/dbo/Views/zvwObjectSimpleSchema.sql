

CREATE VIEW dbo.zvwObjectSimpleSchema
AS
/*
  SELECT * FROM zvwObjectSimple ORDER BY name
	SELECT * FROM zvwObjectSimple WHERE schema_name = 'Sukkot' ORDER BY name
*/
	SELECT o.name, type_desc, s.name AS schema_name 
	--, CONVERT(nvarchar(30), create_date, 101) AS CreateMDY
	--, CONVERT(nvarchar(30), modify_date, 101) AS ModifyMDY
	FROM sys.objects o
		INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
	WHERE o.name NOT LIKE 'sys%' 
		AND o.name NOT LIKE 'sqlagent_%'
		AND o.name <> 'database_firewall_rules'
		AND [type_desc] <> 'INTERNAL_TABLE'
		AND [type_desc] <> 'SERVICE_QUEUE'
	-- ORDER BY name 
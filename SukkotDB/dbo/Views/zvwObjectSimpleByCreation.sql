



CREATE VIEW [dbo].[zvwObjectSimpleByCreation]
AS
/*
  SELECT * FROM zvwObjectSimpleByCreation 
	SELECT * FROM zvwObjectSimpleByCreation WHERE schema_name = 'Sukkot' ORDER BY name
*/

	SELECT TOP 100 o.name, type_desc, s.name AS schema_name 
	, CONVERT(nvarchar(30), create_date, 101) AS CreateMDY
	, CONVERT(nvarchar(30), modify_date, 101) AS ModifyMDY
	, CASE
	    WHEN create_date <> modify_date		
				THEN
				 CONVERT(VARCHAR, DATEDIFF(dd, create_date, modify_date)) + ' Days '
							+ CONVERT(VARCHAR, DATEDIFF(hh, create_date, modify_date) % 24) + ' Hours '
							+ CONVERT(VARCHAR, DATEDIFF(mi, create_date, modify_date) % 60) + ' Minutes '
				ELSE ''
		END AS CreateModDif
	, CASE
	    WHEN create_date = modify_date		
				THEN
					 CASE 
							WHEN [type_desc] = 'VIEW' THEN 'GRANT SELECT ON ' + s.name + '.' + o.name + ' TO [USER NAME]' 
							WHEN [type_desc] = 'SQL_STORED_PROCEDURE' OR [type_desc] = 'SQL_SCALAR_FUNCTION' THEN 'GRANT EXECUTE ON ' + s.name + '.' + o.name + ' TO [USER NAME]' 
							--WHEN [type_desc] = 'SQL_INLINE_TABLE_VALUED_FUNCTION' THEN 'GRANT SELECT ON ' + s.name + '.' + o.name + ' TO [USER NAME]' 
							--WHEN [type_desc] = 'USER_TABLE' THEN 'GRANT SELECT ON ' + s.name + '.' + o.name + ' TO [USER NAME]' 
							ELSE ''
						END
				ELSE 'ALTER NEEDED'
		END AS CodeGen

	FROM sys.objects o
		INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
	WHERE o.name NOT LIKE 'sys%' 
		AND o.name NOT LIKE 'sqlagent_%'
		AND o.name <> 'database_firewall_rules'
		AND [type_desc] <> 'INTERNAL_TABLE'
		AND [type_desc] <> 'SERVICE_QUEUE'
	ORDER BY modify_date desc
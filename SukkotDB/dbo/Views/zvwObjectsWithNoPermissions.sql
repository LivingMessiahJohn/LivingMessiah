
CREATE VIEW dbo.zvwObjectsWithNoPermissions
AS
/*
SELECT * FROM  dbo.zvwObjectsWithNoPermissions ORDER BY Name

GRANT SELECT on dbo.zvwObjectsWithNoPermissions TO [UserName]
*/


SELECT o.*, p.username,
CASE 
	WHEN o.type_desc = 'SQL_STORED_PROCEDURE' OR	o.type_desc = 'SQL_SCALAR_FUNCTION'
		THEN 'GRANT EXECUTE ON ' + o.schema_name + '.' + o.name + ' TO USER_NAME'
		ELSE 'GRANT SELECT ON ' + o.schema_name + '.' + o.name + ' TO USER_NAME'
END AS [Grant]
FROM zvwObjectSimpleByCreation o
LEFT OUTER JOIN zvwPermisions p ON 
  o.schema_name = [p].[Schema] AND o.name = p.Object
WHERE 
	 (
		o.type_desc = 'SQL_STORED_PROCEDURE' OR	o.type_desc = 'SQL_SCALAR_FUNCTION' OR
		o.type_desc = 'VIEW' OR o.type_desc = 'SQL_INLINE_TABLE_VALUED_FUNCTION' OR 
		o.type_desc = 'USER_TABLE'
	 )
AND p.username IS NULL
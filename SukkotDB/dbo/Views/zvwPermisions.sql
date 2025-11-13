

CREATE VIEW [dbo].[zvwPermisions]
AS
/*

SELECT * FROM zvwPermisions ORDER BY 1, 2, 3, 5

*/
SELECT sys.schemas.name 'Schema'
, sys.objects.name Object
, sys.database_principals.name username
, sys.database_permissions.type permissions_type
, sys.database_permissions.permission_name
, sys.database_permissions.state permission_state
, sys.database_permissions.state_desc
, state_desc + ' ' + permission_name + ' on ['+ sys.schemas.name + '].[' + sys.objects.name + '] to [' + sys.database_principals.name + ']' COLLATE LATIN1_General_CI_AS AS SQL
FROM sys.database_permissions 
	JOIN sys.objects on sys.database_permissions.major_id = sys.objects.object_id 
	JOIN sys.schemas on sys.objects.schema_id = sys.schemas.schema_id 
	JOIN sys.database_principals on sys.database_permissions.grantee_principal_id = sys.database_principals.principal_id 

/*

SELECT DISTINCT pr.principal_id, pr.name, pr.type_desc, 
    pr.authentication_type_desc, pe.state_desc, pe.permission_name
FROM sys.database_principals AS pr
JOIN sys.database_permissions AS pe
    ON pe.grantee_principal_id = pr.principal_id;
*/
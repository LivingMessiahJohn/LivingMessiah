--SELECT top 3 * FROM zvwPermisions ORDER BY 1, 2, 3, 5

CREATE   VIEW dbo.zvwPermissionsAndPrincipals
AS 
/*
SELECT principal_id, name, type_desc, authentication_type_desc, state_desc, permission_name FROM dbo.zvwPermissionsAndPrincipals
*/
SELECT DISTINCT pr.principal_id, pr.name, pr.type_desc, 
    pr.authentication_type_desc, pe.state_desc, pe.permission_name
FROM sys.database_principals AS pr
JOIN sys.database_permissions AS pe
    ON pe.grantee_principal_id = pr.principal_id;



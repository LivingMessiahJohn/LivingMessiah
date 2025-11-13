
CREATE VIEW dbo.zvwRoleNamesWithMembers
AS
/*
  SELECT * FROM zvwRoleNamesWithMembers 
*/

SELECT
 'Sukkot' AS DB
, r.name AS role_name
, m.name AS member_name 
FROM sys.database_role_members rm 
INNER JOIN sys.database_principals r ON rm.role_principal_id = r.principal_id
INNER JOIN sys.database_principals m ON rm.member_principal_id = m.principal_id
--where r.name = 'db_owner' and m.name != 'dbo' -- you may want to uncomment this line
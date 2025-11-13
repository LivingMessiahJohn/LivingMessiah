CREATE VIEW dbo.zvwRenameDefaultConstraintsCodeGen
AS

/*
	SELECT CodeGen FROM dbo.zvwRenameDefaultConstraintsCodeGen

  - [Source](https://www.techbrothersit.com/2016/05/how-to-rename-all-default-constraints.html)

	GRANT SELECT on dbo.zvwRenameDefaultConstraintsCodeGen TO [UserName]
*/

 SELECT 'exec sp_rename '''
    +Schema_name(d.Schema_id)+'.' 
    + '' + d.Name + ''''
    + ',''DF_' +Schema_Name(d.schema_id)
    +'_'+t.name
    +'_'+c.name+'''' as CodeGen
FROM sys.default_constraints d
INNER JOIN sys.columns c ON
    d.parent_object_id = c.object_id
    AND d.parent_column_id = c.column_id
INNER JOIN sys.tables t ON
    t.object_id = c.object_id




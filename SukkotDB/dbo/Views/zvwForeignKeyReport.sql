

CREATE VIEW dbo.zvwForeignKeyReport
AS

/*
	SELECT * FROM zvwForeignKeyReport WHERE [Parent Table] = 'ParashaArticleXref'
	SELECT * FROM zvwForeignKeyReport WHERE [Referenced Table] = 'Scripture'
*/

	SELECT 
			fk.name 'FK Name',
			tpar.name 'Parent Table',
			colpar.name 'Parent Column',
			tref.name 'Referenced Table',
			colref.name 'Referenced Column',
			fk.delete_referential_action_desc 'Delete Action',
			fk.update_referential_action_desc 'Update Action'
	FROM
			sys.foreign_keys fk
	INNER JOIN 
			sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
	INNER JOIN 
			sys.tables tpar ON fk.parent_object_id = tpar.object_id
	INNER JOIN 
			sys.columns colpar ON fkc.parent_object_id = colpar.object_id AND fkc.parent_column_id = colpar.column_id
	INNER JOIN 
			sys.tables tref ON fk.referenced_object_id = tref.object_id
	INNER JOIN 
			sys.columns colref ON fkc.referenced_object_id = colref.object_id AND fkc.referenced_column_id = colref.column_id
CREATE FUNCTION Sukkot.udfFormatName (
	@intStyle int 
, @FamilyName varchar(75)
, @FirstName varchar(50)
, @SpouseName VARCHAR(50) -- nullable
, @OtherNames VARCHAR(255)  -- nullable
)
RETURNS nvarchar(255)
/*
	SELECT 
		Sukkot.udfFormatName(1, 'Doe', 'John', NULL, NULL) AS Style1
	,	Sukkot.udfFormatName(2, 'Doe', 'John', 'Jane', NULL) AS Style2
	,	Sukkot.udfFormatName(3, 'Doe', 'John', 'Jane', 'Rudolph, Blitzer') AS Style3

	SELECT 
		Sukkot.udfFormatName(1, 'Doe', 'John', NULL, NULL) AS Style1
	,	Sukkot.udfFormatName(2, 'Doe', 'John', NULL, NULL) AS Style2
	,	Sukkot.udfFormatName(3, 'Doe', 'John', NULL, NULL) AS Style3

	SELECT 
		Sukkot.udfFormatName(1, 'Doe', 'John', NULL, NULL) AS Style1
	,	Sukkot.udfFormatName(2, 'Doe', 'John', ' ', ' ' ) AS Style2
	,	Sukkot.udfFormatName(3, 'Doe', 'John', ' ', ' ') AS Style3

	GRANT SELECT ON Sukkot.udfFormatName  TO XXXXXX
*/
AS
BEGIN
	IF @intStyle = 1 RETURN @FirstName + ' ' +  @FamilyName  
	
	DECLARE @return nvarchar(255) = ''

	DECLARE @and nvarchar(255)
	SET @and = 
		CASE
			WHEN TRIM(ISNULL(@SpouseName,'')) <> '' 
				THEN ' and ' + TRIM(@SpouseName) + ' '
				ELSE ' '
		END

	DECLARE @with nvarchar(255)
	SET @with = 
		CASE
			WHEN TRIM(ISNULL(@OtherNames,'')) <> '' 
				THEN ' with ' + TRIM(@OtherNames)
				ELSE ''
		END

	RETURN @FirstName + @and +  @FamilyName + @with

END
CREATE FUNCTION [dbo].[udfOrdinalPlace] (@intNumber int, @useHtml bit = 0)
RETURNS nvarchar(20)
/*
  SELECT 
		dbo.udfOrdinalPlace(DAY('2018-09-20'), 0) AS Ord20th,
		dbo.udfOrdinalPlace(DAY('2018-09-21'), 0) AS Ord21st,
		dbo.udfOrdinalPlace(DAY('2018-09-22'), 0) AS Ord22nd,
		dbo.udfOrdinalPlace(DAY('2018-09-23'), 0) AS Ord23rd

*/
AS
BEGIN
	DECLARE @strNumber nvarchar(20) = CAST(@intNumber AS VARCHAR(10))
	DECLARE @suffix NVARCHAR(2)

  SET @suffix =
		CASE 
			WHEN @intNumber IN ( 11, 12, 13 )	THEN 'th'
			WHEN @intNumber % 10 = 1					THEN 'st'
			WHEN @intNumber % 10 = 2					THEN 'nd'
			WHEN @intNumber % 10 = 3					THEN 'rd'
																				ELSE 'th'
		END

	DECLARE @return nvarchar(20)

	IF @useHtml = 1 
		SET @return = @strNumber + '<sup>' + @suffix + '</sup>'
	ELSE
		SET @return = @strNumber + @suffix

	RETURN @return
END
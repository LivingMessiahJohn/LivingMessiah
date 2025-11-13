CREATE   FUNCTION dbo.udfDateDiff_D_H_S_M (@BegDate DATETIME, @EndDate DATETIME)
RETURNS  VARCHAR(200)
AS 
/*
  SELECT dbo.udfDateDiff_D_H_S_M('2023-7-7 10:00:00', '2023-08-15 15:30:45')
*/
BEGIN
	DECLARE @s NVARCHAR(200) = ''

	-- Calculate the time difference in seconds
	DECLARE @TimeDiffInSeconds INT = DATEDIFF(SECOND, @BegDate, @EndDate);

	-- Calculate days, hours, minutes, and seconds
	DECLARE @Days INT = @TimeDiffInSeconds / 86400;
	DECLARE @Hours INT = (@TimeDiffInSeconds % 86400) / 3600;
	DECLARE @Minutes INT = ((@TimeDiffInSeconds % 86400) % 3600) / 60;
	DECLARE @Seconds INT = ((@TimeDiffInSeconds % 86400) % 3600) % 60;

	-- Format the result
	--SET @s = CONCAT(@Days, ':', @Hours, ':', @Minutes, ':', @Seconds);
	--SET @s = CONCAT(@Days, 'd_', @Hours, 'h_', @Minutes, 'm_', @Seconds, 's');
	--SET @s = CONCAT(@Days, '<sub>d_</sub>', @Hours, '<sub>h_</sub>', @Minutes, '<sub>m_</sub>', @Seconds, '<sub>s</sub>');
	SET @s = CONCAT(@Days, '<sub><b>d</b>&nbsp;</sub>', @Hours, '<sub><b>h</b>&nbsp;</sub>', @Minutes, '<sub><b>m</b>&nbsp;</sub>', @Seconds, '<sub><b>s</b></sub>');

	RETURN @s
END

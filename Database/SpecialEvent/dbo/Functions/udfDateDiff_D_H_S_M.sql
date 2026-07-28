CREATE   FUNCTION [dbo].[udfDateDiff_D_H_S_M]
(
    @BegDate DATETIME,
    @EndDate DATETIME
)
RETURNS VARCHAR(200)
AS
/*
  SELECT dbo.udfDateDiff_D_H_S_M('2023-7-7 10:00:00', '2023-08-15 15:30:45')
*/
BEGIN
    DECLARE @s NVARCHAR(200) = N'';

    -- Calculate the time difference in seconds
    DECLARE @TimeDiffInSeconds INT = DATEDIFF(SECOND, @BegDate, @EndDate);

    -- Calculate days, hours, minutes, and seconds
    DECLARE @Days INT = @TimeDiffInSeconds / 86400;
    DECLARE @Hours INT = (@TimeDiffInSeconds % 86400) / 3600;
    DECLARE @Minutes INT = ((@TimeDiffInSeconds % 86400) % 3600) / 60;
    DECLARE @Seconds INT = ((@TimeDiffInSeconds % 86400) % 3600) % 60;

    SET @s = CONCAT(
        @Days,   N'<sub><b>d</b>&nbsp;</sub>',
        @Hours,  N'<sub><b>h</b>&nbsp;</sub>',
        @Minutes,N'<sub><b>m</b>&nbsp;</sub>',
        @Seconds,N'<sub><b>s</b></sub>');

    RETURN @s;
END

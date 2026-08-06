
CREATE   FUNCTION dbo.udfGetRegistrationFeeEnumValue (@Adults INT)
RETURNS INT
AS
BEGIN
    RETURN CASE WHEN @Adults > 1 THEN 2 ELSE 1 END
END

GO


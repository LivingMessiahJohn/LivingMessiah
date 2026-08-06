
CREATE   FUNCTION dbo.udfGetRegistrationFeeAmount (@FeeEnumValue INT)
RETURNS MONEY
/*

SELECT 
	dbo.udfGetRegistrationFeeAmount(1) AS SingleFeeAmount
,	dbo.udfGetRegistrationFeeAmount(2) AS FamilyFeeAmount
,	dbo.udfGetRegistrationFeeEnumValue(1) AS OneAdultEnumValue
,	dbo.udfGetRegistrationFeeEnumValue(3) AS ThreeAdultsEnumValue


This logic must match the logic in `RegistrationFee.cs`
- LivingMessiah\Features\Sukkot\Enums\RegistrationFee.cs

GRANT SELECT ON dbo.udfGetRegistrationFeeAmount     TO XXXXXX
GRANT SELECT ON dbo.udfGetRegistrationFeeEnumValue  TO XXXXXX

*/

AS
BEGIN
  --RETURN CASE WHEN @Adults > 1 THEN 100.00 ELSE 50.00 END
  RETURN CASE WHEN @FeeEnumValue = 1 THEN  50.00  ELSE 100.00 END
END

GO


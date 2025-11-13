
CREATE   FUNCTION Sukkot.udfGetRegistrationFeeAmount (@FeeEnumValue INT)
RETURNS MONEY
/*

SELECT 
	Sukkot.udfGetRegistrationFeeAmount(1) AS SingleFeeAmount
,	Sukkot.udfGetRegistrationFeeAmount(2) AS FamilyFeeAmount
,	Sukkot.udfGetRegistrationFeeEnumValue(1) AS OneAdultEnumValue
,	Sukkot.udfGetRegistrationFeeEnumValue(3) AS ThreeAdultsEnumValue


This logic must match the logic in `RegistrationFee.cs`
- LivingMessiah\Features\Sukkot\Enums\RegistrationFee.cs

GRANT SELECT ON Sukkot.udfGetRegistrationFeeAmount     TO XXXXXX
GRANT SELECT ON Sukkot.udfGetRegistrationFeeEnumValue  TO XXXXXX

*/

AS
BEGIN
  --RETURN CASE WHEN @Adults > 1 THEN 100.00 ELSE 50.00 END
  RETURN CASE WHEN @FeeEnumValue = 1 THEN  50.00  ELSE 100.00 END
END

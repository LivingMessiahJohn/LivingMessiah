
CREATE     VIEW Sukkot.vwRegistration
AS
/*

SELECT * FROM Sukkot.vwRegistration ORDER BY FirstName
SELECT * FROM Sukkot.vwRegistration ORDER BY FamilyName

SELECT TOP 500 Id, HouseRulesAgreementId, FamilyName, EMail, Phone, Adults, ChildBig, ChildSmall, FeeEnumValue
--, StatusId, Status
, StepId, Step
, Notes, HouseRulesAgreementDate
FROM Sukkot.vwRegistration
ORDER BY ID

*/

SELECT r.Id, HouseRulesAgreementId
	, r.FamilyName, FirstName, SpouseName, OtherNames
	,	Sukkot.udfFormatName(1, FamilyName, FirstName, NULL, NULL) AS Name
	,	Sukkot.udfFormatName(2, FamilyName, FirstName, SpouseName, NULL) AS NameAndSpouse
	,	Sukkot.udfFormatName(3, FamilyName, FirstName, SpouseName, OtherNames) AS NameAndSpouseWithOther
, r.EMail, Phone, Adults, ChildBig, ChildSmall, FeeEnumValue
--, StatusId, s.Descr AS Status
, StatusId AS StepId, s.Descr AS Step

, Sukkot.udfGetRegistrationFeeAmount(FeeEnumValue)AS  RegistrationFeeAdjusted
/*
, CASE WHEN r.Adults = 1
    THEN (SELECT RegistrationFeeSingle FROM Sukkot.vwConstants)
		ELSE (SELECT RegistrationFeeAdjusted FROM Sukkot.vwConstants)
  END AS  RegistrationFeeAdjusted
*/

, AttendanceBitwise, Sukkot.udfSukkotAttendanceDaysCount(AttendanceBitwise) AS AttendanceTotal
, Notes, AdminNotes, DidNotAttend, Avatar
, FORMAT(hra.AcceptedDate, N'MM/dd hh:mm') + ' ' + hra.TimeZone AS  HouseRulesAgreementDate
FROM Sukkot.Registration r
	INNER JOIN Sukkot.Status s ON r.StatusId = s.Id
	INNER JOIN Sukkot.HouseRulesAgreement hra ON r.HouseRulesAgreementId = hra.Id

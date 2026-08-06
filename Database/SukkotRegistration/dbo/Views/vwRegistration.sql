
CREATE     VIEW dbo.vwRegistration
AS
/*

SELECT * FROM dbo.vwRegistration ORDER BY FirstName
SELECT * FROM dbo.vwRegistration ORDER BY FamilyName

SELECT TOP 500 Id, HouseRulesAgreementId, FamilyName, EMail, Phone, Adults, ChildBig, ChildSmall, FeeEnumValue
--, StatusId, Status
, StepId, Step
, Notes, HouseRulesAgreementDate
FROM dbo.vwRegistration
ORDER BY ID

*/

SELECT r.Id, HouseRulesAgreementId
	, r.FamilyName, FirstName, SpouseName, OtherNames
	,	dbo.udfFormatName(1, FamilyName, FirstName, NULL, NULL) AS Name
	,	dbo.udfFormatName(2, FamilyName, FirstName, SpouseName, NULL) AS NameAndSpouse
	,	dbo.udfFormatName(3, FamilyName, FirstName, SpouseName, OtherNames) AS NameAndSpouseWithOther
, r.EMail, Phone, Adults, ChildBig, ChildSmall, FeeEnumValue
--, StatusId, s.Descr AS Status
, StatusId AS StepId, s.Descr AS Step

, dbo.udfGetRegistrationFeeAmount(FeeEnumValue)AS  RegistrationFeeAdjusted
/*
, CASE WHEN r.Adults = 1
    THEN (SELECT RegistrationFeeSingle FROM dbo.vwConstants)
		ELSE (SELECT RegistrationFeeAdjusted FROM dbo.vwConstants)
  END AS  RegistrationFeeAdjusted
*/

, AttendanceBitwise, dbo.udfSukkotAttendanceDaysCount(AttendanceBitwise) AS AttendanceTotal
, Notes, AdminNotes, DidNotAttend, Avatar
, FORMAT(hra.AcceptedDate, N'MM/dd hh:mm') + ' ' + hra.TimeZone AS  HouseRulesAgreementDate
FROM dbo.Registration r
	INNER JOIN dbo.Status s ON r.StatusId = s.Id
	INNER JOIN dbo.HouseRulesAgreement hra ON r.HouseRulesAgreementId = hra.Id

GO


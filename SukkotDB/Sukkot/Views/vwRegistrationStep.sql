
CREATE    VIEW Sukkot.vwRegistrationStep
/*
	SELECT * FROM Sukkot.vwRegistrationStep WHERE EMail = 'test@yahoo.com'
	SELECT Id, FamilyName, EMail, Adults, FeeEnumValue FROM Sukkot.vwRegistrationStep WHERE EMail='test@yahoo.com'

SELECT
	Id, EMail, TimeZone, AcceptedDate, AcceptedDate2
, RegistrationId, HouseRulesAgreementId, FirstName, FamilyName, RegistrationEMail
, StepId, Step
, Adults, FeeEnumValue
FROM Sukkot.vwRegistrationStep

*/
AS

	SELECT hra.Id, hra.EMail, hra.TimeZone, hra.AcceptedDate, FORMAT(hra.AcceptedDate, N'MM/dd hh:mm') AS AcceptedDate2
	, r.Id AS RegistrationId,	r.HouseRulesAgreementId, r.FirstName, r.FamilyName, r.EMail AS RegistrationEMail
	, r.StepId, r.Step, r.Adults, r.FeeEnumValue
	FROM  Sukkot.HouseRulesAgreement hra
		LEFT OUTER JOIN Sukkot.vwRegistration r 
			ON hra.Id = r.HouseRulesAgreementId
	LEFT OUTER JOIN Sukkot.vwDonationSummary d ON r.Id = d.RegistrationId




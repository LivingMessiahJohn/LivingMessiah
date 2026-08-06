CREATE   VIEW dbo.vwDonationsByRegistration
AS
/*
	SELECT * FROM dbo.vwDonationsByRegistration ORDER BY FamilyName, Detail
	SELECT * FROM dbo.vwDonationsByRegistration ORDER BY Id, Detail

	SELECT 
	Id, FamilyName, FirstName, StatusId, TotalDonation, Detail
	, ISNULL(Amount, 0) AS Amount, NOTES, ReferenceId, CreatedBy, CreateDateMDY
	FROM dbo.vwDonationsByRegistration 
	ORDER BY Id, Detail
*/

SELECT r.Id, d.Detail, r.FamilyName, r.FirstName, StatusId, TotalDonation
, ISNULL(d.Notes,'') AS NOTES , d.ReferenceId, d.CreatedBy
, CONVERT(nvarchar(30), d.CreateDate, 101) AS CreateDateMDY
FROM dbo.vwDonationReport r 
LEFT OUTER JOIN dbo.Donation d ON d.RegistrationId = r.Id

--GRANT SELECT ON dbo.vwDonationsByRegistration TO [insert-user]

GO


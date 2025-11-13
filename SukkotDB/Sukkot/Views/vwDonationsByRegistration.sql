CREATE   VIEW Sukkot.vwDonationsByRegistration
AS
/*
	SELECT * FROM Sukkot.vwDonationsByRegistration ORDER BY FamilyName, Detail
	SELECT * FROM Sukkot.vwDonationsByRegistration ORDER BY Id, Detail

	SELECT 
	Id, FamilyName, FirstName, StatusId, TotalDonation, Detail
	, ISNULL(Amount, 0) AS Amount, NOTES, ReferenceId, CreatedBy, CreateDateMDY
	FROM Sukkot.vwDonationsByRegistration 
	ORDER BY Id, Detail
*/

SELECT r.Id, d.Detail, r.FamilyName, r.FirstName, StatusId, TotalDonation
, ISNULL(d.Notes,'') AS NOTES , d.ReferenceId, d.CreatedBy
, CONVERT(nvarchar(30), d.CreateDate, 101) AS CreateDateMDY
FROM Sukkot.vwDonationReport r 
LEFT OUTER JOIN Sukkot.Donation d ON d.RegistrationId = r.Id

--GRANT SELECT ON Sukkot.vwDonationsByRegistration TO [insert-user]

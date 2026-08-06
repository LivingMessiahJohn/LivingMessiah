CREATE     VIEW [dbo].[vwDonationDetail]
AS
/*

 SELECT Id, RegistrationId, Detail, Amount, Notes, Email, ReferenceId, CreateDate, CreatedBy, FamilyName, CreateDateMDY
 FROM dbo.vwDonationDetail 
 WHERE RegistrationId=20
 ORDER BY Detail

*/
SELECT d.Id, d.RegistrationId, d.Detail, d.Amount, d.Notes, d.Email, d.ReferenceId, d.CreateDate, d.CreatedBy, r.FamilyName
, CONVERT(nvarchar(30), d.CreateDate, 101) AS CreateDateMDY
FROM dbo.Donation d
INNER JOIN dbo.Registration r ON d.RegistrationId = r.Id

--GRANT SELECT ON dbo.vwDonationDetail TO [insert-user]

GO


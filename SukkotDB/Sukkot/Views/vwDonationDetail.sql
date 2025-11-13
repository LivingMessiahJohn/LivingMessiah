CREATE     VIEW [Sukkot].[vwDonationDetail]
AS
/*

 SELECT Id, RegistrationId, Detail, Amount, Notes, Email, ReferenceId, CreateDate, CreatedBy, FamilyName, CreateDateMDY
 FROM Sukkot.vwDonationDetail 
 WHERE RegistrationId=20
 ORDER BY Detail

*/
SELECT d.Id, d.RegistrationId, d.Detail, d.Amount, d.Notes, d.Email, d.ReferenceId, d.CreateDate, d.CreatedBy, r.FamilyName
, CONVERT(nvarchar(30), d.CreateDate, 101) AS CreateDateMDY
FROM Sukkot.Donation d
INNER JOIN Sukkot.Registration r ON d.RegistrationId = r.Id

--GRANT SELECT ON Sukkot.vwDonationDetail TO [insert-user]

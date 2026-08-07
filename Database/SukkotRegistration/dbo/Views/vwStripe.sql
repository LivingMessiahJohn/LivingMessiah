
CREATE    VIEW dbo.vwStripe
AS
/*

SELECT Id, Email, RegistrationId, ModificationCount, LastModifiedDate, FirstName, FamilyName
FROM dbo.vwStripe
ORDER BY RegistrationId

GRANT SELECT ON dbo.vwStripe  TO [INSERT-USER-HERE]


INSERT INTO dbo.Stripe 
        (Email, RegistrationId, ModificationCount, LastModifiedDate)
VALUES  ('aeaij@yahoo.com',77,1, '2023-10-11 16:55:08')

*/

SELECT s.Id, s.Email, s.RegistrationId, s.ModificationCount, s.LastModifiedDate, r.FirstName, r.FamilyName
FROM dbo.Stripe s
LEFT OUTER JOIN 
	dbo.Registration r
		ON r.EMail = s.EMail

GO



CREATE    VIEW Sukkot.vwStripe
AS
/*

SELECT Id, Email, RegistrationId, ModificationCount, LastModifiedDate, FirstName, FamilyName
FROM Sukkot.vwStripe
ORDER BY RegistrationId

GRANT SELECT ON Sukkot.vwStripe  TO [INSERT-USER-HERE]


INSERT INTO Sukkot.Stripe 
        (Email, RegistrationId, ModificationCount, LastModifiedDate)
VALUES  ('aeaij@yahoo.com',77,1, '2023-10-11 16:55:08')

*/

SELECT s.Id, s.Email, s.RegistrationId, s.ModificationCount, s.LastModifiedDate, r.FirstName, r.FamilyName
FROM Sukkot.Stripe s
LEFT OUTER JOIN 
	Sukkot.Registration r
		ON r.EMail = s.EMail




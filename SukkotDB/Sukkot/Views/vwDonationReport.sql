CREATE   VIEW Sukkot.vwDonationReport
AS
/*
SELECT Id, EMail, FamilyName, FirstName, StatusId, StatusDescr
, RegistrationFeeAdjusted, TotalDonation
FROM Sukkot.vwDonationReport
ORDER BY FamilyName

SELECT * FROM Sukkot.vwDonationReport ORDER BY StatusId, FamilyName
WHERE 
	(r.StatusId = @StatusId OR (@StatusId IS NULL))

*/
SELECT r.Id, EMail, FamilyName, FirstName, StatusId, s.Descr AS StatusDescr

, Sukkot.udfGetRegistrationFeeAmount(r.FeeEnumValue)AS  RegistrationFeeAdjusted
/*
, CASE WHEN r.Adults = 1
    THEN (SELECT RegistrationFeeSingle FROM Sukkot.vwConstants)
		ELSE (SELECT RegistrationFeeAdjusted FROM Sukkot.vwConstants)
  END AS  RegistrationFeeAdjusted
*/


/*
### Note
> Based on the No-Partial-Payments business rule defined below, 
> for each RegistrationId, there should only be 0 or 1 rows in Sukkot.Donation.
> The logic still works, so i'm going keep it if this business rule is no longer valie
*/
, ISNULL(vwDonSum.TotalDonation, 0) AS TotalDonation

/*
### No-Partial-Payments Business Rule
If the business rule for RegistraionFee is that the user can only make one payment (therefore the full payment) 
then `AmountDue` doesn't many anything because that implies partial payments 
Therefore just use `TotalDonation` for `AmountDue`
, (SELECT RegistrationFeeAdjusted FROM Sukkot.vwConstants) - ISNULL(vwDonSum.TotalDonation, 0) AS AmountDue
*/

FROM Sukkot.Registration r
	INNER JOIN Sukkot.Status s ON r.StatusId = s.Id
	LEFT OUTER JOIN Sukkot.vwDonationSummary vwDonSum ON r.Id = vwDonSum.RegistrationId

--GRANT SELECT ON Sukkot.vwDonationReport TO [insert-user]

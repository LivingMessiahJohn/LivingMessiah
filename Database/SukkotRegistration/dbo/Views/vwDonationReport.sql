CREATE   VIEW dbo.vwDonationReport
AS
/*
SELECT Id, EMail, FamilyName, FirstName, StatusId, StatusDescr
, RegistrationFeeAdjusted, TotalDonation
FROM dbo.vwDonationReport
ORDER BY FamilyName

SELECT * FROM dbo.vwDonationReport ORDER BY StatusId, FamilyName
WHERE 
	(r.StatusId = @StatusId OR (@StatusId IS NULL))

*/
SELECT r.Id, EMail, FamilyName, FirstName, StatusId, s.Descr AS StatusDescr

, dbo.udfGetRegistrationFeeAmount(r.FeeEnumValue)AS  RegistrationFeeAdjusted
/*
, CASE WHEN r.Adults = 1
    THEN (SELECT RegistrationFeeSingle FROM dbo.vwConstants)
		ELSE (SELECT RegistrationFeeAdjusted FROM dbo.vwConstants)
  END AS  RegistrationFeeAdjusted
*/


/*
### Note
> Based on the No-Partial-Payments business rule defined below, 
> for each RegistrationId, there should only be 0 or 1 rows in dbo.Donation.
> The logic still works, so i'm going keep it if this business rule is no longer valie
*/
, ISNULL(vwDonSum.TotalDonation, 0) AS TotalDonation

/*
### No-Partial-Payments Business Rule
If the business rule for RegistraionFee is that the user can only make one payment (therefore the full payment) 
then `AmountDue` doesn't many anything because that implies partial payments 
Therefore just use `TotalDonation` for `AmountDue`
, (SELECT RegistrationFeeAdjusted FROM dbo.vwConstants) - ISNULL(vwDonSum.TotalDonation, 0) AS AmountDue
*/

FROM dbo.Registration r
	INNER JOIN dbo.Status s ON r.StatusId = s.Id
	LEFT OUTER JOIN dbo.vwDonationSummary vwDonSum ON r.Id = vwDonSum.RegistrationId

--GRANT SELECT ON dbo.vwDonationReport TO [insert-user]

GO


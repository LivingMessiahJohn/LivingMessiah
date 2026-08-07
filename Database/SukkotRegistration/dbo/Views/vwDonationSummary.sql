CREATE   VIEW dbo.vwDonationSummary 
AS
/*
 SELECT TotalDonation, DonationRowCount 
 FROM dbo.vwDonationSummary
 WHERE RegistrationId=11

SELECT TotalDonation, DonationRowCount FROM dbo.vwDonationSummary WHERE RegistrationId=11

*/
SELECT RegistrationId, SUM(Amount) AS TotalDonation, COUNT(Amount) AS DonationRowCount 
FROM dbo.Donation 
GROUP BY RegistrationId

GO


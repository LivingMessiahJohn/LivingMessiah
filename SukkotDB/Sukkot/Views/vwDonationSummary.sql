CREATE   VIEW Sukkot.vwDonationSummary 
AS
/*
 SELECT TotalDonation, DonationRowCount 
 FROM Sukkot.vwDonationSummary
 WHERE RegistrationId=11

SELECT TotalDonation, DonationRowCount FROM Sukkot.vwDonationSummary WHERE RegistrationId=11

*/
SELECT RegistrationId, SUM(Amount) AS TotalDonation, COUNT(Amount) AS DonationRowCount 
FROM Sukkot.Donation 
GROUP BY RegistrationId

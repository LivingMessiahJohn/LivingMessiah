CREATE    VIEW Sukkot.vwManageRegistration
AS
/*

SELECT Id, EMail, FullName, StatusId, Phone, Notes, AdminNotes, Adults, Children, DidNotAttend, IdHRA
, TotalDonation, DonationRowCount
FROM Sukkot.vwManageRegistration 
ORDER BY FullName

GRANT SELECT ON Sukkot.vwManageRegistration  TO [INSERT-USER-HERE]

*/

SELECT r.Id
, r.EMail
, Sukkot.udfFormatName(1, r.FamilyName, r.FirstName, r.SpouseName, NULL) AS FullName
, r.StatusId
, r.Phone
, r.Notes
--, LEFT(r.AdminNotes, 25) AS AdminNotes
, r.AdminNotes
, r.Adults
, r.ChildBig + r.ChildSmall AS Children
, DidNotAttend
, hra.Id AS IdHRA
, TotalDonation
, DonationRowCount
FROM Sukkot.Registration r
CROSS APPLY Sukkot.tvfRegistrationSummary(r.Id) 
LEFT OUTER JOIN 
	Sukkot.HouseRulesAgreement hra
		 ON r.EMail = hra.EMail

UNION ALL

SELECT 0 AS Id
, hra.EMail
, '' AS FullName 
, 2 StatusId
, '' AS Phone
, FORMAT(hra.AcceptedDate, N'MM/dd hh:mm') AS Notes
, '' AS AdminNotes
, 0 as Adults
, 0 AS Children
, 0 as DidNotAttend
, hra.Id AS IdHRA
, 0 AS TotalDonation
, 0 DonationRowCount
FROM Sukkot.HouseRulesAgreement hra
LEFT OUTER JOIN 
	Sukkot.Registration r
		ON r.EMail = hra.EMail
WHERE r.EMail is NULL

--ORDER BY FirstName

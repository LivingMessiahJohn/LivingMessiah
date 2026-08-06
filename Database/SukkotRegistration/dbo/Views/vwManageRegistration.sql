CREATE    VIEW dbo.vwManageRegistration
AS
/*

SELECT Id, EMail, FullName, StatusId, Phone, Notes, AdminNotes, Adults, Children, DidNotAttend, IdHRA
, TotalDonation, DonationRowCount
FROM dbo.vwManageRegistration 
ORDER BY FullName

GRANT SELECT ON dbo.vwManageRegistration  TO [INSERT-USER-HERE]

*/

SELECT r.Id
, r.EMail
, dbo.udfFormatName(1, r.FamilyName, r.FirstName, r.SpouseName, NULL) AS FullName
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
FROM dbo.Registration r
CROSS APPLY dbo.tvfRegistrationSummary(r.Id) 
LEFT OUTER JOIN 
	dbo.HouseRulesAgreement hra
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
FROM dbo.HouseRulesAgreement hra
LEFT OUTER JOIN 
	dbo.Registration r
		ON r.EMail = hra.EMail
WHERE r.EMail is NULL

--ORDER BY FirstName

GO


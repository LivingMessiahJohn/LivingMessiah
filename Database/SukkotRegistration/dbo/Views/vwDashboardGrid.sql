CREATE  VIEW dbo.vwDashboardGrid
AS
/*

SELECT Id, EMail, FullName, OtherNames, StepId, Phone, Notes, AdminNotes, Adults, Children, DidNotAttend, IdHRA
, TotalDonation, DonationRowCount, AttendanceBitwise
FROM dbo.vwDashboardGrid 
ORDER BY FullName

GRANT SELECT ON dbo.vwDashboardGrid  TO [INSERT-USER-HERE]

*/

SELECT r.Id
, r.EMail
, dbo.udfFormatName(1, r.FamilyName, r.FirstName, r.SpouseName, NULL) AS FullName
, r.OtherNames
, r.StatusId AS StepId
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
, r.AttendanceBitwise
FROM dbo.Registration r
CROSS APPLY dbo.tvfRegistrationSummary(r.Id) 
LEFT OUTER JOIN 
	dbo.HouseRulesAgreement hra
		 ON r.EMail = hra.EMail

UNION ALL

SELECT 0 AS Id
, hra.EMail
, '' AS FullName 
, '' AS OtherNames
, 2 AS StepId
, '' AS Phone
, FORMAT(hra.AcceptedDate, N'MM/dd hh:mm') AS Notes
, '' AS AdminNotes
, 0 as Adults
, 0 AS Children
, 0 as DidNotAttend
, hra.Id AS IdHRA
, 0 AS TotalDonation
, 0 DonationRowCount
, 0 AttendanceBitwise -- 0=None
FROM dbo.HouseRulesAgreement hra
LEFT OUTER JOIN 
	dbo.Registration r
		ON r.EMail = hra.EMail
WHERE r.EMail is NULL

--ORDER BY FirstName

GO


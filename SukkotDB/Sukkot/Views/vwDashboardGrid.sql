CREATE  VIEW Sukkot.vwDashboardGrid
AS
/*

SELECT Id, EMail, FullName, OtherNames, StepId, Phone, Notes, AdminNotes, Adults, Children, DidNotAttend, IdHRA
, TotalDonation, DonationRowCount, AttendanceBitwise
FROM Sukkot.vwDashboardGrid 
ORDER BY FullName

GRANT SELECT ON Sukkot.vwDashboardGrid  TO [INSERT-USER-HERE]

*/

SELECT r.Id
, r.EMail
, Sukkot.udfFormatName(1, r.FamilyName, r.FirstName, r.SpouseName, NULL) AS FullName
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
FROM Sukkot.Registration r
CROSS APPLY Sukkot.tvfRegistrationSummary(r.Id) 
LEFT OUTER JOIN 
	Sukkot.HouseRulesAgreement hra
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
FROM Sukkot.HouseRulesAgreement hra
LEFT OUTER JOIN 
	Sukkot.Registration r
		ON r.EMail = hra.EMail
WHERE r.EMail is NULL

--ORDER BY FirstName

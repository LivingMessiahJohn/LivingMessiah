CREATE   FUNCTION [dbo].[tvfRegistrationSummary] (@id int) 
RETURNS TABLE AS RETURN 

/*
  DECLARE @id int = 11
  SELECT * FROM dbo.tvfRegistrationSummary(@id)
	
	SELECT Id, EMail, FamilyName, Adults, ChildBig, ChildSmall, StatusId
	, AttendanceBitwise, TotalDonation,	RegistrationFeeAdjusted
	, TotalDonation, DonationRowCount
	FROM dbo.tvfRegistrationSummary(@id)


	GRANT SELECT ON dbo.tvfRegistrationSummary  TO [INSERT-USER-HERE]

*/

SELECT r.Id, EMail, FamilyName, Adults, ChildBig, ChildSmall, StatusId, AttendanceBitwise
, DATEDIFF(dd, const.EarlyRegistrationLastDay , GETDATE()) AS DaysPastEarlyReg
, DATEDIFF(dd, const.RegistrationLastDay , GETDATE()) AS DaysPastLastReg

--, const.RegistrationFeeAdjusted
, CASE WHEN r.Adults = 1
    THEN const.RegistrationFeeSingle
		ELSE const.RegistrationFee
  END AS  RegistrationFeeAdjusted
, ISNULL(d.TotalDonation, 0) AS TotalDonation
, ISNULL(d.DonationRowCount, 0) AS DonationRowCount
FROM dbo.Registration r
	LEFT OUTER JOIN dbo.vwDonationSummary d ON r.Id = d.RegistrationId
	CROSS JOIN dbo.vwConstants const
WHERE r.Id = @id

GO


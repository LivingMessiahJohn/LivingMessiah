CREATE   FUNCTION [Sukkot].[tvfRegistrationSummary] (@id int) 
RETURNS TABLE AS RETURN 

/*
  DECLARE @id int = 11
  SELECT * FROM Sukkot.tvfRegistrationSummary(@id)
	
	SELECT Id, EMail, FamilyName, Adults, ChildBig, ChildSmall, StatusId
	, AttendanceBitwise, TotalDonation,	RegistrationFeeAdjusted
	, TotalDonation, DonationRowCount
	FROM Sukkot.tvfRegistrationSummary(@id)


	GRANT SELECT ON Sukkot.tvfRegistrationSummary  TO [INSERT-USER-HERE]

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
FROM Sukkot.Registration r
	LEFT OUTER JOIN Sukkot.vwDonationSummary d ON r.Id = d.RegistrationId
	CROSS JOIN Sukkot.vwConstants const
WHERE r.Id = @id

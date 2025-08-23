using System;

namespace LivingMessiah.Features.Sukkot.Domain;

public class vwRegistrationStep
{
	public int Id { get; set; }
	public string? EMail { get; set; }
	public int? Adults { get; set; }
	public string? HouseRulesAgreementTimeZone { get; set; }
	public DateTimeOffset HouseRulesAgreementAcceptedDate { get; set; }
	public int? RegistrationId { get; set; }
	public int? StepId { get; set; }  
	public string FirstName { get; set; } = string.Empty;
	public string FamilyName { get; set; } = string.Empty;
	//public decimal TotalDonation { get; set; } = 0;
	//public decimal RegistrationFeeAdjusted { get; set; } = 0;
}

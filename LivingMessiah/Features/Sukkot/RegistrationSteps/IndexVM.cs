using LivingMessiah.Features.Sukkot.RegistrationSteps.Enums;

namespace LivingMessiah.Features.Sukkot.RegistrationSteps;

public class IndexVM
{
	public string? EmailAddress { get; set; } 
	//public string? UserName { get; set; }
	public Status? Status { get; set; } // = Status.EmailNotConfirmed;
	public HRARecord? HouseRulesAgreement { get; set; }
	public CurrentStepRecord? CurrentStepRecord { get; set; }
}

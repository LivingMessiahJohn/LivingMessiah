using StepEnums = RCL.Features.Sukkot.Enums.Step;

namespace LivingMessiahSukkot.Features.Steps;

public class IndexVM
{
	public string? EmailAddress { get; set; } 
	public int RegistrationId { get; set; }
	public int Adults { get; set; } // If null then set to zero
	public int FeeEnumValue { get; set; }	
	public StepEnums? Step { get; set; } 
	public HRARecord? HouseRulesAgreement { get; set; }

}

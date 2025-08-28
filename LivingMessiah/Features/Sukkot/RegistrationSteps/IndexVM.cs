using LivingMessiah.Features.Sukkot.Enums;

namespace LivingMessiah.Features.Sukkot.RegistrationSteps;

public class IndexVM
{
	public string? EmailAddress { get; set; } 
	public int Adults { get; set; } // If null then set to zero
	public int FeeEnumValue { get; set; }	
	public Step? Step { get; set; } 
	public HRARecord? HouseRulesAgreement { get; set; }
	public CurrentStepRecord? CurrentStepRecord { get; set; }

	/*
	ToDo Use RegistrationFeeHelper.AdultsToRegistrationFee(Adults)
	
using RegistrationFeeEnums = LivingMessiah.Features.Sukkot.Enums;

	public RegistrationFeeEnums.RegistrationFee? RegistrationFee
	{
		get
		{
			return Adults > 1
				? RegistrationFeeEnums.RegistrationFee.Family
				: RegistrationFeeEnums.RegistrationFee.Single;
		}
	}
	*/
}

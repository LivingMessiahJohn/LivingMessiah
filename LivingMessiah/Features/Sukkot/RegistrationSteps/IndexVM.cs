﻿using LivingMessiah.Features.Sukkot.RegistrationSteps.Enums;

namespace LivingMessiah.Features.Sukkot.RegistrationSteps;

public class IndexVM
{
	public string? EmailAddress { get; set; } 
	//public string? UserName { get; set; }
	public Step? Step { get; set; } 
	public HRARecord? HouseRulesAgreement { get; set; }
	public CurrentStepRecord? CurrentStepRecord { get; set; }
}

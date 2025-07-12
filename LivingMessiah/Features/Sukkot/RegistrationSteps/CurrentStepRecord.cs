namespace LivingMessiah.Features.Sukkot.RegistrationSteps;

public record CurrentStepRecord(int Id
, string? FirstName
, string? FamilyName = ""
, decimal TotalDonation = 0
, decimal RegistrationFeeAdjusted = 0);

public static class CurrentStepHelper
{
	public static decimal RemainingCost(decimal registrationFeeAdjusted, decimal totalDonation)
	{
		return registrationFeeAdjusted - totalDonation; // previously this had other costs that are no longer tracked
	}

}


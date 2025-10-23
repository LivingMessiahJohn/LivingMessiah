using LivingMessiah.Features.Sukkot.Constants;

namespace LivingMessiah.Features.Sukkot.LandingPage.Constants;

//ToDo: this only used by RegistrationFeeTableNOTUSING which is not used
public static class RegistrationMeta
{
	public static bool IsThereEarlyRegistration { get; set; } = false;
	public static System.DateTime EarlyRegistrationLastDay = new System.DateTime(Year.Int, 9, 16);
	public const decimal EarlyRegistrationFee = 75.0m;
	public static System.DateTime RegistrationLastDay = new System.DateTime(Year.Int, 10, 1);
	public const decimal RegistrationFee = 100.0m;

}

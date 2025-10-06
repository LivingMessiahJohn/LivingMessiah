namespace LivingMessiahAdmin.Features.Sukkot.Enums.Helpers;

public class RegistrationFeeHelper
{
	public static RegistrationFee AdultsToRegistrationFee(int adults)
	{
		return adults > 1 ? RegistrationFee.Family : RegistrationFee.Single;
	}
}
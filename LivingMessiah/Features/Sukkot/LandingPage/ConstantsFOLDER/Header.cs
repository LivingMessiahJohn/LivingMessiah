using SukkotConstants = LivingMessiah.Features.Sukkot.ConstantsFOLDER;

namespace LivingMessiah.Features.Sukkot.LandingPage.ConstantsFOLDER;

public static class Header
{
	public const string Title = $"Celebrating Sukkot {SukkotConstants.Year.String}";

	private const string events = "https://livingmessiahstorage.blob.core.windows.net/images/events/";
	public static string UrlEvents(string blob)
	{
		return events + blob;
	}
}

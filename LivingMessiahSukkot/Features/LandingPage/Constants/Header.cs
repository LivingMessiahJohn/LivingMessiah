using LivingMessiahSukkot.Features.Constants;

namespace LivingMessiahSukkot.Features.LandingPage.Constants;

public static class Header
{
	public const string Title = $"Celebrating Sukkot {Year.String}";

	private const string events = "https://livingmessiahstorage.blob.core.windows.net/images/events/";
	public static string UrlEvents(string blob)
	{
		return events + blob;
	}
}

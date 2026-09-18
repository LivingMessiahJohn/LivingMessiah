using Sukkot.Features.Constants;

namespace Sukkot.Features.LandingPage.Constants;

public static class Documents
{
	private const string SukkotContentBaseFolder = "https://livingmessiahstorage.blob.core.windows.net/sukkot-content/";

	private static string LiabilityWaiverPDF => SukkotContentBaseFolder + "liability-waiver.pdf";
	private static string HouseRulesPDF => SukkotContentBaseFolder + "sukkot-2026-house-rules.pdf";
	private static string PocketModPDF => SukkotContentBaseFolder + "scheduled-events-pocketmod.pdf";

	public static readonly Dictionary<string, string> Links = new()
	{
		["Liability Waiver"] = LiabilityWaiverPDF,
		[$"Sukkot {Year.String} House Rules"] = HouseRulesPDF,
		["PocketMod Scheduled Events"] = PocketModPDF
	};
}

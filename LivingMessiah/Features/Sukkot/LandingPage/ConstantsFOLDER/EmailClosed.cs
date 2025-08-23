using SukkotConstants = LivingMessiah.Features.Sukkot.ConstantsFOLDER;

namespace LivingMessiah.Features.Sukkot.LandingPage.ConstantsFOLDER;

public static class EmailClosed
{
	public static string Subject { get; set; } = $"Late Sukkot {SukkotConstants.Year.String} Registration Question";
	public const string Name = "Mark";
	public const string EmailTo = "mark@livingmessiah.com";
}

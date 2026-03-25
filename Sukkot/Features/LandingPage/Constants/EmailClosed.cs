using Sukkot.Features.Constants;

namespace Sukkot.Features.LandingPage.Constants;

public static class EmailClosed
{
	public static string Subject { get; set; } = $"Late Sukkot {Year.String} Registration Question";
	public const string Name = "Mark";
	public const string EmailTo = "mark@livingmessiah.com";
}

using Serilog;

namespace LivingMessiah.Helpers;

public static class LogHelper
{
	public static void DumpSectionConfiguration(IConfigurationSection stripeConfig, string section)
	{
		Log.Debug("{Class}, {Method} {Message}", nameof(LogHelper), nameof(DumpSectionConfiguration), $"{section} items...");
		foreach (var kvp in stripeConfig.GetChildren())
		{
			//Log.Debug("{Message}", $"...Key: {kvp.Key}; Value: {kvp.Value}");
			Log.Debug("...: {Key} / {Value}", kvp.Key, kvp.Value);
		}
	}
}
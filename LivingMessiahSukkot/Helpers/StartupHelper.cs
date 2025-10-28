using Serilog;

namespace LivingMessiahSukkot.Helpers;

public static class StartupHelper
{
	public static void DumpSectionConfiguration(IConfigurationSection config, string section)
	{
		Log.Debug("{Class}, {Method} {Message}", nameof(StartupHelper), nameof(DumpSectionConfiguration), $"{section} items...");
		foreach (var kvp in config.GetChildren())
		{
			//Log.Debug("{Message}", $"...Key: {kvp.Key}; Value: {kvp.Value}");
			Log.Debug("...: {Key} / {Value}", kvp.Key, kvp.Value);
		}
	}
}

namespace LivingMessiahSukkot.Features.Components.LifeCycleAuthority;

public class PhaseSetting
{
	public int Phase { get; set; }

	public static string GetPhaseValue(int phase) => Phases.TryGetValue(phase, out var name) ? name : string.Empty; // "Unknown";

	private static readonly Dictionary<int, string> Phases = new()
	{
		{ 1, "GetReady" },
		{ 2, "RegistrationOpen" },
		{ 3, "Over" }
	};

	public static int DefaultPhase => 3;
}


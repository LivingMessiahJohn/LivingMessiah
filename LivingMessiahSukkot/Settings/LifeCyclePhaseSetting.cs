namespace LivingMessiahSukkot.Settings;

public class LifeCyclePhaseSetting
{
	public int LifeCyclePhase { get; set; }
}

public enum LifeCyclePhaseEnum
{
	GetReady = 1,
	RegistrationOpen = 2,
	Over = 3
}

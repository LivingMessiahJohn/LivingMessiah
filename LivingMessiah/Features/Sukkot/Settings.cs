namespace LivingMessiah.Features.Sukkot;

public class Settings
{
	public int LifeCyclePhase { get; set; }
	public string? Domain { get; set; }
}

public enum LifeCyclePhaseEnum
{
	GetReady = 1,
	RegistrationOpen = 2,
	Over = 3
}

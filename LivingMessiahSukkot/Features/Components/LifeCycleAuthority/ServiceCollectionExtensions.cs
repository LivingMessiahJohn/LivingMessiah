using Serilog;

namespace LivingMessiahSukkot.Features.Components.LifeCycleAuthority;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddLifeCyclePhaseSetting(this IServiceCollection services, IConfiguration configuration)
	{
		if (configuration is null) throw new ArgumentNullException(nameof(configuration));

		int phaseValue;
		string phaseName;

		string err = string.Empty;
		const string sectionName = nameof(PhaseSetting);

		var section = configuration.GetSection(sectionName);
		if (!section.Exists())
		{
			err = $"Configuration section '{sectionName}' is missing or empty.";
			Log.Error(err);
			phaseValue = PhaseSetting.DefaultPhase;
		}
		else
		{
			var phaseSetting = section.Get<PhaseSetting>();
			if (phaseSetting is null)
			{
				err = $"Configuration section '{sectionName}' deserialized to null.";
				Log.Error(err);
				phaseValue = PhaseSetting.DefaultPhase;
			}
			else
			{
				phaseValue = phaseSetting.Phase;
				phaseName = PhaseSetting.GetPhaseValue(phaseValue);

				if (phaseName == string.Empty)
				{
					err = $"Invalid Phase value '{phaseValue}' in appsettings section '{sectionName}'.";
					Log.Error(err);
					phaseValue = PhaseSetting.DefaultPhase;
				}
				else
				{
					Log.Information("PhaseSetting.Phase = {phaseValue} ({PhaseName})", phaseValue, phaseName);
				}
			}
		}

		services.Configure<PhaseSetting>(options => section.Bind(options));
		return services;
	}
}
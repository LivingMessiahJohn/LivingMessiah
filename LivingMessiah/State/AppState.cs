using Blazored.LocalStorage;
//using LivingMessiah.Features.Sukkot;

namespace LivingMessiah.State;

public class AppState 
{
	public Features.Sukkot.State? SukkotState { get; }
	public Features.Liturgy.State? LiturgyState { get; }	

	#region Constructor and DI
	private readonly ILogger? Logger;
	private readonly ILocalStorageService?  localStorage; 

	public AppState(ILocalStorageService localStorage, ILogger<AppState> logger) 
	{
		Logger = logger;
		this.localStorage = localStorage;
		SukkotState = new Features.Sukkot.State(localStorage, logger);
		LiturgyState = new Features.Liturgy.State(localStorage, logger);	
		//Logger!.LogInformation("ctor of {Project}!{Class}", nameof(LivingMessiah), nameof(AppState));
	}
	#endregion

	private bool _isInitialized;

	public async Task Initialize()
	{
		//Logger!.LogInformation("{Method}, _isInitialized: {_isInitialized}", nameof(Initialize), _isInitialized);
		if (!_isInitialized)
		{
			try
			{
				await SukkotState!.Initialize();
				await LiturgyState!.Initialize();	
				//Logger!.LogWarning("{Method} ParashaState.Get: {ParashaState}", nameof(Initialize), ParashaState.Get());
				_isInitialized = true;
			}
			catch (Exception ex) 
			{
				Logger!.LogError(ex, "{Class}!{Method}", nameof(AppState), nameof(Initialize));
			}

		}
	}
}

// Ignore Spelling: App, Abrv
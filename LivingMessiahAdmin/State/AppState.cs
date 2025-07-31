using Blazored.LocalStorage;
//using LivingMessiahAdmin.Features.Sukkot;

namespace LivingMessiahAdmin.State;

public class AppState 
{
	//public Features.Sukkot.State? SukkotState { get; }

	#region Constructor and DI
	private readonly ILogger? Logger;
	private readonly ILocalStorageService?  localStorage; 

	public AppState(ILocalStorageService localStorage, ILogger<AppState> logger) 
	{
		Logger = logger;
		this.localStorage = localStorage;
		//SukkotState = new Features.Sukkot.State(localStorage, logger); // This was not the non Admin LMM web app
		//Logger!.LogInformation("ctor of {Project}!{Class}", nameof(LivingMessiahAdmin), nameof(AppState));
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
				//await SukkotState!.Initialize();
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
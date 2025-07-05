using Blazored.LocalStorage;
using LivingMessiah.State;

namespace LivingMessiah.Features.Home;

public class State
{
	//private readonly ILocalStorageService? localStorage;

	#region Constructor and DI
	private readonly ILogger Logger;
	private readonly ILocalStorageService? localStorage;
	public State(ILocalStorageService localStorage, ILogger<AppState> logger)  //BookChapterState
	{
		this.localStorage = localStorage;
		Logger = logger;
		//Logger!.LogInformation("{MethodEvent}", "CTOR");
	}
	#endregion

	private const string KeyIsShowingWelcomeDetails = "home-is-showing-welcome-details";

	private bool _isInitialized;
	private void NotifyStateHasChanged() => OnChange?.Invoke();
	public event Action? OnChange;

	private bool _IsShowingWelcomeDetails;

	public async Task Initialize()
	{
		if (!_isInitialized)
		{
			bool? _bool = await localStorage!.GetItemAsync<bool?>(KeyIsShowingWelcomeDetails);
			if (!_bool.HasValue)
			{
				await UpdateIsShowingWelcomeDetails(true); // default value
			}
			else
			{
				_IsShowingWelcomeDetails = _bool.Value;
			}

			_isInitialized = true;
		}
	}

	public async Task UpdateIsShowingWelcomeDetails(bool isShowingWelcomeDetails)
	{
		_IsShowingWelcomeDetails = isShowingWelcomeDetails;
		await localStorage!.SetItemAsync(KeyIsShowingWelcomeDetails, _IsShowingWelcomeDetails);
		NotifyStateHasChanged();
	}

	public bool GetIsShowingImage()
	{
		return _IsShowingWelcomeDetails;
	}

}

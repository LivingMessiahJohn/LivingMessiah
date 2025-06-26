using Blazored.LocalStorage;
using LivingMessiah.State;

namespace LivingMessiah.Features.Liturgy;

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

	private const string Key = "liturgy-content-id";

	private bool _isInitialized;
	private void NotifyStateHasChanged() => OnChange?.Invoke();
	public event Action? OnChange;

	private int _ContentId;

	public async Task Initialize()
	{
		if (!_isInitialized)
		{
				int? i = await localStorage!.GetItemAsync<int>(Key);
					if (i is null || i == 0)
					{
						await UpdateContent(Enums.Content.CallToService);  
					}
					else
					{
				_ContentId = i.Value;
					}
			
			_isInitialized = true;
		}
	}

	public async Task UpdateContent(int contentId)
	{
		_ContentId = contentId;
		await localStorage!.SetItemAsync(Key, _ContentId);
		NotifyStateHasChanged();
	}

	public int Get()
	{
		//Logger!.LogInformation("{Method}, permutation: {permutation}", nameof(Get), _permutation); // DEBUG 
		return _ContentId;
	}
}

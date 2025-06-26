using Blazored.LocalStorage;
using LivingMessiah.State;

namespace LivingMessiah.Features.Sukkot;

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

	private const string Key = "sukkot-zzz";

	private bool _isInitialized;
	private void NotifyStateHasChanged() => OnChange?.Invoke();
	public event Action? OnChange;

	private int _ZZZ;

	public async Task Initialize()
	{
		if (!_isInitialized)
		{
			/*
					int? i = await localStorage!.GetItemAsync<int>(Key);
					if (i is null || i == 0)
					{
						await UpdateZZZ(Enums.DisplayLanguage.English.Value);  // _DefaultLanguageId;
					}
					else
					{
						_LanguageId = i.Value;
					}
			*/
			_isInitialized = true;
		}
	}

	public async Task UpdateZZZ(int zzz)
	{
		_ZZZ = zzz;
		await localStorage!.SetItemAsync(Key, _ZZZ);
		NotifyStateHasChanged();
	}

	public int Get()
	{
		//Logger!.LogInformation("{Method}, permutation: {permutation}", nameof(Get), _permutation); // DEBUG 
		return _ZZZ;
	}
}

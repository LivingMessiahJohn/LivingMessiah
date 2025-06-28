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
	private const string KeyShowImage = "liturgy-show-image";

	private bool _isInitialized;
	private void NotifyStateHasChanged() => OnChange?.Invoke();
	public event Action? OnChange;

	private int _ContentId;
	private bool _IsShowingImage;

	public async Task Initialize()
	{
		if (!_isInitialized)
		{
			int i = await localStorage!.GetItemAsync<int>(Key);
			if (i == 0)
			{
				await UpdateContentId(Enums.Content.CallToService); // default value
			}
			else
			{
				_ContentId = i;
			}

			bool? _showImage = await localStorage!.GetItemAsync<bool?>(KeyShowImage);
			if (!_showImage.HasValue)
			{
				await UpdateIsShowingImage(false); // default value
			}
			else
			{
				_IsShowingImage = false;
			}

			_isInitialized = true;
		}
	}

	public async Task UpdateContentId(int contentId)
	{
		_ContentId = contentId;
		await localStorage!.SetItemAsync(Key, _ContentId);
		NotifyStateHasChanged();
	}

	public int GetContentId()
	{
		//Logger!.LogInformation("{Method}, permutation: {permutation}", nameof(Get), _permutation); // DEBUG 
		return _ContentId;
	}

	public async Task UpdateIsShowingImage(bool isShowingImage)
	{
		_IsShowingImage = isShowingImage;
		await localStorage!.SetItemAsync(KeyShowImage, _IsShowingImage);
		NotifyStateHasChanged();
	}

	public bool GetIsShowingImage()
	{
		return _IsShowingImage;
	}

}

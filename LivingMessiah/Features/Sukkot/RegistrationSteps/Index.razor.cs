using Microsoft.AspNetCore.Components;
using Link = LivingMessiah.Enums.Account;
using LivingMessiah.Features.Sukkot.Services;

namespace LivingMessiah.Features.Sukkot.RegistrationSteps;

public partial class Index : ComponentBase
{
	[Inject] public ILogger<Index>? Logger { get; set; }
	[Inject] public ISukkotService? Service { get; set; }
	[Inject] NavigationManager? NavigationManager { get; set; }
	//[Inject] AppState? AppState { get; set; }

	protected IndexVM? IndexVM { get; set; }

	protected bool AttemptingToGetRecord;
	protected override async Task OnInitializedAsync()
	{
		// += operator allows you to subscribe to an event
		//AppState!.StateChanged += async (Source, Property) => await AppState_StateChanged(Source, Property);
		await PopulateVM();
	}

	private async Task PopulateVM()
	{
		Logger!.LogDebug("{Method}", nameof(PopulateVM));
		AttemptingToGetRecord = true;
		try
		{
			IndexVM = await Service!.GetRegistrationStep();
			AttemptingToGetRecord = false;
			Logger!.LogDebug("{Method}, just called: {JustCalled}, Status: {Status}, EmailAddress: {EmailAddress}"
				, nameof(PopulateVM), nameof(AttemptingToGetRecord), IndexVM.Status, IndexVM.EmailAddress);
		}
		catch (InvalidOperationException invalidOperationException) //The variable 'invalidOperationException' is declared but never used
		{
			//AppState!.UpdateMessage(this, invalidOperationException.Message);
		}
	}
	/*
	private async Task AppState_StateChanged(ComponentBase Source, string Property)
	{
		if (Source != this)
		{
			await PopulateVM();
			await InvokeAsync(StateHasChanged);
		}
	}
	*/

	void IDisposable.Dispose()
	{
		// -= operator detaches you from an event
		//AppState!.StateChanged -= async (Source, Property) => await AppState_StateChanged(Source, Property);
	}

}

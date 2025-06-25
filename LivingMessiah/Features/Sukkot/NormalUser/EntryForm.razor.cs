using Microsoft.AspNetCore.Components;
using Blazored.FluentValidation;
using Blazored.Toast.Services;
using LivingMessiah.Features.Sukkot.Services;

//using LivingMessiah.Services;
//using static LivingMessiah.Features.Sukkot.Services.Service;

namespace LivingMessiah.Features.Sukkot.NormalUser;

public partial class EntryForm
{
	[Inject] public IService? Service { get; set; }
	[Inject] public ILogger<EntryForm>? Logger { get; set; }
	//[Inject] AppState? AppState { get; set; }
	[Inject] public IToastService? Toast { get; set; }

	[Parameter, EditorRequired] public int? Id { get; set; }
	[Parameter, EditorRequired] public string? Email { get; set; }
		
	public EntryFormVM VM { get; set; } = new EntryFormVM();

	private FluentValidationValidator? _fluentValidationValidator;
	public Enums.DateRangeType DateRangeAttendance { get; set; } = Enums.DateRangeType.Attendance;

	private int Id2;
	protected override async Task OnInitializedAsync()
	{
		Id2 = Id ?? 0;
		Logger!.LogInformation("{Method}, Id2: {Id2}", nameof(OnInitializedAsync), Id2);

		try
		{
			if (Id2 == 0)
			{
				VM.EMail = Email;
			}
			else
			{
				VM = await Service!.GetById(Id2);
				//AppState!.UpdateMessage(this, GetNotificationMessage());
				//Toast!.ShowInfo($"{GetNotificationMessage()}");
			}

			SetUiForAddOrEdit();

		}
		catch (RegistratationException registratationException)
		{
			//AppState!.UpdateMessage(this, registratationException.Message);
			Toast!.ShowError($"{registratationException.Message}");
		}

		catch (InvalidOperationException invalidOperationException)
		{
			//AppState!.UpdateMessage(this, invalidOperationException.Message);
			Toast!.ShowError($"{invalidOperationException.Message}");
		}
		Logger!.LogInformation("{Method} finished", nameof(OnInitializedAsync));
	}

	protected string Title = "";
	protected string Title2 = "";
	protected string SubmitButtonText = "";

	private void SetUiForAddOrEdit()
	{
		if (Id2 == 0)
		{
			Title = "Add - Registration";
			Title2 = $"{Email}";
			SubmitButtonText = "Save";
		}
		else
		{
			Title = "Edit - Registration";
			Title2 = $"{Email} - #{VM.Id}";
			SubmitButtonText = "Update";
		}
	}

	private string GetNotificationMessage()
	{
		if (Id2 != 0)
		{
			return $"Registration record for {Email} - #{VM.Id} received from database";
		}
		else
		{
			return String.Empty; // shouldn't happen
		}
	}

	protected async Task SubmitValidForm()
	{
		Logger!.LogDebug("{Method} Id2:{id}", nameof(SubmitValidForm), Id2);
		if (Id2 == 0)  // Add
		{
			try
			{
				var sprocTuple = await Service!.Create(VM);
				Logger!.LogInformation("{Method} Registration created! Id2:{id}", nameof(SubmitValidForm), Id2);
				//AppState!.UpdateMessage(this, "Registration Added!");
				Toast!.ShowInfo($"Registration Added!");
			}
			catch (InvalidOperationException invalidOperationException)
			{
				//AppState!.UpdateMessage(this, invalidOperationException.Message);
				Toast!.ShowError($"{invalidOperationException.Message}");
			}
		}
		else  // Edit
		{
			try
			{
				var sprocTuple = await Service!.Update(VM);
				Logger!.LogInformation("...Registration Updated!");
				//AppState!.UpdateMessage(this, "Registration Updated!");
				Toast!.ShowInfo($"Registration Updated!");
			}

			catch (InvalidOperationException invalidOperationException)
			{
				//AppState!.UpdateMessage(this, invalidOperationException.Message);
				Toast!.ShowError($"{invalidOperationException.Message}");
			}
		}
	}

}

using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using LivingMessiah.Features.Sukkot.RegistrationSteps;
using LivingMessiah.Features.Sukkot.Domain;
using LivingMessiah.Features.Sukkot.Data;
using LivingMessiah.Features.Sukkot.RegistrationSteps.Enums;
using LivingMessiah.Features.Sukkot.Constants;
//using  LivingMessiah.Infrastructure;
using LivingMessiah.Features.Sukkot.Enums;
using LivingMessiah.SecurityRoot;

namespace LivingMessiah.Features.Sukkot.Services;

public interface ISukkotService
{
	string UserInterfaceMessage { get; set; }
	Task<vwRegistration> Details(int id, ClaimsPrincipal user, bool showPrintInstructionMessage = false);
	Task<vwRegistration> DeleteConfirmation(int id, ClaimsPrincipal user);
	Task<int> DeleteConfirmed(int id);
	Task<RegistrationSummary> Summary(int id, ClaimsPrincipal user);
}

public class SukkotService : ISukkotService
{
	#region Constructor and DI
	private readonly ISukkotRepositoryUsedBySukkotService db;
	private readonly ILogger Logger;
	private readonly AuthenticationStateProvider AuthenticationStateProvider;
	private readonly ISecurityClaimsService SvcClaims;

	public SukkotService(
		ISukkotRepositoryUsedBySukkotService sukkotRepository,
		ILogger<SukkotService> logger,
		AuthenticationStateProvider authenticationStateProvider,
		ISecurityClaimsService securityClaimsService)
	{
		db = sukkotRepository;
		Logger = logger;
		AuthenticationStateProvider = authenticationStateProvider;
		SvcClaims = securityClaimsService;
	}
	#endregion

	private string LogExceptionMessage { get; set; } = "";
	public string UserInterfaceMessage { get; set; } = "";

	public async Task<RegistrationSummary> Summary(int id, ClaimsPrincipal user)
	{
		UserInterfaceMessage = "";
		Logger.LogDebug("{Method}, id: {id}", nameof(Summary), id);
		var vm = new RegistrationSummary();
		try
		{
			vm = await db.GetRegistrationSummary(id);
			if (vm is null)
			{
				UserInterfaceMessage = "Payment Summary Record NOT Found";
				Logger!.LogWarning("{Method}, UserInterfaceMessage: {UserInterfaceMessage}", nameof(Summary), UserInterfaceMessage);
				throw new PaymentSummaryException(UserInterfaceMessage);
			}
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(Summary)}, db.{nameof(db.GetRegistrationSummary)}";
			Logger.LogError(ex, LogExceptionMessage);

			/*
			ToDo: figure this out
			I would think that if a PaymentSummaryRecordNotFoundException is thrown, that would be the end of it.
			But this catchall exception IS ALSO CALLED??? 
			I don't want this, but this will do for now so I concatenated UserInterfaceMessage with a +=
			*/
			UserInterfaceMessage += "An invalid operation occurred, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}

		if (!await IsUserAuthoirized(vm.EMail!, id, user))
		{
			LogExceptionMessage = $"Inside {nameof(Summary)}, logged in user:{vm.EMail} lacks authority for id={id}";
			Logger.LogWarning(LogExceptionMessage);
			UserInterfaceMessage += "User not authorized";
			throw new UserNotAuthoirizedException(UserInterfaceMessage);
		}

		return vm;
	}


	public async Task<vwRegistration> Details(int id, ClaimsPrincipal user, bool showPrintInstructionMessage = false)
	{
		var vm = new vwRegistration();
		try
		{
			vm = await db.ById(id);
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(Details)}, {nameof(db.ById)}";
			Logger.LogError(ex, LogExceptionMessage, id, showPrintInstructionMessage);
			UserInterfaceMessage += "An invalid operation occurred getting registration details, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}

		if (!await IsUserAuthoirized(vm.EMail!, id, user))
		{
			LogExceptionMessage = $"Inside {nameof(Details)}, logged in user:{vm.EMail} lacks authority for id={id}";
			Logger.LogWarning(LogExceptionMessage);
			UserInterfaceMessage += "User not authorized";
			throw new UserNotAuthoirizedException(UserInterfaceMessage);
		}

		if (showPrintInstructionMessage) { vm.PayWithCheckMessage = Other.PayWithCheckModalMessage; }

		var tuple = Helper.GetAttendanceDatesArray(vm.AttendanceBitwise);
		vm.AttendanceDateList = tuple.week1;
		vm.AttendanceDateList2ndMonth = tuple.week2!;
		return vm;
	}

	public async Task<vwRegistration> DeleteConfirmation(int id, ClaimsPrincipal user)
	{
		var vm = new vwRegistration();
		try
		{
			vm = await db.ById(id);
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(DeleteConfirmation)}, {nameof(db.ById)}";
			Logger.LogError(ex, LogExceptionMessage, id);
			UserInterfaceMessage += "An invalid operation occurred getting registration details, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}

		if (!await IsUserAuthoirized(vm.EMail!, id, user))
		{
			LogExceptionMessage = $"Inside {nameof(DeleteConfirmation)}, logged in user:{vm.EMail!} lacks authority for id={id}";
			Logger.LogWarning(LogExceptionMessage);
			UserInterfaceMessage += "User not authorized";
			throw new UserNotAuthoirizedException(LogExceptionMessage);
		}

		return vm;
	}

	public static string DumpDateRange(DateTime[] dateList)
	{
		if (dateList == null) { return ""; }
		string s = "";
		foreach (DateTime day in dateList)
		{
			s += day.ToString("MM/dd") + ", ";
		}
		return s;
	}

	public async Task<int> DeleteConfirmed(int id)
	{
		int count = 0;
		try
		{
			Logger.LogInformation($"Delete registration in one call");
			count = await db.Delete(id);
			Logger.LogInformation($"Registration deleted for id={id}; affected rows={count}");
		}
		catch (Exception ex)
		{
			LogExceptionMessage = $"Inside {nameof(DeleteConfirmed)}, {nameof(db.Delete)}, id={id}";
			Logger.LogError(ex, LogExceptionMessage);
			UserInterfaceMessage += "An invalid operation occurred during registration deletion, contact your administrator";
			throw new InvalidOperationException(UserInterfaceMessage);
		}
		return count;
	}

	/* NOT USED
	private async Task<bool> AdminOrSukkotOverride(ClaimsPrincipal user)
	{
		return await SvcClaims.RoleHasAdminOrSukkot();
	}
	*/

	private async Task<bool> IsUserAuthoirized(string registrationEmail, int id, ClaimsPrincipal user)
	{
		string? userEmail = await SvcClaims.GetEmail();
		if (userEmail == registrationEmail) { return true; }
		return await SvcClaims.RoleHasAdminOrSukkot();
	}

}

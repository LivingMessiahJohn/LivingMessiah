using RCL.Features.Sukkot.Data.DailySchedule;

namespace Sukkot.Features.LandingPage.Data;

public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Daily schedule from Azure Blob (<see cref="BlobLoader"/>), not SQL.
	/// </summary>
	public static IServiceCollection AddSukkotDailyScheduleData(this IServiceCollection services)
		=> services.AddBlob();
}

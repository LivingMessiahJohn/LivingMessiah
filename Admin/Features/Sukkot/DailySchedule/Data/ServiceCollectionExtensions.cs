using RCL.Features.Sukkot;

namespace Admin.Features.Sukkot.DailySchedule.Data;

public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Daily schedule from Azure Blob (<see cref="ScheduleBlobQueryLoader"/>), not SQL.
	/// </summary>
	public static IServiceCollection AddSukkotDailyScheduleData(this IServiceCollection services)
		=> services.AddSukkotScheduleFromBlob();
}

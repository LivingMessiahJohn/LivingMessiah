using RCL.Features.Sukkot.Data.DailySchedule;

namespace Admin.Features.Sukkot.DailySchedule.Data;

public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Daily schedule from Azure Blob (<see cref="BlobLoader"/>), not SQL.
	/// </summary>
	public static IServiceCollection AddSukkotBlob(this IServiceCollection services)
	{
		return RCL.Features.Sukkot.Data.DailySchedule.ServiceCollectionExtensions.AddBlob(services);
	}
}

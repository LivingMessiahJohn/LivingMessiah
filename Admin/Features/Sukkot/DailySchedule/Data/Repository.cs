using Dapper;
using Admin.Data;
using RCL.Features.Sukkot;
using DataEnumsDatabase = Admin.Data.Enums.Database;

namespace Admin.Features.Sukkot.DailySchedule.Data;

public interface IRepository : IScheduleQueryLoader
{
	string BaseSqlDump { get; }
	string BaseServerId { get; }
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, DataEnumsDatabase.Sukkot.ConnectionStringKey)
	{
	}

	public string BaseSqlDump
	{
		get { return SqlDump!; }
	}
	public string BaseServerId => GetServerId();

	// Note: SQL twin of Sukkot\Features\LandingPage\Data\Repository.cs
	public async Task<ScheduleQuery?> GetAsync()
	{
		Sql = "SELECT Markdown, LastRevised FROM dbo.ScheduledEventsMarkdown";
		return await WithConnectionAsync(async connection =>
		{
			Logger.LogDebug("{Method} Sql: {Sql}", nameof(GetAsync), Sql);
			return await connection.QuerySingleOrDefaultAsync<ScheduleQuery>(sql: Sql);
		});
	}
}

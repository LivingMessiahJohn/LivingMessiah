using Dapper;
using Sukkot.Data;
using RCL.Features.Sukkot;

namespace Sukkot.Features.LandingPage.Data;

public interface IRepository : IScheduleQueryLoader
{
	string BaseSqlDump { get; }
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, Sukkot.Constants.ConnectionString.Sukkot)
	{
	}

	public string BaseSqlDump
	{
		get { return SqlDump!; }
	}

	// Note: SQL twin of Admin\Features\Sukkot\DailySchedule\Data\Repository.cs
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

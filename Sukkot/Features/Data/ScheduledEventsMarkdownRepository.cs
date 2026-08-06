using Dapper;
using Sukkot.Data;
using RCL.Features.Sukkot.Data;

namespace Sukkot.Features.Data;

public class ScheduledEventsMarkdownRepository : BaseRepositoryAsync, IScheduledEventsMarkdownRepository
{
	public ScheduledEventsMarkdownRepository(IConfiguration config, ILogger<ScheduledEventsMarkdownRepository> logger)
		: base(config, logger, Sukkot.Constants.ConnectionString.Sukkot)
	{
	}

	public async Task<ScheduledEventsMarkdownQuery?> GetAsync()
	{
		Sql = """
			SELECT Markdown, LastRevised
			FROM dbo.ScheduledEventsMarkdown
			WHERE [Lock] = 'X'
			""";

		return await WithConnectionAsync(async connection =>
		{
			Logger.LogDebug("{Method} Sql: {Sql}", nameof(GetAsync), Sql);
			return await connection.QuerySingleOrDefaultAsync<ScheduledEventsMarkdownQuery>(sql: Sql);
		});
	}
}

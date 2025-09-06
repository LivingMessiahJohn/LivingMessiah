using Dapper;
using LivingMessiahAdmin.Data;
using DataEnumsDatabase = LivingMessiahAdmin.Data.Enums.Database;

namespace LivingMessiahAdmin.Features.Sukkot.Reports.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	Task<(List<AttendanceAllFeastDaysQuery> AllFeastDays, AttendancePeopleSummaryQuery PeopleSummary)> GetAttendanceData();
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

	public async Task<(List<AttendanceAllFeastDaysQuery> AllFeastDays, AttendancePeopleSummaryQuery PeopleSummary)> GetAttendanceData()
	{
		base.Sql = @"
			SELECT FeastDay2, Id, Adults, ChildBig, ChildSmall, TotalPeeps 
			FROM Sukkot.vwAttendanceAllFeastDays 
			ORDER BY Id;
			
			SELECT Adults, ChildBig, ChildSmall, TotalPeeps 
			FROM Sukkot.vwAttendancePeopleSummary;";

		return await WithConnectionAsync(async connection =>
		{
			using var multi = await connection.QueryMultipleAsync(sql: base.Sql);
			
			var allFeastDays = (await multi.ReadAsync<AttendanceAllFeastDaysQuery>()).ToList();
			var peopleSummary = await multi.ReadSingleOrDefaultAsync<AttendancePeopleSummaryQuery>();

			return (allFeastDays, peopleSummary!);
		});
	}
}

using Dapper;

using DataEnumsDatabase = LivingMessiahAdmin.Data.Enums.Database;
using FilterEnums = LivingMessiahAdmin.Features.Sukkot.Dashboard.Enums.Filter;
using LivingMessiahAdmin.Data;


namespace LivingMessiahAdmin.Features.Sukkot.Dashboard.Data;

public interface IRepository
{
	string BaseSqlDump { get; }
	string BaseServerId { get; }
	Task<List<GridQuery>> GetAll(FilterEnums filter);
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

	public async Task<List<GridQuery>> GetAll(FilterEnums filter)
	{
		base.Parms = new DynamicParameters(new
		{
			StepId1 = filter.Step.Value,
			StepId2 = filter.Step2?.Value ?? filter.Step.Value
		});

		Sql = $@"
--DECLARE @stepId int=3
--DECLARE @stepId2 int=4
SELECT Id, EMail, FullName, StepId, Phone, Notes, AdminNotes, Adults, Children, DidNotAttend
, TotalDonation, DonationRowCount
, IdHra
, AttendanceBitwise
FROM Sukkot.vwDashboardGrid
WHERE StepId = @StepId1 OR StepId = @StepId2
ORDER BY FullName
"; 

		base.Logger.LogDebug("{Method}, Sql: {Sql}, Filter: {filter}", nameof(GetAll), Sql, filter.Name);
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<GridQuery>(sql: Sql, param: Parms);
			return rows.ToList();
		});
	}

}

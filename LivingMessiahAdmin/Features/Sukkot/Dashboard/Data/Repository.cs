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
	Task<List<StripeQuery>> GetAllStripe();
	Task<List<DonationTableQuery>> GetAllDonations(int registrationId);
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

	public async Task<List<StripeQuery>> GetAllStripe()
	{
		Sql = @"
SELECT Id, Email, RegistrationId, ModificationCount, LastModifiedDate, FirstName, FamilyName
FROM Sukkot.vwStripe
ORDER BY RegistrationId
";
		base.Logger.LogDebug("{Method}, Sql: {Sql}", nameof(GetAllStripe), Sql);

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<StripeQuery>(sql: Sql);
			return rows.ToList();
		});
	}

	public async Task<List<DonationTableQuery>> GetAllDonations(int registrationId)
	{
		Parms = new DynamicParameters(new { RegistrationId = registrationId });
		Sql = @"
SELECT
Detail, Amount, Notes, ReferenceId, CreateDate, CreatedBy
FROM Sukkot.Donation 
WHERE RegistrationId = @RegistrationId
";
		base.Logger.LogDebug("{Method}, Sql: {Sql}", nameof(GetAllDonations), Sql);

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<DonationTableQuery>(sql: Sql, param: Parms);
			return rows.ToList();
		});
	}
}

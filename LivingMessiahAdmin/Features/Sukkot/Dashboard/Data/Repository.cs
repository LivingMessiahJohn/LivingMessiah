using Dapper;

using DataEnumsDatabase = LivingMessiahAdmin.Data.Enums.Database;
using LivingMessiahAdmin.Data;


namespace LivingMessiahAdmin.Features.Sukkot.Dashboard.Data;

public interface IRepository
{
	string BaseSqlDump { get; }
	string BaseServerId { get; }
	Task<List<GridQuery>> GetAll();
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

	public async Task<List<GridQuery>> GetAll()
	{
		Sql = $@"
SELECT Id, EMail, FullName, StatusId, Phone, Notes, AdminNotes, Adults, Children, DidNotAttend
, TotalDonation, DonationRowCount
, IdHra
FROM Sukkot.vwManageRegistration
ORDER BY FullName
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<GridQuery>(sql: Sql);
			return rows.ToList();
		});
	}




}

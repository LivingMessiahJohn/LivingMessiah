using Dapper;
using LivingMessiahAdmin.Data;
using DataEnumsDatabase = LivingMessiahAdmin.Data.Enums.Database;

namespace LivingMessiahAdmin.Features.Sukkot.Notes.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	Task<List<NotesQuery>> GetAdminOrUserNotes(Enums.Filter filter);
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

	// Only NotesEnum.All.SqlWhere and NotesEnum.All.SqlOrder are used
	public async Task<List<NotesQuery>> GetAdminOrUserNotes(Enums.Filter filter)
	{
		Sql = $@"
SELECT TOP 500 Id, FirstName, FamilyName, AdminNotes, Notes AS UserNotes, Phone, EMail
FROM Sukkot.vwRegistration
{filter.SqlWhere}
{filter.SqlOrder}
";
		Logger!.LogDebug("{Method}, {Message}", nameof(GetAdminOrUserNotes), $"filter: {filter.Name}");
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<NotesQuery>(sql: Sql);
			return rows.ToList();
		});
	}

}

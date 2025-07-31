using Dapper;
using System.Data;
using LivingMessiahAdmin.Data;
using DataEnumsDatabase = LivingMessiahAdmin.Data.Enums.Database;

namespace LivingMessiahAdmin.Features.Sukkot.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

}

// using Primary Constructor Syntax
public class Repository(IConfiguration config, ILogger<Repository> logger) : 
		BaseRepositoryAsync(config, logger, DataEnumsDatabase.Sukkot.ConnectionStringKey), IRepository
{

	public string BaseSqlDump => SqlDump!;

	#region HRA
	#endregion
}

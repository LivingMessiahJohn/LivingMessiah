using Dapper;
using System.Data;

using DataEnumsDatabase = LivingMessiahAdmin.Data.Enums.Database;
using LivingMessiahAdmin.Data;

namespace LivingMessiahAdmin.Features.Sukkot.Home.Donations.Data;

public interface IRepository
{
	string BaseSqlDump { get; }
	string BaseServerId { get; }

	Task<int> InsertDonation(DonationRecord donation);
	//Task<int> DeleteDonationDetail(int id);

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
	
	public async Task<int> InsertDonation(DonationRecord donation)
	{
		Sql = "Sukkot.stpDonationInsert ";
		Parms = new DynamicParameters(new
		{
			donation.RegistrationId,
			//donation.Detail,
			donation.Amount,
			donation.Notes,
			donation.ReferenceId,
			donation.CreatedBy,
			donation.CreateDate,
			donation.Email
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

		/*
		 InsertDonation NewId is null; returning as 0; Check dbo.ErrorLog for FK_Donation_Registration conflict Error; donation.RegistrationId: 130
		 
		 */

		Logger!.LogDebug("{Method} {Sql}", nameof(InsertDonation), $"Sql: {Sql}");

		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			int? x = Parms.Get<int?>("NewId");
			if (x == null)
			{
				Logger!.LogWarning("{Method} {Message}"
					, nameof(InsertDonation)
					, $"NewId is null; returning as 0; Check dbo.ErrorLog for FK_Donation_Registration conflict Error; donation.RegistrationId: {donation.RegistrationId}");
				return 0;
			}
			else
			{
				int NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				Logger.LogDebug("{Method} {Message}", nameof(InsertDonation), $"Return NewId:{NewId}");
				return NewId;
			}
		});
	}

	/* 
	public async Task<int> DeleteDonationDetail(int id)
	{
		Parms = new DynamicParameters(new { Id = id });
		Sql = "DELETE FROM Sukkot.Donation WHERE Id=@Id";

		Logger!.LogDebug("{Method} {Message}", nameof(DeleteDonationDetail), $"Sql: {Sql}, id: {id}");

		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: Sql, param: Parms);
			return affectedrows;
		});
	}
	*/


}

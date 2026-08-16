using Dapper;
using System.Data;
//using Sukkot.Features;
//using Sukkot.Features.Domain;
//using Sukkot.Features.Components.RegistrationForm;
using Sukkot.Data;

namespace Sukkot.Endpoints.Data;

public interface IRepository
{
	string BaseSqlDump { get; }
	Task<Tuple<int, int, string>> StripeMerge(string email, int registrationId);
	Task<Tuple<int, string>> DonationInsert(DonationRecord donation);
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, Sukkot.Constants.ConnectionString.Sukkot)    //  CS0118: 'Sukkot' is a namespace but is used like a variable, 
	{
	}

	public string BaseSqlDump
	{
		get { return SqlDump!; }
	}

	public async Task<Tuple<int, int, string>> StripeMerge(string email, int registrationId)
	{
		Sql = "dbo.stpStripeMerge";
		Parms = new DynamicParameters(new
		{
			EMail = email,
			RegistrationId = registrationId
		});
		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			Logger!.LogDebug("{Method} Sql: {Sql}", nameof(StripeMerge), Sql);
			var affectedRows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");

			int? x = Parms.Get<int?>("NewId");
			if (x != null) 
			{
				NewId = int.TryParse(x.ToString(), out var tempId) ? tempId : 0;
				ReturnMsg = $"StripeMerge did an INSERT for {email}; NewId={NewId}";
				Logger!.LogDebug("{Method} ReturnMsg: {ReturnMsg}", nameof(StripeMerge), ReturnMsg);
			}
			else
			{
				ReturnMsg = $"StripeMerge did an UPDATE for {email}";
				Logger!.LogDebug("{Method} ReturnMsg: {ReturnMsg}", nameof(StripeMerge), ReturnMsg);
			}


			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}

	public async Task<Tuple<int, string>> DonationInsert(DonationRecord donation)
	{
		int NewId = 0;
		string ErrorMsg = "";

		Logger.LogDebug("{Method}, {RegistrationId}, Calling: {Calling}", nameof(DonationInsert), donation.RegistrationId, nameof(GetDonationRowCountByRegistrationId));
		int rowCount = await GetDonationRowCountByRegistrationId(donation.RegistrationId);

		if (rowCount > 0)
		{
			ErrorMsg = $"Donation already exists for registrationId {donation.RegistrationId}; returning 0";
			Logger.LogWarning("{Method}, {Message}", nameof(DonationInsert), ErrorMsg);
			return new Tuple<int, string>(NewId, ErrorMsg);
		}

		Sql = "dbo.stpDonationInsert ";
		Parms = new DynamicParameters(new
		{
			donation.RegistrationId,
			donation.Amount,
			donation.Notes,
			donation.Email,
			donation.ReferenceId,
			donation.CreatedBy,
			donation.CreateDate
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

		Logger.LogDebug("{Method}, {RegistrationId}, {Sql}", nameof(DonationInsert), donation.RegistrationId, Sql);

		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			int? x = Parms.Get<int?>("NewId");
			if (x == null)
			{
				ErrorMsg = $"NewId is null; returning as 0; Check dbo.ErrorLog for FK_Donation_Registration conflict Error; donation.RegistrationId: {donation.RegistrationId}";
				Logger.LogWarning("{Method}, {Message}", nameof(DonationInsert), ErrorMsg);
			}
			else
			{
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				Logger.LogInformation("{Method}, {Message}", nameof(DonationInsert), $"Returning NewId: {NewId}");
			}

			return new Tuple<int, string>(NewId, ErrorMsg);
		});
	}


	
	private async Task<int> GetDonationRowCountByRegistrationId(int registrationId)
	{
		Parms = new DynamicParameters(new { RegistrationId = registrationId });
		Sql = $@"SELECT COUNT(Id) AS Rows FROM dbo.Donation WHERE RegistrationId = @RegistrationId";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<int>(sql: Sql, param: Parms);
			return rows.SingleOrDefault()!;
		});
	}
	

}

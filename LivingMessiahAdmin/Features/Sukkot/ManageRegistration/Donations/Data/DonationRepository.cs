using Dapper;
using System.Data;

using LivingMessiahAdmin.Features.Sukkot.ManageRegistration.Donations.Domain;
using LivingMessiahAdmin.Data;
using DataEnumsDatabase = LivingMessiahAdmin.Data.Enums.Database;
using LivingMessiahAdmin.Features.Sukkot.ManageRegistration.Donations.Enums;

namespace LivingMessiahAdmin.Features.Sukkot.ManageRegistration.Donations.Data;

public interface IDonationRepository
{
	string BaseSqlDump { get; }
	string BaseServerId { get; }
	Task<int> InsertRegistrationDonation(Donation donation);
	Task<List<DonationReport>> GetDonationReport(DonationStatusFilter filter, string sortAndOrder);
	Task<List<DonationDetail>> GetDonationDetails(int registrationId);
	Task<List<DonationDetail>> GetDonationDetailsAll();
	Task<DonationDetail> GetDonationDetail(int id);
	Task<DonationDetail> UpdateDonationDetail(DonationDetail donationDetail);
	Task<List<RegistrationLookup>> PopulateRegistrationLookup();
}
public class DonationRepository : BaseRepositoryAsync, IDonationRepository
{
	public string BaseSqlDump
	{
		get { return SqlDump ?? ""; }
	}
	public string BaseServerId => GetServerId();

	public DonationRepository(IConfiguration config, ILogger<DonationRepository> logger)
		: base(config, logger, DataEnumsDatabase.Sukkot.ConnectionStringKey)
	{
	}

	public async Task<List<RegistrationLookup>> PopulateRegistrationLookup()
	{
		Sql = $@"
SELECT Id AS ID, Sukkot.udfFormatName(1, FamilyName, FirstName, NULL, NULL) AS Text
FROM Sukkot.Registration
ORDER BY FirstName
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<RegistrationLookup>(Sql);
			return rows.ToList();
		});
	}

	public async Task<int> InsertRegistrationDonation(Donation donation)
	{
		Sql = "Sukkot.stpDonationInsert ";
		Parms = new DynamicParameters(new
		{
			donation.RegistrationId,
			donation.Amount,
			donation.Notes,
			donation.ReferenceId,
			donation.CreatedBy,   
			donation.CreateDate
		});

		/*
			RegistrationId = donation.RegistrationId,
			Amount = donation.Amount,
			Notes = donation.Notes,
			ReferenceId = donation.ReferenceId,
			CreatedBy = donation.CreatedBy,   
			CreateDate = donation.CreateDate
		*/

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

		base.Logger.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(DonationRepository)}!{nameof(InsertRegistrationDonation)}, Sql: {Sql}");

		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			int? x = Parms.Get<int?>("NewId");
			if (x == null)
			{
				base.Logger.LogWarning($"NewId is null; returning as 0; Check dbo.ErrorLog for FK_Donation_Registration conflict Error; donation.RegistrationId: {donation.RegistrationId}");
				return 0;
			}
			else
			{
				int NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				base.Logger.LogDebug($"Return NewId:{NewId}");
				return NewId;
			}
		});
	}

	public async Task<DonationDetail> UpdateDonationDetail(DonationDetail donationDetail)
	{
		_ = await UpdateDonationProcess(donationDetail);
		DonationDetail NewDonation = new DonationDetail();
		NewDonation = await GetDonationDetail(donationDetail.Id);
		base.Logger.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(UpdateDonationDetail)}, Sql: {Sql}");
		return NewDonation;
	}

	private async Task<int> UpdateDonationProcess(DonationDetail donationDetail)
	{
		Parms = new DynamicParameters(new
		{
			donationDetail.Id,
			donationDetail.RegistrationId,
			donationDetail.Detail,
			donationDetail.Amount,
			donationDetail.Notes,
			donationDetail.ReferenceId,
			donationDetail.CreateDate,
			donationDetail.CreatedBy
		});

		Sql = $@"
UPDATE Sukkot.Donation SET 
	RegistrationId = @RegistrationId
, Detail = @Detail
, Amount = @Amount
, Notes = @Notes
, ReferenceId = @ReferenceId
, CreateDate = @CreateDate
, CreatedBy = @CreatedBy
WHERE Id=@Id
";
		base.Logger.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(UpdateDonationProcess)}, Sql: {Sql}");

		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: Sql, param: Parms);
			return count;
		});
	}

	public async Task<List<DonationReport>> GetDonationReport(DonationStatusFilter filter, string sortAndOrder)
	{
		Parms = new DynamicParameters(new { DonationStatus = filter.Value });
		//base.Parms = new DynamicParameters(new { SortAndOrder = sortAndOrder });

		Sql = $@"
SELECT Id, EMail, FamilyName, FirstName, StatusId, StatusDescr, RegistrationFeeAdjusted
, TotalDonation, AmountDue
FROM Sukkot.tvfDonationReport(@DonationStatus)
ORDER BY {sortAndOrder}
";

		//base.Logger.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(GetDonationReport)}, filter.Name/filter.Value: {filter.Name}/{filter.Value}");
		//base.Logger.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(GetDonationReport)}, Sql: {Sql}");

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<DonationReport>(Sql, Parms);
			return rows.ToList();
		});
	}

	public async Task<List<DonationDetail>> GetDonationDetails(int registrationId)
	{
		Parms = new DynamicParameters(new { RegistrationId = registrationId });
		Sql = $@"
SELECT 
	Id, RegistrationId, Detail, Amount, Notes, ReferenceId, CreateDate, CreatedBy 
FROM Sukkot.Donation
WHERE RegistrationId = @RegistrationId
ORDER BY Detail
";
		base.Logger.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(GetDonationDetails)}, Sql: {Sql}, registrationId: {registrationId}");

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<DonationDetail>(Sql, Parms);
			return rows.ToList();
		});
	}

	public async Task<List<DonationDetail>> GetDonationDetailsAll()
	{
		Sql = $@"
SELECT 
	d.Id, RegistrationId, Detail, Amount, d.Notes, ReferenceId, CreateDate, CreatedBy 
,	Sukkot.udfFormatName(1, FamilyName, FirstName, NULL, NULL) AS Name
FROM Sukkot.Donation d
INNER JOIN Sukkot.Registration r ON r.Id = d.RegistrationId
ORDER BY RegistrationId, Detail";

		base.Logger.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(GetDonationDetailsAll)}, Sql: {Sql}");

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<DonationDetail>(Sql);
			return rows.ToList();
		});
	}

	public async Task<DonationDetail> GetDonationDetail(int id)
	{
		Parms = new DynamicParameters(new { Id = id });
		Sql = $@"
SELECT 
	d.Id, RegistrationId, Detail, Amount, d.Notes, ReferenceId, CreateDate, CreatedBy 
,	Sukkot.udfFormatName(1, FamilyName, FirstName, NULL, NULL) AS Name
FROM Sukkot.Donation d
INNER JOIN Sukkot.Registration r ON r.Id = d.RegistrationId
WHERE d.Id = @Id	
";
		base.Logger.LogDebug($"Inside {nameof(DonationRepository)}!{nameof(GetDonationDetail)}, Sql: {Sql}, id: {id}");

		return await WithConnectionAsync(async connection =>
		{
			var donationDetail = await connection.QueryAsync<DonationDetail>(Sql, Parms);
			return donationDetail.SingleOrDefault()!;
		});
	}

}

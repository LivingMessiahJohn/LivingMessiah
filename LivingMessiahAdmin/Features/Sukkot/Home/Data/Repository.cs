using Dapper;
using System.Data;

using SukkotEnumsHelper = LivingMessiahAdmin.Features.Sukkot.Enums.Helper;
using DataEnumsDatabase = LivingMessiahAdmin.Data.Enums.Database;
using LivingMessiahAdmin.Data;
using LivingMessiahAdmin.Features.Sukkot.Enums;
using LivingMessiahAdmin.Features.Sukkot.Home.RegistrationDetail;

//using LivingMessiahAdmin.Features.Sukkot.Home.Detail;

namespace LivingMessiahAdmin.Features.Sukkot.Home.Data;

public interface IRepository
{
	string BaseSqlDump { get; }
	string BaseServerId { get; }
	Task<List<RegistrationListQuery>> GetAll();
	Task<Registrant.FormVM> Get(int id);

	Task<Tuple<int, int, string>> InsertHouseRulesAgreement(string email, string timeZone);  // FN:1
	Task<Tuple<int, int, string>> CreateRegistration(Registrant.DTO formVM);
	Task<Tuple<int, int, string>> UpdateRegistration(Registrant.DTO formVM);
	
	Task<RegistrationQuery> ById(int id);

	Task<List<DonationDetailQuery>> GetByRegistrationId(int registrationId);
	Task<int> DeleteDonationDetail(int id);
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


	#region HRA
	public async Task<Tuple<int, int, string>> InsertHouseRulesAgreement(string email, string timeZone)
	{
		Sql = "Sukkot.stpHouseRulesAgreementInsert";
		Parms = new DynamicParameters(new
		{
			EMail = email,
			TimeZone = timeZone
		});
		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			Logger!.LogDebug("{Method} Sql: {Sql}", nameof(InsertHouseRulesAgreement), Sql);
			var affectedRows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = base.Parms.Get<int>("ReturnValue");
			int? x = Parms.Get<int?>("NewId");

			if (x == null)
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not insert a new HRA record because it caused a Unique Index Violation; email: {email}; ";
					Logger!.LogWarning("{Method} ReturnMsg: {ReturnMsg}", nameof(InsertHouseRulesAgreement), ReturnMsg);
				}
				else
				{
					ReturnMsg = $"Database call failed; email: {email ?? "NULL!!"}; SprocReturnValue: {SprocReturnValue}";
					Logger!.LogWarning("{Method} ReturnMsg: {ReturnMsg}", nameof(InsertHouseRulesAgreement), ReturnMsg);
				}
			}
			else
			{
				NewId = int.TryParse(x.ToString(), out var tempId) ? tempId : 0;
				ReturnMsg = $"House Rules Agreement created for {email}; NewId={NewId}";
				Logger!.LogDebug("{Method} {NewId}", nameof(InsertHouseRulesAgreement), NewId);
			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}
	#endregion

	#region GetAll, Get, Create & Update

	public async Task<List<RegistrationListQuery>> GetAll()
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
			var rows = await connection.QueryAsync<RegistrationListQuery>(sql: Sql);
			return rows.ToList();
		});
	}

	public async Task<Registrant.FormVM> Get(int id)
	{
		Parms = new DynamicParameters(new { Id = id });
		Sql = $@"
		--DECLARE @id int= 4
SELECT
Id, FamilyName, FirstName, SpouseName, OtherNames
, EMail, Phone, Adults, ChildBig, ChildSmall
, FeeEnumValue
, StatusId AS StepId
, AttendanceBitwise
, Notes
--, Avatar
FROM Sukkot.Registration
WHERE Id = @Id";

		return await WithConnectionAsync(async connection =>
		{
			Logger!.LogDebug("{Method} {Sql} {id} ", nameof(Get), Sql, id);
			var rows = await connection.QueryAsync<Registrant.FormVM>(sql: Sql, param: Parms);
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<Tuple<int, int, string>> CreateRegistration(Registrant.DTO formVM) //Registrant.FormVM formVM
	{
		Sql = "Sukkot.stpRegistrationInsert";
		Parms = new DynamicParameters(new
		{
			formVM.FamilyName,
			formVM.FirstName,
			formVM.SpouseName,
			formVM.OtherNames,
			Email = formVM.EMail,
			formVM.Phone,
			formVM.Adults,
			formVM.ChildBig,
			formVM.ChildSmall,
			formVM.FeeEnumValue,
			//AttendanceBitwise = SukkotEnumsHelper.GetDaysBitwise(formVM.AttendanceDateList!, formVM.AttendanceDateList2ndMonth!, DateRangeType.Attendance),
			formVM.AttendanceBitwise,
			formVM.StatusId, //	change to 	formVM.StepId, after updating stp
			//LmmDonation = 0,
			formVM.Notes,
			//formVM.AdminNotes,
			//formVM.DidNotAttend,
			formVM.Avatar 	//Avatar = string.Empty
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			Logger!.LogDebug("{Method} {Message}", nameof(CreateRegistration), $"Email: {formVM.EMail}; about to execute SPROC: {Sql}");

			var affectedRows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");
			int? x = Parms.Get<int?>("NewId");
			if (x == null)
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {formVM.EMail}; ";
					Logger.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; registration.EMail: {formVM.EMail ?? "NULL!!"}; SprocReturnValue: {SprocReturnValue}";
					Logger.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
			}
			else
			{
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"Registration created for {formVM.FamilyName}/{formVM.EMail}; NewId={NewId}";
				Logger!.LogDebug("{Method} {Message}", nameof(CreateRegistration), ReturnMsg);
			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}

	public async Task<Tuple<int, int, string>> UpdateRegistration(Registrant.DTO formVM) // Registrant.FormVM 
	{
		Sql = "Sukkot.stpRegistrationUpdate";
		Parms = new DynamicParameters(new
		{
			formVM.Id,
			formVM.FamilyName,
			formVM.FirstName,
			formVM.SpouseName,
			formVM.OtherNames,
			formVM.EMail,
			formVM.Phone,
			formVM.Adults,
			formVM.ChildBig,
			formVM.ChildSmall,
			formVM.FeeEnumValue,
			//AttendanceBitwise = SukkotEnumsHelper.GetDaysBitwise(formVM.AttendanceDateList!, formVM.AttendanceDateList2ndMonth!, DateRangeType.Attendance),
			formVM.AttendanceBitwise,
			formVM.StatusId, //	change to 	formVM.StepId, after updating stp
			formVM.Notes,
			formVM.Avatar   //Avatar = string.Empty
			//AdminNotes = LivingMessiahAdmin.Data.Helper.Scrub(formVM.AdminNotes),
			//formVM.DidNotAttend,
		});

		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int RowsAffected = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			Logger!.LogDebug("{Method} {Message}", nameof(UpdateRegistration), $"Id: {formVM.Id}; Email: {formVM.EMail}; about to execute SPROC: {Sql}");

			Logger!.LogWarning($" Notes: {formVM.Notes}");
			//Logger!.LogWarning($" AdminNotes: {formVM.AdminNotes}");
			//Logger!.LogWarning($" DidNotAttend: {formVM.DidNotAttend}");


			RowsAffected = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");

			if (SprocReturnValue != 0) // ReturnValueOk
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not update the record because it caused a Unique Index Violation; formVM.EMail: {@formVM.EMail}; ";
					Logger.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
				else
				{
					ReturnMsg = $"Database call failed; {nameof(formVM.Id)}:{formVM.Id}, {nameof(formVM.EMail)}:{formVM.EMail}; SprocReturnValue: {SprocReturnValue}";
					Logger.LogWarning($"...ReturnMsg: {ReturnMsg}; {Environment.NewLine} {Sql}");
				}
			}
			else
			{
				ReturnMsg = $"Registration updated for {formVM.FamilyName}/{formVM.EMail}";
			}

			return new Tuple<int, int, string>(RowsAffected, SprocReturnValue, ReturnMsg);
		});
	}

	#endregion

	//public async Task<RegistrationListQuery> ById(int id)
	// KeyWord: Dapper Multi-Mapping (one-to-one) Header / 1 Detail
	public async Task<RegistrationQuery> ById(int id)
	{
		Parms = new DynamicParameters(new { Id = id });
		Sql = @"
SELECT
    Id,
    FamilyName, FirstName, SpouseName,  OtherNames,
    EMail, Phone,
    Adults, ChildBig, ChildSmall,
    FeeEnumValue,
    StepId,
    Notes,
    AttendanceBitwise,
    HouseRulesAgreementDate
FROM Sukkot.vwRegistration
WHERE Id = @Id
";
		return await WithConnectionAsync(async connection =>
		{
		//var registration = (await connection.QueryAsync<RegistrationListQuery>(sql: Sql, param: Parms)).SingleOrDefault();
			var registration = (await connection.QueryAsync<RegistrationQuery>(sql: Sql, param: Parms)).SingleOrDefault();

			if (registration != null)
			{
				// Query for DonationQuery
				var donationSql = @"
SELECT Amount, ReferenceId, CreateDate
FROM Sukkot.Donation
WHERE RegistrationId = @Id
";
				var donation = (await connection.QueryAsync<DonationQuery>(donationSql, Parms)).SingleOrDefault();
				registration.DonationQuery = donation;
			}

			return registration!;
		});
	}


	#region Donation

	public async Task<List<DonationDetailQuery>> GetByRegistrationId(int registrationId)
	{
		Sql = $@"
-- DECLARE @registrationId int = 20
SELECT Id, Detail, Amount, Notes, ReferenceId, CreateDate, CreatedBy --, FamilyName
FROM Sukkot.vwDonationDetail 
WHERE RegistrationId=@registrationId
ORDER BY Detail
";
		Parms = new DynamicParameters(new { RegistrationId = registrationId });

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<DonationDetailQuery>(sql: Sql, param: Parms);
			return rows.ToList();
		});
	}

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

	#endregion



}

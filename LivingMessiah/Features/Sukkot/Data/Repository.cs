using Dapper;
using System.Data;
using LivingMessiah.Features.Sukkot.NormalUser;

using LivingMessiah.Data;
using DataEnumsDatabase = LivingMessiah.Data.Enums.Database;
using LivingMessiah.Features.Sukkot.Domain;

namespace LivingMessiah.Features.Sukkot.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	// used by both ManageRegistration &  RegistrationStep

	Task<Tuple<int, int, string>> InsertHouseRulesAgreement(string email, string timeZone);  // FN:1
	Task<int> DeleteHRA(int id);  // stpHRADelete
	Task<Tuple<int, int, string>> DeleteRegistration(int id);

	Task<vwRegistration> ById(int id);
	Task<vwRegistrationStep> GetByEmail(string email);
	Task<EntryFormVM> GetById2(int id);   //ViewModel_RE_DELETE
	Task<Tuple<int, int, string>> Create(DTO registration);
	Task<Tuple<int, int, string>> Update(DTO registration);
	// FN:1 Also used by RegistrationSteps/Wrapper/YesNoButtons
	Task<RegistrationSummary> GetRegistrationSummary(int id);

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
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"House Rules Agreement created for {email}; NewId={NewId}";
				Logger!.LogDebug("{Method} {NewId}", nameof(InsertHouseRulesAgreement), NewId);
			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}

	public async Task<int> DeleteHRA(int id)
	{
		Sql = "Sukkot.stpHRADelete";
		Parms = new DynamicParameters(new { Id = id });
		return await WithConnectionAsync(async connection =>
		{
			Logger!.LogDebug("{Method} {Sql} {Id}", nameof(DeleteHRA), id, Sql);
			var affectedRows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			return affectedRows;
		});
	}

	public async Task<Tuple<int, int, string>> DeleteRegistration(int id)
	{
		Sql = "Sukkot.stpRegistrationDelete";
		Parms = new DynamicParameters(new { RegistrationId = id });

		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int RowsAffected = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			Logger!.LogDebug("{Method} {id}, {Sql} ", nameof(DeleteRegistration), id, Sql);
			RowsAffected = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");

			if (SprocReturnValue != 0) // ReturnValueOk
			{
				if (SprocReturnValue == 51000) // Can not have donation rows when deleting registration
				{
					ReturnMsg = $"Database call did not delete the registration record because it has donation rows; RegistrationId: {id}; Manually delete the donation row(s) then delete the registration.";
					Logger!.LogWarning("{Method} {ReturnMsg}", nameof(DeleteRegistration), ReturnMsg);
				}
				else
				{
					ReturnMsg = $"Database call failed to delete RegistrationId: {id}; SprocReturnValue: {SprocReturnValue}";
					Logger!.LogError("{Method} {ReturnMsg}", nameof(DeleteRegistration), ReturnMsg);
				}
			}
			else
			{
				ReturnMsg = $"Registration deleted for RegistrationId: {id}";
			}

			return new Tuple<int, int, string>(RowsAffected, SprocReturnValue, ReturnMsg);

		});
	}

	#endregion


	#region Registration used by Service

	public async Task<vwRegistration> ById(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $@"SELECT TOP 1 * FROM Sukkot.vwRegistration WHERE Id = @id";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwRegistration>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<vwRegistrationStep> GetByEmail(string email)
	{
		base.Logger.LogDebug("{Method}, email: {email}", nameof(GetByEmail), email);
		base.Parms = new DynamicParameters(new { EMail = email });
		base.Sql = $@"
--DECLARE @EMail varchar(100) = 'info@test.com'
SELECT Id, EMail
, TimeZone AS HouseRulesAgreementTimeZone, AcceptedDate AS HouseRulesAgreementAcceptedDate
, RegistrationId, FirstName, FamilyName, StepId
, TotalDonation, RegistrationFeeAdjusted
FROM Sukkot.vwRegistrationStep 
WHERE EMail = @EMail
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<vwRegistrationStep>(sql: base.Sql, param: base.Parms);
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<EntryFormVM> GetById2(int id)  //ViewModel_RE_DELETE
	{
		Parms = new DynamicParameters(new { Id = id });
		Sql = $@"
--DECLARE @id int=4
SELECT TOP 1 
Id, FamilyName, FirstName, SpouseName, OtherNames, EMail, Phone, Adults, ChildBig, ChildSmall
, StatusId AS StepId
, AttendanceBitwise, LmmDonation, Notes
--, Avatar
FROM Sukkot.Registration 
WHERE Id = @Id";
		return await WithConnectionAsync(async connection =>
		{
			Logger!.LogDebug("{Method} {Sql} {id} ", nameof(GetById2), Sql, id);
			var rows = await connection.QueryAsync<EntryFormVM>(sql: Sql, param: Parms);  //ViewModel_RE_DELETE
			return rows.SingleOrDefault()!;
		});
	}

	public async Task<Tuple<int, int, string>> Create(DTO registration)
	{
		Sql = "Sukkot.stpRegistrationInsert";
		Parms = new DynamicParameters(new
		{
			registration.FamilyName,
			registration.FirstName,
			registration.SpouseName,
			registration.OtherNames,
			Email = registration.EMail,
			registration.Phone,
			registration.Adults,
			registration.ChildBig,
			registration.ChildSmall,
			registration.StatusId, // ToDo Convert to StepId
			registration.AttendanceBitwise,
			LmmDonation = 0,
			registration.Avatar,
			registration.Notes,
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int NewId = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			Logger!.LogDebug("{Method} {Sql} {EMail}", nameof(Create), Sql, registration.EMail);
			var affectedRows = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");
			int? x = Parms.Get<int?>("NewId");
			if (x == null)
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not insert a new record because it caused a Unique Index Violation; registration.EMail: {@registration.EMail}; ";
					Logger!.LogWarning("{Method} {ReturnMsg}", nameof(Create), ReturnMsg);
				}
				else
				{
					ReturnMsg = $"Database call failed; registration.EMail: {@registration.EMail}; SprocReturnValue: {SprocReturnValue}";
					Logger!.LogWarning("{Method} {ReturnMsg}", nameof(Create), ReturnMsg);
				}
			}
			else
			{
				NewId = int.TryParse(x.ToString(), out NewId) ? NewId : 0;
				ReturnMsg = $"Registration created for {registration.FamilyName}/{registration.EMail}; NewId={NewId}";
				Logger!.LogDebug("{Method} {NewId}", nameof(Create), NewId);
			}

			return new Tuple<int, int, string>(NewId, SprocReturnValue, ReturnMsg);

		});
	}

	public async Task<Tuple<int, int, string>> Update(DTO registration)
	{
		Sql = "Sukkot.stpRegistrationUpdate";
		Parms = new DynamicParameters(new
		{
			registration.Id,
			registration.FamilyName,
			registration.FirstName,
			registration.SpouseName,
			registration.OtherNames,
			registration.EMail,
			registration.Phone,
			registration.Adults,
			registration.ChildBig,
			registration.ChildSmall,
			registration.AttendanceBitwise,
			registration.StatusId, // ToDo Convert to StepId
			registration.LmmDonation,
			Notes = LivingMessiah.Data.Helper.Scrub(registration.Notes),
			registration.Avatar
		});

		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int RowsAffected = 0;
		int SprocReturnValue = 0;
		string ReturnMsg = "";

		// Can't remove `Tuple<...>` with `(...)`, see C:\Source\LivingMessiahWiki\Tuples\Removing-Tuple-Conflicts-with-BaseRepositoryAsync.md
		return await WithConnectionAsync(async connection =>
		{
			Logger!.LogDebug("{Method} {Sql} {Id}", nameof(Update), Sql, registration.Id);
			RowsAffected = await connection.ExecuteAsync(sql: Sql, param: Parms, commandType: CommandType.StoredProcedure);
			SprocReturnValue = Parms.Get<int>("ReturnValue");

			if (SprocReturnValue != 0) // ReturnValueOk
			{
				if (SprocReturnValue == 2601) // Unique Index Violation
				{
					ReturnMsg = $"Database call did not update the record because it caused a Unique Index Violation; registration.EMail: {@registration.EMail}; ";
					Logger!.LogWarning("{Method} {ReturnMsg}", nameof(Update), ReturnMsg);
				}
				else
				{
					ReturnMsg = $"Database call failed; {nameof(registration.Id)}:{registration.Id}, {nameof(registration.EMail)}:{registration.EMail}; SprocReturnValue: {SprocReturnValue}";
					Logger!.LogWarning("{Method} {ReturnMsg}", nameof(Update), ReturnMsg);
				}
			}
			else
			{
				ReturnMsg = $"Registration updated for {registration.FamilyName}/{registration.EMail}";
			}

			return new Tuple<int, int, string>(RowsAffected, SprocReturnValue, ReturnMsg);
		});
	}

	#endregion


	public async Task<RegistrationSummary> GetRegistrationSummary(int id)
	{
		base.Parms = new DynamicParameters(new { id = id });  // This should be { Id = id })
		base.Sql = $@"
--DECLARE @id int=2
SELECT Id, EMail, FamilyName, Adults, ChildBig, ChildSmall, StatusId AS StepId
, AttendanceBitwise, RegistrationFeeAdjusted, TotalDonation
FROM Sukkot.tvfRegistrationSummary(@id)
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<RegistrationSummary>(base.Sql, base.Parms);
			return rows.SingleOrDefault()!;
		});
	}

}

using Dapper;
using System.Data;

using LivingMessiahAdmin.Data;
using DataEnumsDatabase = LivingMessiahAdmin.Data.Enums.Database;
using CalendarEnums = LivingMessiahAdmin.Features.KeyDates.Enums; 

namespace LivingMessiahAdmin.Features.KeyDates.Data;

public interface IRepository
{
	string BaseSqlDump { get; }
	string BaseServerId { get; }

	Task<List<CalendarEntryQuery>> GetCalendarEntries(int yearId, CalendarEnums.DateType filter);
	Task<List<CalendarAnalysisQuery>> GetCalendarAnalysisQuery(int yearId, CalendarEnums.DateType filter);
	Task<KeyDateConstantsQuery> GetKeyDateConstants();
	Task<int> UpdateKeyDateCalendar(int yearId, int detail, DateTime date);
}
public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump {	get { return base.SqlDump ?? ""; } }
	public string BaseServerId => GetServerId();

	public async Task<List<CalendarEntryQuery>> GetCalendarEntries(int yearId, CalendarEnums.DateType filter)
	{
		Logger!.LogDebug("{Method} {Message}", nameof(GetCalendarEntries), $"yearId: {yearId}, filter:{filter.Name}");
		base.Parms = new DynamicParameters(new { YearId = yearId });

		base.Sql = $@"
-- DECLARE @yearId int=9999
SELECT
Date, Detail, DateTypeId, EnumId, Description
FROM KeyDate.Calendar
WHERE YearId=@yearId
ORDER BY Date
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<CalendarEntryQuery>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}

	public async Task<List<CalendarAnalysisQuery>> GetCalendarAnalysisQuery(int yearId, CalendarEnums.DateType filter)
	{
		Logger!.LogDebug("{Method} {Message}", nameof(GetCalendarAnalysisQuery), $"yearId: {yearId}, filter:{filter.Name}");
		base.Parms = new DynamicParameters(new
		{
			YearId = yearId,
			DateTypeId = filter.Value
		});

		base.Sql = $@"
-- DECLARE @YearId int=2023, @DateTypeId int=2
SELECT
YearId, EventDescr, DateTypeId, Detail, EnumId, DiffFromPrevDate, PrevDateYMD, DateYMD
FROM KeyDate.tvfCalendarAnalysis_02_Union(@YearId) 
WHERE ((DateTypeId = @DateTypeId) OR (@DateTypeId = -1))  -- All is -1 NOT 0!!!
ORDER BY Date
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<CalendarAnalysisQuery>(sql: base.Sql, param: base.Parms);
			Logger!.LogDebug("{Method} {Message}", nameof(GetCalendarAnalysisQuery), $"rows: {rows.Count()}");
			return rows.ToList();
		});
	}

	public async Task<KeyDateConstantsQuery> GetKeyDateConstants() 
	{
		base.Sql = $"SELECT PreviousYear, CurrentYear, NextYear FROM KeyDate.Constants";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<KeyDateConstantsQuery>(sql: base.Sql);
			return rows.SingleOrDefault()!;
		});
	}

	#region Command

	public async Task<int> UpdateKeyDateCalendar(int yearId, int detail, DateTime date)
	{
		base.Parms = new DynamicParameters(new { YearId = yearId, Detail = detail, Date = date });
		base.Sql = $@"
-- DECLARE int @YearId={yearId}, int @Detail={detail}
UPDATE KeyDate.Calendar SET Date = @Date 
WHERE YearId = @YearId AND Detail=@Detail;
";
		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			Logger!.LogDebug("{Method} {Message}, {Sql}", nameof(UpdateKeyDateCalendar), $"count: {count}", base.Sql); //, base.Parms)
			return count;
		});
	}

	#endregion

}


/*
## ShiftYearForward NOT USED
- `KeyDate.stpShiftYearForward` is never used in the UI
	
```csharp
  Task<(int SprocReturnValue, string ReturnMsg)> ShiftYearForward(int NewCurrentYear);

	public async Task<(int SprocReturnValue, string ReturnMsg)> ShiftYearForward(int newCurrentYear)
	{
		base.Sql = "KeyDate.stpShiftYearForward ";

		Parms = new DynamicParameters(new
		{
			NewCurrentYear = newCurrentYear
		});
		Parms.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int SprocReturnValue = 0;
		string ReturnMsg = "";


		return await WithConnectionAsync(async connection =>
		{
			string parameters = $"newCurrentYear: {newCurrentYear}";
			string inside = $"{nameof(Repository)}!{nameof(ShiftYearForward)}; about to execute SPROC: {Sql}";
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, commandType: System.Data.CommandType.StoredProcedure);
			SprocReturnValue = base.Parms.Get<int>("ReturnValue");

			if (SprocReturnValue != 0)
			{
				ReturnMsg = $"Database call failed; newCurrentYear={newCurrentYear}; SprocReturnValue: {SprocReturnValue}";
				Logger.LogWarning(string.Format("inside {0}, SprocReturnValue != 0, parameters:{1}, ReturnMsg:{2}, {3} Sql: {4}"
					, inside, parameters, ReturnMsg, Environment.NewLine, Sql));
			}
			else
			{
				ReturnMsg = $"Key dates shifted forward; {parameters}";
			}

			
			
			//return new Tuple<int, int, string>(affectedRows, SprocReturnValue, ReturnMsg);
			return (affectedrows, ReturnMsg);
		});
	}
```

*/

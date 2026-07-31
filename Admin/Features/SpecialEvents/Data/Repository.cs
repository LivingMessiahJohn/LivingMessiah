using Dapper;
using Admin.Data;
using System.Data;
using DataEnumsDatabase = Admin.Data.Enums.Database;

using static Admin.Features.SpecialEvents.Data.SqlServer;
using Admin.Features.Database;

namespace Admin.Features.SpecialEvents.Data;

public interface IRepository
{
	string BaseSqlDump { get; }
	string BaseServerId { get; }

	Task<List<EventQuery>> GetEventsByDateRange(DateTimeOffset? dateBegin, DateTimeOffset? dateEnd);
	Task<FormVM?> GetEventById(int id);
	Task<List<FormVM>> GetCurrentEvents();

	Task<(int NewId, int SprocReturnValue, string ReturnMsg)> CreateSpecialEvent(FormVM formVM);
	Task<(int Affectedrows, string ReturnMsg)> UpdateSpecialEvent(SpecialEvents.FormVM formVM);
	Task<int> RemoveSpecialEvent(int id);

	#region database test
	Task<int> LogErrorTest();
	Task<List<zvwErrorLog>> GetzvwErrorLog();
	Task<int> EmptyErrorLog();
	#endregion
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, DataEnumsDatabase.SpecialEvent.ConnectionStringKey)
	{
	}

	public string BaseSqlDump { get { return base.SqlDump ?? ""; } }
	public string BaseServerId => GetServerId();


	public async Task<(int NewId, int SprocReturnValue, string ReturnMsg)> CreateSpecialEvent(SpecialEvents.FormVM formVM)
	{
		Sql = "dbo.stpEventInsert";
		Parms = new DynamicParameters(new
		{
			formVM.EventDate,
			formVM.ShowBeginDate,
			formVM.ShowEndDate,
			EventTypeId = formVM.EventTypeId,
			formVM.Title,
			formVM.SubTitle,
			formVM.Description,
			formVM.ImageUrl,
			formVM.WebsiteUrl,
			formVM.WebsiteDescr,
			formVM.YouTubeId
		});

		Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		Parms.Add(ReturnValueParm, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		int newId = 0;
		int sprocReturnValue = 0;
		string returnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			Logger.LogDebug("{Method} {Message}", nameof(CreateSpecialEvent), $"Title: {formVM.Title}; about to execute SPROC: {Sql}");

			var affectedrows = await connection.ExecuteAsync(
				sql: Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);

			newId = base.Parms.Get<int>("@NewId");
			sprocReturnValue = base.Parms.Get<int>(ReturnValueParm);

			returnMsg = $"Special Event created for {formVM.Title}; NewId={newId}";
			Logger.LogDebug("{Method} {Message}", nameof(CreateSpecialEvent), $"newId: {newId}, Affected Rows: {affectedrows}");

			return (newId, sprocReturnValue, returnMsg);
		});
	}

	public async Task<(int Affectedrows, string ReturnMsg)> UpdateSpecialEvent(SpecialEvents.FormVM formVM)
	{
		base.Sql = "dbo.stpEventUpdate";
		base.Parms = new DynamicParameters(new
		{
			formVM.Id,
			EventDate = formVM.EventDate,
			formVM.ShowBeginDate,
			formVM.ShowEndDate,
			EventTypeId = formVM.EventTypeId,
			formVM.Title,
			formVM.SubTitle,
			formVM.Description,
			formVM.ImageUrl,
			formVM.WebsiteUrl,
			formVM.WebsiteDescr,
			formVM.YouTubeId
		});

		base.Parms.Add(ReturnValueParm, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

		string returnMsg = "";

		return await WithConnectionAsync(async connection =>
		{
			Logger.LogDebug("{Method} {Message}", nameof(UpdateSpecialEvent), $"Title: {formVM.Title}; about to execute SPROC: {Sql}");

			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms, commandType: System.Data.CommandType.StoredProcedure);

			returnMsg = $"Special Event updated for {formVM.Title}; Id={formVM.Id}";
			Logger.LogDebug("{Method} {Message}", nameof(UpdateSpecialEvent), $"returnMsg: {returnMsg}, Affected Rows: {affectedrows}");
			return (affectedrows, returnMsg);

		});
	}

	public async Task<int> RemoveSpecialEvent(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $"DELETE FROM dbo.Event WHERE Id=@Id";
		return await WithConnectionAsync(async connection =>
		{
			Logger.LogDebug("{Method} {Message}", nameof(RemoveSpecialEvent), $"Sql: {Sql}; id={id}");
			var affectedrows = await connection.ExecuteAsync(sql: base.Sql, param: base.Parms);
			return affectedrows;
		});
	}

	public async Task<FormVM?> GetEventById(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });

		base.Sql = $@"
--DECLARE @Id int =1
SELECT
  Id, [DateTime] AS EventDate
, ShowBeginDate, ShowEndDate
, EventTypeId
, Title, SubTitle
, ISNULL(Description, '') AS Description 
, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
FROM dbo.Event
WHERE Id=@Id
";
		return await WithConnectionAsync(async connection =>
		{
			var row = await connection.QueryAsync<FormVM>(base.Sql, base.Parms);
			return row.SingleOrDefault();
		});
	}

	public async Task<List<FormVM>> GetCurrentEvents()  // Models.SpecialEventVM
	{
		Sql = $@"
SELECT
  Id, EventDate
, EventTypeId
, DaysDiff, DaysDiffDescr
, Title, SubTitle, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
, ISNULL(Description, '') AS Description 
FROM dbo.vwSpecialEvent
WHERE DATEADD(d, -1, ShowBeginDate) <= GETUTCDATE() AND  
			DATEADD(d, 1, ShowEndDate)		>= GETUTCDATE()
ORDER BY EventDate
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<FormVM>(sql: Sql);  //Models.SpecialEventVM
			return rows.ToList();
		});
	}

	//https://stackoverflow.com/questions/4331189/datetime-vs-datetimeoffset
	public async Task<List<EventQuery>> GetEventsByDateRange(DateTimeOffset? dateBegin, DateTimeOffset? dateEnd)
	{
		base.Parms = new DynamicParameters(new
		{
			DateBegin = dateBegin,
			DateEnd = dateEnd
		});

		// --Description is modified because MarkDig doesn't like nulls
		//--DECLARE @DateBegin smalldatetime = '2021-03-01', @DateEnd smalldatetime = '2023-01-31'
		base.Sql = $@"
SELECT
  Id, EventDate
, EventTypeId
, DaysDiff, DaysDiffDescr
, Title, SubTitle, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
, ISNULL(Description, '') AS Description 
, ShowBeginDate, ShowEndDate
FROM dbo.vwSpecialEvent
WHERE EventDate >= @DateBegin AND EventDate <=  @DateEnd
ORDER BY EventDate
";

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<EventQuery>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}


	#region database test
	public async Task<int> LogErrorTest()
	{
		Sql = "dbo.stpLogErrorTest ";
		return await WithConnectionAsync(async connection =>
		{
			var count = await connection.ExecuteAsync(sql: Sql, commandType: System.Data.CommandType.StoredProcedure);
			return count;
		});
	}
	
	public async Task<List<zvwErrorLog>> GetzvwErrorLog()
	{
		Sql = $@"SELECT TOP 75 * FROM zvwErrorLog ORDER BY ErrorLogID DESC";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<zvwErrorLog>(sql: Sql);
			return rows.ToList();
		});
	}

	public async Task<int> EmptyErrorLog()
	{
		Sql = "dbo.stpLogErrorEmpty";
		return await WithConnectionAsync(async connection =>
		{
			var affectedrows = await connection.ExecuteAsync(sql: Sql, commandType: System.Data.CommandType.StoredProcedure);
			return affectedrows;
		});
	}
	#endregion


}

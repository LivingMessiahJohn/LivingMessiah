using Dapper;
using System.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using static Admin.Features.SpecialEvents.Data.SqlServer;
using Admin.Data;
using DataEnumsDatabase = Admin.Data.Enums.Database;

namespace Admin.Features.SpecialEvents.Data;

public interface IRepository
{
	string BaseSqlDump { get; }

	Task<List<SpecialEventQuery>> GetEventsByDateRange(DateTimeOffset? dateBegin, DateTimeOffset? dateEnd);
	Task<FormVM?> GetEventById(int id);
	Task<List<FormVM>> GetCurrentEvents();  

	Task<(int NewId, int SprocReturnValue, string ReturnMsg)> CreateSpecialEvent(FormVM formVM);
	Task<(int Affectedrows, string ReturnMsg)> UpdateSpecialEvent(SpecialEvents.FormVM formVM);
	Task<int> RemoveSpecialEvent(int id);
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(IConfiguration config, ILogger<Repository> logger)
		: base(config, logger, DataEnumsDatabase.LivingMessiah.ConnectionStringKey)
	{
	}

	public string BaseSqlDump { get { return base.SqlDump ?? ""; } }

	public async Task<(int NewId, int SprocReturnValue, string ReturnMsg)> CreateSpecialEvent(SpecialEvents.FormVM formVM)
	{
		Sql = "SpecialEvent.stpSpecialEventInsert";
		Parms = new DynamicParameters(new
		{
			formVM.EventDate,
			formVM.ShowBeginDate,
			formVM.ShowEndDate,
			formVM.SpecialEventTypeId,
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

		base.Parms.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		base.Parms.Add(ReturnValueParm, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

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
		base.Sql = "SpecialEvent.stpSpecialEventUpdate";
		base.Parms = new DynamicParameters(new
		{
			formVM.Id,
			EventDate = formVM.EventDate,
			formVM.ShowBeginDate,
			formVM.ShowEndDate,
			formVM.SpecialEventTypeId,
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
			Logger.LogDebug("{Method} {Message}", nameof(CreateSpecialEvent), $"returnMsg: {returnMsg}, Affected Rows: {affectedrows}");
			return (affectedrows, returnMsg);

		});
	}

	public async Task<int> RemoveSpecialEvent(int id)
	{
		base.Parms = new DynamicParameters(new { Id = id });
		base.Sql = $"DELETE FROM SpecialEvent.Event WHERE Id=@Id";  
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
, SpecialEventTypeId
, Title, SubTitle
, ISNULL(Description, '') AS Description 
, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
FROM SpecialEvent.Event
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
, ShowBeginDate, ShowEndDate
, SpecialEventTypeId
, DaysDiff, DaysDiffDescr
, Title, SubTitle, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
, ISNULL(Description, '') AS Description 
FROM SpecialEvent.vwSpecialEvent
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
	public async Task<List<SpecialEventQuery>> GetEventsByDateRange(DateTimeOffset? dateBegin, DateTimeOffset? dateEnd)
	{
		base.Parms = new DynamicParameters(new
		{
			DateBegin = dateBegin,
			DateEnd = dateEnd
		});

		base.Sql = $@"
--Description is modified because MarkDig doesn't like nulls
--DECLARE @DateBegin smalldatetime =  '2021-03-01', @DateEnd smalldatetime = '2023-01-31' 
SELECT
  Id, EventDate
, SpecialEventTypeId
, DaysDiff, DaysDiffDescr
, Title, SubTitle, ImageUrl, WebsiteUrl, WebsiteDescr, YouTubeId
, ISNULL(Description, '') AS Description 
, ShowBeginDate, ShowEndDate
FROM SpecialEvent.vwSpecialEvent
WHERE EventDate >= @DateBegin AND EventDate <=  @DateEnd
ORDER BY EventDate
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<SpecialEventQuery>(sql: base.Sql, param: base.Parms);
			return rows.ToList();
		});
	}

}


using System.Data;
using System.Net;
using Api.Models;
using Dapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Api.Functions;

public class GetSpecialEventsFunction
{
	private readonly ILogger<GetSpecialEventsFunction> _logger;

	// Matches Admin SpecialEvents date-window (1-day buffer). Joins EventType for description (no client-side enum).
	private const string CurrentEventsSql = """
		SELECT
		  e.Id,
		  e.[DateTime] AS EventDate,
		  e.ShowBeginDate,
		  e.ShowEndDate,
		  e.EventTypeId,
		  t.Descr AS EventTypeDescr,
		  e.Title,
		  e.SubTitle,
		  e.ImageUrl,
		  e.WebsiteUrl,
		  e.WebsiteDescr,
		  e.YouTubeId,
		  ISNULL(e.Description, '') AS Description
		FROM dbo.Event e
		INNER JOIN dbo.EventType t ON e.EventTypeId = t.Id
		WHERE DATEADD(d, -1, e.ShowBeginDate) <= GETUTCDATE()
		  AND DATEADD(d, 1, e.ShowEndDate) >= GETUTCDATE()
		ORDER BY e.[DateTime]
		""";

	public GetSpecialEventsFunction(ILogger<GetSpecialEventsFunction> logger)
	{
		_logger = logger;
	}

	[Function("GetSpecialEvents")]
	public async Task<HttpResponseData> GetSpecialEvents(
		[HttpTrigger(AuthorizationLevel.Anonymous, "get", "options", Route = "special-events")] HttpRequestData req)
	{
		_logger.LogInformation("GetSpecialEvents function processing request. Method: {Method}", req.Method);

		if (req.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
		{
			var optionsResponse = req.CreateResponse(HttpStatusCode.OK);
			AddCorsHeaders(optionsResponse);
			return optionsResponse;
		}

		try
		{
			string? connectionString = Environment.GetEnvironmentVariable("SpecialEventConnectionString");

			if (string.IsNullOrWhiteSpace(connectionString))
			{
				_logger.LogError("SpecialEventConnectionString is missing");
				var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
				AddCorsHeaders(errorResponse);
				await errorResponse.WriteAsJsonAsync(new SpecialEventsResponse
				{
					Message = "SpecialEvent database configuration is missing"
				});
				return errorResponse;
			}

			await using var connection = new SqlConnection(connectionString);
			await connection.OpenAsync();

			var rows = (await connection.QueryAsync<SpecialEventDto>(
				sql: CurrentEventsSql,
				commandType: CommandType.Text)).ToList();

			_logger.LogInformation("GetSpecialEvents returned {Count} event(s)", rows.Count);

			var successResponse = req.CreateResponse(HttpStatusCode.OK);
			AddCorsHeaders(successResponse);
			await successResponse.WriteAsJsonAsync(new SpecialEventsResponse
			{
				Events = rows,
				Message = rows.Count == 0
					? "No special events are currently scheduled to display"
					: "Special events retrieved successfully"
			});

			return successResponse;
		}
		catch (SqlException ex) when (IsTransientSqlError(ex))
		{
			_logger.LogWarning(ex, "Transient SQL error in GetSpecialEvents");
			var response = req.CreateResponse(HttpStatusCode.ServiceUnavailable);
			AddCorsHeaders(response);
			await response.WriteAsJsonAsync(new SpecialEventsResponse
			{
				Message = "Service temporarily unavailable. Please try again.",
				ErrorDetails = FormatError(ex),
				IsTransient = true
			});
			return response;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error processing GetSpecialEvents request");
			var response = req.CreateResponse(HttpStatusCode.InternalServerError);
			AddCorsHeaders(response);
			await response.WriteAsJsonAsync(new SpecialEventsResponse
			{
				Message = "An error occurred while processing your request",
				ErrorDetails = FormatError(ex)
			});
			return response;
		}
	}

	private static void AddCorsHeaders(HttpResponseData response)
	{
		response.Headers.Add("Access-Control-Allow-Origin", "https://localhost:7211");
		response.Headers.Add("Access-Control-Allow-Methods", "GET, OPTIONS");
		response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
		response.Headers.Add("Access-Control-Max-Age", "86400");
	}

	// Common transient SQL error numbers (timeout, connection, deadlocks, etc.)
	private static bool IsTransientSqlError(SqlException ex) =>
		ex.Number is -2 or 53 or 121 or 1205 or 40197 or 40501 or 40613 or 49918 or 49919 or 49920;

	private static string FormatError(Exception ex) =>
		$"{ex.GetType().Name}: {ex.Message}";
}

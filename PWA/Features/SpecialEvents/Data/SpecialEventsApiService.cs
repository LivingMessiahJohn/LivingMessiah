using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using PWA.Features.SpecialEvents.Constants;
using RCL.Features.SpecialEvents;

namespace PWA.Features.SpecialEvents.Data;

public sealed class SpecialEventsLoadResult
{
	public IReadOnlyList<FormVM> Events { get; init; } = Array.Empty<FormVM>();
	public string? ErrorMessage { get; init; }
	public bool Succeeded => ErrorMessage is null;
}

public interface ISpecialEventsApiService
{
	/// <summary>
	/// Loads special events currently within their ShowBeginDate..ShowEndDate window.
	/// </summary>
	Task<SpecialEventsLoadResult> GetCurrentEventsAsync(CancellationToken ct = default);
}

public class SpecialEventsApiService : ISpecialEventsApiService
{
	private static readonly JsonSerializerOptions JsonOptions = new()
	{
		PropertyNameCaseInsensitive = true
	};

	private readonly HttpClient _httpClient;
	private readonly ILogger<SpecialEventsApiService> _logger;

	public SpecialEventsApiService(HttpClient httpClient, ILogger<SpecialEventsApiService> logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	public async Task<SpecialEventsLoadResult> GetCurrentEventsAsync(CancellationToken ct = default)
	{
		try
		{
			_logger.LogDebug("{Method} calling {Uri}", nameof(GetCurrentEventsAsync), AzureFunctionAPI.SpecialEventsHttpClientUri);

			using var response = await _httpClient.GetAsync(AzureFunctionAPI.SpecialEventsHttpClientUri, ct);

			var result = await response.Content.ReadFromJsonAsync<SpecialEventsApiResponse>(JsonOptions, ct);

			if (!response.IsSuccessStatusCode)
			{
				var msg = result?.Message ?? $"Request failed ({(int)response.StatusCode} {response.StatusCode})";
				_logger.LogWarning("{Method} failed. Status: {StatusCode}. Message: {Message}",
					nameof(GetCurrentEventsAsync), response.StatusCode, msg);
				return new SpecialEventsLoadResult { ErrorMessage = msg };
			}

			if (result is null)
			{
				_logger.LogWarning("{Method} response was null", nameof(GetCurrentEventsAsync));
				return new SpecialEventsLoadResult { ErrorMessage = "Empty response from special-events API" };
			}

			_logger.LogInformation("{Method} received {Count} event(s). Message: {Message}",
				nameof(GetCurrentEventsAsync), result.Events.Count, result.Message);

			return new SpecialEventsLoadResult { Events = result.Events };
		}
		catch (HttpRequestException ex)
		{
			_logger.LogError(ex, "{Method} HTTP error while loading special events", nameof(GetCurrentEventsAsync));
			return new SpecialEventsLoadResult { ErrorMessage = "Could not reach the special-events API. Is the Api project running?" };
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "{Method} error while loading special events", nameof(GetCurrentEventsAsync));
			return new SpecialEventsLoadResult { ErrorMessage = "Unexpected error loading special events" };
		}
	}

	// Matches Api.Models.SpecialEventsResponse shape (PascalCase or camelCase JSON).
	private sealed class SpecialEventsApiResponse
	{
		public List<FormVM> Events { get; set; } = new();
		public string Message { get; set; } = string.Empty;
		public bool IsTransient { get; set; }
	}
}

using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace PWA.Features.Home.WeeklyDownload;

public interface IBlobApiService
{
	Task<BlobInfoResponse> GetBlobInfoAsync(string blobName, CancellationToken ct = default);
}

public class BlobApiService : IBlobApiService
{
	private readonly HttpClient _httpClient;
	private readonly ILogger<BlobApiService> _logger;

	public BlobApiService(HttpClient httpClient, ILogger<BlobApiService> logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	public async Task<BlobInfoResponse> GetBlobInfoAsync(string blobName, CancellationToken ct = default)
	{
		try
		{
			//_logger.LogDebug("Requesting blob info for: {BlobName}", blobName);

			var request = new BlobInfoRequest(blobName);

			var response = await _httpClient.PostAsJsonAsync("/api/blob-info", request, ct);

			if (!response.IsSuccessStatusCode)
			{
				if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					_logger.LogInformation("Blob not found: {BlobName}", blobName);
					return new BlobInfoResponse(
							Exists: false,
							BlobInfo: null,
							Message: "Blob not found");
				}

				_logger.LogWarning("Failed to get blob info. Status: {StatusCode}", response.StatusCode);
				return new BlobInfoResponse(
						Exists: false,
						BlobInfo: null,
						Message: $"Request failed with status: {response.StatusCode}");
			}

			var result = await response.Content.ReadFromJsonAsync<BlobInfoResponse>(cancellationToken: ct);

			if (result is null)
			{
				_logger.LogWarning("Failed to deserialize blob info response");
				return new BlobInfoResponse(
						Exists: false,
						BlobInfo: null,
						Message: "Failed to read response");
			}

			_logger.LogInformation("Blob info retrieved successfully. Exists: {Exists}", result.Exists);
			return result;
		}

		catch (HttpRequestException ex)
		{
			_logger.LogError(ex, "HTTP error while getting blob info for {BlobName}", blobName);
			return new BlobInfoResponse(
					Exists: false,
					BlobInfo: null,
					Message: "Network error occurred");
		}

		catch (Exception ex)
		{
			_logger.LogError(ex, "Error getting blob info for {BlobName}", blobName);
			return new BlobInfoResponse(
					Exists: false,
					BlobInfo: null,
					Message: "An error occurred");
		}
	}
}

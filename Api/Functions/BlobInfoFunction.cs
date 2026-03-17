using System.Net;
using System.Text.Json;
using Api.Models;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using BlobInfo = Api.Models.BlobInfo;

namespace Api.Functions;

public class BlobInfoFunction
{
	private readonly ILogger<BlobInfoFunction> _logger;

	public BlobInfoFunction(ILogger<BlobInfoFunction> logger)
	{
		_logger = logger;
	}

	[Function("GetBlobInfo")]
	public async Task<HttpResponseData> GetBlobInfo(
			[HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "blob-info")] HttpRequestData req)
	{
		_logger.LogInformation("GetBlobInfo function processing request");

		try
		{
			// Read request body
			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			var request = JsonSerializer.Deserialize<BlobInfoRequest>(requestBody,
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			if (request is null || string.IsNullOrWhiteSpace(request.BlobName))
			{
				var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
				await badRequestResponse.WriteAsJsonAsync(new BlobInfoResponse(
						Exists: false,
						BlobInfo: null,
						Message: "BlobName cannot be null or empty"));
				return badRequestResponse;
			}

			string blobName = request.BlobName.Trim();
			_logger.LogInformation("Checking blob: {BlobName}", blobName);

			// Get configuration from environment variables
			string? connectionString = Environment.GetEnvironmentVariable("AzureStorageConnectionString");
			string? containerName = Environment.GetEnvironmentVariable("BlobContainerName");

			if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(containerName))
			{
				_logger.LogError("Azure Storage configuration is missing");
				var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
				await errorResponse.WriteAsJsonAsync(new BlobInfoResponse(
						Exists: false,
						BlobInfo: null,
						Message: "Azure Storage configuration is missing"));
				return errorResponse;
			}

			var containerClient = new BlobContainerClient(connectionString, containerName);
			var blobClient = containerClient.GetBlobClient(blobName);

			// Check if blob exists
			Response<bool> existsResponse = await blobClient.ExistsAsync();

			if (!existsResponse.Value)
			{
				_logger.LogInformation("Blob does not exist: {BlobName}", blobName);
				var notFoundResponse = req.CreateResponse(HttpStatusCode.OK);
				await notFoundResponse.WriteAsJsonAsync(new BlobInfoResponse(
						Exists: false,
						BlobInfo: null,
						Message: $"Blob '{blobName}' does not exist"));
				return notFoundResponse;
			}

			// Get blob properties
			Response<BlobProperties> propertiesResponse = await blobClient.GetPropertiesAsync();
			string url = blobClient.Uri.ToString();

			var blobInfo = new BlobInfo(blobName, url, propertiesResponse.Value.ContentLength);

			_logger.LogInformation("Blob info retrieved for {BlobName}: {Size} bytes", blobName, propertiesResponse.Value.ContentLength);

			var successResponse = req.CreateResponse(HttpStatusCode.OK);
			await successResponse.WriteAsJsonAsync(new BlobInfoResponse(
					Exists: true,
					BlobInfo: blobInfo,
					Message: "Blob info retrieved successfully"));

			return successResponse;
		}
		catch (RequestFailedException ex) when (IsTransientError(ex))
		{
			_logger.LogWarning(ex, "Transient error occurred");
			var response = req.CreateResponse(HttpStatusCode.ServiceUnavailable);
			await response.WriteAsJsonAsync(new BlobInfoResponse(
					Exists: false,
					BlobInfo: null,
					Message: "Service temporarily unavailable. Please try again.",
					IsTransient: true));
			return response;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error processing GetBlobInfo request");
			var response = req.CreateResponse(HttpStatusCode.InternalServerError);
			await response.WriteAsJsonAsync(new BlobInfoResponse(
					Exists: false,
					BlobInfo: null,
					Message: "An error occurred while processing your request"));
			return response;
		}
	}

	private static bool IsTransientError(RequestFailedException ex) =>
			ex.Status == 503 || // Service Unavailable
			ex.Status == 408 || // Request Timeout
			ex.Status == 429;   // Too Many Requests
}

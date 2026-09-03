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
		[HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "options", Route = "blob-info")] HttpRequestData req)
	{
		_logger.LogInformation("GetBlobInfo function processing request. Method: {Method}", req.Method);

		// Handle CORS preflight
		if (req.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
		{
			var optionsResponse = req.CreateResponse(HttpStatusCode.OK);
			AddCorsHeaders(optionsResponse);
			return optionsResponse;
		}

		try
		{
			// Read request body
			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			var request = JsonSerializer.Deserialize<BlobInfoRequest>(requestBody,
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			if (request is null || string.IsNullOrWhiteSpace(request.BlobName))
			{
				var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
				AddCorsHeaders(badRequestResponse);
				await badRequestResponse.WriteAsJsonAsync(new BlobInfoResponse(
						Exists: false,
						BlobInfo: null,
						Message: "BlobName cannot be null or empty"));
				return badRequestResponse;
			}

			string blobName = request.BlobName.Trim();

			// Get configuration from environment variables
			string? connectionString = Environment.GetEnvironmentVariable("AzureStorageConnectionString");
			string? defaultContainerName = Environment.GetEnvironmentVariable("BlobContainerName");

			if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(defaultContainerName))
			{
				_logger.LogError("Azure Storage configuration is missing");
				var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
				AddCorsHeaders(errorResponse);
				await errorResponse.WriteAsJsonAsync(new BlobInfoResponse(
						Exists: false,
						BlobInfo: null,
						Message: "Azure Storage configuration is missing"));
				return errorResponse;
			}

			if (!TryResolveContainerName(request.ContainerName, defaultContainerName, out string containerName, out string? containerError))
			{
				_logger.LogWarning("Rejected container name: {ContainerName}", request.ContainerName);
				var badContainerResponse = req.CreateResponse(HttpStatusCode.BadRequest);
				AddCorsHeaders(badContainerResponse);
				await badContainerResponse.WriteAsJsonAsync(new BlobInfoResponse(
						Exists: false,
						BlobInfo: null,
						Message: containerError ?? "ContainerName is not allowed"));
				return badContainerResponse;
			}

			_logger.LogInformation("Checking blob: {BlobName} in container {ContainerName}", blobName, containerName);

			var containerClient = new BlobContainerClient(connectionString, containerName);
			var blobClient = containerClient.GetBlobClient(blobName);

			// Check if blob exists
			Response<bool> existsResponse = await blobClient.ExistsAsync();

			if (!existsResponse.Value)
			{
				_logger.LogInformation("Blob does not exist: {BlobName} in container {ContainerName}", blobName, containerName);
				var notFoundResponse = req.CreateResponse(HttpStatusCode.OK);
				AddCorsHeaders(notFoundResponse);
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
			AddCorsHeaders(successResponse);
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
			AddCorsHeaders(response);
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
			AddCorsHeaders(response);
			await response.WriteAsJsonAsync(new BlobInfoResponse(
					Exists: false,
					BlobInfo: null,
					Message: "An error occurred while processing your request"));
			return response;
		}
	}
	
	private static readonly HashSet<string> AllowedContainerNames = new(StringComparer.OrdinalIgnoreCase)
	{
		"shabbat-service",
		"shabbat-service-teaching"
	};

	private static bool TryResolveContainerName(
		string? requestedContainerName,
		string defaultContainerName,
		out string containerName,
		out string? errorMessage)
	{
		if (string.IsNullOrWhiteSpace(requestedContainerName))
		{
			containerName = defaultContainerName;
			errorMessage = null;
			return true;
		}

		string requested = requestedContainerName.Trim();
		if (AllowedContainerNames.Contains(requested)
			|| string.Equals(requested, defaultContainerName, StringComparison.OrdinalIgnoreCase))
		{
			containerName = requested;
			errorMessage = null;
			return true;
		}

		containerName = defaultContainerName;
		errorMessage = "ContainerName is not allowed";
		return false;
	}

	private static void AddCorsHeaders(HttpResponseData response)
	{
		response.Headers.Add("Access-Control-Allow-Origin", "https://localhost:7211");
		response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
		response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
		response.Headers.Add("Access-Control-Max-Age", "86400");
	}
	
	private static bool IsTransientError(RequestFailedException ex) =>
			ex.Status == 503 || // Service Unavailable
			ex.Status == 408 || // Request Timeout
			ex.Status == 429;   // Too Many Requests
}

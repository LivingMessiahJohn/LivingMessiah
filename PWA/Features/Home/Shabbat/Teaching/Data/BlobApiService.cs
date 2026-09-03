using System.Net.Http.Json;
using RCL.Features.Parasha.Enums;
using PWA.Features.Home.Shabbat.Teaching.Constants;
using ParashaEnums = RCL.Features.Parasha.Enums;

namespace PWA.Features.Home.Shabbat.Teaching.Data;

public interface IBlobApiService
{
	Task<BlobDTO> GetParasha(Triennial? triennial, ParashaEnums.PdfType pdfType, CancellationToken ct = default);
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

	public async Task<BlobDTO> GetParasha(Triennial? triennial, ParashaEnums.PdfType pdfType, CancellationToken ct = default)
	{

		BlobDTO dto = new(
			Url: string.Empty, 
			Parasha: string.Empty, 
			PdfType: pdfType,
			Exists: false, 
			ExceptionOccurred: false
		);


		string blobName = string.Empty;
		
		try
		{

			string containerName;
			(dto, blobName, containerName) = GetCurrentParasha(triennial, pdfType);

			if (dto.ExceptionOccurred || string.IsNullOrEmpty(blobName)) { return dto; }

			_logger!.LogDebug("{Method}, {Message}", nameof(GetParasha),
				$"blobName: {blobName}, containerName: {containerName}");

			var request = new BlobInfoRequest(blobName, containerName);
			var response = await _httpClient.PostAsJsonAsync(AzureFunctionAPI.HttpClientUri, request, ct);

			if (!response.IsSuccessStatusCode)
			{
				if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
				{
					//_logger.LogDebug("Blob not found: {BlobName}", blobName);
					dto = dto with { Exists = false };
					return dto; 
				}

				_logger!.LogWarning("{Method}, {Message}", nameof(GetParasha), $"Failed to get blob info. Status: {response.StatusCode}");
				dto = dto with { ExceptionOccurred = true };
				return dto;
			}

			var result = await response.Content.ReadFromJsonAsync<BlobInfoResponse>(cancellationToken: ct);

			if (result is null)
			{
				_logger!.LogWarning("{Method}, {Message}", nameof(GetParasha), "Failed to deserialize blob info response");
				dto = dto with { ExceptionOccurred = true };
				return dto;
			}

			if (result.Exists)
			{
				string blobUrl = !string.IsNullOrEmpty(result.BlobInfo?.Url)
					? result.BlobInfo.Url
					: Blob.PublicUrl(blobName, pdfType);

				dto = dto with { Exists = true, Url = blobUrl };
				_logger!.LogInformation("{Method}, {Message}", nameof(GetParasha),
					$"containerName: {containerName}, blobUrl: {blobUrl}");
			}
			return dto;
		}

		catch (HttpRequestException ex)
		{
			_logger!.LogError(ex, "{Method}, {Message}", nameof(GetParasha), $"HTTP error while getting blob info for {blobName}");
			dto = dto with { ExceptionOccurred = true };
			return dto;
		}

		catch (Exception ex)
		{
			_logger!.LogError(ex, "{Method}, {Message}", nameof(GetParasha), $"Error while getting blob info for {blobName}");
			dto = dto with { ExceptionOccurred = true };
			return dto;
		}
	}

	private (BlobDTO dto, string blobName, string containerName) GetCurrentParasha(Triennial? triennial, ParashaEnums.PdfType pdfType)
	{
		string containerName = Blob.ContainerName(pdfType);
		Triennial? resolved = triennial ?? RCL.Features.Parasha.Helpers.GetCurrentReading();
		if (resolved is null)
		{
			_logger!.LogWarning("{Method}, {Message}", nameof(GetCurrentParasha), "Current triennial not found");
			return (new BlobDTO(
				Url: string.Empty,
				Parasha: string.Empty,
				PdfType: pdfType,
				Exists: false,
				ExceptionOccurred: false
			), string.Empty, containerName);
		}

		string file = RCL.Features.Parasha.Helpers.GetPdfFile(resolved);

		return (new BlobDTO(
			Url: string.Empty,
			Parasha: resolved.Date.ToString("yyyy MMMM dd") + " | " + resolved.BCV,
			PdfType: pdfType,
			Exists: false,
			ExceptionOccurred: false
		), file, containerName);
	}
}
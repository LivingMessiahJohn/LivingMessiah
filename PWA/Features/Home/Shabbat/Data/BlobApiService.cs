using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using RCL.Features.Parasha.Enums;
using PWA.Features.Home.Shabbat.Constants;

namespace PWA.Features.Home.Shabbat.Data;

public interface IBlobApiService
{
	Task<BlobDTO> GetParasha(Triennial? triennial, Enums.PdfType pdfType, CancellationToken ct = default);
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

	public async Task<BlobDTO> GetParasha(Triennial? triennial, Enums.PdfType pdfType, CancellationToken ct = default)
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

			(dto, blobName) = GetCurrentParasha(triennial, pdfType);

			if (dto.ExceptionOccurred) { return dto; }

			_logger!.LogDebug("{Method}, {Message}", nameof(GetParasha), $"blobName: {blobName}");

			var request = new BlobInfoRequest(blobName);
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

			string? blobUrl = result.BlobInfo?.Url;
			if(!string.IsNullOrEmpty(blobUrl))
			{
				dto = dto with { Exists = true, Url = blobUrl };
				_logger!.LogInformation("{Method}, {Message}", nameof(GetParasha), $"blobUrl: {blobUrl}");
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

	private (BlobDTO, string WeeklyReadingParashaFile) GetCurrentParasha(Triennial? triennial, Enums.PdfType pdfType)
	{
		if (triennial == null)
		{
			Triennial? currentTriennial = RCL.Features.Parasha.Helpers.GetCurrentReading();
			if (currentTriennial is null)
			{
				_logger!.LogWarning("{Method}, {Message}", nameof(GetCurrentParasha), "Current triennial not found");
				return (new BlobDTO(
					Url: string.Empty, 
					Parasha: string.Empty, 
					PdfType: pdfType,
					Exists: false, 
					ExceptionOccurred: false
				), string.Empty);
			}
			else
			{
				string baseName = NormalizeBaseName(currentTriennial.WeeklyReadingParashaFile);
				string file = !string.IsNullOrEmpty(baseName)
					? (pdfType == Enums.PdfType.TeachingOnly ? $"{baseName}-teaching.pdf" : $"{baseName}.pdf")
					: string.Empty;

				return (new BlobDTO(
					Url: string.Empty, 
					Parasha: currentTriennial.Date.ToString("yyyy MMMM dd") + " | " + currentTriennial.BCV, 
					PdfType: pdfType,
					Exists: false, 
					ExceptionOccurred: false
				), file);
			}
		}
		else
		{
			string baseName = NormalizeBaseName(triennial.WeeklyReadingParashaFile);
			string file = !string.IsNullOrEmpty(baseName)
				? (pdfType == Enums.PdfType.TeachingOnly ? $"{baseName}-teaching.pdf" : $"{baseName}.pdf")
				: string.Empty;

			return (new BlobDTO(
				Url: string.Empty, 
				Parasha: triennial.Date.ToString("yyyy MMMM dd") + " | " + triennial.BCV, 
				PdfType: pdfType,
				Exists: false, 
				ExceptionOccurred: false
			), file);
		}
	}

	// helper to make filename generation idempotent
	private static string NormalizeBaseName(string? baseName)
	{
		if (string.IsNullOrWhiteSpace(baseName)) return string.Empty;
		baseName = baseName.Trim();

		// if passed a full filename, strip .pdf and any trailing -teaching
		if (baseName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
			baseName = baseName.Substring(0, baseName.Length - 4);

		if (baseName.EndsWith("-teaching", StringComparison.OrdinalIgnoreCase))
			baseName = baseName.Substring(0, baseName.Length - "-teaching".Length);

		return baseName;
	}
}
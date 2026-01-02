using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace RCL.Features.Storage;

public class AzureBlobService
{
  private readonly BlobContainerClient _container;
  private readonly ILogger<AzureBlobService> Logger;

  public AzureBlobService(string connectionString, string containerName, ILogger<AzureBlobService>? logger = null)
  {
    _container = new BlobContainerClient(connectionString, containerName);
    Logger = logger ?? NullLogger<AzureBlobService>.Instance;
  }

  public async Task UploadStreamAsync(Stream stream, string fileName, string? contentType = null)
  {
    var blobClient = _container.GetBlobClient(fileName);  // _containerClient
    var headers = new BlobHttpHeaders { ContentType = contentType ?? "application/octet-stream" };
    await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = headers });
  }

  public async Task<FileUploadResultRecord> UploadAsync(string sourceFilePath, string blobName, CancellationToken ct = default)
  {
    string message = string.Empty;
    Logger.LogInformation("{Method}, {Message}", nameof(UploadAsync), $"source: {sourceFilePath}, target blob: {blobName}");

    sourceFilePath = sourceFilePath?.Trim() ?? string.Empty;
    blobName = blobName?.Trim() ?? string.Empty;

    BlobClient targetBlob = _container.GetBlobClient(blobName);

    try
    {
      if (!File.Exists(sourceFilePath))
      {
        message = $"file '{sourceFilePath}' not found.";
        Logger.LogWarning("{Method}, {Message}", nameof(UploadAsync), message);
        return new FileUploadResultRecord(false, message);
      }

      Response<bool> existsResponse = await targetBlob.ExistsAsync(ct);
      bool blobExists = existsResponse.Value;

      await using FileStream fs = File.OpenRead(sourceFilePath);
      await targetBlob.UploadAsync(fs, overwrite: true, cancellationToken: ct);

      message = blobExists
        ? $"File '{blobName}' overwritten successfully."
        : $"File '{blobName}' uploaded successfully.";

      Logger.LogDebug("{Method}, {Message}", nameof(UploadAsync), message);
      return new FileUploadResultRecord(true, message);
    }
    catch (Exception ex)
    {
      Logger!.LogError(ex, "{Method}, {Message}", nameof(UploadAsync), $"source: {sourceFilePath}, target blob: {blobName}");
      return new FileUploadResultRecord(false, ex.Message);
    }
  }

  /*
  Check if a blob exists in the configured container.
  */
  public async Task<bool> ExistsAsync(string blobName, CancellationToken ct = default)
  {
    try
    {
      BlobClient blob = _container.GetBlobClient(blobName?.Trim() ?? string.Empty);
      var resp = await blob.ExistsAsync(ct);
      Logger.LogDebug("{Method}, {Message}", nameof(ExistsAsync), $"blobName: {blobName}; resp.Value: {resp.Value}");
      return resp.Value;
    }
    catch (Exception ex)
    {
      Logger.LogWarning(ex, "{Method} failed for blob '{BlobName}'", nameof(ExistsAsync), blobName);
      return false;
    }
  }

  /*
	Get a public URL for the blob (container must allow public read or client must provide SAS).
	*/
  public string GetBlobUrl(string blobName)
  {
    return _container.GetBlobClient(blobName?.Trim() ?? string.Empty).Uri.ToString();
  }

  /*
  Get blob info including name, URL, and size.
  */
  public async Task<BlobInfo?> GetBlobInfoAsync(string blobName, CancellationToken ct = default)
  {
    try
    {
      BlobProperties? properties = await GetBlobPropertiesAsync(blobName, ct);
      if (properties is null)
      {
        return null;
      }
      string url = GetBlobUrl(blobName);
      return new BlobInfo(blobName, url, properties.ContentLength);
    }
    catch (Exception ex)
    {
      Logger.LogWarning(ex, "{Method} failed for blob '{BlobName}'", nameof(GetBlobInfoAsync), blobName);
      return null;
    }
  }

  private async Task<BlobProperties?> GetBlobPropertiesAsync(string blobName, CancellationToken ct = default)
  {
    try
    {
      BlobClient blob = _container.GetBlobClient(blobName?.Trim() ?? string.Empty);
      Response<BlobProperties> response = await blob.GetPropertiesAsync(cancellationToken: ct);
      Logger.LogDebug("{Method}, {Message}", nameof(GetBlobPropertiesAsync), $"blobName: {blobName}; size: {response.Value.ContentLength}");
      return response.Value;
    }
    catch (Exception ex)
    {
      Logger.LogWarning(ex, "{Method} failed for blob '{BlobName}'", nameof(GetBlobPropertiesAsync), blobName);
      return null;
    }
  }

}


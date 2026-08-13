using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace RCL.Features.Storage;

public class AzureBlobService : IAzureBlobService
{
  private readonly BlobContainerClient _container;
  private readonly ILogger<AzureBlobService> Logger;

  public AzureBlobService(string connectionString, string containerName, ILogger<AzureBlobService>? logger = null)
  {
    _container = new BlobContainerClient(connectionString, containerName);
    Logger = logger ?? NullLogger<AzureBlobService>.Instance;
  }

  private static bool IsTransientError(RequestFailedException ex) =>
      ex.Status == 503 || // Service Unavailable
      ex.Status == 408 || // Request Timeout
      ex.Status == 429;   // Too Many Requests

  public async Task<BlobOperationResult> UploadStreamAsync(
      Stream stream,
      string fileName,
      string? contentType = null,
      IDictionary<string, string>? metadata = null,
      CancellationToken ct = default)
  {
    if (string.IsNullOrWhiteSpace(fileName))
      return BlobOperationResult.Failure("fileName cannot be null or empty");

    if (stream == null)
      return BlobOperationResult.Failure("stream cannot be null");

    //Logger!.LogInformation("{Method} {Message}", $"{nameof(AzureBlobService)}!{nameof(UploadStreamAsync)}"
    //  , $"containerName: {_container.Name}, fileName: {fileName}");

    try
    {
      var blobClient = _container.GetBlobClient(fileName.Trim());
      var headers = new BlobHttpHeaders { ContentType = contentType ?? "application/octet-stream" };
      // overwrite: true so Admin schedule save and re-uploads replace existing blobs
      await blobClient.UploadAsync(stream, overwrite: true, cancellationToken: ct);
      await blobClient.SetHttpHeadersAsync(headers, cancellationToken: ct);
      if (metadata is { Count: > 0 })
        await blobClient.SetMetadataAsync(new Dictionary<string, string>(metadata), cancellationToken: ct);

      Logger.LogInformation("Stream uploaded successfully to blob: {BlobName}", fileName);
      return BlobOperationResult.Success($"Stream uploaded to '{fileName}' successfully.");
    }
    catch (RequestFailedException ex) when (IsTransientError(ex))
    {
      return BlobOperationResult.Failure(
          $"Transient error uploading stream to '{fileName}'",
          ex,
          isTransient: true);
    }
    catch (Exception ex)
    {
      Logger.LogError(ex, "Failed to upload stream to blob: {BlobName}", fileName);
      return BlobOperationResult.Failure($"Failed to upload stream to '{fileName}'", ex);
    }
  }

  public async Task<BlobOperationResult> UploadAsync(
      string sourceFilePath,
      string blobName,
      CancellationToken ct = default)
  {
    sourceFilePath = sourceFilePath?.Trim() ?? string.Empty;
    blobName = blobName?.Trim() ?? string.Empty;

    if (string.IsNullOrEmpty(sourceFilePath))
      return BlobOperationResult.Failure("sourceFilePath cannot be null or empty");

    if (string.IsNullOrEmpty(blobName))
      return BlobOperationResult.Failure("blobName cannot be null or empty");

    if (!File.Exists(sourceFilePath))
    {
      Logger.LogWarning("Source file not found: {FilePath}", sourceFilePath);
      return BlobOperationResult.Failure($"File '{sourceFilePath}' not found.");
    }

    Logger.LogInformation("Beginning upload from {SourcePath} to blob {BlobName}", sourceFilePath, blobName);

    try
    {
      BlobClient targetBlob = _container.GetBlobClient(blobName);
      bool blobExists = (await targetBlob.ExistsAsync(ct)).Value;

      await using FileStream fs = File.OpenRead(sourceFilePath);
      await targetBlob.UploadAsync(fs, overwrite: true, cancellationToken: ct);

      string message = blobExists
          ? $"File '{blobName}' overwritten successfully."
          : $"File '{blobName}' uploaded successfully.";

      Logger.LogInformation(message);
      return BlobOperationResult.Success(message);
    }
    catch (RequestFailedException ex) when (IsTransientError(ex))
    {
      return BlobOperationResult.Failure(
          $"Transient error uploading '{blobName}'",
          ex,
          isTransient: true);
    }
    catch (Exception ex)
    {
      Logger.LogError(ex, "Upload failed for {SourcePath} to {BlobName}", sourceFilePath, blobName);
      return BlobOperationResult.Failure($"Upload failed for '{blobName}'", ex);
    }
  }

  public async Task<BlobOperationResult<bool>> ExistsAsync(
      string blobName,
      CancellationToken ct = default)
  {
    blobName = blobName?.Trim() ?? string.Empty;

    if (string.IsNullOrEmpty(blobName))
      return BlobOperationResult<bool>.Failure("blobName cannot be null or empty");

    try
    {
      BlobClient blob = _container.GetBlobClient(blobName);
      bool exists = (await blob.ExistsAsync(ct)).Value;

      Logger.LogDebug("Blob '{BlobName}' exists: {Exists}", blobName, exists);
      return BlobOperationResult<bool>.Success(exists, $"Blob '{blobName}' found.");
    }
    catch (RequestFailedException ex) when (IsTransientError(ex))
    {
      return BlobOperationResult<bool>.Failure(
          $"Transient error checking blob '{blobName}'",
          ex,
          isTransient: true);
    }
    catch (Exception ex)
    {
      Logger.LogWarning(ex, "Failed to check if blob exists: {BlobName}", blobName);
      return BlobOperationResult<bool>.Failure($"Failed to check blob '{blobName}'", ex);
    }
  }

  public async Task<BlobOperationResult<string>> GetBlobUrlAsync(
      string blobName,
      CancellationToken ct = default)
  {
    blobName = blobName?.Trim() ?? string.Empty;

    if (string.IsNullOrEmpty(blobName))
      return BlobOperationResult<string>.Failure("blobName cannot be null or empty");

    try
    {
      var existsResult = await ExistsAsync(blobName, ct);
      if (!existsResult.IsSuccess && existsResult.IsTransient)
        return BlobOperationResult<string>.Failure(
            existsResult.Message,
            existsResult.Exception,
            isTransient: true);

      if (!existsResult.Data)
        return BlobOperationResult<string>.Failure($"Blob '{blobName}' does not exist.");

      string url = _container.GetBlobClient(blobName).Uri.ToString();
      return BlobOperationResult<string>.Success(url, "URL retrieved successfully.");
    }
    catch (Exception ex)
    {
      Logger.LogError(ex, "Failed to get URL for blob: {BlobName}", blobName);
      return BlobOperationResult<string>.Failure($"Failed to get URL for '{blobName}'", ex);
    }
  }

  public async Task<BlobOperationResult<BlobInfo>> GetBlobInfoAsync(
      string blobName,
      CancellationToken ct = default)
  {
    blobName = blobName?.Trim() ?? string.Empty;

    if (string.IsNullOrEmpty(blobName))
      return BlobOperationResult<BlobInfo>.Failure("blobName cannot be null or empty");

    try
    {
      BlobClient blob = _container.GetBlobClient(blobName);
      Response<BlobProperties> response = await blob.GetPropertiesAsync(cancellationToken: ct);

      string url = blob.Uri.ToString();
      var info = new BlobInfo(blobName, url, response.Value.ContentLength);

      Logger.LogDebug("Retrieved blob info for {BlobName}: {Size} bytes", blobName, response.Value.ContentLength);
      return BlobOperationResult<BlobInfo>.Success(info, "Blob info retrieved successfully.");
    }
    catch (RequestFailedException ex) when (IsTransientError(ex))
    {
      return BlobOperationResult<BlobInfo>.Failure(
          $"Transient error getting blob info for '{blobName}'",
          ex,
          isTransient: true);
    }
    catch (Exception ex)
    {
      Logger.LogWarning(ex, "Failed to get blob info: {BlobName}", blobName);
      return BlobOperationResult<BlobInfo>.Failure($"Failed to get info for '{blobName}'", ex);
    }
  }

  public async Task<BlobOperationResult<BlobTextContent>> DownloadTextAsync(
      string blobName,
      CancellationToken ct = default)
  {
    blobName = blobName?.Trim() ?? string.Empty;

    if (string.IsNullOrEmpty(blobName))
      return BlobOperationResult<BlobTextContent>.Failure("blobName cannot be null or empty");

    try
    {
      BlobClient blob = _container.GetBlobClient(blobName);
      Response<BlobDownloadResult> response = await blob.DownloadContentAsync(cancellationToken: ct);
      BlobDownloadResult download = response.Value;
      string text = download.Content.ToString();
      DateTime lastRevised = ResolveLastRevised(download.Details.Metadata, download.Details.LastModified);

      Logger.LogDebug("Downloaded text blob {BlobName}: {Length} chars, LastRevised {LastRevised}",
          blobName, text.Length, lastRevised);
      return BlobOperationResult<BlobTextContent>.Success(
          new BlobTextContent(text, lastRevised),
          "Blob text downloaded successfully.");
    }
    catch (RequestFailedException ex) when (ex.Status == 404)
    {
      Logger.LogDebug("Blob not found: {BlobName}", blobName);
      return BlobOperationResult<BlobTextContent>.Failure($"Blob '{blobName}' does not exist.", ex);
    }
    catch (RequestFailedException ex) when (IsTransientError(ex))
    {
      return BlobOperationResult<BlobTextContent>.Failure(
          $"Transient error downloading '{blobName}'",
          ex,
          isTransient: true);
    }
    catch (Exception ex)
    {
      Logger.LogWarning(ex, "Failed to download text blob: {BlobName}", blobName);
      return BlobOperationResult<BlobTextContent>.Failure($"Failed to download '{blobName}'", ex);
    }
  }

  /// <summary>
  /// Prefer user metadata <c>lastrevised</c>; otherwise use blob LastModified (UTC → local).
  /// </summary>
  private static DateTime ResolveLastRevised(
      IDictionary<string, string> metadata,
      DateTimeOffset lastModified)
  {
    const string key = "lastrevised";
    if (metadata is not null)
    {
      foreach (var pair in metadata)
      {
        if (!string.Equals(pair.Key, key, StringComparison.OrdinalIgnoreCase))
          continue;
        if (DateTime.TryParse(pair.Value, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime parsed))
          return parsed;
        break;
      }
    }

    return lastModified.LocalDateTime;
  }
}


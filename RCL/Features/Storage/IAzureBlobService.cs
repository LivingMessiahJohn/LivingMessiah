namespace RCL.Features.Storage;

public interface IAzureBlobService
{
	Task<BlobOperationResult> UploadStreamAsync(
		Stream stream,
		string fileName,
		string? contentType = null,
		CancellationToken ct = default);

	Task<BlobOperationResult> UploadAsync(
		string sourceFilePath,
		string blobName,
		CancellationToken ct = default);

	Task<BlobOperationResult<bool>> ExistsAsync(
		string blobName,
		CancellationToken ct = default);

	Task<BlobOperationResult<string>> GetBlobUrlAsync(
		string blobName,
		CancellationToken ct = default);

	Task<BlobOperationResult<BlobInfo>> GetBlobInfoAsync(
		string blobName,
		CancellationToken ct = default);
}

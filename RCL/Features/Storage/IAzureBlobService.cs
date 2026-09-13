namespace RCL.Features.Storage;

public interface IAzureBlobService
{
	Task<BlobOperationResult> UploadStreamAsync(
		Stream stream,
		string fileName,
		string? contentType = null,
		IDictionary<string, string>? metadata = null,
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

	/// <summary>
	/// Downloads blob content as UTF-8 text. <see cref="BlobTextContent.LastRevised"/>
	/// comes from metadata key <c>lastrevised</c> when set; otherwise blob LastModified.
	/// Fails if the blob is missing.
	/// </summary>
	Task<BlobOperationResult<BlobTextContent>> DownloadTextAsync(
		string blobName,
		CancellationToken ct = default);

	/// <summary>
	/// Lists blob names in the container whose path starts with <paramref name="prefix"/>.
	/// Folder placeholders (names ending in <c>/</c>) are omitted.
	/// </summary>
	Task<BlobOperationResult<IReadOnlyList<string>>> ListBlobNamesAsync(
		string prefix,
		CancellationToken ct = default);

	/// <summary>
	/// Read-only blob SAS URI. Requires an account-key connection string
	/// (<see cref="Azure.Storage.Blobs.BlobClient.CanGenerateSasUri"/>).
	/// Fails if the blob is missing.
	/// </summary>
	Task<BlobOperationResult<string>> GetReadSasUriAsync(
		string blobName,
		TimeSpan lifetime,
		CancellationToken ct = default);
}

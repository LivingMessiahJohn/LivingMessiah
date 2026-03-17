namespace Api.Models;

public record BlobInfoRequest(string BlobName);

public record BlobInfoResponse(
                bool Exists,
                BlobInfo? BlobInfo,
                string Message,
                bool IsTransient = false);

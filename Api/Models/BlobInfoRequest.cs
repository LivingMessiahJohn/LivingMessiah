namespace Api.Models;

public record BlobInfoRequest(string BlobName, string? ContainerName = null);

public record BlobInfoResponse(
                bool Exists,
                BlobInfo? BlobInfo,
                string Message,
                bool IsTransient = false);

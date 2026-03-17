namespace PWA.Features.Home.WeeklyDownload;

public record BlobInfoRequest(string BlobName);

public record BlobInfoResponse(
    bool Exists,
    BlobInfo? BlobInfo,
    string Message,
    bool IsTransient = false);

public record BlobInfo(string Name, string Url, long SizeBytes);

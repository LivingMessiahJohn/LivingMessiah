
namespace PWA.Features.Home.Data;

public record BlobInfoRequest(string BlobName);

public record BlobInfoResponse(
    bool Exists,
    BlobInfo? BlobInfo,
    string CurrentReading,
		string Message,
    bool IsTransient = false);

/*
IsTransient:  
- true: The error is temporary (network glitch, service throttling, timeout). Retry might succeed.
- false: The error is permanent (bad request, not found, configuration error). Retry won't help.
*/

public record BlobInfo(string Name, string Url, long SizeBytes);

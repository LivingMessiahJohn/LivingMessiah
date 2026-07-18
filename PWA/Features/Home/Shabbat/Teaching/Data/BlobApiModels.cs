namespace PWA.Features.Home.Shabbat.Teaching.Data;

public record BlobInfoRequest(string BlobName);

// Must stay aligned with Api.Models.BlobInfoResponse (no CurrentReading on the function).
public record BlobInfoResponse(
    bool Exists,
    BlobInfo? BlobInfo,
    string Message,
    bool IsTransient = false);

/*
IsTransient:  
- true: The error is temporary (network glitch, service throttling, timeout). Retry might succeed.
- false: The error is permanent (bad request, not found, configuration error). Retry won't help.
*/

public record BlobInfo(string Name, string Url, long SizeBytes);

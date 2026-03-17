namespace Api.Models;

public record BlobOperationResult(
    bool IsSuccess,
    string Message,
    string? ErrorDetails = null,
    bool IsTransient = false);

public record BlobOperationResult<T>(
    bool IsSuccess,
    string Message,
    T? Data = default,
    string? ErrorDetails = null,
    bool IsTransient = false);

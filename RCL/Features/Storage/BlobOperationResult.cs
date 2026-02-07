namespace RCL.Features.Storage;

public record BlobOperationResult(
    bool IsSuccess,
    string Message,
    Exception? Exception = null,
    bool IsTransient = false)
{
    public static BlobOperationResult Success(string message) =>
        new(true, message);

    public static BlobOperationResult Failure(string message, Exception? ex = null, bool isTransient = false) =>
        new(false, message, ex, isTransient);
}

public record BlobOperationResult<T>(
    bool IsSuccess,
    string Message,
    T? Data = default,
    Exception? Exception = null,
    bool IsTransient = false)
{
    public static BlobOperationResult<T> Success(T data, string message) =>
        new(true, message, data);

    public static BlobOperationResult<T> Failure(string message, Exception? ex = null, bool isTransient = false) =>
        new(false, message, default, ex, isTransient);
}
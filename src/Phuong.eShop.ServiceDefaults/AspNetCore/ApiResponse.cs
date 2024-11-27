namespace Phuong.eShop.ServiceDefaults.AspNetCore;

public class ApiResponse<TValue>
{
    private ApiResponse() { }

    public TValue? Value { get; private set; }
    public bool IsSuccess { get; private set; }
    public ApiError? Error { get; private set; }

    public static ApiResponse<TValue> Success(TValue value) => new()
    {
        IsSuccess = true,
        Value = value
    };

    public static ApiResponse<TValue> Failure(ApiError error) => new()
    {
        IsSuccess = false,
        Error = error
    };

    public static implicit operator ApiResponse<TValue>(ApiError error) => Failure(error);

    public static implicit operator ApiResponse<TValue>(TValue value) => Success(value);
}

public sealed record ApiError(string Code, string Description)
{
    public static readonly ApiError InternalServerError = new("InternalServerError", "An unhandled error occurred.");
    public static readonly ApiError BadRequest = new("BadRequest", "The request is invalid.");
    public static readonly ApiError Unauthorized = new("Unauthorized", "The request is unauthorized.");

    public static ApiError NotFound(string message = "The specified resource was not found.") => new("NotFound", message);
}

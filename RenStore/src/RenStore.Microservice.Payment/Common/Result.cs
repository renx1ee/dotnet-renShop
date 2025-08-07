namespace RenStore.Microservice.Payment.Common;

public readonly struct Result<T>
{
    public readonly T Value;
    public readonly string Error;
    public readonly bool IsSuccess;

    private Result(T value, string error, bool isSuccess)
    {
        Value = value;
        Error = error;
        IsSuccess = isSuccess;
    }

    public static Result<T> Success(T value) => new(value, null, true);
    public static Result<T> Failure(string error) => new(default, error, false);
}
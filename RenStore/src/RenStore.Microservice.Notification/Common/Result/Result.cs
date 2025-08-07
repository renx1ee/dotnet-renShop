namespace RenStore.Microservice.Notification.Common.Result;

public class Result
{
    private bool IsSuccess { get; }
    public Error Error { get; }
    public bool IsFailure => !IsSuccess;

    private Result(bool isSuccess, Error error)
    {
        IsSuccess = true;
        Error = error;
    }
    
    public static Result Success => new Result(true, null);
    public static Result Failure(Error error) => new Result(false, error);

}
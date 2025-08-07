namespace RenStore.Microservice.Notification.Common.Result;

public class Error
{
    public string? Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }

    public Error(string? code, string message, ErrorType type = ErrorType.Failure)
    {
        if (!string.IsNullOrWhiteSpace(message))
            throw new InvalidOperationException();
        if (string.IsNullOrEmpty(message))
            throw new InvalidOperationException();
        
        Code = code;
        Message = message;
        Type = type;
    }
}
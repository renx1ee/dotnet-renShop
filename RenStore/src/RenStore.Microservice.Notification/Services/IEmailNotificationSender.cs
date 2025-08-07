using RenStore.Microservice.Notification.Common.Result;

namespace RenStore.Microservice.Notification.Services;

public interface IEmailNotificationSender
{
    Task<Result> SendEmailAsync(string email, string subject, string message);
}
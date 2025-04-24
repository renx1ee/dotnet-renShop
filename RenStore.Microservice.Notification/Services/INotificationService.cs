using RenStore.Microservice.Notification.Common.Result;
using RenStore.Microservice.Notification.Enums;

namespace RenStore.Microservice.Notification.Services;
/// <summary>
/// Service for working with entity Notification.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Send by User Email.
    /// </summary>
    /// <param name="userId">User Id.</param>
    /// <param name="email">User email.</param>
    /// <param name="subject">Message Title.</param>
    /// <param name="body">Message.</param>
    /// <returns>Returns result. If result is success return Success else return Failure.</returns>
    Task<Result> SendEmailAsync(Guid userId, string email, string subject, string body);
    Task SendSmsAsync(string phoneNumber, string message);
    Task SendPushAsync(string deviceToken, string message);
    Task<Result> UpdateStatusAsync(Guid userId, NotificationStatus status);
    Task<IList<Models.Notification>> GetAllAsync();
}
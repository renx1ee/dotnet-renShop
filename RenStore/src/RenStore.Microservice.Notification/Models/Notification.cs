using RenStore.Microservice.Notification.Enums;

namespace RenStore.Microservice.Notification.Models;

public class Notification
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public NotificationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReadAt { get; set; }
}
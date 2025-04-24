using RenStore.Microservice.Notification.Enums;

namespace RenStore.Microservice.Notification.Models;

public class Message
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SenderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    public MessageStatus Status { get; set; }
    public Guid ChatId { get; set; }
}
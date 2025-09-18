using RenStore.Microservice.Notification.Enums;

namespace RenStore.Microservice.Notification.DTOs;

public class MessageRequestDto
{
    public Guid SenderId { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public DateTime CreatedAt { get; set; }
    public MessageStatus Status { get; set; }
}
using RenStore.Microservice.Notification.Enums;

namespace RenStore.Microservice.Notification.DTOs;

public class UpdateMessageRequestDto
{
    public Guid SenderId { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public DateTime UpdatedAt { get; set; }
    public MessageStatus Status { get; set; }
}
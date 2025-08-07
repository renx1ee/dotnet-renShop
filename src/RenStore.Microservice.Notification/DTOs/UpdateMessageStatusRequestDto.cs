using RenStore.Microservice.Notification.Enums;

namespace RenStore.Microservice.Notification.DTOs;

public class UpdateMessageStatusRequestDto
{
    public MessageStatus MessageStatus { get; set; }
}
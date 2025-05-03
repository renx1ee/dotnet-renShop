namespace RenStore.Microservice.Notification.DTOs;

public record UpdateNotificationStatusRequestDto
{
    public GCNotificationStatus Status { get; set; } 
}
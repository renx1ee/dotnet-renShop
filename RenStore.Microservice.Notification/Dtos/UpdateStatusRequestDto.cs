namespace RenStore.Microservice.Notification.Dtos;

public record UpdateStatusRequestDto
{
    public GCNotificationStatus Status { get; set; } 
}
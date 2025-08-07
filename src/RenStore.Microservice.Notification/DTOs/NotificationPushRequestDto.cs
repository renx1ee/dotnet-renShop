namespace RenStore.Microservice.Notification.DTOs;

public record NotificationPushRequestDto
(
    Guid UserId,
    string To,
    string Subject, 
    string Body
);
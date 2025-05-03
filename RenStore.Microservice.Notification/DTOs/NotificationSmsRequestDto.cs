namespace RenStore.Microservice.Notification.DTOs;

public record NotificationSmsRequestDto
(
    Guid UserId,
    string To,
    string Subject, 
    string Body
);
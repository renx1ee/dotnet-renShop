namespace RenStore.Microservice.Notification.DTOs;

public record NotificationEmailRequestDto
(
    Guid UserId,
    string To,
    string Subject, 
    string Body
);
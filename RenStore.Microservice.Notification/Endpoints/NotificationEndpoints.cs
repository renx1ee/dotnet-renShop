using Microsoft.AspNetCore.Mvc;
using RenStore.Microservice.Notification.DTOs;
using RenStore.Microservice.Notification.Services;

namespace RenStore.Microservice.Notification.Endpoints;

public static class NotificationEndpoints
{
    public static IEndpointRouteBuilder MapNotificationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/notification");

        group.MapPost("/notification/email", SendEmailAsync);
        group.MapGet("/notifications", GetAllAsync);
        group.MapGet("/notifications/{userId:guid}", GetByUserIdAsync);

        return group;
    }

    public static async Task<IResult> SendEmailAsync(
        [FromBody] NotificationEmailRequestDto request,
        INotificationService notificationService)
    {
        var result = await notificationService.SendEmailAsync(
            userId: request.UserId, 
            email: request.To, 
            subject: request.Subject, 
            body: request.Body); 
        
        return Results.NoContent();
    }

    public static async Task<IResult> GetAllAsync()
    {
        return Results.Ok();
    }
    
    public static async Task<IResult> GetByUserIdAsync()
    {
        return Results.Ok();
    }
}
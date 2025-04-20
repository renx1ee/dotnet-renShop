using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RenStore.Microservice.Notification.Dtos;
using RenStore.Microservice.Notification.Enums;
using RenStore.Microservice.Notification.Models;
using RenStore.Microservice.Notification.Services;

namespace RenStore.Microservice.Notification.Controllers;

[ApiController]
[ApiVersion(1, Deprecated = false)]
[Route("api/v{version:apiVersion}/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService notificationService;
    public NotificationController(INotificationService notificationService) => 
        this.notificationService = notificationService;

    [HttpPost]
    [MapToApiVersion(1)] 
    [Route("/api/v{version:apiVersion}/notification")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationRequestDto requestDto)
    {
        switch (requestDto.Type)
        {
            case NotificationType.Email:
                var result = await notificationService.SendEmailAsync(
                    userId: requestDto.UserId, 
                    email: requestDto.To, 
                    subject: requestDto.Subject, 
                    body: requestDto.Body); 
                if(result.IsFailure)
                    return BadRequest(result.Error);
                break;
            case NotificationType.Sms:
                await notificationService.SendSmsAsync(requestDto.To, requestDto.Body);
                break;
            case NotificationType.Push:
                await notificationService.SendPushAsync("", requestDto.Body);
                break;
            default:
                return BadRequest("");
        }
        return NoContent();
    }
    
    [HttpPatch]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/notifications/{notificationId:guid}")]
    public async Task<IActionResult> UpdateStatus([FromQuery] Guid notificationId, [FromBody] UpdateStatusRequestDto requestDto)
    {
        return Ok();
    }
    
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/notifications/{userId:guid}")]
    public async Task<IActionResult> GetNotificationsByUserId([FromQuery] Guid userId)
    {
        return Ok();
    }
}
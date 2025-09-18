using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RenStore.Microservice.Notification.DTOs;
using RenStore.Microservice.Notification.Services;

namespace RenStore.Microservice.Notification.Controllers;

[ApiController]
[ApiVersion(1, Deprecated = false)]
[Route("api/v{version:apiVersion}/[controller]")]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    [HttpPost]
    [MapToApiVersion(1)] 
    [Route("/api/v{version:apiVersion}/notification/email")]
    public async Task<IActionResult> SendEmail([FromBody] NotificationEmailRequestDto request)
    {
        var result = await notificationService.SendEmailAsync(
            userId: request.UserId, 
            email: request.To, 
            subject: request.Subject, 
            body: request.Body); 
        
        if(result.IsFailure) 
            return BadRequest(result.Error);
       
        return NoContent();
    }
    
    [HttpPost]
    [MapToApiVersion(1)] 
    [Route("/api/v{version:apiVersion}/notification/sms")]
    public async Task<IActionResult> SendSms([FromBody] NotificationSmsRequestDto request)
    {
        // TODO: сделать отправку sms 
        return NoContent();
    }
    
    [HttpPost]
    [MapToApiVersion(1)] 
    [Route("/api/v{version:apiVersion}/notification/push")]
    public async Task<IActionResult> SendPush([FromBody] NotificationPushRequestDto request)
    {
        // TODO: сделать отправку push уведомлений
        return NoContent();
    }
    
    [HttpPatch]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/notifications/push/status/{id:guid}")]
    public async Task<IActionResult> UpdatePushStatus([FromQuery] Guid id, [FromBody] UpdateNotificationStatusRequestDto requestDto)
    {
        // TODO: сделать обновление статуса push 
        return Ok();
    }
    
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/notifications/{userId:guid}")]
    public async Task<IActionResult> GetNotificationsByUserId(Guid userId)
    {
        var result = await notificationService.GetByUserIdAsync(userId);
        if(result is null)
            return NotFound();
        
        return Ok(result);
    }
    
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/notifications")]
    public async Task<IActionResult> GetAll()
    {
        var result = await notificationService.GetAllAsync();
        if(result is null)
            return NotFound();
        
        return Ok(result);
    }
}
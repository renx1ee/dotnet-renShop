using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RenStore.Microservice.Notification.DTOs;

namespace RenStore.Microservice.Notification.Controllers;

[ApiController]
[ApiVersion(1, Deprecated = false)]
[Route("api/v{version:apiVersion}/[controller]")]
public class MessageController : ControllerBase
{
    [HttpPost]
    [MapToApiVersion(1)] 
    [Route("/api/v{version:apiVersion}/message")]
    public async Task<IActionResult> SendMessage([FromBody] MessageRequestDto request)
    {
        return NoContent();
    }
    
    [HttpPatch]
    [MapToApiVersion(1)] 
    [Route("/api/v{version:apiVersion}/message/{messageId:guid}")]
    public async Task<IActionResult> UpdateMessage(Guid messageId, [FromBody] UpdateMessageRequestDto request)
    {
        return NoContent();
    }
    
    [HttpPatch]
    [MapToApiVersion(1)] 
    [Route("/api/v{version:apiVersion}/message/status/{messageId:guid}")]
    public async Task<IActionResult> UpdateStatusMessage([FromBody] UpdateMessageStatusRequestDto request)
    {
        return NoContent();
    }
    
    [HttpDelete]
    [MapToApiVersion(1)] 
    [Route("/api/v{version:apiVersion}/message/{messageId:guid}")]
    public async Task<IActionResult> DeleteMessage([FromBody] Guid messageId)
    {
        return NoContent();
    }
    
    [HttpGet]
    [MapToApiVersion(1)] 
    [Route("/api/v{version:apiVersion}/messages")]
    public async Task<IActionResult> GetAllMessages()
    {
        return NoContent();
    }
    
    [HttpGet]
    [MapToApiVersion(1)] 
    [Route("/api/v{version:apiVersion}/messages/{chatId:guid}")]
    public async Task<IActionResult> GetByChatIdMessages([FromBody] Guid chatId)
    {
        return NoContent();
    }
}
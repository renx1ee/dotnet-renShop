using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RenStore.Application.Features.Review.Commands.Moderate;
using RenStore.Application.Features.Review.Queries.GetAllForModeration;
using RenStore.Domain.Dto.Review;

namespace RenStore.WebApi.Controllers;

[ApiController]
[ApiVersion(1, Deprecated = false)]
[Route("api/v{version:apiVersion}/[controller]")]
public class ModerationController : BaseController
{
    [HttpPost]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/moderate")]
    public async Task<IActionResult> ModerateReview([FromBody] ModerateReviewDto dto)
    {
        await Mediator.Send(new ModerateReviewCommand()
        {
            Approve = dto.Approve,
            ReviewId = dto.ReviewId
        });
        
        return Accepted();
    }
    
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("/api/v{version:apiVersion}/moderate")]
    public async Task<IActionResult> GetReviews()
    {
        var reviews = 
            await Mediator.Send(new GetAllForModerationRequest());

        if (reviews is null)
            return NotFound();
        
        return Ok(reviews);
    }
}
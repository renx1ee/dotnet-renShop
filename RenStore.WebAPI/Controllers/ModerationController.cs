using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RenStore.Application.Services;
using RenStore.Domain.Dto.Review;

namespace RenStore.WebApi.Controllers;

[ApiController]
[ApiVersion(1, Deprecated = false)]
[Route("api/v{version:apiVersion}/[controller]")]
public class ModerationController : BaseController
{
    private readonly ReviewService reviewService;
    public ModerationController(ReviewService reviewService)
    {
        this.reviewService = reviewService;
    }
    
    [HttpPost]
    [MapToApiVersion(1)]
    [Route("api/v{version:apiVersion}/moderate")]
    public async Task<IActionResult> ModerateReview(Guid id, [FromBody] ModerateReviewDto dto)
    {
        return Ok();
    }
    
    [HttpGet]
    [MapToApiVersion(1)]
    [Route("api/v{version:apiVersion}/moderate")]
    public async Task<IActionResult> GetReviews()
    {
        return Ok();
    }
}
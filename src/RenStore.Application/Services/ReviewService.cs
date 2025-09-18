using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;
using RenStore.Domain.Enums;

namespace RenStore.Application.Services;

public class ReviewService
{
    private readonly ILogger<ReviewService> logger;
    private readonly IReviewRepository reviewRepository;
    public ReviewService(
        ILogger<ReviewService> logger,
        IReviewRepository reviewRepository)
    {
        this.logger = logger;
        this.reviewRepository = reviewRepository;
    }
    
    
    public async Task LikeReviewAsync(Guid reviewId, string userId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Like review method is starting");
        
        try
        {
            if (await CheckUserLikedByIdAsync(userId)) 
                await reviewRepository.LikeAsync(reviewId, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError($"Like review method error. Review Id: {reviewId}, User Id: {userId}, Error: {ex.Message}");
        }
        
        logger.LogInformation("Like review method is stopped.");
    }

    public async Task<string> UsersLikedAsync()
    {
        return null;
    }
    
    public async Task<bool> CheckUserLikedByIdAsync(string userId)
    {
        return false;
    }

    public async Task SubmitForModerationAsync(Guid reviewId)
    {
    }
    
    public async Task ModerationReviewAsync(
        Domain.Entities.Review review, 
        ReviewStatus status, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Moderation review method is starting");

        try
        {
            review.Status = status;
            review.ModeratedAt = DateTime.UtcNow;
            await reviewRepository.UpdateAsync(review, cancellationToken);

            if (review.Status == ReviewStatus.Published)
                return;

        }
        catch (Exception ex)
        {
            logger.LogError($"Moderation review method error. Review Id: {review.Id}, Error: {ex.Message}");
        }
        
        logger.LogError($"Moderation review method is stopped.");
    }
}
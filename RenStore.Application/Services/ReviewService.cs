using RenStore.Application.Repository;
using RenStore.Domain.Enums;

namespace RenStore.Application.Services;

public class ReviewService
{
    private readonly IReviewRepository reviewRepository;
    public ReviewService(IReviewRepository reviewRepository) => 
        this.reviewRepository = reviewRepository;
    
    public async Task LikeReviewAsync(Guid reviewId, string userId, CancellationToken cancellationToken)
    {
        var check = await CheckUserLikedByIdAsync(userId);
        
        if (check)
            await reviewRepository.LikeAsync(reviewId, cancellationToken);
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
        review.Status = status;
        review.ModeratedAt = DateTime.UtcNow;
        await reviewRepository.UpdateAsync(review, cancellationToken);
    }
}
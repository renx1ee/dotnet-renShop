using RenStore.Application.Repository;

namespace RenStore.Application.Services.Review;

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
    
    public async Task<bool> ModerationReview(Domain.Entities.Review review, CancellationToken cancellationToken)
    {
        // Moderation realization

        review.IsApproved = true;
        review.ModeratedAt = DateTime.UtcNow;

        await reviewRepository.UpdateAsync(review, cancellationToken);
        
        return true;
    }
}
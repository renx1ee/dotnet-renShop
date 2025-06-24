using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Review.Queries.GetAllForModeration;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.Review.Queries.GetAllForModeration;

public class GetAllForModerationRequestHandler
    : IRequestHandler<GetAllForModerationRequest,
        IEnumerable<GetAllReviewsForModerationVm>>
{
    private readonly ILogger<GetAllForModerationRequestHandler> logger;
    private readonly IReviewRepository reviewRepository;

    public GetAllForModerationRequestHandler(
        ILogger<GetAllForModerationRequestHandler> logger,
        IReviewRepository reviewRepository)
    {
        this.logger = logger;
        this.reviewRepository = reviewRepository;
    }
    
    public async Task<IEnumerable<GetAllReviewsForModerationVm>> Handle(
        GetAllForModerationRequest request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Get reviews for moderation handler starting.");

        try
        {
            var result = await reviewRepository.GetAllForModerationAsync(cancellationToken);

            if (result is not null)
            {
                return result.Select(review => 
                    new GetAllReviewsForModerationVm(
                        review.Id,
                        review.CreatedDate,
                        review.LastUpdatedDate,
                        review.IsUpdated,
                        review.Message,
                        review.Rating,
                        review.Status,
                        review.ImagesUrls,
                        review.LikesCount,
                        review.ApplicationUserId,
                        review.ProductId));    
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Get All Reviews for moderation handler error. Error: {ex.Message}");
            
        }
        
        logger.LogInformation("Get reviews for moderation handler stopped.");
        
        return null;
    }
}
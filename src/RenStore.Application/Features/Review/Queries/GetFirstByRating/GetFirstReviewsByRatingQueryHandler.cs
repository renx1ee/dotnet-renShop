using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;

namespace RenStore.Application.Features.Review.Queries.GetFirstByRating;

public class GetFirstReviewsByRatingQueryHandler
    : IRequestHandler<GetFirstReviewsByRatingQuery, IList<GetFirstReviewsByRatingVm>>
{
    private ILogger<GetFirstReviewsByRatingQueryHandler> logger;
    private readonly IReviewRepository reviewRepository;

    public GetFirstReviewsByRatingQueryHandler(
        ILogger<GetFirstReviewsByRatingQueryHandler> logger,
        IReviewRepository reviewRepository)
    {
        this.logger = logger;
        this.reviewRepository = reviewRepository;
    }
    
    public async Task<IList<GetFirstReviewsByRatingVm>> Handle(GetFirstReviewsByRatingQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetFirstReviewsByRatingQueryHandler)}");
        
        var items = await reviewRepository.GetFirstByRatingAsync(request.Count, request.ProductId, cancellationToken);
        
        var result = items
            .Select(review => 
                new GetFirstReviewsByRatingVm(
                    review.Id,
                    review.CreatedDate,
                    review.LastUpdatedDate,
                    review.IsUpdated,
                    review.Message,
                    review.Rating,
                    review.ImagesUrls,
                    review.LikesCount,
                    review.ApplicationUserId,
                    review.ProductId))
            .ToList();
        
        logger.LogInformation($"Handled {nameof(GetFirstReviewsByRatingQueryHandler)}");

        return result;
    }
}
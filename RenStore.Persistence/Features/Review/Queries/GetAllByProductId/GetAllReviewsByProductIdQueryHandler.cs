using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Review.Queries.GetAllByProductId;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.Review.Queries.GetAllByProductId;

public class GetAllReviewsByProductIdQueryHandler
    : IRequestHandler<GetAllReviewsByProductIdQuery, IList<GetAllReviewsByProductIdVm>>
{
    private ILogger<GetAllReviewsByProductIdQueryHandler> logger;
    private readonly IReviewRepository reviewRepository;

    public GetAllReviewsByProductIdQueryHandler(
        ILogger<GetAllReviewsByProductIdQueryHandler> logger,
        IReviewRepository reviewRepository)
    {
        this.logger = logger;
        this.reviewRepository = reviewRepository;
    }
    
    public async Task<IList<GetAllReviewsByProductIdVm>> Handle(GetAllReviewsByProductIdQuery request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetAllReviewsByProductIdQueryHandler)}");

        var items = await reviewRepository
            .GetByProductIdAsync(request.ProductId, cancellationToken);
        
        var result = items
            .Select(review => 
                new GetAllReviewsByProductIdVm(
                    review.Id,
                    review.CreatedDate,
                    review.LastUpdatedDate,
                    review.IsUpdated,
                    review.Message,
                    review.Rating,
                    review.ImagesUrls,
                    review.LikesCount,
                    review.ApplicationUserId,
                    review.ProductId)
            )
            .ToList();
        
        logger.LogInformation($"Handled {nameof(GetAllReviewsByProductIdQueryHandler)}");

        return result;
    }
}
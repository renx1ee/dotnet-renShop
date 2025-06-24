using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Review.Queries.GetFirstByCreatedDate;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.Review.Queries.GetFirstByCreatedDate;

public class GetFirstReviewsByCreatedDateQueryHandler
    : IRequestHandler<GetFirstReviewsByDateQuery, IList<GetFirstReviewsByDateVm>>
{
    private ILogger<GetFirstReviewsByCreatedDateQueryHandler> logger;
    private readonly IReviewRepository reviewRepository;

    public GetFirstReviewsByCreatedDateQueryHandler(
        ILogger<GetFirstReviewsByCreatedDateQueryHandler> logger,
        IReviewRepository reviewRepository)
    {
        this.logger = logger;
        this.reviewRepository = reviewRepository;
    }
    
    public async Task<IList<GetFirstReviewsByDateVm>> Handle(GetFirstReviewsByDateQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetFirstReviewsByCreatedDateQueryHandler)}");
        
        var items = await reviewRepository.GetFirstByDateAsync(request.Count, request.ProductId, cancellationToken);
        
        var result = items
            .Select(review => 
                new GetFirstReviewsByDateVm(
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
        
        logger.LogInformation($"Handled {nameof(GetFirstReviewsByCreatedDateQueryHandler)}");

        return result;
    }
}
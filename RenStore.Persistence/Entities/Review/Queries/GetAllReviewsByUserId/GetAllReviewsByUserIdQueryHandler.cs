using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RenStore.Application.Entities.Review.Queries.GetAllReviewsByUserId;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Entities.Review.Queries.GetAllReviewsByUserId;

public class GetAllReviewsByUserIdQueryHandler 
    : IRequestHandler<GetAllReviewsByUserIdQuery, IList<GetAllReviewsByUserIdVm>>
{
    private ILogger<GetAllReviewsByUserIdQueryHandler> logger;
    private readonly IReviewRepository reviewRepository;

    public GetAllReviewsByUserIdQueryHandler(
        ILogger<GetAllReviewsByUserIdQueryHandler> logger,
        IReviewRepository reviewRepository)
    {
        this.logger = logger;
        this.reviewRepository = reviewRepository;
    }
    
    public async Task<IList<GetAllReviewsByUserIdVm>> Handle(GetAllReviewsByUserIdQuery request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetAllReviewsByUserIdQueryHandler)}");
        
        var items = await reviewRepository
            .GetByUserIdAsync(
                status: request.Status, 
                userId: request.UserId, 
                cancellationToken: cancellationToken);
        
        var result = items
            .Select(review => 
                new GetAllReviewsByUserIdVm(
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
        
        logger.LogInformation($"Handled {nameof(GetAllReviewsByUserIdQueryHandler)}");

        return result;
    }
}
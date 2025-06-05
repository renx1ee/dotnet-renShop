using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Entities.Review.Commands.Moderate;
using RenStore.Application.Repository;
using RenStore.Application.Services;
using RenStore.Domain.Enums;

namespace RenStore.Persistence.Entities.Review.Commands.Moderate;

public class ModerateReviewCommandHandler : IRequestHandler<ModerateReviewCommand>
{
    private readonly ILogger<ModerateReviewCommandHandler> logger;
    private readonly ReviewService reviewService;
    private readonly IReviewRepository reviewRepository;

    public ModerateReviewCommandHandler(
        ILogger<ModerateReviewCommandHandler> logger,
        ReviewService reviewService,
        IReviewRepository reviewRepository)
    {
        this.logger = logger;
        this.reviewService = reviewService;
        this.reviewRepository = reviewRepository;
    }
    
    public async Task Handle(ModerateReviewCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Moderation handler is starting");

        try
        {
            var review = await reviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);

            if (request.Approve)
            {
                await reviewService.ModerationReviewAsync(
                    review: review!, 
                    status: ReviewStatus.Published, 
                    cancellationToken: cancellationToken);
            }
            else
            {
                await reviewService.ModerationReviewAsync(
                    review: review!, 
                    status: ReviewStatus.Rejected, 
                    cancellationToken: cancellationToken);
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Moderation Handler Error! Review Id: {request.ReviewId} Error message: {ex.Message}");
        }
        
        logger.LogInformation("Moderation handler is stopped");    
    }
}
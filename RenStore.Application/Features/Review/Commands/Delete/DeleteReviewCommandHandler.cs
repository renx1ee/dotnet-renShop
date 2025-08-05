using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Review.Commands.Delete;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.Review.Commands.Delete;

public class DeleteReviewCommandHandler
    : IRequestHandler<DeleteReviewCommand>
{
    private ILogger<DeleteReviewCommandHandler> logger;
    private IReviewRepository reviewRepository;

    public DeleteReviewCommandHandler(
        ILogger<DeleteReviewCommandHandler> logger,
        IReviewRepository reviewRepository)
    {
        this.logger = logger;
        this.reviewRepository = reviewRepository;
    }
    
    public async Task Handle(DeleteReviewCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(DeleteReviewCommandHandler)}");
        
        await reviewRepository.DeleteAsync(request.Id, cancellationToken);
        
        logger.LogInformation($"Handled {nameof(DeleteReviewCommandHandler)}");
    }
}
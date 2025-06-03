using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Entities.Review.Commands.Update;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Entities.Review.Commands.Update;

public class UpdateReviewCommandHandler 
    : IRequestHandler<UpdateReviewCommand>
{
    private ILogger<UpdateReviewCommandHandler> logger;
    private IReviewRepository reviewRepository;

    public UpdateReviewCommandHandler(
        ILogger<UpdateReviewCommandHandler> logger,
        IReviewRepository reviewRepository)
    {
        this.logger = logger;
        this.reviewRepository = reviewRepository;
    }
    
    public async Task Handle(UpdateReviewCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(UpdateReviewCommandHandler)}");
        
        var review = await reviewRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if(review.ApplicationUserId != request.ApplicationUserId.ToString())
            throw new NotFoundException(typeof(Domain.Entities.Review), request.ApplicationUserId);
        
        review.LastUpdatedDate = DateTime.UtcNow;
        review.IsUpdated = true;
        review.Message = request.Message;
        review.Rating = request.Rating;
        review.ImagesUrls = request.ImagesUrls;
        
        await reviewRepository.UpdateAsync(review, cancellationToken);
        
        logger.LogInformation($"Handled {nameof(UpdateReviewCommandHandler)}");
    }
}
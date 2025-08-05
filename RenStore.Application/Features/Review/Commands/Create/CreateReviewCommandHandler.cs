using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Review.Commands.Create;
using RenStore.Application.Repository;
using RenStore.Application.Services;
using RenStore.Domain.Enums;

namespace RenStore.Persistence.Features.Review.Commands.Create;

public class CreateReviewCommandHandler
    : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly IMapper mapper;
    private ILogger<CreateReviewCommandHandler> logger;
    private IReviewRepository reviewRepository;
    private ReviewService reviewService;
    private ProductService productService; 

    public CreateReviewCommandHandler(
        IMapper mapper, 
        ILogger<CreateReviewCommandHandler> logger,
        IReviewRepository reviewRepository,
        ReviewService reviewService,
        ProductService productService)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.reviewRepository = reviewRepository;
        this.reviewService = reviewService;
        this.productService = productService;
    }
    
    public async Task<Guid> Handle(CreateReviewCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(CreateReviewCommandHandler)}");
        
        var userReviewExist = 
            await reviewRepository.CheckExistByUserIdAsync(
                userId: request.ApplicationUserId, 
                productId: request.ProductId,
                cancellationToken: cancellationToken);

        if (userReviewExist) return Guid.Empty; 
        
        var review = mapper.Map<Domain.Entities.Review>(request);
        review.CreatedDate = DateTime.UtcNow;
        review.LastUpdatedDate = null;
        review.IsUpdated = false; 
        review.Status = ReviewStatus.SentForModeration;
        
        var result = await reviewRepository.CreateAsync(review, cancellationToken);

        if (result.Equals(Guid.Empty))
            throw new Exception("Error with create a review.");

        var newRating = await productService
            .CalculateProductRatingAsync(
                productId: request.ProductId,
                cancellationToken: cancellationToken);
        
        /*await reviewService.SubmitForModerationAsync(review.Id);
        
        var moderationResult = await reviewService.ModerationReviewAsync(review, cancellationToken);
        
        await productService.CalculateProductRatingAsync(request.ProductId, cancellationToken);*/
        
        logger.LogInformation($"Handled {nameof(CreateReviewCommandHandler)}");
        
        return result;
    }
}
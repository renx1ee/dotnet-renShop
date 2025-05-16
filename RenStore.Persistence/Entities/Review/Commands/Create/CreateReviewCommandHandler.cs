using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.BackgroundServices;
using RenStore.Application.Entities.Review.Commands.Create;
using RenStore.Application.Repository;
using RenStore.Application.Services.Review;

namespace RenStore.Persistence.Entities.Review.Commands.Create;

public class CreateReviewCommandHandler
    : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly IMapper mapper;
    private ILogger<CreateReviewCommandHandler> logger;
    private IReviewRepository reviewRepository;
    private IProductRepository productRepository;
    private ReviewService reviewService;
    private CalculateProductRatingBg calculateService; 

    public CreateReviewCommandHandler(IMapper mapper, 
        ILogger<CreateReviewCommandHandler> logger,
        IReviewRepository reviewRepository,
        ReviewService reviewService,
        IProductRepository productRepository,
        CalculateProductRatingBg calculateService)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.reviewRepository = reviewRepository;
        this.productRepository = productRepository;
        this.reviewService = reviewService;
        this.calculateService = calculateService;
    }
    
    public async Task<Guid> Handle(CreateReviewCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(CreateReviewCommandHandler)}");

        var userReviewExist = 
            await reviewRepository.FindByUserIdAsync(
                userId: request.ApplicationUserId, 
                cancellationToken: cancellationToken);

        if (userReviewExist is not null)
            return Guid.Empty;
        
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        
        var review = mapper.Map<Domain.Entities.Review>(request);
        review.CreatedDate = DateTime.UtcNow;
        review.LastUpdatedDate = null;
        review.IsUpdated = false;
        review.IsApproved = false;
        
        var result = await reviewRepository.CreateAsync(review, cancellationToken);
        
        var moderationResult = await reviewService.ModerationReviewAsync(review, cancellationToken);

        if (moderationResult)
        {
            review.IsApproved = true;
            review.ModeratedAt = DateTime.UtcNow;
            await reviewRepository.UpdateAsync(review, cancellationToken);
        }
        else
        {
            return Guid.Empty;
        }
        
        // TODO: Send push a message that a review has been added 
        
        logger.LogInformation($"Handled {nameof(CreateReviewCommandHandler)}");
        
        return result;
    }
}
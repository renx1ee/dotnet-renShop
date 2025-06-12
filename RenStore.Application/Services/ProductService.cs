using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;

namespace RenStore.Application.Services;

public class ProductService
{
    private readonly ILogger<ProductService> logger;
    private readonly IReviewRepository reviewRepository;
    private readonly IProductRepository productRepository;

    public ProductService(
        ILogger<ProductService> logger,
        IReviewRepository reviewRepository,
        IProductRepository productRepository)
    {
        this.logger = logger;
        this.reviewRepository = reviewRepository;
        this.productRepository = productRepository;
    }
    
    public async Task<decimal> CalculateDiscountByPriceAsync(decimal price, decimal oldPrice)
    {
        return (price / oldPrice) * 100;
    }
    
    public async Task<uint> CreateArticleAsync()
    {
        Random random = new Random();
        return (uint)random.Next(100000000, 999999999);
    }

    public async Task<decimal> CalculateProductRatingAsync(Guid productId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Calculate Product Rating method is starting.");

        try
        {
            var reviews = await reviewRepository.GetByProductIdAsync(productId, cancellationToken);
        
            var product = await productRepository.GetByIdAsync(productId, cancellationToken);

            var averageRating = reviews.Average(r => r.Rating);
        
            product.Rating = (double)averageRating;

            await productRepository.UpdateAsync(product, cancellationToken);
            
            logger.LogInformation("Calculate Product Rating method is stopped.");
            
            return Math.Round(averageRating, 1);
        }
        catch (Exception ex)
        {
            logger.LogError($"Calculate Product Rating method error. ProductId: {productId}, Error: {ex.Message}");
            throw;
        }
    }

    ~ProductService()
    {
        Console.WriteLine("Диструктор завершен!");
    }
}
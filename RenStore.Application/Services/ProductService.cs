using RenStore.Application.Repository;

namespace RenStore.Application.Services;

public class ProductService
{
    private readonly IReviewRepository reviewRepository;
    private readonly IProductRepository productRepository;

    public ProductService(IReviewRepository reviewRepository,
        IProductRepository productRepository)
    {
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
        var reviews = await reviewRepository.GetByProductIdAsync(productId, cancellationToken);
        
        var product = await productRepository.GetByIdAsync(productId, cancellationToken);

        var averageRating = reviews.Average(r => r.Rating);
        
        product.Rating = (double)averageRating;

        await productRepository.UpdateAsync(product, cancellationToken);
        
        return Math.Round(averageRating, 1);
    }
}
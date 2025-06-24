using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Product.Queries.GetSortedByRating;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.Product.Queries.GetSortedByRating;

public class GetSortedProductsByRatingQueryHandler 
    : IRequestHandler<GetSortedProductsByRatingQuery, IList<GetSortedProductsByRatingVm>>
{
    private readonly ILogger<GetSortedProductsByRatingQueryHandler> logger;
    private readonly IProductRepository productRepository;

    public GetSortedProductsByRatingQueryHandler(
        ILogger<GetSortedProductsByRatingQueryHandler> logger, 
        IProductRepository productRepository)
    {
        this.logger = logger;
        this.productRepository = productRepository;
    }
    
    public async Task<IList<GetSortedProductsByRatingVm>> Handle(GetSortedProductsByRatingQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetSortedProductsByRatingQueryHandler)}");
        
        var items = await productRepository.FindSortedByRatingAsync(
            cancellationToken: cancellationToken, 
            descending: request.Descending);

        var result = items.Select(product =>
                new GetSortedProductsByRatingVm(
                    product.Id,
                    product.ProductName, 
                    product.Price,
                    product.Rating,
                    product.ImageMiniPath,
                    product.SellerName))
            .ToList();
        
        logger.LogInformation($"Handled {nameof(GetSortedProductsByRatingQueryHandler)}");
        
        return result;
    }
}
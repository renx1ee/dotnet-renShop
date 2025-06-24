using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Product.Queries.GetSortedByPrice;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.Product.Queries.GetSortedByPrice;

public class GetSortedProductsByPriceQueryHandler
    : IRequestHandler<GetSortedProductsByPriceQuery, IList<GetSortedProductsByPriceVm>>
{
    private readonly ILogger<GetSortedProductsByPriceQueryHandler> logger;
    private readonly IProductRepository productRepository;

    public GetSortedProductsByPriceQueryHandler(
        ILogger<GetSortedProductsByPriceQueryHandler> logger, 
        IProductRepository productRepository)
    {
        this.logger = logger;
        this.productRepository = productRepository;
    }
    
    public async Task<IList<GetSortedProductsByPriceVm>> Handle(GetSortedProductsByPriceQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetSortedProductsByPriceQueryHandler)}");
        
        var items = await productRepository.FindSortedByPriceAsync(
            cancellationToken: cancellationToken, 
            maxPrice: request.MaxPrice, 
            minPrice: request.MinPrice, 
            descending: request.Descending);

        var result = items.Select(product =>
            new GetSortedProductsByPriceVm(
                product.Id,
                product.ProductName, 
                product.Price,
                product.Rating,
                product.ImageMiniPath,
                product.SellerName))
            .ToList();
        
        logger.LogInformation($"Handled {nameof(GetSortedProductsByPriceQueryHandler)}");
        
        return result;
    }
}
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Product.Queries.GetMinimizedProducts;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.Product.Queries.GetAllMinimizedProducts;

public class GetMiniProductListQueryHandler
    : IRequestHandler<GetMiniProductListQuery, List<ProductMiniLookupDto>>
{
    private readonly ILogger<GetMiniProductListQueryHandler> logger;
    private readonly IProductRepository productRepository;

    public GetMiniProductListQueryHandler(
        IProductRepository productRepository,
        ILogger<GetMiniProductListQueryHandler> logger)
    {
        this.productRepository = productRepository;
        this.logger = logger;
    }
    
    public async Task<List<ProductMiniLookupDto>> Handle(
        GetMiniProductListQuery query,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetMiniProductListQueryHandler)}");
        
        var items = await productRepository.GetAllAsync(cancellationToken);
            
        var result = items
            .Select(product => 
                new ProductMiniLookupDto(
                    product.Id,
                    product.ProductName,
                    product.Price,
                    product.Rating,
                    product.ImageMiniPath,
                    product.SellerName)
                )
            .ToList();
        
        logger.LogInformation($"Handled {nameof(GetMiniProductListQueryHandler)}");
        
        return result;
    }
}
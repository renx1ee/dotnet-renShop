using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Product.Queries.GetBySellerId;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.Product.Queries.GetBySellerId;

public class GetProductBySellerIdQueryHandler 
    : IRequestHandler<GetProductBySellerIdQuery, IList<GetProductBySellerIdVm>>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<GetProductByIdQueryHandler> logger;
    
    public GetProductBySellerIdQueryHandler(
        IProductRepository productRepository,
        ILogger<GetProductByIdQueryHandler> logger)
    {
        this.productRepository = productRepository;
        this.logger = logger;
    }
    
    public async Task<IList<GetProductBySellerIdVm>> Handle(GetProductBySellerIdQuery request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetProductBySellerIdQueryHandler)}");

        var products = await productRepository
            .FindBySellerIdAsync(request.SellerId, cancellationToken);
        
        var result = products
            .Select(product => 
                new GetProductBySellerIdVm(
                    product.Id,
                    product.ProductName,
                    product.Price,
                    product.Rating,
                    product.ImageMiniPath,
                    product.SellerName)
            )
            .ToList();
        
        logger.LogInformation($"Handled {nameof(GetProductBySellerIdQueryHandler)}");
        
        return result;
    }
}
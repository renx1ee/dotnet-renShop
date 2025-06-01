using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Entities.Product.Queries.GetByName;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Entities.Product.Queries.GetByName;

public class GetProductByNameQueryHandler
    : IRequestHandler<GetProductByNameQuery, IList<GetProductByNameVm>>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<GetProductByNameQueryHandler> logger;
    
    public GetProductByNameQueryHandler(
        IProductRepository productRepository,
        ILogger<GetProductByNameQueryHandler> logger)
    {
        this.productRepository = productRepository;
        this.logger = logger;
    }
    
    public async Task<IList<GetProductByNameVm>> Handle(GetProductByNameQuery request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetProductByNameQueryHandler)}");

        var products = await productRepository
            .SearchByNameAsync(request.Name, cancellationToken);
        
        var result = products
            .Select(product => 
                new GetProductByNameVm(
                    product.Id,
                    product.ProductName,
                    product.Price,
                    product.Rating,
                    product.ImageMiniPath,
                    product.SellerName))
            .ToList();
        
        logger.LogInformation($"Handled {nameof(GetProductByNameQueryHandler)}");
        
        return result;
    }
}
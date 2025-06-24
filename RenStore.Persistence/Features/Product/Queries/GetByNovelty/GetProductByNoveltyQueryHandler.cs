using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Product.Queries.GetByNovelty;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.Product.Queries.GetByNovelty;

public class GetProductByNoveltyQueryHandler
    : IRequestHandler<GetProductByNoveltyQuery, IList<GetProductByNoveltyVm>>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<GetProductByNoveltyQueryHandler> logger;
    
    public GetProductByNoveltyQueryHandler(
        IProductRepository productRepository,
        ILogger<GetProductByNoveltyQueryHandler> logger)
    {
        this.productRepository = productRepository;
        this.logger = logger;
    }
    
    public async Task<IList<GetProductByNoveltyVm>> Handle(GetProductByNoveltyQuery request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetProductByNoveltyQueryHandler)}");

        var products = await productRepository
            .FindSortedByNoveltyAsync(request.Descending, cancellationToken);
        
        var result = products
            .Select(product => 
                new GetProductByNoveltyVm(
                    product.Id,
                    product.ProductName,
                    product.Price,
                    product.Rating,
                    product.ImageMiniPath,
                    product.SellerName)
            )
            .ToList();
        
        logger.LogInformation($"Handled {nameof(GetProductByNoveltyQueryHandler)}");
        
        return result;
    }
}
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;

namespace RenStore.Application.Features.Product.Commands.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<DeleteProductCommandHandler> logger;

    public DeleteProductCommandHandler(
        IProductRepository productRepository,
        ILogger<DeleteProductCommandHandler> logger)
    {
        this.productRepository = productRepository;
        this.logger = logger;
    }
    
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(DeleteProductCommandHandler)}");
        
        await productRepository.DeleteAsync(request.ProductId, cancellationToken);
        
        logger.LogInformation($"Handled {nameof(DeleteProductCommandHandler)}");
    }
}
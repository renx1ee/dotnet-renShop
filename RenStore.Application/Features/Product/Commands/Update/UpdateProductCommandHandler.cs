using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;

namespace RenStore.Application.Features.Product.Commands.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    // TODO: доделать обновление по категориям.
    private readonly IProductRepository productRepository;
    private readonly ILogger logger;
    
    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        ILogger<UpdateProductCommand> logger)
    {
        this.productRepository = productRepository;
        this.logger = logger;
    }
    
    public async Task Handle(UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(UpdateProductCommand)}");

        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
            throw new NotFoundException(typeof(Domain.Entities.Product), request.Id);
        
        product.ProductName = request.ProductName;
        product.Description = request.Description;
        product.Price = request.Price;

        await productRepository.UpdateAsync(product, cancellationToken);
        
        logger.LogInformation($"Handled {nameof(UpdateProductCommand)}");
    }
}
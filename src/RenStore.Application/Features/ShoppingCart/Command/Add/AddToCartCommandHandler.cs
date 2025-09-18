using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Application.Features.ShoppingCart.Command.Add;

public class AddToCartCommandHandler 
    : IRequestHandler<AddToCartCommand>
{
    private readonly ILogger logger;
    private readonly IProductRepository productRepository;
    private readonly IShoppingCartRepository shoppingCartRepository;
    
    
    public AddToCartCommandHandler(
        ILogger<AddToCartCommandHandler> logger,
        IProductRepository productRepository,
        IShoppingCartRepository shoppingCartRepository)
    {
        this.logger = logger;
        this.productRepository = productRepository;
        this.shoppingCartRepository = shoppingCartRepository;
    }
    
    public async Task Handle(AddToCartCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(AddToCartCommandHandler)}");
        
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        
        var shoppingCartItem = await shoppingCartRepository
            .GetItemUserIdAndProductIdAsync(
                productId: request.ProductId, 
                userId: request.UserId, 
                cancellationToken: cancellationToken);

        if (shoppingCartItem == null)
        {
            if (request.Amount > product.InStock) return;
            
            shoppingCartItem = new ShoppingCartItem()
            {
                ProductId = request.ProductId,
                Amount = Math.Min(product.InStock, request.Amount),
                ApplicationUserId = request.UserId
            };
            await shoppingCartRepository.AddToCartAsync(shoppingCartItem, cancellationToken);
        }
        else
        {
            if (product.InStock - shoppingCartItem.Amount - request.Amount >= 0)
                shoppingCartItem.Amount += request.Amount;
            else
                shoppingCartItem.Amount += (product.InStock - shoppingCartItem.Amount);
            
            await shoppingCartRepository.UpdateItemAsync(shoppingCartItem, cancellationToken);
        }
        
        logger.LogInformation($"Handled {nameof(AddToCartCommandHandler)}");
    }
}
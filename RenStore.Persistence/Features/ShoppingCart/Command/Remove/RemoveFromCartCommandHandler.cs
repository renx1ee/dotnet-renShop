using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.ShoppingCart.Command.Remove;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.ShoppingCart.Command.Remove;

public class RemoveFromCartCommandHandler 
    : IRequestHandler<RemoveFromCartCommand>
{
    private readonly ILogger logger;
    private readonly IShoppingCartRepository shoppingCartRepository;
    
    public RemoveFromCartCommandHandler(
        ILogger<RemoveFromCartCommandHandler> logger,
        IShoppingCartRepository shoppingCartRepository)
    {
        this.logger = logger;
        this.shoppingCartRepository = shoppingCartRepository;
    }
    
    public async Task Handle(RemoveFromCartCommand request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(RemoveFromCartCommandHandler)}");
        
        var item = await shoppingCartRepository.GetByItemIdAsync(request.ItemId, cancellationToken);

        if (item is null) return;
        
        if (item.Amount == 1 || item.Amount == request.Amount)
            await shoppingCartRepository.RemoveFromCartAsync(request.ItemId, cancellationToken);
        else
        {
            if (item.Amount >= request.Amount)
            {
                item.Amount -= request.Amount;
                await shoppingCartRepository.UpdateItemAsync(item, cancellationToken);
            }
        }
        
        logger.LogInformation($"Handled {nameof(RemoveFromCartCommandHandler)}");
    }
}
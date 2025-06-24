using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.ShoppingCart.Query.GetAll;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.ShoppingCart.Query.GetAll;

public class GetAllCartItemsQueryHandler
    : IRequestHandler<GetAllCartItemsQuery, IList<GetAllCartItemsVm>>
{
    private readonly ILogger<GetAllCartItemsQueryHandler> logger;
    private readonly IShoppingCartRepository shoppingCartRepository;

    public GetAllCartItemsQueryHandler(
        ILogger<GetAllCartItemsQueryHandler> logger, 
        IShoppingCartRepository shoppingCartRepository)
    {
        this.logger = logger;
        this.shoppingCartRepository = shoppingCartRepository;
    }
    
    public async Task<IList<GetAllCartItemsVm>> Handle(GetAllCartItemsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetAllCartItemsQueryHandler)}");
        
        var items = await shoppingCartRepository.GetAllItemsAsync(cancellationToken);

        var result = items.Select(item =>
            new GetAllCartItemsVm(
                item.Id,
                item.Amount,
                item.ProductId,
                item.ApplicationUserId))
            .ToList();
        
        logger.LogInformation($"Handled {nameof(GetAllCartItemsQueryHandler)}");
        
        return result;
    }
}
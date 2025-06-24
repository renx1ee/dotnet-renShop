using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.ShoppingCart.Query.GetTotalPrice;
using RenStore.Application.Repository;
using RenStore.Application.Services;

namespace RenStore.Persistence.Features.ShoppingCart.Query.GetTotalPrice;

public class GetTotalShoppingCartPriceQueryHandler 
    : IRequestHandler<GetTotalShoppingCartPriceQuery, GetTotalShoppingCartPriceVm>
{
    private readonly ILogger<GetTotalShoppingCartPriceQueryHandler> logger;
    private readonly IShoppingCartRepository shoppingCartRepository;
    private readonly ShoppingCartService shoppingCartService;
    
    public GetTotalShoppingCartPriceQueryHandler(
        ILogger<GetTotalShoppingCartPriceQueryHandler> logger, 
        IShoppingCartRepository shoppingCartRepository,
        ShoppingCartService shoppingCartService)
    {
        this.logger = logger;
        this.shoppingCartRepository = shoppingCartRepository;
        this.shoppingCartService = shoppingCartService;
    }
    
    public async Task<GetTotalShoppingCartPriceVm> Handle(GetTotalShoppingCartPriceQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetTotalShoppingCartPriceQueryHandler)}");
        
        var items = await shoppingCartRepository.GetCartByUserIdAsync(request.UserId, cancellationToken);

        var price = await shoppingCartService
            .GetShoppingCartTotal(items);
            
        var result = new GetTotalShoppingCartPriceVm
        {
            TotalPrice = price,
        };
        
        logger.LogInformation($"Handled {nameof(GetTotalShoppingCartPriceQueryHandler)}");

        return result;
    }
}
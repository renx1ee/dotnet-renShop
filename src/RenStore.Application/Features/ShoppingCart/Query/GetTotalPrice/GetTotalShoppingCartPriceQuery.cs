using MediatR;

namespace RenStore.Application.Features.ShoppingCart.Query.GetTotalPrice;

public class GetTotalShoppingCartPriceQuery : IRequest<GetTotalShoppingCartPriceVm>
{
    public string UserId { get; set; }
}
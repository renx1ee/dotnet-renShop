using MediatR;

namespace RenStore.Application.Features.ShoppingCart.Query.GetAll;

public class GetAllCartItemsQuery : IRequest<IList<GetAllCartItemsVm>>
{
}
using MediatR;

namespace RenStore.Application.Features.Product.Queries.GetBySellerId;

public class GetProductBySellerIdQuery : IRequest<IList<GetProductBySellerIdVm>>
{
    public int SellerId { get; set; }
}
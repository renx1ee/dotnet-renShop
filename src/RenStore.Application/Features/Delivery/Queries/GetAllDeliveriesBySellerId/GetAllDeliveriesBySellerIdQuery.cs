using MediatR;

namespace RenStore.Application.Features.Delivery.Queries.GetAllDeliveriesBySellerId;

public class GetAllDeliveriesBySellerIdQuery : IRequest<IList<GetAllDeliveriesBySellerIdVm>>
{
    public int SellerId { get; set; }
}
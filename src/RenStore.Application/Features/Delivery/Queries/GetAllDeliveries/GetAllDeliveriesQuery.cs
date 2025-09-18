using MediatR;

namespace RenStore.Application.Features.Delivery.Queries.GetAllDeliveries;

public class GetAllDeliveriesQuery : IRequest<IList<GetAllDeliveriesVm>>
{
    public Guid ProductId { get; set; }
}
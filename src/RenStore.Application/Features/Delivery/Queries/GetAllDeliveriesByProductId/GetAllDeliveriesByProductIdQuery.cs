using MediatR;

namespace RenStore.Application.Features.Delivery.Queries.GetAllDeliveriesByProductId;

public class GetAllDeliveriesByProductIdQuery : IRequest<IList<GetAllDeliveriesByProductIdVm>>
{
    public Guid ProductId { get; set; }
}
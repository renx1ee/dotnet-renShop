using MediatR;

namespace RenStore.Application.Features.Orders.Queries.GetByProductId;

public class GetOrdersByProductIdQuery : IRequest<IList<GetOrdersByProductIdVm>>
{
    public Guid ProductId { get; set; }
}
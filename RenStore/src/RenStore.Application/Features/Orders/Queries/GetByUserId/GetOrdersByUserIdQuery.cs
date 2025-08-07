using MediatR;

namespace RenStore.Application.Features.Orders.Queries.GetByUserId;

public class GetOrdersByUserIdQuery : IRequest<IList<GetOrdersByUserIdVm>>
{
    public string UserId { get; set; }
}
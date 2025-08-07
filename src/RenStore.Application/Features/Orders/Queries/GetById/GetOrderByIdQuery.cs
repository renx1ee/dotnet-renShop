using MediatR;

namespace RenStore.Application.Features.Orders.Queries.GetById;

public class GetOrderByIdQuery : IRequest<GetOrderByIdVm>
{
    public Guid Id { get; set; }
}
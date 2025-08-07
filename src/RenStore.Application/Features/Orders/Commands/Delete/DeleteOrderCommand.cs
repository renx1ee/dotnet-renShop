using MediatR;

namespace RenStore.Application.Features.Orders.Commands.Delete;

public class DeleteOrderCommand : IRequest
{
    public Guid OrderId { get; set; }
}
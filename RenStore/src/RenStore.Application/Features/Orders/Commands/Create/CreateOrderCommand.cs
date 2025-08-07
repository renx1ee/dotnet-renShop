using MediatR;
using RenStore.Domain.Enums;

namespace RenStore.Application.Features.Orders.Commands.Create;

public class CreateOrderCommand : IRequest<Guid>
{
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public uint Amount { get; set; } 
    public string ApplicationUserId { get; set; }
    public Guid ProductId { get; set; }
}
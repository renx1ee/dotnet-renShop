using MediatR;

namespace RenStore.Application.Features.Orders.Commands.Update;

public class UpdateOrderCommand : IRequest
{
    public Guid Id { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public uint Amount { get; set; } 
    public Guid ApplicationUserId { get; set; }
    public Guid ProductId { get; set; }
}
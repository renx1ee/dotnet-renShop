using MediatR;

namespace RenStore.Application.Features.ShoppingCart.Command.Remove;

public class RemoveFromCartCommand : IRequest
{
    public Guid ItemId { get; set; }
    public uint Amount { get; set; }
}
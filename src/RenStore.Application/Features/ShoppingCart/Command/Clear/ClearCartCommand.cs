using MediatR;

namespace RenStore.Application.Features.ShoppingCart.Command.Clear;

public class ClearCartCommand : IRequest
{
    public string UserId { get; set; }
}
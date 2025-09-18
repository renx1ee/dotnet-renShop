using MediatR;

namespace RenStore.Application.Features.Delivery.Commands.Create;

public abstract class CreateDeliveryCommand : IRequest<Guid>
{
    
}
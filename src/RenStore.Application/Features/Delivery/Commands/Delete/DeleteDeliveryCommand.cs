using MediatR;

namespace RenStore.Application.Features.Delivery.Command.Delete;

public class DeleteDeliveryCommand : IRequest
{
    public Guid Id { get; set; }
}
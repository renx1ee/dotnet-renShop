using MediatR;

namespace RenStore.Application.Features.Delivery.Queries.GetDeliveryById;

public class GetDeliveryByIdQuery : IRequest<GetDeliveryByIdVm>
{
    public Guid Id { get; set; }
}
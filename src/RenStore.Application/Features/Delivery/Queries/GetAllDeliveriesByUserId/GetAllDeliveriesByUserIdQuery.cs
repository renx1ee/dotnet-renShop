using MediatR;

namespace RenStore.Application.Features.Delivery.Queries.GetAllDeliveriesByUserId;

public class GetAllDeliveriesByUserIdQuery : IRequest<IList<GetAllDeliveriesByUserIdVm>>
{
    public string UserId { get; set; }
}
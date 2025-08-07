using MediatR;

namespace RenStore.Application.Features.Orders.Queries.GetAll;

public class GetAllOrdersQuery : IRequest<IList<GetAllOrdersVm>>
{
}
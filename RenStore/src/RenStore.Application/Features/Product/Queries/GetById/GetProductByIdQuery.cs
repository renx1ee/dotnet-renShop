using MediatR;

namespace RenStore.Application.Features.Product.Queries.GetById;

public class GetProductByIdQuery : IRequest<GetProductByIdVm>
{
    public Guid Id { get; set; }
}
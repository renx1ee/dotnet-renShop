using MediatR;

namespace RenStore.Application.Features.Product.Commands.Delete;

public class DeleteProductCommand : IRequest
{
    public Guid ProductId { get; set; }
}
using MediatR;

namespace RenStore.Application.Features.ProductAnswer.Command.Delete;

public class DeleteProductAnswerCommand : IRequest
{
    public Guid Id { get; set; }
}
using MediatR;

namespace RenStore.Application.Entities.ProductAnswer.Command.Delete;

public class DeleteProductAnswerCommand : IRequest
{
    public Guid Id { get; set; }
}
using MediatR;

namespace RenStore.Application.Entities.ProductQuestion.Command.Delete;

public class DeleteProductQuestionCommand : IRequest
{
    public Guid Id { get; set; }
}
using MediatR;

namespace RenStore.Application.Features.ProductQuestion.Command.Delete;

public class DeleteProductQuestionCommand : IRequest
{
    public Guid Id { get; set; }
}
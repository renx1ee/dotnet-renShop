using MediatR;

namespace RenStore.Application.Features.ProductQuestion.Command.Create;

public class CreateProductQuestionCommand : IRequest<Guid>
{
    public Guid ProductId { get; set; }
    public string ApplicationUserId { get; set; }
    public string Message { get; set; }
}
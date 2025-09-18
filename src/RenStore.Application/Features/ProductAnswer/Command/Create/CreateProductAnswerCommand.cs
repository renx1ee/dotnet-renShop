using MediatR;

namespace RenStore.Application.Features.ProductAnswer.Command.Create;

public class CreateProductAnswerCommand : IRequest<Guid>
{
    public string Message { get; set; }
    public Guid ProductQuestionId { get; set; }
    public int SellerId { get; set; }
}
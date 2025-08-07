using MediatR;

namespace RenStore.Application.Features.ProductQuestion.Queries.GetAllByProductId;

public class GetAllQuestionsByProductIdQuery : IRequest<IEnumerable<GetAllQuestionsByProductIdVm>>
{
    public Guid ProductId { get; set; }
    public uint Count { get; set; }
}
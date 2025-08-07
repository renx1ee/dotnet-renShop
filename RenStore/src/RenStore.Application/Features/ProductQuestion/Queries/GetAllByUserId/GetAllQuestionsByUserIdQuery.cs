using MediatR;

namespace RenStore.Application.Features.ProductQuestion.Queries.GetAllByUserId;

public class GetAllQuestionsByUserIdQuery : IRequest<IEnumerable<GetAllQuestionsByUserIdVm>>
{
    public string UserId { get; set; }
    public uint Count { get; set; }
}
using MediatR;

namespace RenStore.Application.Features.ProductQuestion.Queries.GetQuestionWithAnswerById;

public class GetQuestionWithAnswerByIdQuery : IRequest<GetQuestionWithAnswerByIdVm>
{
    public Guid Id { get; set; }
}
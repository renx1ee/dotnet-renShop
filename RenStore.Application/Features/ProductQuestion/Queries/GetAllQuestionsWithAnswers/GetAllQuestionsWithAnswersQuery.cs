using MediatR;

namespace RenStore.Application.Features.ProductQuestion.Queries.GetAllQuestionsWithAnswers;

public class GetAllQuestionsWithAnswersQuery : IRequest<IEnumerable<GetAllQuestionsWithAnswersVm>>
{
}
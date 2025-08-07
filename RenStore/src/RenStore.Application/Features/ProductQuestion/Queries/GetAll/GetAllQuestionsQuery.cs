using MediatR;

namespace RenStore.Application.Features.ProductQuestion.Queries.GetAll;

public class GetAllQuestionsQuery : IRequest<IEnumerable<GetAllQuestionsVm>>
{
}
using MediatR;

namespace RenStore.Application.Features.ProductQuestion.Queries.GetAll;

public class GetAllQuestionsCommand : IRequest<IEnumerable<GetAllQuestionsVm>>
{
}
using MediatR;

namespace RenStore.Application.Entities.ProductQuestion.Queries.GetAll;

public class GetAllQuestionsCommand : IRequest<IEnumerable<GetAllQuestionsVm>>
{
}
using MediatR;

namespace RenStore.Application.Features.ProductAnswer.Queries.GetAll;

public class GetAllAnswersQuery : IRequest<IEnumerable<GetAllAnswersVm>>
{
}
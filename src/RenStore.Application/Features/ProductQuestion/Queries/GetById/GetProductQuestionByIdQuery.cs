using MediatR;

namespace RenStore.Application.Features.ProductQuestion.Queries.GetById;

public class GetProductQuestionByIdQuery : IRequest<GetProductQuestionByIdVm>
{
    public Guid Id { get; set; }
}
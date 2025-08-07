using MediatR;

namespace RenStore.Application.Features.ProductAnswer.Queries.GetById;

public class GetAnswerByIdQuery : IRequest<GetAnswerByIdVm>
{
    public Guid Id { get; set; }
}
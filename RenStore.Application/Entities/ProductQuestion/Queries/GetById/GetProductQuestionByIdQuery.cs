using MediatR;

namespace RenStore.Application.Entities.ProductQuestion.Queries.GetById;

public class GetProductQuestionByIdQuery : IRequest
{
    public Guid Id { get; set; }
}
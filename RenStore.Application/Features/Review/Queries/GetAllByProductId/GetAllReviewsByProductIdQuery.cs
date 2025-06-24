using MediatR;

namespace RenStore.Application.Features.Review.Queries.GetAllByProductId;

public class GetAllReviewsByProductIdQuery : IRequest<IList<GetAllReviewsByProductIdVm>>
{
    public Guid ProductId { get; set; }
}
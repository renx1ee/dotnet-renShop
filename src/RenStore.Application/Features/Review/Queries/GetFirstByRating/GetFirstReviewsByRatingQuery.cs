using MediatR;

namespace RenStore.Application.Features.Review.Queries.GetFirstByRating;

public class GetFirstReviewsByRatingQuery : IRequest<IList<GetFirstReviewsByRatingVm>>
{
    public Guid ProductId { get; set; }
    public int Count { get; set; }
}
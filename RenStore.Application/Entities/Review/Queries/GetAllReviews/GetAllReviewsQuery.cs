using MediatR;

namespace RenStore.Application.Entities.Review.Queries.GetAllReviews;

public class GetAllReviewsQuery : IRequest<IList<GetAllReviewsVm>>
{
    public bool IsApproved { get; set; }
}
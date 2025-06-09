using MediatR;
using RenStore.Domain.Enums;

namespace RenStore.Application.Entities.Review.Queries.GetAllReviews;

public class GetAllReviewsQuery : IRequest<IList<GetAllReviewsVm>>
{
    public ReviewStatusFilter Status { get; set; }
}
using MediatR;
using RenStore.Domain.Enums;

namespace RenStore.Application.Features.Review.Queries.GetAllReviews;

public class GetAllReviewsQuery : IRequest<IList<GetAllReviewsVm>>
{
    public ReviewStatusFilter Status { get; set; }
}
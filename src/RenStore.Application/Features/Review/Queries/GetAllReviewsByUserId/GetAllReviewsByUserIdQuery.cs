using MediatR;
using RenStore.Domain.Enums;

namespace RenStore.Application.Features.Review.Queries.GetAllReviewsByUserId;

public class GetAllReviewsByUserIdQuery : IRequest<IList<GetAllReviewsByUserIdVm>>
{
    public string UserId { get; set; }
    public ReviewStatusFilter Status { get; set; }
}
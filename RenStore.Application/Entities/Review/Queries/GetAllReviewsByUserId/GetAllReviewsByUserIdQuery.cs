using MediatR;

namespace RenStore.Application.Entities.Review.Queries.GetAllReviewsByUserId;

public class GetAllReviewsByUserIdQuery : IRequest<IList<GetAllReviewsByUserIdVm>>
{
    public string UserId { get; set; }
    public bool IsApproved { get; set; }
}
using MediatR;

namespace RenStore.Application.Entities.Review.Queries.GetAllForModeration;

public class GetAllForModerationRequest : IRequest<IEnumerable<GetAllReviewsForModerationVm>>
{
}
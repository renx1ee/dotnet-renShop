using MediatR;

namespace RenStore.Application.Features.Review.Queries.GetAllForModeration;

public class GetAllForModerationRequest : IRequest<IEnumerable<GetAllReviewsForModerationVm>>
{
}
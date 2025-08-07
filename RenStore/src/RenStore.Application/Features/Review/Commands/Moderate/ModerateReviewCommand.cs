using MediatR;

namespace RenStore.Application.Features.Review.Commands.Moderate;

public class ModerateReviewCommand : IRequest
{
    public Guid ReviewId { get; set; }
    public bool Approve { get; set; }
}
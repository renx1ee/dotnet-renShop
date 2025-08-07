using MediatR;

namespace RenStore.Application.Features.Review.Commands.Delete;

public class DeleteReviewCommand : IRequest
{
    public Guid Id { get; set; }
}
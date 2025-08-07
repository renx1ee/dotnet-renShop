using MediatR;

namespace RenStore.Application.Features.Review.Commands.Update;

public class UpdateReviewCommand : IRequest
{
    public Guid Id { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsUpdated  { get; set; }
    public string Message { get; set;  }
    public decimal Rating { get; set; }
    public IEnumerable<string> ImagesUrls { get; set; }
    public Guid ApplicationUserId { get; set; }
}
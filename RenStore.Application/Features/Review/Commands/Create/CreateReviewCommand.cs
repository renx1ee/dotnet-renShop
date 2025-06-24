using MediatR;

namespace RenStore.Application.Features.Review.Commands.Create;

public class CreateReviewCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Message { get; set;  }
    public decimal Rating { get; set; }
    public IEnumerable<string> ImagesUrls { get; set; }
    public string ApplicationUserId { get; set; }
    public Guid ProductId { get; set; }
}
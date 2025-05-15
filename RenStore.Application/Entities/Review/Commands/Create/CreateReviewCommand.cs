using MediatR;

namespace RenStore.Application.Entities.Review.Commands.Create;

public class CreateReviewCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsUpdated  { get; set; }
    public string Message { get; set;  }
    public decimal Rating { get; set; }
    public IEnumerable<string> ImagesUrls { get; set; }
    public uint LikesCount { get; set; }
    public List<string> UsersLikedIds { get; set; }
    public string ApplicationUserId { get; set; }
    public Guid ProductId { get; set; }
}
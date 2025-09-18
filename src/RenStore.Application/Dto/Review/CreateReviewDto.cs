namespace RenStore.Application.Dto.Review;

public class CreateReviewDto
{
    public string Message { get; set;  }
    public decimal Rating { get; set; }
    public IEnumerable<string>? ImagesUrls { get; set; }
    public string ApplicationUserId { get; set; }
    public Guid ProductId { get; set; }
}
namespace RenStore.Application.Dto.Review;

public class UpdateReviewDto
{
    public Guid Id { get; set; }
    public string Message { get; set;  }
    public decimal Rating { get; set; }
    public IEnumerable<string> ImagesUrls { get; set; }
    public string ApplicationUserId { get; set; }
}
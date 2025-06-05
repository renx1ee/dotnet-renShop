namespace RenStore.Domain.Dto.Review;

public class ModerateReviewDto
{
    public Guid ReviewId { get; set; }
    public bool Approve { get; set; }
}
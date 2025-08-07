using RenStore.Domain.Enums;

namespace RenStore.Application.Features.Review.Queries.GetAllReviews;

public class GetAllReviewsVm
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsUpdated  { get; set; }
    public string Message { get; set;  }
    public decimal Rating { get; set; }
    public ReviewStatus Status { get; set; }
    public IEnumerable<string> ImagesUrls { get; set; }
    public uint LikesCount { get; set; }
    public string ApplicationUserId { get; set; }
    public Guid ProductId { get; set; }

    public GetAllReviewsVm(Guid id, 
        DateTime createdDate, 
        DateTime? lastUpdatedDate, 
        bool isUpdated, 
        string message, 
        decimal rating, 
        ReviewStatus status,
        IEnumerable<string> imagesUrls, 
        uint likesCount,
        string applicationUserId,
        Guid productId)
    {
        Id = id;
        CreatedDate = createdDate;
        LastUpdatedDate = lastUpdatedDate;
        IsUpdated = isUpdated;
        Message = message;
        Rating = rating;
        Status = status;
        ImagesUrls = imagesUrls;
        LikesCount = likesCount;
        ApplicationUserId = applicationUserId;
        ProductId = productId;
    }
}
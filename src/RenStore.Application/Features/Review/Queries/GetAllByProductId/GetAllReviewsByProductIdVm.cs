namespace RenStore.Application.Features.Review.Queries.GetAllByProductId;

public class GetAllReviewsByProductIdVm
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public bool IsUpdated  { get; set; }
    public string Message { get; set;  }
    public decimal Rating { get; set; }
    public IEnumerable<string> ImagesUrls { get; set; }
    public uint LikesCount { get; set; }
    public string ApplicationUserId { get; set; }
    public Guid ProductId { get; set; }

    public GetAllReviewsByProductIdVm(Guid id, 
        DateTime createdDate, 
        DateTime? lastUpdatedDate, 
        bool isUpdated, 
        string message, 
        decimal rating, 
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
        ImagesUrls = imagesUrls;
        LikesCount = likesCount;
        ApplicationUserId = applicationUserId;
        ProductId = productId;
    }
}
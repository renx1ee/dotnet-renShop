namespace RenStore.Application.Features.ProductAnswer.Queries.GetAll;

public class GetAllAnswersVm
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string SellerName { get; set; }
    public uint SellerId { get; set; }
    public string Message { get; set; }
    public Guid ProductQuestionId { get; set; }

    public GetAllAnswersVm(Guid id, DateTime createdDate, string sellerName, uint sellerId, string message,
        Guid productQuestionId)
    {
        this.Id = id;
        this.CreatedDate = createdDate;
        this.SellerName = sellerName;
        this.SellerId = sellerId;
        this.Message = message;
        this.ProductQuestionId = productQuestionId;
    }
}
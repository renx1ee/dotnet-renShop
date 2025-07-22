namespace RenStore.Application.Features.ProductAnswer.Queries.GetById;

public class GetAnswerByIdVm
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string SellerName { get; set; }
    public uint SellerId { get; set; }
    public string Message { get; set; }
    public Guid ProductQuestionId { get; set; }

    public GetAnswerByIdVm(
        Guid id, 
        DateTime createdDate, 
        string sellerName, 
        uint sellerId, 
        string message,
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
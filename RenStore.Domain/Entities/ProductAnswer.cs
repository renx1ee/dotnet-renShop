namespace RenStore.Domain.Entities;

public class ProductAnswer
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string SellerName { get; set; }
    // TODO: add seller id with create handler
    public uint SellerId { get; set; }
    public string Message { get; set; }
    public Guid ProductQuestionId { get; set; }
    public ProductQuestion ProductQuestion { get; set; }
}
namespace RenStore.Application.Features.ShoppingCart.Query.GetAll;

public class GetAllCartItemsVm
{
    public Guid Id { get; set; }
    public uint Amount { get; set; }
    public Guid ProductId { get; set; }
    public string ApplicationUserId { get; set; }

    public GetAllCartItemsVm(
        Guid id, 
        uint amount, 
        Guid productId, 
        string userId)
    {
        this.Id = id;
        this.Amount = amount;
        this.ProductId = productId;
        this.ApplicationUserId = userId;
    }
}
namespace RenStore.Application.Dto.ShoppingCart;

public class AddToCartDto
{
    public Guid ProductId { get; set; }
    public int Amount { get; set; } 
}
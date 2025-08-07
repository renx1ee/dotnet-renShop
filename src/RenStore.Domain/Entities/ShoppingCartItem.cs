namespace RenStore.Domain.Entities;

public class ShoppingCartItem
{
    public Guid Id { get; set; }
    public uint Amount { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}
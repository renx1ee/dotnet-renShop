namespace RenStore.Domain.Entities;

public class ProductQuestion
{
    public Guid Id { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    public Guid ApplicationUserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UserName { get; set; }
    public string Message { get; set; }
    public ProductAnswer ProductAnswer { get; set; }
}
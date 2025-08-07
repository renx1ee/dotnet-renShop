namespace RenStore.Domain.Entities;

public class Seller
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public string ImageFooterPath { get; set; }
    public IEnumerable<Product>? Products { get; set; }
}

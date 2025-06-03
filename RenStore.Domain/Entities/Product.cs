namespace RenStore.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public decimal OldPrice { get; set; }
    public decimal Discount { get; set; }
    public string Description { get; set; }
    public uint InStock { get; set; }
    public string? ImagePath { get; set; }
    public string ImageMiniPath { get; set; }
    public List<string>? ImagesListPath { get; set; }
    public double? Rating { get; set; }
    public IEnumerable<Review>? Reviews { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public Seller Seller { get; set; }
    public string SellerName { get; set; } 
    public int SellerId { get; set; }
    public ProductDetail ProductDetail { get; set; }
    public Guid ProductDetailId { get; set; }
    public ClothesProduct? ClothesProduct { get; set; }
    public Guid ClothesProductId { get; set; }
    public ShoesProduct? ShoesProduct { get; set; }
    public Guid ShoesProductId { get; set; }
    public DateTime CreatedDate { get; set; }
}
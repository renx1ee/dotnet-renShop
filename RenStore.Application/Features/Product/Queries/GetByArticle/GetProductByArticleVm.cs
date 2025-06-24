using RenStore.Domain.Entities;
using RenStore.Domain.Entities.Products;
using RenStore.Domain.Enums;

namespace RenStore.Application.Features.Product.Queries.GetByArticle;

public class GetProductByArticleVm
{
    public Guid ProductId { get; set; }
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
    public string CategoryName { get; set; }
    public int CategoryId { get; set; }
    public string SellerName { get; set; }
    public int SellerId { get; set; }
    public Guid ProductDetailId { get; set; }
    public ProductDetail ProductDetail { get; set; }
    public uint Article { get; set; }
    public string Brend { get; set; }
    public string? CountryOfManufacture { get; set; }
    public string? ModelFeatures { get; set; } 
    public string? DecorativeElements { get; set; }
    public string? Equipment { get; set; }
    public uint? QuantityPerPackage { get; set; }
    public string? Composition { get; set; }
    public ColorStatus? Color { get; set; }
    public TypeOfPackaging? TypeOfPackaging { get; set; }
    public Guid? ClothesProductId { get; set; }
    public ClothesProduct? ClothesProduct { get; set; }
    public Guid? ShoesProductId { get; set; }
    public ShoesProduct? ShoesProduct { get; set; }
}
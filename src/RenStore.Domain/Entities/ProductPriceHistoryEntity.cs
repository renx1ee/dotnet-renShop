namespace RenStore.Domain.Entities;

public class ProductPriceHistoryEntity
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public decimal OldPrice { get; set; }
    public decimal DiscountPrice { get; set; }
    public decimal DiscountPercent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ChangedBy { get; set; } = string.Empty;
    public Guid ProductVariantId { get; set; }
    public ProductVariantEntity? ProductVariant { get; set; }
}
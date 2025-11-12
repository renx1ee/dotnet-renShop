namespace RenStore.Domain.Entities;

public class ProductAttributeEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public Guid ProductVariantId { get; set; }
    public ProductVariantEntity? ProductVariant { get; set; }
}
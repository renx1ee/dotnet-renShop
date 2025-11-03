namespace RenStore.Domain.Entities;

public class ColorEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
    public string ColorCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    /*public IList<ProductVariantEntity> ProductVariants { get; set; } = [];*/
}
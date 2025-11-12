using RenStore.Domain.Enums.Clothes;

namespace RenStore.Domain.Entities;

public class ProductClothSizeEntity
{
    public Guid Id { get; set; }
    public ClothesSizes? ClothesSize { get; set; }
    public int Amount { get; set; }
    public Guid ProductClothId { get; set; }
    public ProductClothEntity? ProductCloth { get; set; }
}
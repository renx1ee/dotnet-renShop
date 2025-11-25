using RenStore.Domain.Enums.Clothes;

namespace RenStore.Domain.DTOs.Product.FullPage;

public record ProductClothSizeDto
(
    Guid ClothSizeId,
    int Amount,
    Guid ProductClothId,
    ClothesSizes ClothesSize
);
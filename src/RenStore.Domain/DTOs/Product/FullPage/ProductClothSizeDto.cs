using RenStore.Domain.Enums.Clothes;

namespace RenStore.Domain.DTOs.Product.FullPage;

public record ProductClothSizeDto
(
    Guid Id,
    int Amount,
    Guid ProductClothId,
    ClothesSizes ClothesSize
);
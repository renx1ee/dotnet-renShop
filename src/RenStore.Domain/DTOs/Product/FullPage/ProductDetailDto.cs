using RenStore.Domain.Enums;

namespace RenStore.Domain.DTOs.Product.FullPage;

public record ProductDetailDto
(
    Guid Id,
    string Description,
    string ModelFeatures,
    string DecorativeElements,
    string Equipment,
    string Composition,
    string CaringOfThings,
    TypeOfPackaging? TypeOfPacking,
    int CountryId,
    Guid ProductVariantId
);
namespace RenStore.Domain.DTOs.Product.FullPage;

public record ProductAttributeDto
(
    Guid Id,
    string Name,
    string Value,
    Guid ProductVariantId
);
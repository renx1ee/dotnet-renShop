namespace RenStore.Domain.DTOs.Product.FullPage;

public record ProductAttributeDto
(
    Guid AttributeId,
    string Name,
    string Value,
    Guid ProductVariantId
);
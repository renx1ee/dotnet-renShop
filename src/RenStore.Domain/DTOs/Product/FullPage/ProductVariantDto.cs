namespace RenStore.Domain.DTOs.Product.FullPage;

public record ProductVariantDto
(
    Guid Id,
    string Name,
    string NormalizedName,
    decimal Rating,
    long Article,
    int InStock,
    bool IsAvailable,
    DateTime CreatedDate,
    Guid ProductId,
    int ColorId,
    string Url
);
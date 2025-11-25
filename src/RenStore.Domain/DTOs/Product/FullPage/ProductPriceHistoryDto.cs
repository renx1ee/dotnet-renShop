namespace RenStore.Domain.DTOs.Product.FullPage;

public record ProductPriceHistoryDto
(
    Guid PriceHistoryId,
    decimal Price,
    decimal OldPrice,
    decimal DiscountPrice,
    decimal DiscountPercent,
    DateTime StartDate,
    DateTime? EndDate,
    string ChangedBy,
    Guid ProductVariantId
);
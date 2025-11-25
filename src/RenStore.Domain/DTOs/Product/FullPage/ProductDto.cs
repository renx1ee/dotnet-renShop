namespace RenStore.Domain.DTOs.Product.FullPage;

public record ProductDto
(
    Guid ProductId,
    bool IsBlocked,
    decimal OverallRating,
    long SellerId,
    int CategoryId
);
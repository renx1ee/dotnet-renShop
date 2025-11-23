namespace RenStore.Domain.DTOs.Product.FullPage;

public record ProductFullDto
(
    ProductDto Product,
    ProductVariantDto Variant,
    ProductDetailDto Detail,
    ProductClothDto Cloth,
    SellerDto Seller,
    SellerImageDto SellerImage,
    IReadOnlyList<ProductClothSizeDto> ClothSize,
    IReadOnlyList<ProductAttributeDto> Attributes,
    IReadOnlyList<ProductPriceHistoryDto> Prices,
    IReadOnlyList<ProductImageDto> ProductImages
    /*IReadOnlyList<ProductReviewDto> RecentReviews*/
);
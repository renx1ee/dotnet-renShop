namespace RenStore.Domain.DTOs.Product.FullPage;

public record ProductFullDto
(
    ProductDto Product,
    ProductDetailDto Detail,
    ProductClothDto Cloth,
    SellerDto Seller,
    IReadOnlyList<KeyValuePair<Guid, ProductVariantDto>> Variants,
    IReadOnlyList<ProductClothSizeDto> ClothSizes,
    IReadOnlyList<ProductAttributeDto> Attributes,
    IReadOnlyList<ProductPriceHistoryDto> Prices
);
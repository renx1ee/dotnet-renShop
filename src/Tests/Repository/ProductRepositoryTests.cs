/*using Microsoft.EntityFrameworkCore;
using RenStore.Domain.Entities;
using RenStore.Domain.Entities.Products;
using RenStore.Domain.Enums;
using RenStore.Domain.Enums.Clothes;
using RenStore.Persistence;
using RenStore.Persistence.Repository;
using Tests.Common;

namespace Tests.Repository;

public class ProductRepositoryTests
{
    private readonly ProductRepository repository;
    private readonly ApplicationDbContext context;

    public ProductRepositoryTests()
    {
        context = TestContextFactory.CreateReadyContext();
        repository = new ProductRepository(context, TestContextFactory.ConnectionString);
    }
    
    [Fact]
    public async Task CreateClothesProductAsync_Success_Test()
    {
        Guid productId = Guid.NewGuid();
        Guid productDetailId = Guid.NewGuid();
        // Arrange
        var clothesProduct = new ClothesProduct
        {
            Neckline = Neckline.BoatNeck,
            TheCut = TheCut.Free,
            TypeOfPockets = TypeOfPockets.None,
            Gender = Gender.Man,
            Season = Season.Autumn,
            Sizes = new[] { ClothesSizes.S },
            ProductId = productId,
        };

        var productDetail = new ProductDetail
        {
            Id = productDetailId,
            Article = 000001,
            Brand = "TestBrand",
            ProductId = productId,
        };
        
        var product = new Product
        {
            Id = productId,
            ProductName = "New created rpoduct",
            Price = 1234,
            OldPrice = 1255,
            Discount = 0,
            Description = "New product",
            InStock = 4,
            ImagePath = "/images/products/new.jpg",
            ImageMiniPath = "/images/products/new.jpg",
            Rating = 0,
            CategoryId = TestContextFactory.CategoryIdForGetting,
            CategoryName = "Clothes",
            SellerId = TestContextFactory.SellerIdForGetting,
            SellerName = "SellerNameForGetting",
            ClothesProduct = clothesProduct,
            ClothesProductId = clothesProduct.Id,
            CreatedDate = DateTime.UtcNow,
            ProductDetailId = productDetailId,
            ProductDetail = productDetail
        };
        // Act
        var result = await repository
            .CreateAsync(product, CancellationToken.None);
        // Assert
        Assert.NotEqual(result, Guid.Empty);

        var existingProduct = await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == product.Id);
        
        Assert.NotNull(existingProduct);
        Assert.Equal(product.Id, existingProduct.Id);
    }
}*/
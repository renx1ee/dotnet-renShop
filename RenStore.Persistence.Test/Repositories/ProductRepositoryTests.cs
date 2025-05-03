/*using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using RenStore.Application.Data.Common.Exceptions;
using RenStore.Domain.Entities;
using RenStore.Domain.Entities.Products;
using RenStore.Domain.Enums;
using RenStore.Domain.Enums.Clothes;
using RenStore.Persistence.Repository;
using RenStore.Persistence.Test.Common;

namespace RenStore.Persistence.Test.Repositories;

public class ProductRepositoryTests 
{
    ApplicationDbContext context = DbContextFactory.Create();
    private ProductRepository productRepository;
    
    [Fact]
    public async Task CreateProductAsync_Success_Test()
    {
        // Arrange
        productRepository = new ProductRepository(context, null);
        // Act
        var productId = await productRepository.CreateAsync(new Product
        {
            ProductName = "Pants Arc’teryx Gore-Tex",
            Price = 5900,
            OldPrice = 7900,
            Discount = 20,
            Description = "The Longsleeve Carhartt WIP is the perfect choice for those who want comfort, ",
            InStock = 32,
            ImagePath = "/images/main/img.png",
            ImageMiniPath = "/images/main/img.png",
            ImagesListPath = new List<string>
            {
                "/images/main/img_1.png",
                "/images/main/img_2.png",
                "/images/main/img_3.png",
                "/images/main/img_4.png"
            },
            Rating = 0,
            Reviews = null,
            Category = null,
            CategoryId = 1,
            CategoryName = "categoryName",
            SellerName = "sellerName",
            Seller = null,
            SellerId = 1,
            ProductDetail = new ProductDetail
            {
                Article = 0,
                Brend = "",
                CountryOfManufacture = "",
                ModelFeatures = "",
                DecorativeElements = "",
                Equipment = "",
                QuantityPerPackage = 1,
                Composition = "",
                Color = ColorStatus.Black,
                TypeOfPackaging = TypeOfPackaging.Box,
            },
            ClothesProduct = new ClothesProduct
            {
                Neckline = Neckline.Horseshoe,
                TheCut = TheCut.Free,
                TypeOfPockets = TypeOfPockets.None,
                Gender = Gender.Man,
                Season = Season.Autumn, 
                TakingCareOfThings = "wfawfe jljkweaij wea",
                Sizes =
                [
                    ClothesSizes.XXXS, 
                    ClothesSizes.XXL, 
                    ClothesSizes.M
                ]
            }
        }, 
        CancellationToken.None);
        // Assert
        var product = await productRepository.GetByIdAsync(productId, CancellationToken.None);
        Assert.NotNull(product);
    }

    [Fact]
    public async Task UpdateProductAsync_Success_Test()
    {
        // Arrange
        productRepository = new ProductRepository(context, null);
        string updatedName = "Updated Product Name";
        string updatedDescription = "Updated Product Description";
        // Act
        var product = await productRepository.GetByIdAsync(
            DbContextFactory.ProductIdForUpdate,
            CancellationToken.None);

        if (product == null)
            return;

        product.ProductName = updatedName;
        product.Description = updatedDescription;
        
        await productRepository.UpdateAsync(product, CancellationToken.None);
        // Assert
        var result = await productRepository.GetByIdAsync(
            DbContextFactory.ProductIdForUpdate,
            CancellationToken.None
        );
        
        Assert.Equal(updatedName, result?.ProductName);
        Assert.Equal(updatedDescription, result?.Description);
    }

    [Fact]
    public async Task UpdateProductAsync_FainOnWrong_TestId()
    {
        // Arrange
        productRepository = new ProductRepository(context, null);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await productRepository.UpdateAsync(new Product
            { Id = Guid.NewGuid() }, 
            CancellationToken.None
        ));
    }
    
    [Fact]
    public async Task DeleteProductAsync_Success_Test()
    {
        // Arrange
        productRepository = new ProductRepository(context, null);
        // Act
        await productRepository.DeleteAsync(DbContextFactory.ProductIdForDelete, CancellationToken.None);
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await productRepository.GetByIdAsync(
                DbContextFactory.ProductIdForDelete, 
                CancellationToken.None
            )
        );
    }
    
    [Fact]
    public async Task DeleteProductAsync_FailOnWrongId_Test()
    {
        // Arrange
        productRepository = new ProductRepository(context, null);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await productRepository.DeleteAsync(
                Guid.Parse("e090f3f2-ac9e-45a6-9392-995e56564731"), 
                CancellationToken.None
            )
        );
    }

    [Fact]
    public async Task GetAllProductsAsync_Success_Test()
    {
        // Arrange
        productRepository = new ProductRepository(context, null);
        // Act
        var products = await productRepository.GetAllAsync(CancellationToken.None);
        // Assert
        Assert.Equal(3, products.Count());
    }

    [Fact]
    public async Task GetProductByIdAsync_Success_Test()
    {
        // Arrange
        productRepository = new ProductRepository(context, null);
        // Act
        var product = await productRepository.GetByIdAsync(
            DbContextFactory.ProductIdForUpdate, 
            CancellationToken.None
        );
        // Assert
        Assert.NotNull(product);
        Assert.Equal(DbContextFactory.ProductIdForUpdate, product.Id);
    }
    
    [Fact]
    public async Task GetProductByIdAsync_FailOnWrongId_Test()
    {
        // Arrange
        productRepository = new ProductRepository(context, null);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await productRepository.GetByIdAsync(
                Guid.NewGuid(), 
                CancellationToken.None
            )
        );
    }
}*/
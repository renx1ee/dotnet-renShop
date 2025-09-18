using Microsoft.EntityFrameworkCore;
using Persistence.Tests.Common;
using RenStore.Application.Common.Exceptions;
using RenStore.Domain.Entities;
using RenStore.Persistence;
using RenStore.Persistence.Repository;

namespace Persistence.Tests.Repository;

public class SellerRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext context;
    private readonly SellerRepository sellerRepository;
    
    public SellerRepositoryTests()
    {
        context = TestContextFactory.CreateReadyContext();
        sellerRepository = new SellerRepository(context, TestContextFactory.ConnectionString);
    }

    [Fact]
    public async Task CreateSellerAsync_Success_Test()
    {
        // Arrange
        int sellerId = 14;
        var seller = new Seller
        {
            Id = sellerId,
            Name = "Created Seller",
            Description = "Created Seller Description",
        };
        // Act
        var createdSellerId = await sellerRepository
            .CreateAsync(seller, CancellationToken.None);
        // Assert
        var existingSeller = await context.Sellers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == sellerId);
        Assert.NotNull(existingSeller);
        Assert.Equal(sellerId, existingSeller.Id);
    }

    /*[Fact]
    public async Task UpdateSellerAsync_Success_Test()
    {
        // Arrange
        // Act
        // Assert
    }*/

    [Fact]
    public async Task DeleteSellerAsync_Success_Test()
    {
        // Arrange
        // Act
        var seller = await context.Sellers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => 
                s.Id == TestContextFactory.SellerIdForDelete);
        Assert.NotNull(seller);
        await sellerRepository.DeleteAsync(seller.Id, CancellationToken.None);
        // Assert
        var categoryResult = await context.Sellers
            .AsNoTracking()
            .FirstOrDefaultAsync(s =>
                s.Id == TestContextFactory.SellerIdForDelete);
        Assert.Null(categoryResult);
    }

    [Fact]
    public async Task DeleteSellerAsync_FailOnWrongId_Test()
    {
        // Arrange
        int  sellerId = 1453634;
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await sellerRepository.DeleteAsync(
                sellerId,
                CancellationToken.None));
    }

    [Fact]
    public async Task GetAllSellersAsync_Success_Test()
    {
        // Arrange
        // Act
        var sellers = await sellerRepository
            .GetAllAsync(CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
    }

    [Fact]
    public async Task GetSellerByIdAsync_Success_Test()
    {
        // Arrange
        // Act
        var sellers = await sellerRepository
            .GetByIdAsync(
                TestContextFactory.SellerIdForGetting, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
    }

    [Fact]
    public async Task GetSellerByIdAsync_FailOnWrongId_Test()
    {
        // Arrange
        int  sellerId = 1456344;
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => 
            await sellerRepository
            .GetByIdAsync(
                id: sellerId, 
                CancellationToken.None));
    }
    
    [Fact]
    public async Task FindSellerByIdAsync_Success_Test()
    {
        // Arrange
        // Act
        var sellers = await sellerRepository
            .FindByIdAsync(
                TestContextFactory.SellerIdForGetting, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
    }
    
    [Fact]
    public async Task FindSellerByIdAsync_FailOnWrongId_Test()
    {
        // Arrange
        int  sellerId = 1456344;
        // Act
        var sellers = await sellerRepository
            .FindByIdAsync(
                id: sellerId,
                CancellationToken.None);
        // Assert
        Assert.Null(sellers);
    }
    
    [Fact]
    public async Task GetSellerByNameAsync_Success_Test()
    {
        // Arrange
        // Act
        var sellers = await sellerRepository
            .GetByNameAsync(
                TestContextFactory.SellerNameForGetting, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
    }

    [Fact]
    public async Task GetSellerByNameAsync_FailOnWrongName_Test()
    {
        // Arrange
        string  sellerName = "fwewfwwfwawf";
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => 
            await sellerRepository
                .GetByNameAsync(
                    name: sellerName, 
                    CancellationToken.None));
    }
    
    [Fact]
    public async Task FindSellerByNameAsync_Success_Test()
    {
        // Arrange
        // Act
        var sellers = await sellerRepository
            .FindByNameAsync(
                TestContextFactory.SellerNameForGetting, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
    }
    
    [Fact]
    public async Task FindSellerByNameAsync_FailOnWrongName_Test()
    {
        // Arrange
        string sellerName = "fwewfwwfwawf";
        // Act
        var sellers = await sellerRepository
            .FindByNameAsync(
                name: sellerName,
                CancellationToken.None);
        // Assert
        Assert.Null(sellers);
    }
    
    public void Dispose()
    {
    }
}
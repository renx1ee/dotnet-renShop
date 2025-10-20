/*using Microsoft.EntityFrameworkCore;
using RenStore.Application.Common.Exceptions;
using RenStore.Domain.Entities;
using RenStore.Persistence;
using RenStore.Persistence.Repository;
using Tests.Common;

namespace Tests.Repository;

public class SellerRepositoryTest : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly SellerRepository _sellerRepository;
    
    public SellerRepositoryTest()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
    }
    // TODO: переделать миграции и TODO
    [Fact]
    public async Task CreateSellerAsync_Success_Test()
    {
        // Arrange
        var seller = new Seller
        {
            Id = TestContextFactory.SellerIdForCreate,
            Name = "Sample Name for Update",
            Description = "Sample Description for Update",
            NormalizedName = Guid.NewGuid().ToString().ToUpper(),
            CreatedDate = DateTime.UtcNow,
            IsBlocked = false
        };
        // Act
        var result = await _sellerRepository.CreateAsync(seller, CancellationToken.None);
        // Assert
        var sellerExists = await _context.Sellers
            .FirstOrDefaultAsync(s => 
                s.Id == TestContextFactory.SellerIdForCreate);
        
        Assert.NotNull(sellerExists);
        Assert.Equal(TestContextFactory.SellerIdForCreate, sellerExists.Id);
        Assert.Equal(seller.Name, sellerExists.Name);
        Assert.Equal(seller.Description, sellerExists.Description);
        Assert.Equal(seller.NormalizedName, sellerExists.NormalizedName);
        Assert.Equal(seller.CreatedDate, sellerExists.CreatedDate);
        Assert.Equal(seller.IsBlocked, sellerExists.IsBlocked);
    }
    
    [Fact]
    // TODO: 
    public async Task CreateSellerAsync_FailOnEmpty_Test()
    {
    }
    
    [Fact]
    public async Task UpdateSellerAsync_Success_Test()
    {
    }
    
    [Fact]
    public async Task UpdateSellerAsync_FailOnEmpty_Test()
    {
    }

    [Fact]
    public async Task DeleteSellerAsync_Success_Test()
    {
        // Arrange
        // Act
        await _sellerRepository.DeleteAsync(
            TestContextFactory.SellerIdForDelete, 
            CancellationToken.None);
        // Assert
        var sellerExists = await _context.Sellers
            .FirstOrDefaultAsync(s => 
                s.Id == TestContextFactory.SellerIdForDelete);
        
        Assert.Null(sellerExists);
    }
    
    [Fact]
    public async Task DeleteSellerAsync_FailOnWrongId_Test()
    {
        // Arrange
        long wrongId = 3242367;
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _sellerRepository.DeleteAsync(
                id: wrongId,
                CancellationToken.None));
    }
    
    [Fact]
    public async Task FindAllSellersAsync_Success_Test()
    {
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindAllAsync(CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
    }
    
    [Fact]
    public async Task FindSellerByIdAsync_Success_Test()
    {
        // Arrange
        // Act
        var seller = await _sellerRepository
            .FindByIdAsync(
                TestContextFactory.SellerIdForGetting, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(seller);
        Assert.Equal(TestContextFactory.SellerIdForGetting, seller.Id);
    }
    
    [Fact]
    public async Task FindSellerByIdAsync_FailOnWrongId_Test()
    {
        // Arrange
        long wrongId = 32445362367;
        // Act
        var seller = await _sellerRepository
            .FindByIdAsync(
                id: wrongId, 
                CancellationToken.None);
        // Assert
        Assert.Null(seller);
    }
    
    [Fact]
    public async Task GetSellerByIdAsync_Success_Test()
    {
        // Arrange
        // Act
        var seller = await _sellerRepository
            .GetByIdAsync(
                TestContextFactory.SellerIdForGetting, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(seller);
        Assert.Equal(TestContextFactory.SellerIdForGetting, seller.Id);
    }
    
    [Fact]
    public async Task GetSellerByIdAsync_FailOnWrongId_Test()
    {
        // Arrange
        long wrongId = 324453653;
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _sellerRepository 
                .GetByIdAsync(
                    wrongId,
                    CancellationToken.None));
    }
    // TODO:
    [Fact]
    public async Task FindSellerByNameAsync_Success_Test()
    {
        // Arrange
        // Act
        var seller = await _sellerRepository
            .FindByNameAsync(
                TestContextFactory.SellerNameForGetting, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(seller);
        // TODO: 
    }
    
    [Fact]
    public async Task FindSellerByNameAsync_FailOnWrongName_Test()
    {
        // Arrange
        string wrongName = "wrong";
        // Act
        var seller = await _sellerRepository
            .FindByNameAsync(
                name: wrongName, 
                CancellationToken.None);
        // Assert
        Assert.Null(seller);
    }
    
    [Fact]
    public async Task GetSellerByNameAsync_Success_Test()
    {
    }
    
    [Fact]
    public async Task GetSellerByNameAsync_FailOnWrongName_Test()
    {
        // Arrange
        Guid wrongUserId = Guid.NewGuid();
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _sellerRepository
                .FindByUserIdAsync(
                    userId: wrongUserId,
                    CancellationToken.None));
    }
    
    [Fact]
    public async Task FindSellerByUserIdAsync_Success_Test()
    {
        // Arrange
        // Act
        var seller = await _sellerRepository
            .FindByUserIdAsync(
                TestContextFactory.UserIdForGettingSeller, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(seller);
        Assert.Equal(TestContextFactory.SellerIdForGetting, seller.Id);
    }
    
    [Fact]
    public async Task FindSellerByUserIdAsync_FailOnWrongUserId_Test()
    {
        
    }
    
    [Fact]
    public async Task GetSellerByUserIdAsync_Success_Test()
    {
        // Arrange
        // Act
        var seller = await _sellerRepository
            .GetByUserIdAsync(
                TestContextFactory.UserIdForGettingSeller, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(seller);
        Assert.Equal(TestContextFactory.SellerIdForGetting, seller.Id);
    }
    
    [Fact]
    public async Task GetSellerByUserIdAsync_FailOnWrongUserId_Test()
    {
        // Arrange
        Guid wrongUserId = Guid.NewGuid();
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _sellerRepository
                .GetByUserIdAsync(
                    userId: wrongUserId,
                    CancellationToken.None));
    }
    
    public void Dispose()
    {
        _context.Dispose();
        _context.Database.EnsureDeleted();
    }
}*/
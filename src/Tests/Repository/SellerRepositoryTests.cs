using Microsoft.EntityFrameworkCore;
using RenStore.Application.Common.Exceptions;
using RenStore.Domain.Entities;
using RenStore.Domain.Enums.Sorting;
using RenStore.Persistence;
using RenStore.Persistence.Repository.Postgresql;
using Tests.Common;

namespace Tests.Repository;

public class SellerRepositoryTests : IDisposable
{
    private ApplicationDbContext _context;
    private SellerRepository _sellerRepository;
    #region Create Update Delete
    [Fact]
    public async Task CreateSellerAsync_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arranges
        var user = await _context.AspNetUsers.FirstOrDefaultAsync(u =>
            u.Id == TestContextFactory.UserIdForCreateSeller);
        
        var seller = new SellerEntity
        {
            Id = TestContextFactory.SellerIdForCreate,
            Name = "Sample Name for Update",
            Description = "Sample Description for Update",
            NormalizedName = Guid.NewGuid().ToString().ToUpper(),
            CreatedDate = DateTime.UtcNow,
            IsBlocked = false,
            ApplicationUser = user,
            ApplicationUserId = TestContextFactory.UserIdForCreateSeller
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
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
    }
    
    [Fact]
    public async Task UpdateSellerAsync_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        string updatedSellerName = nameof(updatedSellerName);
        string updatedSellerNormalizedName = nameof(updatedSellerNormalizedName);
        string updatedSellerDescription = nameof(updatedSellerDescription);
        bool updatedSellerIsBlocked = true;
        var sellerExists = await _context.Sellers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => 
                s.Id == TestContextFactory.SellerIdForUpdate);
        
        if (sellerExists is null)
            Assert.Fail();
        // Act
        sellerExists.Name = updatedSellerName;
        sellerExists.NormalizedName = updatedSellerNormalizedName;
        sellerExists.Description = updatedSellerDescription;
        sellerExists.IsBlocked = updatedSellerIsBlocked;
        
        await _sellerRepository.UpdateAsync(sellerExists, CancellationToken.None);
        // Assert
        var sellerResult = await _context.Sellers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => 
                s.Id == TestContextFactory.SellerIdForUpdate);
        Assert.NotNull(sellerResult);
        Assert.Equal(updatedSellerName, sellerResult.Name);
        Assert.Equal(updatedSellerNormalizedName, sellerResult.NormalizedName);
        Assert.Equal(updatedSellerDescription, sellerResult.Description);
        Assert.Equal(updatedSellerIsBlocked, sellerResult.IsBlocked);
    }
    
    [Fact]
    public async Task UpdateSellerAsync_FailOnWrongId_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        long wrongSelleId = 2357894329785;
        string updatedSellerName = nameof(updatedSellerName);
        string updatedSellerNormalizedName = nameof(updatedSellerNormalizedName);
        string updatedSellerDescription = nameof(updatedSellerDescription);
        bool updatedSellerIsBlocked = true;
        // Act
        var seller = new SellerEntity()
        {
            Id = wrongSelleId,
            Name = updatedSellerName,
            NormalizedName = updatedSellerNormalizedName,
            Description = updatedSellerDescription,
            IsBlocked = updatedSellerIsBlocked
        };
        // Assert
        var sellerResult = 
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _sellerRepository.UpdateAsync(
                seller, 
                CancellationToken.None));
    }

    [Fact]
    public async Task DeleteSellerAsync_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Assert
        var sellerExists = await _context.Sellers
            .AsNoTracking()
            .FirstOrDefaultAsync(s => 
                s.Id == TestContextFactory.SellerIdForDelete);
        Assert.NotNull(sellerExists);
        // Act
        await _sellerRepository.DeleteAsync(
            TestContextFactory.SellerIdForDelete, 
            CancellationToken.None);
        // Assert
        var sellerResult = await _context.Sellers
            .FirstOrDefaultAsync(s => 
                s.Id == TestContextFactory.SellerIdForDelete);
        
        Assert.Null(sellerResult);
    }
    
    [Fact]
    public async Task DeleteSellerAsync_FailOnWrongId_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        long wrongId = 3242367;
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _sellerRepository.DeleteAsync(
                id: wrongId,
                CancellationToken.None));
    }
    #endregion
    
    #region All
    [Fact]
    public async Task FindAllSellersAsync_WithDefaultParameters_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindAllAsync(CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
        Assert.Equal(6, sellers.Count());
    }
    
    [Fact]
    public async Task FindAllSellersAsync_CountLimit_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindAllAsync(
                pageCount: 2,
                cancellationToken: CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
        Assert.Equal(2, sellers.Count());
    }
    
    [Fact]
    public async Task FindAllSellersAsync_SortByName_DescendingFalse_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindAllAsync(
                sortBy: SellerSortBy.Name,
                descending: false,
                cancellationToken: CancellationToken.None);
        
        var result = sellers.ToList();
        // Assert
        Assert.NotNull(sellers);
        Assert.Equal(6, sellers.Count());
        Assert.Equal(TestContextFactory.SellerIdForUpdate, result[0].Id);
        Assert.Equal(TestContextFactory.SellerIdForDelete, result[1].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[2].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting2, result[3].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting3, result[4].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting4, result[5].Id);
        
    }
    
    [Fact]
    public async Task FindAllSellersAsync_SortByName_DescendingTrue_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindAllAsync(
                sortBy: SellerSortBy.Name,
                descending: true,
                cancellationToken: CancellationToken.None);
        
        var result = sellers.ToList();
        // Assert
        Assert.NotNull(sellers);
        Assert.Equal(6, sellers.Count());
        Assert.Equal(TestContextFactory.SellerIdForGetting4 , result[0].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting3, result[1].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting2, result[2].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[3].Id);
        Assert.Equal(TestContextFactory.SellerIdForDelete, result[4].Id);
        Assert.Equal(TestContextFactory.SellerIdForUpdate, result[5].Id);
    }
    
    [Fact]
    public async Task FindAllSellersAsync_SortById_DescendingFalse_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindAllAsync(
                sortBy: SellerSortBy.Id,
                descending: false,
                cancellationToken: CancellationToken.None);
        
        var result = sellers.ToList();
        // Assert
        Assert.NotNull(sellers);
        Assert.Equal(6, sellers.Count());
        Assert.Equal(TestContextFactory.SellerIdForUpdate , result[0].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[1].Id);
        Assert.Equal(TestContextFactory.SellerIdForDelete, result[2].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting2, result[3].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting3, result[4].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting4, result[5].Id);
    }
    
    [Fact]
    public async Task FindAllSellersAsync_SortById_DescendingTrue_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindAllAsync(
                sortBy: SellerSortBy.Id,
                descending: true,
                cancellationToken: CancellationToken.None);
        
        var result = sellers.ToList();
        // Assert
        Assert.NotNull(sellers);
        Assert.Equal(6, sellers.Count());
        Assert.Equal(TestContextFactory.SellerIdForGetting4 , result[0].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting3, result[1].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting2, result[2].Id);
        Assert.Equal(TestContextFactory.SellerIdForDelete, result[3].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[4].Id);
        Assert.Equal(TestContextFactory.SellerIdForUpdate, result[5].Id);
    }
    // TODO:
    [Fact]
    public async Task FindAllSellersAsync_SortByCreatedDate_DescendingFalse_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindAllAsync(CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
    }
    // TODO:
    [Fact]
    public async Task FindAllSellersAsync_SortByCreatedDate_DescendingTrue_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindAllAsync(CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
    }
    
    [Fact]
    public async Task FindAllSellersAsync_WithIsBlockedTrue_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindAllAsync(
                isBlocked: true,
                cancellationToken: CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
        Assert.Equal(2, sellers.Count());
    }
    
    [Fact]
    public async Task FindAllSellersAsync_WithIsBlockedFalse_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindAllAsync(
                isBlocked: false,
                cancellationToken: CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
        Assert.Equal(4, sellers.Count());
    }
    #endregion
    #region By Id
    [Fact]
    public async Task FindSellerByIdAsync_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var seller = await _sellerRepository
            .FindByIdAsync(
                TestContextFactory.SellerIdForGetting1, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(seller);
        Assert.Equal(TestContextFactory.SellerIdForGetting1, seller.Id);
    }
    
    [Fact]
    public async Task FindSellerByIdAsync_FailOnWrongId_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
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
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var seller = await _sellerRepository
            .GetByIdAsync(
                TestContextFactory.SellerIdForGetting1, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(seller);
        Assert.Equal(TestContextFactory.SellerIdForGetting1, seller.Id);
    }
    
    [Fact]
    public async Task GetSellerByIdAsync_FailOnWrongId_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
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
    #endregion
    #region By Name
    // TODO:
    [Fact]
    public async Task FindSellerByNameAsync_WithDefaultParameters_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindByNameAsync(
                TestContextFactory.SellerNameForGetting1, 
                CancellationToken.None);
        
        var result = sellers.ToList();
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Count());
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[0].Id);
    }
    
    [Fact]
    public async Task FindSellerByNameAsync_FailOnWrongName_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        string wrongName = Guid.NewGuid().ToString();
        // Act
        var seller = await _sellerRepository
            .FindByNameAsync(
                name: wrongName, 
                CancellationToken.None);
        // Assert
        Assert.Empty(seller);
    }
    
    [Fact]
    public async Task GetSellerByNameAsync_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .GetByNameAsync(
                TestContextFactory.SellerNameForGetting1, 
                CancellationToken.None);
        
        var result = sellers.ToList();
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Count());
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[0].Id);
    }
    
    [Fact]
    public async Task GetSellerByNameAsync_FailOnWrongName_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        string wrongUserId = Guid.NewGuid().ToString();
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _sellerRepository
                .GetByUserIdAsync(
                    userId: wrongUserId,
                    CancellationToken.None));
    }
    //TODO: зачем повторяется
    [Fact]
    public async Task FindByNameSellersAsync_WithDefaultParameters_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindByNameAsync(TestContextFactory.SellerNameForGetting4,CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
        Assert.Equal(2, sellers.Count());
    }
    
    [Fact]
    public async Task FindByNameSellersAsync_CountLimit_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindByNameAsync(TestContextFactory.SellerNameForGetting4,
                pageCount: 2,
                cancellationToken: CancellationToken.None);
        // Assert
        Assert.NotNull(sellers);
        Assert.Equal(2, sellers.Count());
    }
    
    [Fact]
    public async Task FindByNameSellersAsync_SortByName_DescendingFalse_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindByNameAsync(TestContextFactory.SellerNameForGetting4,
                sortBy: SellerSortBy.Name,
                descending: false,
                cancellationToken: CancellationToken.None);
        
        var result = sellers.ToList();
        // Assert
        Assert.NotNull(sellers);
        Assert.Equal(2, sellers.Count());
        
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[0].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting4, result[1].Id);
        
    }
    
    [Fact]
    public async Task FindByNameSellersAsync_SortByName_DescendingTrue_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindByNameAsync(TestContextFactory.SellerNameForGetting4,
                sortBy: SellerSortBy.Name,
                descending: true,
                cancellationToken: CancellationToken.None);
        
        var result = sellers.ToList();
        // Assert
        Assert.NotNull(sellers);
        Assert.Equal(2, sellers.Count());
        Assert.Equal(TestContextFactory.SellerIdForGetting4, result[0].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[1].Id);
    }
    
    [Fact]
    public async Task FindByNameSellersAsync_SortById_DescendingFalse_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindByNameAsync(TestContextFactory.SellerNameForGetting4,
                sortBy: SellerSortBy.Id,
                descending: false,
                cancellationToken: CancellationToken.None);
        
        var result = sellers.ToList();
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[0].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting4, result[1].Id);
    }
    
    [Fact]
    public async Task FindByNameSellersAsync_SortById_DescendingTrue_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindByNameAsync(TestContextFactory.SellerNameForGetting4,
                sortBy: SellerSortBy.Id,
                descending: true,
                cancellationToken: CancellationToken.None);
        
        var result = sellers.ToList();
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(TestContextFactory.SellerIdForGetting4, result[0].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[1].Id);
    }
    
    [Fact]
    public async Task FindByNameSellersAsync_SortByCreatedDate_DescendingFalse_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindByNameAsync(
                name: TestContextFactory.SellerNameForGetting4, 
                cancellationToken: CancellationToken.None,
                sortBy: SellerSortBy.CreatedDate,
                descending: false);
        
        var result = sellers.ToList();
        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[0].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting4, result[1].Id);
    }
    
    [Fact]
    public async Task FindByNameSellersAsync_SortByCreatedDate_DescendingTrue_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindByNameAsync(name: TestContextFactory.SellerNameForGetting4, 
                cancellationToken: CancellationToken.None,
                sortBy: SellerSortBy.CreatedDate,
                descending: true);
        
        var result = sellers.ToList();
        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(TestContextFactory.SellerIdForGetting4, result[0].Id);
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[1].Id);
    }
    
    [Fact]
    public async Task FindByNameSellersAsync_WithIsBlockedTrue_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindByNameAsync(
                TestContextFactory.SellerNameForGetting4,
                isBlocked: true,
                cancellationToken: CancellationToken.None);
        var result = sellers.ToList();
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Count());
        Assert.Equal(TestContextFactory.SellerIdForGetting4, result[0].Id);
    }
    
    [Fact]
    public async Task FindByNameSellersAsync_WithIsBlockedFalse_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var sellers = await _sellerRepository
            .FindByNameAsync(TestContextFactory.SellerNameForGetting4,
                isBlocked: false,
                cancellationToken: CancellationToken.None);
        var result = sellers.ToList();
        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Count());
        Assert.Equal(TestContextFactory.SellerIdForGetting1, result[0].Id);
    }
    
    #endregion
    #region By User ID
    [Fact]
    public async Task FindSellerByUserIdAsync_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var seller = await _sellerRepository
            .FindByUserIdAsync(
                TestContextFactory.UserIdForGettingSeller1, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(seller);
        Assert.Equal(TestContextFactory.SellerIdForGetting1, seller.Id);
    }
    
    [Fact]
    public async Task FindSellerByUserIdAsync_FailOnWrongUserId_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        string wrongUserId = Guid.NewGuid().ToString();
        // Act
        var result = await _sellerRepository
            .FindByUserIdAsync(
                userId: wrongUserId, 
                CancellationToken.None);
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetSellerByUserIdAsync_Success_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        // Act
        var seller = await _sellerRepository
            .GetByUserIdAsync(
                TestContextFactory.UserIdForGettingSeller1, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(seller);
        Assert.Equal(TestContextFactory.SellerIdForGetting1, seller.Id);
    }
    
    [Fact]
    public async Task GetSellerByUserIdAsync_FailOnWrongUserId_Test()
    {
        _context = TestContextFactory.CreateReadyContext();
        _sellerRepository = new SellerRepository(_context, TestContextFactory.ConnectionString);
        // Arrange
        string wrongUserId = Guid.NewGuid().ToString();
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _sellerRepository
                .GetByUserIdAsync(
                    userId: wrongUserId,
                    CancellationToken.None));
    }
    #endregion
    public void Dispose()
    {
        /*_context.Dispose();
        _context.Database.EnsureDeleted();*/
    }
}
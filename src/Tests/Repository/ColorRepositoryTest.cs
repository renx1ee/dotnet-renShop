using Microsoft.EntityFrameworkCore;
using RenStore.Application.Common.Exceptions;
using RenStore.Persistence;
using RenStore.Persistence.Repository;
using Tests.Common;
using Color = RenStore.Domain.Entities.Color;

namespace Tests.Repository;

public class ColorRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ColorRepository _colorRepository;

    public ColorRepositoryTests()
    {
        _context = TestContextFactory.CreateReadyContext();
        _colorRepository = new ColorRepository(_context, TestContextFactory.ConnectionString);
    }

    [Fact]
    public async Task CreateColorAsync_Success_Test()
    {
        // Arrange
        int colorId = 84587;
        var color = new Color()
        {
            Id = colorId,
            Name = TestContextFactory.ColorNameForCreate,
            NormalizedName = TestContextFactory.ColorNameForCreate.ToUpper(),
            NameRu = Guid.NewGuid().ToString(),
            ColorCode = "#123",
            Description = Guid.NewGuid().ToString()
        };
        // Act
        var result = await _colorRepository.CreateAsync(color, CancellationToken.None);
        // Assert
        Assert.Equal(colorId, result);
        var savedColor = await _context.Colors
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == TestContextFactory.ColorNameForCreate);
        
        Assert.NotNull(savedColor);
        Assert.Equal(color.Name, savedColor.Name);
        Assert.Equal(color.NormalizedName, savedColor.NormalizedName);
        Assert.Equal(color.NameRu, savedColor.NameRu);
        Assert.Equal(color.ColorCode, savedColor.ColorCode);
        Assert.Equal(color.Description, savedColor.Description);
    }
    // TODO: сделать duplicate exception
    [Fact]
    public async Task CreateColorAsync_FailOnDuplicateName_Test()
    {
        // Arrange
        int colorId = 4345;
        var color = new Color()
        {
            Id = colorId,
            Name = TestContextFactory.ColorNameForCreate,
            NormalizedName = TestContextFactory.ColorNameForCreate.ToUpper(),
            NameRu = "белый",
            ColorCode = "#FFF",
            Description = "Test Description"
        };
        var duplicateColor = new Color()
        {
            Id = colorId + 1,
            Name = TestContextFactory.ColorNameForCreate,
            NormalizedName = TestContextFactory.ColorNameForCreate.ToUpper(),
            NameRu = "белый",
            ColorCode = "#FFF",
            Description = "Test Description"
        };
        // Act 
        await _colorRepository.CreateAsync(color, CancellationToken.None);
        // Act & Assert
        await Assert.ThrowsAsync<DbUpdateException>(async () =>
            await _colorRepository.CreateAsync(
                color: duplicateColor, 
                CancellationToken.None));
        
        var duplicateSavedColor = await _context.Colors
            .AsNoTracking()
            .FirstOrDefaultAsync(c => 
                c.Id == colorId + 1);
        
        Assert.Null(duplicateSavedColor);
    }
    // TODO:
    [Fact]
    public async Task UpdateColorAsync_Success_Test()
    {
        // Arrange
        // Act
        // Assert
    }
    // TODO:
    [Fact]
    public async Task UpdateColorAsync_FailOnWrongId_Test()
    {
        // Arrange
        // Act
        // Assert
    }
    
    [Fact]
    public async Task DeleteColorAsync_Success_Test()
    {
        // Arrange
        // Act
        await _colorRepository.DeleteAsync(
            TestContextFactory.ColorIdForDelete, 
            CancellationToken.None);
        // Assert
        var color = await _context.Colors
            .AsNoTracking()
            .FirstOrDefaultAsync(c => 
                c.Id == TestContextFactory.ColorIdForDelete);
        
        Assert.Null(color);
    }
    
    [Fact]
    public async Task DeleteColorAsync_FailOnWrongId_Test()
    {
        // Arrange
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _colorRepository.DeleteAsync(
                id: 254363, 
                CancellationToken.None));
    }
    
    [Fact]
    public async Task FindAllColorsAsync_Success_Test()
    {
        // Arrange
        // Act
        var result = await _colorRepository
            .FindAllAsync(CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        /*Assert.Equal(1, colors.Count());*/
    }
    
    [Fact]
    public async Task FindColorByIdAsync_Success_Test()
    {
        // Arrange
        // Act
        var result = await _colorRepository
            .FindByIdAsync(
                TestContextFactory.ColorIdForGetting, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(TestContextFactory.ColorIdForGetting, result.Id);
    }
    
    [Fact]
    public async Task FindColorByIdAsync_FailOnWrongId_Test()
    {
        // Arrange
        // Act
        var result = await _colorRepository
            .FindByIdAsync(
                id: 43242242, 
                CancellationToken.None);
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetColorByIdAsync_Success_Test()
    {
        // Arrange
        // Act
        var result = await _colorRepository
            .GetByIdAsync(
                id: TestContextFactory.ColorIdForGetting, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(TestContextFactory.ColorIdForGetting, result.Id);
    }
    
    [Fact]
    public async Task GetColorByIdAsync_FailOnWrongId_Test()
    {
        // Arrange
        int colorId = 5353336;
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _colorRepository.GetByIdAsync(
                id: colorId, 
                CancellationToken.None));
    }
    
    [Fact]
    public async Task FindColorByNameAsync_Success_Test()
    {
        // Arrange
        // Act
        var result = await _colorRepository
            .FindByNameAsync(
                name: TestContextFactory.ColorNameForGetting, 
                CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(TestContextFactory.ColorNameForGetting, result.Name);
    }
    
    [Fact]
    public async Task FindColorByNameAsync_FailOnWrongName_Test()
    {
        // Arrange
        string colorName = Guid.NewGuid().ToString();
        // Act
        var result = await _colorRepository
            .FindByNameAsync(colorName, CancellationToken.None);
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task FindColorByNameAsync_FailOnNullName_Test()
    {
        // Arrange
        // Act
        var result = await _colorRepository
            .FindByNameAsync(null, CancellationToken.None);
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task FindColorByNameAsync_FailOnEmptyName_Test()
    {
        // Arrange
        // Act
        var result = await _colorRepository
            .FindByNameAsync(string.Empty, CancellationToken.None);
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetColorByNameAsync_Success_Test()
    {
        // Arrange
        // Act
        var result = await _colorRepository
            .GetByNameAsync(
                name: TestContextFactory.ColorNameForGetting,
                CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(TestContextFactory.ColorNameForGetting, result.Name);
    }
    
    [Fact]
    public async Task GetColorByNameAsync_FailOnWrongName_Test()
    {
        // Arrange
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _colorRepository
                .GetByNameAsync(
                    name: Guid.NewGuid().ToString(), 
                    CancellationToken.None));

    }
    
    [Fact]
    public async Task GetColorByNameAsync_FailOnNullName_Test()
    {
        // Arrange
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _colorRepository
                .GetByNameAsync(
                    name: null,
                    CancellationToken.None));
    }
    
    [Fact]
    public async Task GetColorByNameAsync_FailOnEmptyName_Test()
    {
        // Arrange
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _colorRepository
                .GetByNameAsync(
                    name: string.Empty,
                    CancellationToken.None));
    }
    
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Persistence.Tests.Common;
using RenStore.Application.Common.Exceptions;
using RenStore.Domain.Entities;
using RenStore.Persistence;
using RenStore.Persistence.Repository;

namespace Persistence.Tests.Repository;

public class CategoryRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext context;
    private readonly CategoryRepository repository;
    
    public CategoryRepositoryTests()
    {
        context = TestContextFactory.CreateReadyContext();
        repository = new CategoryRepository(context, TestContextFactory.ConnectionString);
    }
    
    [Fact]
    public async Task CreateCategoryAsync_Success_Test()
    {
        // Arrange
        int categoryId = 324232;
        var category = new Category
        {
            Id = categoryId,
            Name = "New category",
            Description = "Created Category Description",
            ImagePath = "/images/main/img.png",
        };
        // Act
        var result = await repository.CreateAsync(category, CancellationToken.None);
        // Assert
        Assert.Equal(categoryId, result);
        var savedCategory = await context.Categories.FindAsync(category.Id);
        Assert.NotNull(savedCategory);
        Assert.Equal(category.Name, savedCategory.Name);
    }

    // TODO: decide a mistake
    [Fact]
    public async Task UpdateCategoryAsync_Success_Test()
    {
        // Arrange
        string newCategoryName = "New Updated Category Name";
        string newCategoryDescription = "New Updated Category Description";
        // Act 
        var category = await context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => 
                c.Id == TestContextFactory.CategoryIdForUpdate);
        
        Assert.NotNull(category);
        
        category.Name = newCategoryName;
        category.Description = newCategoryDescription;
        
        await repository.UpdateAsync(category, CancellationToken.None);
        // Assert
        var result = await context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c =>
                c.Id == TestContextFactory.CategoryIdForUpdate);
        Assert.Equal(newCategoryName, result.Name);
        Assert.Equal(newCategoryDescription, result.Description);
    }
    
    [Fact]
    public async Task DeleteCategoryAsync_Success_Test()
    {
        // Arrange
        // Act
        var category = await context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => 
                c.Id == TestContextFactory.CategoryIdForDelete);
        Assert.NotNull(category);
        await repository.DeleteAsync(category.Id, CancellationToken.None);
        // Assert
        var categoryResult = await context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c =>
                c.Id == TestContextFactory.CategoryIdForDelete);
        
        Assert.Null(categoryResult);
    }
    
    [Fact]
    public async Task DeleteCategoryAsync_FailOnWrongId_Test()  
    {
        // Arrange
        int wrongCategoryId = 980289;
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => 
            await repository.DeleteAsync(
                wrongCategoryId,
                CancellationToken.None));
    }

    [Fact]
    public async Task GetAllAsync_Success_Test()
    {
        // Arrange
        // Act
        var result = await repository.GetAllAsync(
            CancellationToken.None);
        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task FindByIdAsync_Success_Test()
    {
        // Arrange
        int categoryId = TestContextFactory.CategoryIdForGetting;
        // Act
        var result = await repository
            .FindByIdAsync(
                categoryId,
                CancellationToken.None);
        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task FindByIdAsync_FailOnWrongId_Test()
    {
        // Arrange
        int categoryId = 848483;
        // Act
        var result = await repository
            .FindByIdAsync(
                categoryId,
                CancellationToken.None);
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_Success_Test()
    {
        // Arrange
        int categoryId = TestContextFactory.CategoryIdForGetting;
        // Act
        var result = await repository
            .GetByIdAsync(
                categoryId,
                CancellationToken.None);
        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByIdAsync_FailOnWrongId_Test()
    {
        // Arrange
        int categoryId = 848483;
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await repository
                .GetByIdAsync(
                    categoryId,
                    CancellationToken.None)
        );
    }
    
    [Fact]
    public async Task FindByNameAsync_Success_Test()
    {
        // Arrange
        string categoryName = TestContextFactory.CategoryNameForGetting;
        // Act
        var result = await repository
            .FindByNameAsync(
                categoryName,
                CancellationToken.None);
        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task FindByNameAsync_FailOnWrongName_Test()
    {
        // Arrange
        string wrongCategoryName = "WrongCategoryName";
        // Act
        var result = await repository
            .FindByNameAsync(
                wrongCategoryName,
                CancellationToken.None);
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByNameAsync_Success_Test()
    {
        // Arrange
        string categoryName = TestContextFactory.CategoryNameForGetting;
        // Act
        var result = await repository
            .GetByNameAsync(
                categoryName,
                CancellationToken.None);
        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetByNameAsync_FailOnWrongName_Test()
    {
        // Arrange
        string wrongCategoryName = "WrongCategoryName";
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await repository.GetByNameAsync(
                wrongCategoryName, 
                CancellationToken.None));
    }
    
    public void Dispose()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
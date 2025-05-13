using Microsoft.Extensions.Configuration;
using Moq;
using Npgsql;
using RenStore.Domain.Entities;
using RenStore.Persistence.Repository;
using RenStore.Persistence.Test.Common;

namespace RenStore.Persistence.Test.Repositories;

public class CategoryRepositoryTests 
{
    private readonly CategoryRepository categoryRepository;
    private readonly IConfiguration configuration;
    private readonly ApplicationDbContext context;

    public CategoryRepositoryTests()
    {
        var connection = new NpgsqlConnection("DataSource=:memory:");
        context = DbContextFactory.Create();
        configuration = DbContextFactory.GetMockConfiguration();

        /*var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c.GetConnectionString("DefaultConnection"))
            .Returns(connection.ConnectionString);
        configuration = configMock.Object;*/
        
        categoryRepository = new CategoryRepository(context, configuration);
    }

    [Fact]
    public async Task CreateCategoryAsync_Success_Test()
    {
        // Arrange
        // Act
        var categoryId = await categoryRepository.CreateAsync(new Category
        {
            Id = 324232,
            Name = "Created Category Name",
            Description = "Created Category Description",
            ImagePath = "/images/main/img.png",
        }, CancellationToken.None);
        // Assert
        Assert.NotNull(
            await categoryRepository.GetByIdAsync(
                id: categoryId, 
                cancellationToken: CancellationToken.None));
    }

    /*[Fact]
    public async Task UpdateCategoryAsync_Success_Test()
    {
        // Arrange
        categoryRepository = new CategoryRepository(context, null);
        string updatedName = "Updated Category Name";
        string updatedDescription = "Updated Category Name";
        // Act
        var category = await categoryRepository.GetByIdAsync(
            DbContextFactory.CategoryIdForUpdate,
            CancellationToken.None);

        category.Name = updatedName;
        category.Description = updatedDescription;
        
        await categoryRepository.UpdateAsync(category, CancellationToken.None);
        // Assert
        var result = await categoryRepository.GetByIdAsync(
            category.Id, 
            CancellationToken.None);
        
        Assert.Equal(result.Name, result.Name);
        Assert.Equal(result.Description, result.Description);
    }

    [Fact]
    public async Task UpdateCategoryAsync_FainOnWrong_TestId()
    {
        // Arrange
        categoryRepository = new CategoryRepository(context, null);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await categoryRepository.UpdateAsync(new Category()
                { Id = 5483834 }, 
                CancellationToken.None
            ));
    }

    [Fact]
    public async Task DeleteCategoryAsync_Success_Test()
    {
        // Arrange
        categoryRepository = new CategoryRepository(context, null);
        // Act
        await categoryRepository.DeleteAsync(DbContextFactory.CategoryIdForDelete, CancellationToken.None);
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await categoryRepository.GetByIdAsync(
                DbContextFactory.CategoryIdForDelete,
                CancellationToken.None)
        );
    }

    [Fact]
    public async Task DeleteCategoryAsync_FailOnWrongId_Test()
    {
        // Arrange
        categoryRepository = new CategoryRepository(context, null);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await categoryRepository.DeleteAsync(
                358376347, 
                CancellationToken.None
            )
        );
    }

    [Fact]
    public async Task GetAllCategoryAsync_Success_Test()
    {
        // Arrange
        categoryRepository = new CategoryRepository(context, null);
        // Act
        var products = await categoryRepository.GetAllAsync(CancellationToken.None);
        // Assert
        Assert.Equal(3, products.Count());
    }

    [Fact]
    public async Task GetCategoryByIdAsync_Success_Test()
    {
        // Arrange
        categoryRepository = new CategoryRepository(context, null);
        // Act
        var category = await categoryRepository.GetByIdAsync(
            DbContextFactory.CategoryIdForUpdate, 
            CancellationToken.None
        );
        // Assert
        Assert.NotNull(category);
        Assert.Equal(DbContextFactory.CategoryIdForUpdate, category.Id);
    }

    [Fact]
    public async Task GetCategoryByIdAsync_FailOnWrongId_Test()
    {
        // Arrange
        categoryRepository = new CategoryRepository(context, null);
        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await categoryRepository.GetByIdAsync(
                7445675, 
                CancellationToken.None
            )
        );
    }*/
}
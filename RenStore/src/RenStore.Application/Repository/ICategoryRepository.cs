using RenStore.Domain.Entities;

namespace RenStore.Application.Repository;
/// <summary>
/// Repository for working with entity Category.
/// Provides basic CRUD operations and methods for working with data.
/// </summary>
/// <remarks>
/// Initializes a new repository instance.
/// </remarks>
/// <param name="context">Database context.</param>
/// <param name="connectionString">Database connection string.</param>
public interface ICategoryRepository
{
    /// <summary>
    /// Create a new Category.
    /// </summary>
    /// <param name="category">Category Model for create.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Category ID if Category is created.</returns>
    Task<int> CreateAsync(Category category, CancellationToken cancellationToken);
    /// <summary>
    /// Category Update.
    /// </summary>
    /// <param name="category">Category Model for update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Category is not found.</exception>
    Task UpdateAsync(Category category, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes a Category by ID.
    /// </summary>
    /// <param name="id">Category ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Category is not found.</exception>
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    /// <summary>
    /// Get all Categories.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return a IEnumerable collection of categories.</returns>
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Category by Category ID.
    /// </summary>
    /// <param name="id">Category ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Category if Category is found else returns null.</returns>
    Task<Category?> FindByIdAsync(int id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Category by Category ID.
    /// </summary>
    /// <param name="id">Category ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Category if Category is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Category is not found.</exception>
    Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Category by Category Name.
    /// </summary>
    /// <param name="name">Category name.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Category if Category is found else returns null.</returns>
    Task<Category?> FindByNameAsync(string name, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Category by Category Name.
    /// </summary>
    /// <param name="name">Category name.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Returns Category.</returns>
    /// <exception cref="NotFoundException">Thrown if the Category is not found.</exception>
    Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken);
}

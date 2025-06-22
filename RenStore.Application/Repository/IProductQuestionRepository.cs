using RenStore.Domain.Entities;

namespace RenStore.Application.Repository;
/// <summary>
/// Repository for working with entity Product.
/// Provides basic CRUD operations and methods for working with data.
/// </summary>
/// <remarks>
/// Initializes a new repository instance.
/// </remarks>
/// <param name="context">Database context.</param>
/// <param name="connectionString">Database connection string.</param>
public interface IProductQuestionRepository
{
    /// <summary>
    /// Create a new Answer.
    /// </summary>
    /// <param name="question">ProductQuestion Model for create.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductQuestion ID if ProductQuestion is created.</returns>
    Task<Guid> CreateAsync(ProductQuestion question, CancellationToken cancellationToken);
    /// <summary>
    /// ProductAnswer Update.
    /// </summary>
    /// <param name="question">ProductQuestion Model for update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the ProductQuestion is not found.</exception>
    Task UpdateAsync(ProductQuestion question, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes a ProductQuestion by ID.
    /// </summary>
    /// <param name="id">ProductQuestion ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the ProductQuestion is not found.</exception>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Get all ProductQuestion.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return a IEnumerable collection of ProductQuestion.</returns>
    Task<IEnumerable<ProductQuestion>> GetAllAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Finds a ProductQuestion by ProductQuestion ID.
    /// </summary>
    /// <param name="id">ProductQuestion ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductQuestion if ProductQuestion is found else returns null.</returns>
    Task<ProductQuestion?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a ProductQuestion by ProductQuestion ID.
    /// </summary>
    /// <param name="id">ProductQuestion ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductQuestion if ProductQuestion is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the ProductQuestion is not found.</exception>
    Task<ProductQuestion?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a ProductQuestion by Product ID.
    /// </summary>
    /// <param name="productId">Product ID.</param>
    /// <param name="count">Count of items.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductQuestion if ProductQuestion is found else returns null.</returns>
    Task<IEnumerable<ProductQuestion>> FindByProductIdAsync(Guid productId, uint count, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a ProductQuestion by Product ID.
    /// </summary>
    /// <param name="productId">Product ID.</param>
    /// <param name="count">Product ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductQuestion if ProductQuestion is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the ProductQuestion is not found.</exception>
    Task<IEnumerable<ProductQuestion>> GetByProductIdAsync(Guid productId, uint count, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a ProductQuestion by User ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="count">Count of items.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductQuestion if ProductQuestion is found else returns null.</returns>
    Task<IEnumerable<ProductQuestion>> FindByUserIdAsync(string userId, uint count, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a ProductQuestion by User ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="count">Count of items.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductQuestion if ProductQuestion is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the ProductQuestion is not found.</exception>
    Task<IEnumerable<ProductQuestion>> GetByUserIdAsync(string userId, uint count, CancellationToken cancellationToken);
}
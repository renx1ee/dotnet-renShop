using RenStore.Domain.Entities;

namespace RenStore.Application.Repository;
/// <summary>
/// Repository for working with entity ProductAnswer.
/// Provides basic CRUD operations and methods for working with data.
/// </summary>
/// <remarks>
/// Initializes a new repository instance.
/// </remarks>
/// <param name="context">Database context.</param>
/// <param name="connectionString">Database connection string.</param>
public interface IProductAnswerRepository
{
    /// <summary>
    /// Create a new Answer.
    /// </summary>
    /// <param name="answer">ProductAnswer Model for create.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductAnswer ID if ProductAnswer is created.</returns>
    Task<Guid> CreateAsync(ProductAnswer answer, CancellationToken cancellationToken);
    /// <summary>
    /// ProductAnswer Update.
    /// </summary>
    /// <param name="answer">ProductAnswer Model for update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the ProductAnswer is not found.</exception>
    Task UpdateAsync(ProductAnswer answer, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes a Product by ID.
    /// </summary>
    /// <param name="id">ProductAnswer ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the ProductAnswer is not found.</exception>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Get all ProductAnswers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return a IEnumerable collection of ProductAnswers.</returns>
    Task<IEnumerable<ProductAnswer>> GetAllAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Finds a ProductAnswers by ProductAnswers ID.
    /// </summary>
    /// <param name="id">ProductAnswers ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductAnswers if ProductAnswers is found else returns null.</returns>
    Task<ProductAnswer?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a ProductAnswers by ProductAnswers ID.
    /// </summary>
    /// <param name="id">ProductAnswers ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductAnswers if ProductAnswers is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the ProductAnswers is not found.</exception>
    Task<ProductAnswer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a ProductAnswers by Product ID.
    /// </summary>
    /// <param name="productId">Product ID.</param>
    /// <param name="count">Count of items.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductAnswers if ProductAnswers is found else returns null.</returns>
    Task<IEnumerable<ProductAnswer>> FindByProductIdAsync(Guid productId, uint count, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a ProductAnswers by Product ID.
    /// </summary>
    /// <param name="productId">Product ID.</param>
    /// <param name="count">Product ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductAnswers if ProductAnswers is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the ProductAnswers is not found.</exception>
    Task<IEnumerable<ProductAnswer>> GetByProductIdAsync(Guid productId, uint count, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a ProductAnswers by User ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="count">Count of items.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductAnswers if ProductAnswers is found else returns null.</returns>
    Task<IEnumerable<ProductAnswer>> FindByUserIdAsync(string userId, uint count, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a ProductAnswers by User ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="count">Count of items.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductAnswers if ProductAnswers is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the ProductAnswers is not found.</exception>
    Task<IEnumerable<ProductAnswer>> GetByUserIdAsync(string userId, uint count, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a ProductAnswers by Question ID.
    /// </summary>
    /// <param name="questionId">Question ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductAnswers if ProductAnswers is found else returns null.</returns>
    Task<ProductAnswer> FindByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken);
    /// <summary>
    /// Get a ProductAnswers by Question ID.
    /// </summary>
    /// <param name="questionId">Question ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductAnswers if ProductAnswers is found else returns null.</returns>
    /// <exception cref="NotFoundException">Thrown if the ProductAnswers is not found.</exception>
    Task<ProductAnswer> GetByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a ProductAnswers by Seller ID.
    /// </summary>
    /// <param name="sellerId">Seller ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductAnswers if ProductAnswers is found else returns null.</returns>
    Task<ProductAnswer?> FindBySellerIdAsync(Guid sellerId, CancellationToken cancellationToken);
    /// <summary>
    /// Get a ProductAnswers by Seller ID.
    /// </summary>
    /// <param name="sellerId">Seller ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return ProductAnswers if ProductAnswers is found else returns null.</returns>
    /// <exception cref="NotFoundException">Thrown if the ProductAnswers is not found.</exception>
    Task<ProductAnswer> GetBySellerIdAsync(Guid sellerId, CancellationToken cancellationToken);
}
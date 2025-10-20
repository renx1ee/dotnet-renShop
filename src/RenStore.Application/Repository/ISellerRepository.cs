using RenStore.Domain.Entities;

namespace RenStore.Application.Repository;
/// <summary>
/// Repository for working with entity Seller.
/// Provides basic CRUD operations and methods for working with data.
/// </summary>
/// <remarks>
/// Initializes a new repository instance.
/// </remarks>
/// <param name="context">Database context.</param>
/// <param name="connectionString">Database connection string.</param>
public interface ISellerRepository 
{
    /// <summary>
    /// Create a new Seller.
    /// </summary>
    /// <param name="seller">Seller Model for create.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Seller ID if Seller is created.</returns>
    Task<long> CreateAsync(Seller seller, CancellationToken cancellationToken);
    /// <summary>
    /// Seller Update.
    /// </summary>
    /// <param name="seller">Seller Model for update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Seller is not found.</exception>
    Task UpdateAsync(Seller seller, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes a Seller by Seller ID.
    /// </summary>
    /// <param name="id">Seller ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Seller is not found.</exception>
    Task DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Get all Sellers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return a IEnumerable collection of Seller.</returns>
    Task<IEnumerable<Seller>> GetAllAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Seller by Seller ID.
    /// </summary>
    /// <param name="id">Seller ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Seller if Seller is found else returns null.</returns>
    Task<Seller?> FindByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Seller by Seller ID.
    /// </summary>
    /// <param name="id">Seller ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Seller if Seller is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Seller is not found.</exception>
    Task<Seller> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Seller by Seller Name.
    /// </summary>
    /// <param name="name">Seller Name.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Seller if Seller is found else returns null.</returns>
    Task<Seller?> FindByNameAsync(string name, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Seller by Seller Name.
    /// </summary>
    /// <param name="name">Seller Name.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Seller if Seller is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Seller is not found.</exception>
    Task<Seller> GetByNameAsync(string name, CancellationToken cancellationToken);
}
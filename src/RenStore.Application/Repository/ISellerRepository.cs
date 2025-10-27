using RenStore.Domain.Entities;

namespace RenStore.Application.Repository;
/// <summary>
/// Repository for working with entity SellerEntity.
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
    /// Create a new SellerEntity.
    /// </summary>
    /// <param name="sellerEntity">SellerEntity Model for create.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return SellerEntity ID if SellerEntity is created.</returns>
    Task<long> CreateAsync(SellerEntity sellerEntity, CancellationToken cancellationToken);
    /// <summary>
    /// SellerEntity Update.
    /// </summary>
    /// <param name="sellerEntity">SellerEntity Model for update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the SellerEntity is not found.</exception>
    Task UpdateAsync(SellerEntity sellerEntity, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes a SellerEntity by SellerEntity ID.
    /// </summary>
    /// <param name="id">SellerEntity ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the SellerEntity is not found.</exception>
    Task DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Get all Sellers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return a IEnumerable collection of SellerEntity.</returns>
    Task<IEnumerable<SellerEntity>> GetAllAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Finds a SellerEntity by SellerEntity ID.
    /// </summary>
    /// <param name="id">SellerEntity ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return SellerEntity if SellerEntity is found else returns null.</returns>
    Task<SellerEntity?> FindByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a SellerEntity by SellerEntity ID.
    /// </summary>
    /// <param name="id">SellerEntity ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return SellerEntity if SellerEntity is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the SellerEntity is not found.</exception>
    Task<SellerEntity> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a SellerEntity by SellerEntity Name.
    /// </summary>
    /// <param name="name">SellerEntity Name.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return SellerEntity if SellerEntity is found else returns null.</returns>
    Task<SellerEntity?> FindByNameAsync(string name, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a SellerEntity by SellerEntity Name.
    /// </summary>
    /// <param name="name">SellerEntity Name.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return SellerEntity if SellerEntity is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the SellerEntity is not found.</exception>
    Task<SellerEntity> GetByNameAsync(string name, CancellationToken cancellationToken);
}
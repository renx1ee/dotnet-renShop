using RenStore.Domain.Entities;

namespace RenStore.Application.Repository;
/// <summary>
/// Repository for working with entity Delivery.
/// Provides basic CRUD operations and methods for working with data.
/// </summary>
/// /// <remarks>
/// Initializes a new repository instance.
/// </remarks>
/// <param name="context">Database context.</param>
/// <param name="connectionString">Database connection string.</param>
public interface IDeliveryRepository
{
    /// <summary>
    /// Create a new Delivery.
    /// </summary>
    /// <param name="delivery">Delivery Model for create.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Delivery ID if Delivery is created.</returns>
    Task<Guid> CreateAsync(Delivery delivery, CancellationToken cancellationToken);
    /// <summary>
    /// Delivery Update.
    /// </summary>
    /// <param name="delivery">Delivery Model for update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Delivery is not found.</exception>
    Task UpdateAsync(Delivery delivery, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes a Delivery by ID.
    /// </summary>
    /// <param name="id">Delivery ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Delivery is not found.</exception>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Delivery>> GetAllAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Delivery by Delivery ID.
    /// </summary>
    /// <param name="id">Delivery ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Delivery if Delivery is found else returns null.</returns>
    Task<Delivery?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Delivery by Delivery ID.
    /// </summary>
    /// <param name="id">Delivery ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Delivery if Delivery is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Delivery is not found.</exception>
    Task<Delivery> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Delivery>> FindByProductIdAsync(Guid productId, CancellationToken cancellationToken);
    Task<IEnumerable<Delivery>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken);
    Task<IEnumerable<Delivery>> FindBySellerIdAsync(int sellerId, CancellationToken cancellationToken);
    Task<IEnumerable<Delivery>> GetBySellerIdAsync(int sellerId, CancellationToken cancellationToken);
    Task<IEnumerable<Delivery>> FindByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<IEnumerable<Delivery>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    
}
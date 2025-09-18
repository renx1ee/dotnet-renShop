using RenStore.Domain.Entities;

namespace RenStore.Application.Repository;
/// <summary>
/// Repository for working with entity Order.
/// Provides basic CRUD operations and methods for working with data.
/// </summary>
/// <remarks>
/// Initializes a new repository instance.
/// </remarks>
/// <param name="context">Database context.</param>
/// <param name="connectionString">Database connection string.</param>
public interface IOrderRepository
{
    /// <summary>
    /// Create a new Order.
    /// </summary>
    /// <param name="order">Order Model for create.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Order ID if Order is created.</returns>
    Task<Guid> CreateAsync(Order order, CancellationToken cancellationToken);
    /// <summary>
    /// Order Update.
    /// </summary>
    /// <param name="order">Order Model for update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Order is not found.</exception>
    Task UpdateAsync(Order order, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes Order by ID.
    /// </summary>
    /// <param name="id">Order ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Order is not found.</exception>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Get all Orders.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return a IEnumerable collection of Orders.</returns>
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Finds Order by Order ID.
    /// </summary>
    /// <param name="id">Order ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Order if Order is found else returns null.</returns>
    Task<Order?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets Order by Order ID.
    /// </summary>
    /// <param name="id">Order ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Order if Order is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Order is not found.</exception>
    Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Finds Orders by User ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Orders if Order is found else returns null.</returns>
    Task<IEnumerable<Order>?> FindByUserIdAsync(string userId, CancellationToken cancellationToken);
    /// <summary>
    /// Gets Orders by User ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Orders if Order is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Orders is not found.</exception>
    Task<IEnumerable<Order>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    /// <summary>
    /// Finds Orders by Product ID.
    /// </summary>
    /// <param name="productId">Product ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Orders if Order is found else returns null.</returns>
    Task<IEnumerable<Order>?> FindByProductIdAsync(Guid productId, CancellationToken cancellationToken);
    /// <summary>
    /// Gets Orders by Product ID.
    /// </summary>
    /// <param name="productId">Product ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Orders if Order is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Orders is not found.</exception>
    Task<IEnumerable<Order>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken);
}
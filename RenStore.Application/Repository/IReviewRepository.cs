using RenStore.Domain.Entities;
using RenStore.Domain.Enums;
using RenStore.Domain.Interfaces.Repository;

namespace RenStore.Application.Repository;
/// <summary>
/// Repository for working with entity Review.
/// Provides basic CRUD operations and methods for working with data.
/// </summary>
/// <remarks>
/// Initializes a new repository instance.
/// </remarks>
/// <param name="context">Database context.</param>
/// <param name="connectionString">Database connection string.</param>
public interface IReviewRepository 
{
    /// <summary>
    /// Create a new Review.
    /// </summary>
    /// <param name="review">Review Model for create.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Review ID if Review is created.</returns>
    Task<Guid> CreateAsync(Review review, CancellationToken cancellationToken);
    /// <summary>
    /// Review Update.
    /// </summary>
    /// <param name="review">Review Model for update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Review is not found.</exception>
    Task UpdateAsync(Review review, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes a Review by ID.
    /// </summary>
    /// <param name="id">Review ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Review is not found.</exception>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Get all Reviews.
    /// </summary>
    /// <param name="status">The filter status to receive a review.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return a IEnumerable collection of Reviews.</returns>
    Task<IEnumerable<Review?>> GetAllAsync(ReviewStatusFilter status, CancellationToken cancellationToken);
    /// <summary>
    /// Get all Reviews for moderation.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return a IEnumerable collection of Reviews.</returns>
    Task<IEnumerable<Review>?> GetAllForModerationAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Review by Review ID.
    /// </summary>
    /// <param name="id">Review ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Review if Review is found else returns null.</returns>
    Task<Review?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Review by Review ID.
    /// </summary>
    /// <param name="id">Review ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return Review if Review is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Review is not found.</exception>
    Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Reviews by User ID.
    /// </summary>
    /// <param name="status">The filter status to receive a review.</param>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Reviews if Reviews is found else returns an empty collection.</returns>
    Task<IEnumerable<Review>?> FindByUserIdAsync(ReviewStatusFilter status, string userId, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Reviews by User ID.
    /// </summary>
    /// <param name="status">The filter status to receive a review.</param>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Reviews if Reviews is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Review is not found.</exception>
    Task<IEnumerable<Review>> GetByUserIdAsync(ReviewStatusFilter status, string userId, CancellationToken cancellationToken);
    /// <summary>
    /// Finds a Reviews by Product ID.
    /// </summary>
    /// <param name="productId">Product ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Reviews if Reviews is found else returns an empty collection.</returns>
    Task<IEnumerable<Review>?> FindByProductIdAsync(Guid productId, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Reviews by Product ID.
    /// </summary>
    /// <param name="productId">User ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Reviews if Reviews is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Review is not found.</exception>
    Task<IEnumerable<Review>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Reviews by User ID and Product ID.
    /// </summary>
    /// <param name="productId">Product ID.</param>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Reviews if Reviews is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Review is not found.</exception>
    Task<IEnumerable<Review>?> FindByUserIdAndProductIdAsync(string userId, Guid productId, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a first Reviews by Date.
    /// </summary>
    /// <param name="count">Count of Reviews.</param>
    /// <param name="productId">Product ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Reviews if Reviews is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Review is not found.</exception>
    Task<IEnumerable<Review>> GetFirstByDateAsync(int count, Guid productId, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a first Reviews by Rating.
    /// </summary>
    /// <param name="count">Count of Reviews.</param>
    /// <param name="productId">Product ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Return IEnumerable collection of Reviews if Reviews is found.</returns>
    /// <exception cref="NotFoundException">Thrown if the Review is not found.</exception>
    Task<IEnumerable<Review>> GetFirstByRatingAsync(int count, Guid productId, CancellationToken cancellationToken);
    /// <summary>
    /// Like a Review.
    /// Increases the number of likes by 1.
    /// </summary>
    /// <param name="reviewId">Review ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Thrown if the Review is not found.</exception>
    Task LikeAsync(Guid reviewId, CancellationToken cancellationToken);
    /// <summary>
    /// Checks if there is a review from a given user.
    /// </summary>
    /// <param name="productId">Product ID.</param>
    /// <param name="userId">User ID.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Returns true if user has already created review for this product.</returns>
    /// <exception cref="NotFoundException">Thrown if the Review is not found.</exception>
    Task<bool> CheckExistByUserIdAsync(Guid productId, string userId, CancellationToken cancellationToken);
}

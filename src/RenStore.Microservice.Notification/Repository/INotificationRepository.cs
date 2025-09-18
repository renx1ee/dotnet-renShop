using RenStore.Microservice.Notification.Enums;

namespace RenStore.Microservice.Notification.Repository;
/// <summary>
/// Repository for working with entity Notification.
/// Provides basic CRUD operations and methods for working with data.
/// </summary>
/// <remarks>
/// Initializes a new repository instance.
/// </remarks>
/// <param name="context">Database context.</param>
public interface INotificationRepository
{
    Task AddAsync(Models.Notification notification, CancellationToken cancellationToken);
    Task UpdateAsync(Models.Notification notification, CancellationToken cancellationToken);
    Task DeleteAsync(Guid userId, CancellationToken cancellationToken);
    Task<Models.Notification> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<IList<Models.Notification>> GetAllAsync(CancellationToken cancellationToken);
    Task<IList<Models.Notification>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
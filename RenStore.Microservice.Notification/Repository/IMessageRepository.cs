using RenStore.Microservice.Notification.Models;

namespace RenStore.Microservice.Notification.Repository;

public interface IMessageRepository
{
    public Task AddAsync(Message message, CancellationToken cancellationToken);
    public Task UpdateAsync(Message message, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    public Task<IList<Message>> GetAllAsync(CancellationToken cancellationToken);
    public Task<IList<Message>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IList<Message>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    public Task<IList<Message>> GetByChatIdAsync(Guid userId, CancellationToken cancellationToken);
    public Task<IList<Message>> GetBySenderIdAsync(Guid userId, CancellationToken cancellationToken);
}